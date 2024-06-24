using ApiGateway.Gateway.Dtos;
using ApiGateway.Gateway.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiGateway.Gateway.Controllers
{
    [ApiController]
    [Route("gateway/[controller]")]
    public class JuegosController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public JuegosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<JuegoDto>>> ObtenerJuegos()
        {
            var clienteMondoDB = _httpClientFactory.CreateClient(ApiClients.MongoDB.ToString());
            var response = await clienteMondoDB.GetAsync($"api/Juegos/");

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var result = JsonSerializer.Deserialize<List<JuegoDto>>(content, options);

            return Ok(result);
        }
    }
}
