using System;
using System.Security.Cryptography;
using System.Text;

namespace DearlerPlatform.Common.Md5Module
{
    public static class Md5Helper
    {
        
        public static string ToMd5(this string str){
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(Encoding.Default.GetBytes(str+"@Holy"));
            var md5Str = BitConverter.ToString(bytes).Replace("-","");
            return md5Str;
        }
    }
}