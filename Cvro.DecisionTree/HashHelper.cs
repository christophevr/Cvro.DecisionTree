using System;
using System.Security.Cryptography;
using System.Text;

namespace DecisionTree
{
    internal static class HashHelper
    {
        public static string CalculateShortHash(string input)
        {
            using (var sha1 = new SHA1Managed())
            {
                var inputBytes = Encoding.Unicode.GetBytes(input);
                var hash = sha1.ComputeHash(inputBytes);
                return Convert.ToBase64String(hash).Substring(0, 8);
            }
        }
    }
}