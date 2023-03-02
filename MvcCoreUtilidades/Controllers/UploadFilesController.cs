﻿using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using MvcCoreUtilidades.Models;
using System.Diagnostics;

namespace MvcCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPath;

        public UploadFilesController (HelperPathProvider helperPath)
        {
            this.helperPath = helperPath;
        }
        /*public IActionResult Index()
        {
            return View();
        }*/
        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            /*string tempFolder = Path.GetTempPath();*/
            /*string rootFolder = this.environment.WebRootPath;*/
            string fileName = fichero.FileName;
            string path = this.helperPath.MapPath(fileName, Folders.Uploads);
            /*string path = Path.Combine(tempFolder, fileName);*/
/*            string path = Path.Combine(rootFolder, "uploads", fileName);
*/            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            return View();
        }
    }
}