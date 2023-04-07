using System;

namespace ETMS.Components.Basic.API.Entity.Security
{
    /// <summary>
    /// 角色所处环境
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// 环境ID
        /// </summary>
        Int32 ID { get;}

        /// <summary>
        /// 环境名称
        /// </summary>
        string Name { get;}

        /// <summary>
        /// 环境类型
        /// </summary>
        String Type { get;}

        /// <summary>
        /// 环境类型所对应参数
        /// </summary>
        String Parm { get;}
    }
}
