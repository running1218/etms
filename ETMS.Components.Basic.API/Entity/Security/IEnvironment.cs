using System;

namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// ��ɫ��������
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// ����ID
        /// </summary>
        Int32 ID { get;}

        /// <summary>
        /// ��������
        /// </summary>
        string Name { get;}

        /// <summary>
        /// ��������
        /// </summary>
        String Type { get;}

        /// <summary>
        /// ������������Ӧ����
        /// </summary>
        String Parm { get;}
    }
}
