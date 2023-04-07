using System;
using System.Text;

using System.IO;
using System.Security.Cryptography;

namespace ETMS.Utility.Cryptography
{
    /// <summary>
    /// 3DES��/����
    /// </summary>
    public class TripleDesCryptographyUtility
    {
        private static byte[] TripleDesKey
        {
            get
            {
                //TODO: Config Key
                return new ASCIIEncoding().GetBytes("ProgrammedBy");//24���ַ�
            }
        }

        private static byte[] TripleDesIV
        {
            get
            {
                //TODO: Config Key
                return new ASCIIEncoding().GetBytes("Open2008");//8���ַ�
            }
        }
        /// <summary>
        /// 3DES����
        /// </summary>
        /// <param name="source">Դ�ı�</param>
        /// <returns>���ܺ�Bytes</returns>
        public static byte[] EncryptTextToBytes(string source)
        {
            try
            {
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();

                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    new TripleDESCryptoServiceProvider().CreateEncryptor(TripleDesKey, TripleDesIV),
                    CryptoStreamMode.Write);

                // Convert the passed string to a byte array.
                byte[] toEncrypt = new ASCIIEncoding().GetBytes(source);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("���ܴ���: {0}", e.Message);
                return null;
            }

        }

        /// <summary>
        /// 3DES���ܣ�UnicodeEncoding��
        /// </summary>
        /// <param name="source">Դ�ı�</param>
        /// <returns>���ܺ��ı�</returns>
        public static string EncryptTextToString(string source)
        {
            byte[] bytes = EncryptTextToBytes(source);
            string encrypted = Convert.ToBase64String(bytes);
            return encrypted;
        }

        /// <summary>
        /// 3DES����
        /// </summary>
        /// <param name="source">ԴBytes</param>
        /// <returns>���ܺ��ı�</returns>
        public static string DecryptTextFromBytes(byte[] source)
        {
            try
            {
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();

                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(source);

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    new TripleDESCryptoServiceProvider().CreateDecryptor(TripleDesKey, TripleDesIV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[source.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                string result = new ASCIIEncoding().GetString(fromEncrypt);

                //�Ƴ���λ�ַ�
                result = result.Replace('\0', ' ').TrimEnd();

                return result;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("���ܴ���: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 3DES���ܣ�UnicodeEncoding��
        /// </summary>
        /// <param name="source">Դ�ı�</param>
        /// <returns>���ܺ��ı�</returns>
        public static string DecryptTextFromString(string source)
        {
            byte[] bSource = Convert.FromBase64String(source);
            string decrypted = DecryptTextFromBytes(bSource);
            return decrypted;
        }

    }
}
