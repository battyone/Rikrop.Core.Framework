using System.Security.Cryptography;
using System.Text;

namespace Rikrop.Core.Framework
{
    public static class CryptoHelper
    {
        public static string GetShaHash(string str)
        {
            var sha512 = new SHA512Managed();
            var data = Encoding.UTF8.GetBytes(str);
            data = sha512.ComputeHash(data);
            var s = new StringBuilder();
            foreach (var b in data)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
    }
}
