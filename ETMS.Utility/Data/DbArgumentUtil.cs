using System;

namespace ETMS.Utility.Data
{
    public class DbArgumentUtil
    {
        /// <summary>
        /// 检查查询字符串，如果不符合要求，抛出异常
        /// 当前设置：不允许包含注释
        /// </summary>
        /// <param name="argument"></param>
        private static void CheckQueryString(string argument)
        {
            if (!string.IsNullOrEmpty(argument) && (argument.IndexOf("--") >= 0 || argument.IndexOf("/*") >= 0))//不允许包含注释
            {
                throw new Exception(string.Format("参数{0}中包含无效字符！", argument));
            }
        }

        /// <summary>
        /// 检查查询字符串
        /// 当前设置：单引号("'")改为2个单引号("''")，不允许包含注释
        /// </summary>
        /// <param name="argument"></param>
        public static void CheckSqlQueryString(ref string argument)
        {
            CheckQueryString(argument);

            if (!string.IsNullOrEmpty(argument))
            {
                argument = argument.Replace("'", "''");
            }
        }

        /// <summary>
        /// 检查字符串参数，如果不符合要求，抛出异常
        /// 当前设置：不允许为空，不允许为空串/全空格
        /// 警告：空格会被截取
        /// </summary>
        /// <param name="argument">待检查的字符串参数</param>
        /// <param name="argumentName">参数名称</param>
        public static void CheckStringArgument(ref string argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);

            argument = argument.Trim();

            if (argument.Length < 1)
                throw new ArgumentException("不能为空，不能全为空格！", argumentName);
        }

        /// <summary>
        /// 检查字符串参数，如果不符合要求，抛出异常
        /// 当前设置：不允许为空，不允许为空串/全空格，不允许超出最大长度
        /// 警告：空格会被截取
        /// </summary>
        /// <param name="argument">待检查的字符串参数</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="argumentName">参数名称</param>
        public static void CheckStringArgument(ref string argument, int maxLength, string argumentName)
        {
            CheckStringArgument(ref argument, argumentName);

            if (argument.Length > maxLength)
                throw new ArgumentException("超出最大长度！", argumentName);
        }


        /// <summary>
        /// Checks if the argument is null or empty.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void CheckStringArrayArgument(ref string[] argument, string argumentName)
        {
            CheckStringArrayArgument(ref argument, -1, argumentName);
        }

        /// <summary>
        /// Checks if the argument is null or empty.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        public static void CheckStringArrayArgument(ref string[] argument, int maxLength, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);

            if (argument.Length < 1)
                throw new ArgumentException("参数不能为空！", argumentName);

            for (int i = 0; i < argument.Length; i++)
            {
                argument[i] = argument[i].Trim();
                if (argument[i].Length < 1)
                    throw new ArgumentException("不能有空参数项！", argumentName);
                else if ((argument[i].Length > maxLength) && (maxLength > -1))
                    throw new ArgumentOutOfRangeException("有参数项超出最大长度！", argumentName);
            }
        }

        /// <summary>
        /// Int参数检查
        /// </summary>
        /// <param name="argument">参数值</param>
        /// <param name="minvalue">最小值</param>
        /// <param name="maxvalue">最大值</param>
        /// <param name="argumentName">参数名称</param>
        public static void CheckIntArgument(ref int argument, int minvalue, int maxvalue, string argumentName)
        {
            if ((argument < minvalue) || (argument > maxvalue))
            {
                throw new ArgumentOutOfRangeException("参数超出值范围！", argumentName);
            }
        }
    }
}
