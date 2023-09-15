using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Licitacion.Servicios.Utiles
{
    public class Encrypt
    {
        /// <summary>
        /// Funcion para hashear un string con el algoritmo SHA256
        /// </summary>
        /// <param name="str">cadena sin encriptar</param>
        /// <returns>cadena encriptada</returns>
        public static string GetSHA256(string str)
        {
            try
            {
                SHA256 sha256 = SHA256Managed.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha256.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
