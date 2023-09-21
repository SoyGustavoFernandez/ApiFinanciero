using System.Security.Cryptography;
using System.Text;

namespace SharedBackEnd
{
    public class Encriptar
    {
        public static string EncryptarCadena(string strCadena)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(strCadena);
            byte[] hashBytes = SHA256.HashData(inputBytes);

            StringBuilder builder = new();
            foreach (byte b in hashBytes) builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}