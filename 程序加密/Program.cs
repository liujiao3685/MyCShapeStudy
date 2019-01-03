using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace 程序加密
{
    class Program
    {
        static void Main(string[] args)
        {

            string original = "9999";

            EncryptMD5(original);

            Console.ReadKey();
        }

        //DES加密
        private static byte[] buffer;
        private static DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string DncryptDes()
        {
            MemoryStream ms = new MemoryStream(buffer);//将加密后的字节数据加入内存流中
            CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateDecryptor(), CryptoStreamMode.Read);//内存流连接到解密流中
            StreamReader sr = new StreamReader(cryStream);
            string original = sr.ReadLine();//将解密流读取为字符串
            sr.Close();
            cryStream.Close();
            ms.Close();

            return original;

        }


        private static CspParameters param;

        /// <summary>
        /// RAS解密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string DncryptRAS(string original)
        {
            param = new CspParameters();
            param.KeyContainerName = "Olive";
            string ciphertext;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(original);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                ciphertext = Encoding.Default.GetString(decryptdata);
            }
            return ciphertext;
        }

        /// <summary>
        /// RAS加密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string EncryptRAS(string original)
        {
            param = new CspParameters();
            string ciphertext;
            param.KeyContainerName = "Olive";//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(original);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                ciphertext = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }

            return ciphertext;
        }

        /// <summary>
        /// 破解简单的数字密码
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <returns>原文</returns>
        public static string CrackOrignal(string ciphertext)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string key = string.Empty;
            for (int i = 0; i < 9999; i++)
            {
                if (CheckOriginal(i.ToString(), ciphertext))
                {
                    key = i.ToString();
                    break;
                }
            }
            return key;
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="orignal"></param>
        /// <param name="ciphertext"></param>
        /// <returns></returns>
        public static bool CheckOriginal(string orignal, string ciphertext)
        {
            string cip = EncryptMD5(orignal);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;//不区分大小写

            return comparer.Compare(cip, ciphertext) == 0 ? true : false;

        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="original">原文</param>
        /// <returns>密文</returns>
        public static string EncryptMD5(string original)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(original);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            string ciphertext = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串

            Console.WriteLine(String.Format("原文：{0},密文：{1}", original, ciphertext));

            ciphertext = BitConverter.ToString(md5.ComputeHash((UTF8Encoding.Default.GetBytes(original)))).Replace("-", "");

            Console.WriteLine(String.Format("原文：{0},密文：{1}", original, ciphertext));

            //多次加密
            ciphertext = BitConverter.ToString(md5.ComputeHash((UTF8Encoding.Default.GetBytes(original)))).Replace("-", "") +
               BitConverter.ToString(md5.ComputeHash((UTF8Encoding.Default.GetBytes(original)))).Replace("-", "") +
               BitConverter.ToString(md5.ComputeHash((UTF8Encoding.Default.GetBytes(original)))).Replace("-", "");

            Console.WriteLine(String.Format("原文：{0},密文：{1}", original, ciphertext));

            return ciphertext;
        }

    }
}
