using Microsoft.AspNetCore.Mvc;
using MvcCoreUtilidades.Extensions;
using MvcCoreUtilidades.Models;
using MvcCoreUtilidades.Repositories;

namespace MvcCoreUtilidades.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
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

        public IActionResult SessionEmpleadosOK(int? idempleado)
        {
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

        public IActionResult EmpleadosAlmacenadosOK()
        {
            List<int> idsEmpleados = HttpContext.Session.GetObject<List<int>>("IDSEMPLEADOS");
            if (idsEmpleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados almacenados";
                return View();
            } else
            {
                List<Empleado> empleadosSession = this.repo.GetEmpleadosSession(idsEmpleados);
                return View(empleadosSession);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
