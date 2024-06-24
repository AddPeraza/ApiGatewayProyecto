namespace ApiGateway.Gateway.Dtos
{
    public class JuegoDto
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Clasificacion { get; set; }
        public string Genero { get; set; }
        public DateTime FechaDeLanzamiento { get; set; }
        public decimal Precio { get; set; }
    }
}
