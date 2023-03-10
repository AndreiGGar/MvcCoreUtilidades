using MvcCoreUtilidades.Context;
using MvcCoreUtilidades.Helpers;
using MvcCoreUtilidades.Models;

namespace MvcCoreUtilidades.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;

        public RepositoryUsuarios(UsuariosContext context)
        {
            this.context = context;
        }

        public int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUsuario) + 1;
            }
        }

        public async Task RegisterUser(string nombre, string email, string password, string imagen)
        {
            Usuario user = new Usuario();
            user.IdUsuario = this.GetMaxIdUsuario();
            user.Nombre = nombre;
            user.Email = email;
            user.Imagen = imagen;
            user.Salt = HelperCrypto.GenerateSalt();
            user.Password = HelperCrypto.EncryptPassword(password, user.Salt);
            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        public Usuario LogInUser(string email, string password)
        {
            Usuario user = this.context.Usuarios.FirstOrDefault(z => z.Email == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                byte[] passUsuario = user.Password;
                string salt = user.Salt;
                byte[] temp = HelperCrypto.EncryptPassword(password, salt);
                bool respuesta = HelperCrypto.CompareArrays(passUsuario, temp);
                if (respuesta == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
