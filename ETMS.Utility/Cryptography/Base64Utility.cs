using System;
using System.Text;

namespace ETMS.Utility.Cryptography
{
    /// <summary>
    /// 编码
    /// </summary>
    public class Base64Utility
    {
        /// <summary>
        /// Base64加码（ASCIIEncoding）
        /// </summary>
        /// <param name="source">待加码文本</param>
        /// <returns>Base64加码文本</returns>
        public static string Base64Encode(string source)
        {
            return Base64Encode(source, new ASCIIEncoding());
        }

        /// <summary>
        /// Base64加码
        /// </summary>
        /// <param name="source">待加码文本</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>Base64加码文本</returns>
        public static string Base64Encode(string source, Encoding encoding)
        {
            byte[] bEncoded = encoding.GetBytes(source);
            return Convert.ToBase64String(bEncoded);
        }

        /// <summary>
        /// Base64解码（ASCIIEncoding）
        /// </summary>
        /// <param name="source">待解码文本</param>
        /// <returns>Base64解码文本</returns>
        public static string Base64Decode(string source)
        {
            return Base64Decode(source, new ASCIIEncoding());
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="source">待解码文本</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>Base64解码文本</returns>
        public static string Base64Decode(string source, Encoding encoding)
        {
            byte[] bDecoded = Convert.FromBase64String(source);
            return encoding.GetString(bDecoded);
        }
    }
}
