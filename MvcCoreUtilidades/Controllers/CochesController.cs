using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Models;

namespace MvcCoreUtilidades.Controllers
{
    public class CochesController : Controller
    {
        List<Coche> Cars;
        public CochesController()
        {
            this.Cars = new List<Coche>
            {
                new Coche { IdCoche = 1, Marca = "Lamborghini", Modelo = "Aventador", Imagen = "https://img.remediosdigitales.com/3b7cdd/lamborghini-aventador-ultimae-todo-vendido-01/840_560.jpeg"},
                new Coche {IdCoche = 2, Marca = "Ford", Modelo = "Mustang", Imagen = "https://cdn.media.kaavan.es/blobs/noticias/d9784156-f905-4205-836e-052ea0724527/medias/5645.jpg"},
                new Coche {IdCoche = 3, Marca = "Koenigsegg", Modelo = "Regera", Imagen = "https://cdn-images.motor.es/image/m/800w.webp/fotos-noticias/2021/01/koenigsegg-regera-prueba-record-video-202174376-1610384291_1.jpg"},
                new Coche {IdCoche = 4 , Marca = "Corvette", Modelo = "Stingray", Imagen = "https://soymotor.com/sites/default/files/imagenes/noticia/2020-chevrolet-corvette-stingray-048_0.jpg"}
            };
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int idcoche)
        {
            Coche car = this.Cars.FirstOrDefault(x => x.IdCoche == idcoche);
            return View(car);
        }

        public IActionResult _CochesPartial()
        {
            return PartialView("_CochesPartial", this.Cars);
        }

        public IActionResult _DetailsCoche(int idcoche)
        {
            Coche car = this.Cars.FirstOrDefault(x => x.IdCoche == idcoche);
            return PartialView("_DetailsCoche", car);
        }
    }
}
