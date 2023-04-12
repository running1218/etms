
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using MCS.Library.Core;

namespace University.Mooc.Security
{
    /// <summary>
    /// �ַ������ܣ����ܴ�����
    /// </summary>
    public class StringEncryption : IStringEncryption
    {
        //�漴key
        private const string Key = "DE717CFB69EC4FB88DD00E3EAF9A562D";
        private static byte[] S_DesKeys;
        private static byte[] DesKeys
        {
            get
            {
                if (S_DesKeys == null)
                {
                    S_DesKeys = System.Text.ASCIIEncoding.UTF8.GetBytes(Key.Substring(0, 8));
                }
                return S_DesKeys;
            }
        }
        private static byte[] DesIVs = { 227, 105, 5, 40, 162, 158, 143, 156 };

        /// <summary>
        /// 
        /// </summary>
        public StringEncryption()
        {
        }

        #region IEncryptString ��Ա
        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strData">�������ַ���</param>
        /// <returns>���ܺ��ֽ�</returns>
        /// <remarks>    /// </remarks>
        public byte[] EncryptString(string strData)
        {
            return EncryptString(strData, GetDesObject());
        }

        /// <summary>
        /// �����ֽ�
        /// </summary>
        /// <param name="encryptedData">�������ֽ�</param>
        /// <returns>���ܺ���ַ���</returns>
        /// <remarks>  /// </remarks>
        public string DecryptString(byte[] encryptedData)
        {
            return DecryptString(encryptedData, GetDesObject());
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="des"></param>
        /// <returns></returns>
        public byte[] EncryptString(string strData, DES des)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(des != null, "des");

            byte[] bytes = Encoding.UTF8.GetBytes(strData);

            MemoryStream mStream = new MemoryStream();

            try
            {
                CryptoStream encStream = new CryptoStream(mStream, des.CreateEncryptor(), CryptoStreamMode.Write);

                try
                {
                    encStream.Write(bytes, 0, bytes.Length);
                }
                finally
                {
                    encStream.Close();
                }

                return mStream.ToArray();
            }
            finally
            {
                mStream.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="des"></param>
        /// <returns></returns>
        public string DecryptString(byte[] encryptedData, DES des)
        {
            ExceptionHelper.FalseThrow<ArgumentNullException>(des != null, "des");

            string strResult = string.Empty;

            MemoryStream mStream = new MemoryStream();

            try
            {
                mStream.Write(encryptedData, 0, encryptedData.Length);
                mStream.Seek(0, SeekOrigin.Begin);

                CryptoStream cryptoStream = new CryptoStream(mStream,
                    des.CreateDecryptor(),
                    CryptoStreamMode.Read);

                try
                {
                    strResult = (new StreamReader(cryptoStream, Encoding.UTF8)).ReadToEnd();
                }
                finally
                {
                    cryptoStream.Close();
                }
            }
            finally
            {
                mStream.Close();
            }

            return strResult;
        }

        private static DES GetDesObject()
        {
            DES des = new DESCryptoServiceProvider();

            des.Key = StringEncryption.DesKeys;
            des.IV = StringEncryption.DesIVs;

            return des;
        }
    }
}
