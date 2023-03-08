using System.Text;
using System.Security.Cryptography;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        public static string Salt { get; set; }

        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 50 ; i++)
            {
                int aleat = random.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static string EncriptarContenido (string contenido, bool comparar)
        {
            if (comparar == false)
            {
                Salt = GenerateSalt();
            }
            string contenidosalt = contenido + Salt;
            SHA256 sha256 = new SHA256Managed();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidosalt);
            for (int i = 1; i <= 55; i++)
            {
                salida = sha256.ComputeHash(salida);
            }
            sha256.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        public static string EncriptarTextoBasico(string contenido)
        {
            byte[] entrada;
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            SHA1Managed sHA1 = new SHA1Managed();
            entrada = encoding.GetBytes(contenido);
            salida = sHA1.ComputeHash(entrada);
            string result = encoding.GetString(salida);
            return result;
        }
    }
}
