using System.Collections.Generic;
using System.Security.Cryptography;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MovieRS.API.Helper
{
    public class HashHelper
    {
        public static byte[] Hash(string content)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(content));
            }
        }

        public static string HashToString(string content)
        {
            byte[] bytes = Hash(content);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
