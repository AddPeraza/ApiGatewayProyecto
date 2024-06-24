using ApiGateway.Gateway.Dtos;
using ApiGateway.Gateway.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiGateway.Gateway.Controllers
{
    public class VentasController
    {
        [ApiController]
        [Route("gateway/[controller]")]
        public class VentasCont : ControllerBase
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public VentasCont(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            [HttpGet("{nombre}")]
            public async Task<ActionResult<List<VentasDto>>> ObtenerNombre(string nombre)
            {
                var ClientePostgreSql = _httpClientFactory.CreateClient(ApiClients.PostgreSql.ToString());
                var response = await ClientePostgreSql.GetAsync($"api/Usuarios/{nombre}");

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest();
                }

                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<List<VentasDto>>(content, options);

                return Ok(result);
            }
        }
    }
}
