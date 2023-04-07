using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace ETMS.Utility.Cryptography
{
    public class Rijndael
    {
        private SymmetricAlgorithm cryptoservice;
        private readonly string strKey = "ETMS20120215";
        private readonly string strIV = "ETMS20120215";

        /// <summary> 
        /// 对称加密类的构造函数 
        /// </summary> 
        public Rijndael()
        {
            cryptoservice = new RijndaelManaged();
        }

        public Rijndael(string key, string iv)
            : this()
        {
            strKey = key;
            strIV = iv;
        }

        /// <summary> 
        /// 获得密钥 
        /// </summary> 
        /// <returns>密钥</returns> 
        private byte[] getLegalKey()
        {
            string stemp = strKey;
            cryptoservice.GenerateKey();
            byte[] byttemp = cryptoservice.Key;
            int keylength = byttemp.Length;
            if (stemp.Length > keylength)
                stemp = stemp.Substring(0, keylength);
            else if (stemp.Length < keylength)
                stemp = stemp.PadRight(keylength);
            return ASCIIEncoding.ASCII.GetBytes(stemp);
        }

        /// <summary> 
        /// 获得初始向量iv 
        /// </summary> 
        /// <returns>初始向量iv</returns> 
        private byte[] getLegalIV()
        {
            string stemp = strIV;
            cryptoservice.GenerateIV();
            byte[] byttemp = cryptoservice.IV;
            int ivlength = byttemp.Length;
            if (stemp.Length > ivlength)
                stemp = stemp.Substring(0, ivlength);
            else if (stemp.Length < ivlength)
                stemp = stemp.PadRight(ivlength);
            return ASCIIEncoding.ASCII.GetBytes(stemp);
        }

        /// <summary> 
        /// 加密方法 
        /// </summary> 
        /// <param name="source">待加密的串</param> 
        /// <returns>经过加密的串</returns> 
        public string Encrypto(string source)
        {
            if (source == null)
                throw new ArgumentNullException("source", "带加密的字符串不能为null");

            byte[] bytin = UTF8Encoding.UTF8.GetBytes(source);

            cryptoservice.Key = getLegalKey();
            cryptoservice.IV = getLegalIV();

            ICryptoTransform encrypto = cryptoservice.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytin, 0, bytin.Length);
                cs.FlushFinalBlock();
                byte[] bytout = ms.ToArray();
                return Convert.ToBase64String(bytout);
            }
        }

        /// <summary> 
        /// 解密方法 
        /// </summary> 
        /// <param name="source">待解密的串</param> 
        /// <returns>经过解密的串</returns> 
        public string Decrypto(string source)
        {
            if (source == null)
                throw new ArgumentNullException("source", "带解密的字符串不能为null");

            byte[] bytin = Convert.FromBase64String(source);

            cryptoservice.Key = getLegalKey();
            cryptoservice.IV = getLegalIV();

            ICryptoTransform encrypto = cryptoservice.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream(bytin, 0, bytin.Length))
            {
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 获得签名字符串
        /// </summary>
        /// <param name="source">获得签名的种子</param>
        /// <returns></returns>
        public string CreateSignature(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key", "种子字符串不能为null");

            byte[] data = ASCIIEncoding.ASCII.GetBytes(key);
            SHA1 shaM = new SHA1Managed();
            byte[] buffer = shaM.ComputeHash(data);

            return Convert.ToBase64String(buffer);
        }
    }
}
