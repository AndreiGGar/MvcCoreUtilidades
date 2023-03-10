using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MvcCoreUtilidades.Extensions;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;
        private IMemoryCache memoryCache;

        public EmpleadosController(RepositoryEmpleados repo, IMemoryCache memoryCache)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
        }
        public IActionResult SessionSalarios(int? salario)
        {
            if (salario != null)
            {
                int sumasalarial = 0;
                if (HttpContext.Session.GetString("SUMASALARIAL") != null)
                {
                    sumasalarial = int.Parse(HttpContext.Session.GetString("SUMASALARIAL"));
                }
                sumasalarial += salario.Value;
                HttpContext.Session.SetString("SUMASALARIAL", sumasalarial.ToString());
                ViewData["MENSAJE"] = "Salario almacenado: " + salario.Value;
            }
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult SumaSalarios()
        {
            return View();
        }

        public IActionResult SessionEmpleados(int? idempleado)
        {
            if (idempleado != null)
            {
                Empleado empleado = this.repo.FindEmpleado(idempleado.Value);
                List<Empleado> empleadosSession;
                if (HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS") != null)
                {
                    empleadosSession = HttpContext.Session.GetObject<List<Empleado>>("EMPLEADOS");
                } else
                {
                    empleadosSession = new List<Empleado>();
                }
                empleadosSession.Add(empleado);
                HttpContext.Session.SetObject("EMPLEADOS", empleadosSession);
                ViewData["MENSAJE"] = "Empleado " + empleado.Apellido + " almacenado en Session.";
            }
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult EmpleadosAlmacenados()
        {
            return View();
        }

        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult SessionEmpleadosOK(int? idempleado, int? idfavorito)
        {

            if (idfavorito != null)
            {
                List<Empleado> empleadosFavoritos;
                if (this.memoryCache.Get("FAVORITOS") == null)
                {
                    empleadosFavoritos = new List<Empleado>();
                }
                else
                {
                    empleadosFavoritos = this.memoryCache.Get<List<Empleado>>("FAVORITOS");
                }
                Empleado empleado = this.repo.FindEmpleado(idfavorito.Value);
                empleadosFavoritos.Add(empleado);
                this.memoryCache.Set("FAVORITOS", empleadosFavoritos);
            }
            if (idempleado != null)
            {
                List<int> idsEmpleado;
                if (HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS") != null)
                {
                    idsEmpleado = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
                }
                else
                {
                    idsEmpleado = new List<int>();
                }
                idsEmpleado.Add(idempleado.Value);
                HttpContext.Session.SetObject("IDSEMPLEADOS", idsEmpleado);
                ViewData["MENSAJE"] = "Empleados almacenados: " + idsEmpleado.Count;
            }
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        /*public IActionResult EmpleadosAlmacenadosOK(int? ideliminar)
        {
            List<int> idsEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
            if (idsEmpleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados almacenados";
                return View();
            } else
            {
                if (ideliminar != null)
                {
                    idsEmpleados.Remove(ideliminar.Value);
                    if (idsEmpleados.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSEMPLEADOS");
                    } else
                    {
                        HttpContext.Session.SetObject("IDSEMPLEADOS", idsEmpleados);
                    }
                }
                List<Empleado> empleadosSession = this.repo.GetEmpleadosSession(idsEmpleados);
                return View(empleadosSession);
            }
        }*/

        public IActionResult EmpleadosAlmacenadosOK(int? ideliminar)
        {
            List<int> idsEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
            if (idsEmpleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados almacenados";
                return View();
            }
            else
            {
                if (ideliminar != null)
                {
                    idsEmpleados.Remove(ideliminar.Value);
                    if (idsEmpleados.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSEMPLEADOS");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IDSEMPLEADOS", idsEmpleados);
                    }
                }
                List<Empleado> empleadosSession = this.repo.GetEmpleadosSession(idsEmpleados);
                return View(empleadosSession);
            }
        }

        public IActionResult EmpleadosFavoritos()
        {
            return View();
        }
    }
}
