using System;

namespace ETMS.Utility
{
    /// <summary>
    /// ���������������Է�����չ
    /// </summary>
    public static class DataPKUtility
    {
        /// <summary>
        /// Guid�Ƿ�Ϊ��
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid pk)
        {
            return pk.Equals(Guid.Empty);
        }

        /// <summary>
        /// Guid�Ƿ�Ϊ��
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static Guid NewID(this Guid pk)
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Int32�Ƿ�Ϊ��
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Int32 pk)
        {
            return pk.Equals(0);
        }


        /// <summary>
        ///  Int32��ID(Ĭ�Ϸ���0)
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static Int32 NewID(this Int32 pk)
        {
            return 0;
        }

        /// <summary>
        /// Int16�Ƿ�Ϊ��
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Int16 pk)
        {
            return pk.Equals(0);
        }

        /// <summary>
        /// Int16��ID(Ĭ�Ϸ���0)
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static Int16 NewID(this Int16 pk)
        {
            return 0;
        }

        /// <summary>
        /// String�Ƿ�Ϊ��
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this String pk)
        {
            return string.IsNullOrEmpty(pk);
        }


        /// <summary>
        /// String��ID(Ĭ�Ϸ��ؿ�)
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static String NewID(this  String pk)
        {
            return string.Empty;
        }
    }
}
