using System;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;

namespace ETMS.Utility
{
    public class CrypProvider
    {
        #region properties

        private static string rgbKeys = null;
        public static byte[] RgbKeys
        {
            get
            {
                if (null == rgbKeys)
                {
                    rgbKeys = ConfigurationManager.AppSettings["RgbKeys"] ?? new string('1', 8);

                    if (rgbKeys.Length >= 8)
                    {
                        rgbKeys = rgbKeys.Substring(0, 8);
                    }
                    else
                    {
                        rgbKeys = string.Concat(rgbKeys, new string('0', 8 - rgbKeys.Length));
                    }
                }

                return System.Text.Encoding.UTF8.GetBytes(rgbKeys);
            }
        }

        /// <summary>
        /// 密匙为：@pen2o12
        /// 请勿修改
        /// </summary>
        public static byte[] RgbIV
        {
            get
            {
                return new byte[] { 0x40, 0x70, 0x65, 0x6e, 0x32, 0x6f, 0x31, 0x32 };
            }
        }

        #endregion

        #region methods

        #region RC2加解密字符串

        /// <summary>
        /// RC2加密字符串
        /// </summary>
        /// <param name="plainString">待加密的字符串</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptString(string plainString)
        {
            try
            {
                string sKey = string.Empty;
                string sIV = string.Empty;
                string sCipherbytes = string.Empty;
                string sTmp = string.Empty;

                SymmetricAlgorithm sa = RC2.Create();
                sa.GenerateKey();
                sa.GenerateIV();

                sa.Mode = CipherMode.ECB;
                sa.Padding = PaddingMode.PKCS7;

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);

                byte[] plainbytes = Encoding.UTF8.GetBytes(plainString);
                cs.Write(plainbytes, 0, plainbytes.Length);
                cs.Close();
                byte[] cipherbytes = ms.ToArray();
                ms.Close();

                sKey = getString(sa.Key);
                sIV = getString(sa.IV);
                sCipherbytes = getString(cipherbytes);
                sTmp = sCipherbytes.Insert(4, sKey);
                sTmp = sTmp.Insert(sTmp.Length - 4, sIV);
                return sTmp;
            }
            catch
            {
                return plainString;
            }
        }

        /// <summary>
        /// RC2解密字符串
        /// </summary>
        /// <param name="cipherString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返回源串</returns>
        public static string DecryptString(string cipherString)
        {
            try
            {
                string sPlain = string.Empty;
                string sKey = string.Empty;
                string sIV = string.Empty;
                string sCipherbytes = string.Empty;
                string sTmp = string.Empty;

                sKey = cipherString.Substring(4, 32);
                sIV = cipherString.Substring(cipherString.Length - 20, 16);
                sTmp = cipherString.Remove(4, 32);
                sTmp = sTmp.Remove(sTmp.Length - 20, 16);

                SymmetricAlgorithm sa = RC2.Create();

                sa.Key = getByte(sKey);
                sa.IV = getByte(sIV);

                byte[] cipherbytes = getByte(sTmp);

                sa.Mode = CipherMode.ECB;
                sa.Padding = PaddingMode.PKCS7;

                MemoryStream ms = new MemoryStream(cipherbytes);
                CryptoStream cs = new CryptoStream(ms, sa.CreateDecryptor(), CryptoStreamMode.Read);

                byte[] plainbytes = new Byte[cipherbytes.Length];
                cs.Read(plainbytes, 0, cipherbytes.Length);
                cs.Close();
                ms.Close();

                sPlain = Encoding.UTF8.GetString(plainbytes).TrimEnd('\0');

                return sPlain;
            }
            catch
            {
                return cipherString;
            }
        }

        private static string getString(byte[] b)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(string.Format("{0:X2}", b[i]));
            }
            return sb.ToString();
        }

        private static byte[] getByte(string s)
        {
            byte[] rB = new byte[s.Length / 2];
            int j = 0;
            for (int i = 0; i < s.Length; i += 2)
            {
                string st = s.Substring(i, 2);
                rB[j] = Convert.ToByte(st, 16);
                j++;
            }
            return rB;
        }
        #endregion

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string Encryptor(string original)
        {
            StringBuilder builder = new StringBuilder();
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = RgbKeys;
            provider.IV = RgbIV;
            byte[] bytes = Encoding.UTF8.GetBytes(original);

            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                }

                foreach (byte num in stream.ToArray())
                {
                    builder.AppendFormat("{0:X2}", num);
                }
                stream.Close();
            }
            return builder.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="crypt"></param>
        /// <returns></returns>
        public static string Decryptor(string crypt)
        {
            string original = string.Empty;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = RgbKeys;
            provider.IV = RgbIV;
            byte[] buffer = new byte[crypt.Length / 2];

            for (int i = 0; i < (crypt.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(crypt.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                }
                original = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
            }

            return original;
        }

        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.Zeros
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return BitConverter.ToString(resultArray).Replace("-", string.Empty).ToLower();
        }

        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        #endregion

        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GeneralRandom(int len)
        {
            string seeds = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random randrom = new Random((int)DateTime.Now.Ticks);
            string result = "";

            for (int i = 0; i < len; i++)
            {
                result += seeds[randrom.Next(seeds.Length)];
            }
            return result;
        }
    }
}