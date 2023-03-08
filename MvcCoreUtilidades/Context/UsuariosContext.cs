using Microsoft.EntityFrameworkCore;
using MvcCoreUtilidades.Models;

namespace MvcCoreUtilidades.Context
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
