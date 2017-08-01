using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace transmanage
{
    public class Security
    {
        public string HashPass(string input)
        {
            HashAlgorithm sha = SHA256.Create();
            Byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        public bool Compare(string pass, string stored)
        {
            HashAlgorithm sha = SHA1.Create();
            Byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
            string passHash = Convert.ToBase64String(hash);

            if (passHash == stored)
                return true;
           return  false;
        }
    }
}
