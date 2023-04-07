using System;

namespace ETMS.Utility
{
    /// <summary>
    /// 数据主键类型属性方法扩展
    /// </summary>
    public static class DataPKUtility
    {
        /// <summary>
        /// Guid是否为空
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid pk)
        {
            return pk.Equals(Guid.Empty);
        }

        /// <summary>
        /// Guid是否为空
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static Guid NewID(this Guid pk)
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Int32是否为空
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Int32 pk)
        {
            return pk.Equals(0);
        }


        /// <summary>
        ///  Int32新ID(默认返回0)
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static Int32 NewID(this Int32 pk)
        {
            return 0;
        }

        /// <summary>
        /// Int16是否为空
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Int16 pk)
        {
            return pk.Equals(0);
        }

        /// <summary>
        /// Int16新ID(默认返回0)
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static Int16 NewID(this Int16 pk)
        {
            return 0;
        }

        /// <summary>
        /// String是否为空
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static bool IsEmpty(this String pk)
        {
            return string.IsNullOrEmpty(pk);
        }


        /// <summary>
        /// String新ID(默认返回空)
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public static String NewID(this  String pk)
        {
            return string.Empty;
        }
    }
}
