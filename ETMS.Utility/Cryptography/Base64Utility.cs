using System;
using System.Text;

namespace ETMS.Utility.Cryptography
{
    /// <summary>
    /// ����
    /// </summary>
    public class Base64Utility
    {
        /// <summary>
        /// Base64���루ASCIIEncoding��
        /// </summary>
        /// <param name="source">�������ı�</param>
        /// <returns>Base64�����ı�</returns>
        public static string Base64Encode(string source)
        {
            return Base64Encode(source, new ASCIIEncoding());
        }

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="source">�������ı�</param>
        /// <param name="encoding">���뷽ʽ</param>
        /// <returns>Base64�����ı�</returns>
        public static string Base64Encode(string source, Encoding encoding)
        {
            byte[] bEncoded = encoding.GetBytes(source);
            return Convert.ToBase64String(bEncoded);
        }

        /// <summary>
        /// Base64���루ASCIIEncoding��
        /// </summary>
        /// <param name="source">�������ı�</param>
        /// <returns>Base64�����ı�</returns>
        public static string Base64Decode(string source)
        {
            return Base64Decode(source, new ASCIIEncoding());
        }

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="source">�������ı�</param>
        /// <param name="encoding">���뷽ʽ</param>
        /// <returns>Base64�����ı�</returns>
        public static string Base64Decode(string source, Encoding encoding)
        {
            byte[] bDecoded = Convert.FromBase64String(source);
            return encoding.GetString(bDecoded);
        }
    }
}
