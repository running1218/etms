using System;

namespace ETMS.Utility.Data
{
    public class DbArgumentUtil
    {
        /// <summary>
        /// ����ѯ�ַ��������������Ҫ���׳��쳣
        /// ��ǰ���ã����������ע��
        /// </summary>
        /// <param name="argument"></param>
        private static void CheckQueryString(string argument)
        {
            if (!string.IsNullOrEmpty(argument) && (argument.IndexOf("--") >= 0 || argument.IndexOf("/*") >= 0))//���������ע��
            {
                throw new Exception(string.Format("����{0}�а�����Ч�ַ���", argument));
            }
        }

        /// <summary>
        /// ����ѯ�ַ���
        /// ��ǰ���ã�������("'")��Ϊ2��������("''")�����������ע��
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
        /// ����ַ������������������Ҫ���׳��쳣
        /// ��ǰ���ã�������Ϊ�գ�������Ϊ�մ�/ȫ�ո�
        /// ���棺�ո�ᱻ��ȡ
        /// </summary>
        /// <param name="argument">�������ַ�������</param>
        /// <param name="argumentName">��������</param>
        public static void CheckStringArgument(ref string argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);

            argument = argument.Trim();

            if (argument.Length < 1)
                throw new ArgumentException("����Ϊ�գ�����ȫΪ�ո�", argumentName);
        }

        /// <summary>
        /// ����ַ������������������Ҫ���׳��쳣
        /// ��ǰ���ã�������Ϊ�գ�������Ϊ�մ�/ȫ�ո񣬲���������󳤶�
        /// ���棺�ո�ᱻ��ȡ
        /// </summary>
        /// <param name="argument">�������ַ�������</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="argumentName">��������</param>
        public static void CheckStringArgument(ref string argument, int maxLength, string argumentName)
        {
            CheckStringArgument(ref argument, argumentName);

            if (argument.Length > maxLength)
                throw new ArgumentException("������󳤶ȣ�", argumentName);
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
                throw new ArgumentException("��������Ϊ�գ�", argumentName);

            for (int i = 0; i < argument.Length; i++)
            {
                argument[i] = argument[i].Trim();
                if (argument[i].Length < 1)
                    throw new ArgumentException("�����пղ����", argumentName);
                else if ((argument[i].Length > maxLength) && (maxLength > -1))
                    throw new ArgumentOutOfRangeException("�в��������󳤶ȣ�", argumentName);
            }
        }

        /// <summary>
        /// Int�������
        /// </summary>
        /// <param name="argument">����ֵ</param>
        /// <param name="minvalue">��Сֵ</param>
        /// <param name="maxvalue">���ֵ</param>
        /// <param name="argumentName">��������</param>
        public static void CheckIntArgument(ref int argument, int minvalue, int maxvalue, string argumentName)
        {
            if ((argument < minvalue) || (argument > maxvalue))
            {
                throw new ArgumentOutOfRangeException("��������ֵ��Χ��", argumentName);
            }
        }
    }
}
