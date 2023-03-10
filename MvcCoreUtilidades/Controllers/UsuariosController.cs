using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.Controllers
{
    public class UsuariosController : Controller
    {
        private HelperPathImages helperPathImages;
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo, HelperPathImages helperPathImages)
        {
            this.repo = repo;
            this.helperPathImages = helperPathImages;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register (string nombre, string email, string password, IFormFile imagen)
        {
            string protocol = HttpContext.Request.IsHttps ? "https://" : "http://";
            string domainName = HttpContext.Request.Host.Value.ToString();
            string url = protocol + domainName;
            string max = this.repo.GetMaxIdUsuario().ToString();
            string path = await this.helperPathImages.UploadFileAsync(imagen, max + "-" + imagen.FileName, url, AllFolders.Images);
            path = path.Replace("\\", "/");
            await this.repo.RegisterUser(nombre, email, password, path);
            ViewData["MENSAJE"] = "Usuario registrado correctamente";
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            Usuario user = this.repo.LogInUser(email, password);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                return View(user);
            }
        }
    }
}
