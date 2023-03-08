using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;

namespace MvcCoreUtilidades.Controllers
{
    public class CifradoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string contenido, string resultado, string accion)
        {
            string response = HelperCryptography.EncriptarTextoBasico(contenido);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["TEXTOCIFRADO"] = response;
            }
            else if (accion.ToLower() == "comparar")
            {
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "Los valores no coinciden";
                }
                else
                {
                    ViewData["MENSAJE"] = "Contenidos iguales!!!";
                }
            }
            return View();
        }

        public IActionResult CifradoEficiente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoEficiente(string contenido, string resultado, string accion)
        {
            if (accion.ToLower() == "cifrar")
            {
                string response = HelperCryptography.EncriptarContenido(contenido, false);
                ViewData["TEXTOCIFRADO"] = response;
            }
            else if (accion.ToLower() == "comparar")
            {
                string response = HelperCryptography.EncriptarContenido(contenido, true);
                if (response != resultado)
                {
                    ViewData["MENSAJE"] = "<h1 style='color:red'>No son iguales</h1>";
                }
                else
                {
                    ViewData["MENSAJE"] = "<h1 style='color:blue'>Son iguales!!!</h1>";
                }
            }
            return View();
        }
    }
}
