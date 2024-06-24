using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace ApiGateway.MongoDb
{
    public class Juego
    {
        private readonly IMongoCollection<Juego> _collection;
        
        public Juego()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("apigateway");
            _collection = database.GetCollection<Juego>("juegos");
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("clasificacion")]
        public string Clasificacion { get; set; }

        [BsonElement("genero")]
        public string Genero { get; set; }

        [BsonElement("fechadelanzamiento")]
        public DateTime FechaDeLanzamiento { get; set; }

        [BsonElement("precio")]
        public decimal Precio { get; set; }

        public async Task<List<Juego>> ObtenerJuegos()
        {
            return await _collection.Find(_=> true).ToListAsync();
        }

        public async Task Agregar(Juego juego)
        {
            await _collection.InsertOneAsync(juego);
        }

        public void Agregar(List<Juego> juegos)
        {
            _collection.InsertMany(juegos);
        }

        public Juego ConsultarPorTitulo(string titulo)
        {
            return _collection
                .Find(a => a.Titulo == titulo)
                .FirstOrDefault<Juego>();
        }

        public Juego ConsultarPorClasificacion(string clasificacion)
        {
            return _collection
                .Find(a => a.Clasificacion == clasificacion)
                .FirstOrDefault<Juego>();
        }

        public void Actualizar(string clasificacion, Juego juego)
        {
            Juego juegoDB = ConsultarPorClasificacion(clasificacion);
            if (juegoDB != null)
            {
                _collection.ReplaceOne(a => a.Id == juegoDB.Id, juego);
            }
        }

        public void Eliminar(string titulo)
        {
            Juego juegoDB = ConsultarPorTitulo(titulo);
            if (juegoDB != null)
            {
                _collection.DeleteOne(a => a.Id == juegoDB.Id);
            }
        }

        public override string ToString()
        {
            return $"{Titulo} clasificacion: {Clasificacion} genero: {Genero}";
        }
    }
}
