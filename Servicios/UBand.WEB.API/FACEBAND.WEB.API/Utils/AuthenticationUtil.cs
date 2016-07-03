using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FACEBAND.WEB.API.Utils
{
    public class AuthenticationUtil
    {
        public static string EncodePwd(string salt, string pwd)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(salt + pwd);
            byte[] byteHashedPassword;
            using (MD5 md5 = MD5CryptoServiceProvider.Create())
            {
                byteHashedPassword = md5.ComputeHash(bytePassword);
            }

            return BitConverter.ToString(byteHashedPassword).Replace("-", "");
        }
    }
}