using MvcCoreUtilidades.Context;
using MvcCoreUtilidades.Models;

namespace MvcCoreUtilidades.Repositories
{
    public class RepositoryEmpleados
    {
        private UsuariosContext context;

        public RepositoryEmpleados(UsuariosContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }

        public Empleado FindEmpleado(int idempleado)
        {
            return this.context.Empleados.FirstOrDefault(x => x.IdEmpleado == idempleado);
        }

        public List<Empleado> GetEmpleadosSession(List<int> ids)
        {
            var consulta = from datos in this.context.Empleados
                           where ids.Contains(datos.IdEmpleado)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            return consulta.ToList();
        }
    }
}
