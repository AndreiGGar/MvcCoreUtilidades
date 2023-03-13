using MvcCoreUtilidades.Models;

namespace MvcCoreUtilidades.Repositories
{
    public class RepositoryCoches
    {
        List<Coche> Cars;
        public RepositoryCoches()
        {
            this.Cars = new List<Coche>
            {
                new Coche { IdCoche = 1, Marca = "Lamborghini", Modelo = "Aventador", Imagen = "https://img.remediosdigitales.com/3b7cdd/lamborghini-aventador-ultimae-todo-vendido-01/840_560.jpeg"},
                new Coche {IdCoche = 2, Marca = "Ford", Modelo = "Mustang", Imagen = "https://cdn.media.kaavan.es/blobs/noticias/d9784156-f905-4205-836e-052ea0724527/medias/5645.jpg"},
                new Coche { IdCoche = 3, Marca = "Koenigsegg", Modelo = "Regera", Imagen = "https://cdn-images.motor.es/image/m/800w.webp/fotos-noticias/2021/01/koenigsegg-regera-prueba-record-video-202174376-1610384291_1.jpg" },
                new Coche { IdCoche = 4, Marca = "Corvette", Modelo = "Stingray", Imagen = "https://soymotor.com/sites/default/files/imagenes/noticia/2020-chevrolet-corvette-stingray-048_0.jpg" }
            };
        }

        public List<Coche> GetCoches()
        {
            return this.Cars;
        }

        public Coche FindCoche(int id)
        {
            return this.Cars.FirstOrDefault(x => x.IdCoche == id);
        }
    }
}
