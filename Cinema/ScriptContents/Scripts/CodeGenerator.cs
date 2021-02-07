using System;
using System.Security.Cryptography;
using System.Text;

namespace Scripts
{
    static public class CodeGenerator
    {
        #region Variables
        
        static public readonly string StringAccessSymbols = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        static private readonly byte SizeCode = 7;

        #endregion

        #region Public Class Methods
        
        public static string GenerateNewCode()
        {
            string NewCode = string.Empty;
            Random random = new Random();

            for (int i = 0; i < SizeCode; i++)
            {
                NewCode += StringAccessSymbols[random.Next(0, StringAccessSymbols.Length - 1)];
            }

            return NewCode;
        }

        public static Guid GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
 
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte unity in byteHash)
            {
                hash += string.Format("{0:x2}", unity);

            }

            return new Guid(hash);
        }

        #endregion
    }
}
