using System.Security.Cryptography;
using System.Text;

namespace API_AppPousada.Suporte
{
    public static class Seguranca
    {
        public static string Criptografar(string Message)
        {
            byte[] Results;
            UTF8Encoding UTF8 = new UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes("LbrtsCgj"));
            TripleDESCryptoServiceProvider TDESAlgorithm = new()
            {
                Key = TDESKey,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            return Convert.ToBase64String(Results);
        }
    }
}
