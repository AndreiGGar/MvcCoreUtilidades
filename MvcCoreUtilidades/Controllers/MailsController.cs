using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Helpers;
using MvcCoreUtilidades.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace MvcCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private HelperUploadFiles helperUploadFiles;
        private HelperMail helperMail;

        public MailsController(HelperUploadFiles helperUploadFiles, HelperMail helperMail)
        {
            this.helperUploadFiles = helperUploadFiles;
            this.helperMail = helperMail;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string para, string asunto, string mensaje, List<IFormFile> files)
        {
            if (files != null)
            {
                if (files.Count > 1)
                {
                    List<string> paths = await this.helperUploadFiles.UploadFileAsync(files, Folders.Temporal);
                    await this.helperMail.SendMailAsync(para, asunto, mensaje, paths);
                } else
                {
                    string path = await this.helperUploadFiles.UploadFileAsync(files[0], Folders.Temporal);
                    await this.helperMail.SendMailAsync(para, asunto, mensaje, path);
                }
            } else
            {
                await this.helperMail.SendMailAsync(para, asunto, mensaje);
            }
            ViewData["MENSAJE"] = "Email enviado correctamente";
            return View();
        }
    }
}