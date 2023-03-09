using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MvcCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache memoryCache;

        public CachingController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        /*public IActionResult CustomMemory()
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            if (this.memoryCache.Get("FECHA") == null)
            {
                this.memoryCache.Set("FECHA", fecha);
                ViewData["MENSAJE"] = "Almacenando en Cache";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA");
            }
            else
            {
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Recuperando Cache";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }*/

        public IActionResult CustomMemory(int? tiempo)
        {
            if (tiempo == null)
            {
                tiempo = 5;
            }
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            if (this.memoryCache.Get("FECHA") == null)
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
                this.memoryCache.Set("FECHA", fecha, options);
                ViewData["MENSAJE"] = "Almacenando en Cache";
                ViewData["FECHA"] = this.memoryCache.Get("FECHA");
            }
            else
            {
                fecha = this.memoryCache.Get<string>("FECHA");
                ViewData["MENSAJE"] = "Recuperando Cache";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }

        [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Client)]

        public IActionResult DistributedMemory()
        {
            string fecha = DateTime.Now.ToLongDateString() + " -- " + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
    }
}
