using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace University.Mooc.AppContext
{
    /// <summary>
    /// 定义了对象
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// 主键信息（键值对）
        /// </summary>
        KeyValuePair<string, object> PK { get;}
        
        /// <summary>
        /// 主键名
        /// </summary>
        String KeyName { get;set;}
        
        /// <summary>
        /// 主键值
        /// </summary>
        Object KeyValue { get;set;}
    }

    /// <summary>
    /// 抽象对象实现，方便子类继承
    /// </summary>
    [Serializable]
    public abstract class AbstractObject : IObject
    {
        #region IObject 成员

        public KeyValuePair<string, object> PK
        {
            get { return new KeyValuePair<string, object>(this.KeyName, this.KeyValue); }
        }

        [JsonIgnore]
        public abstract string DefaultKeyName
        {
            get;
        }

        [JsonIgnore]
        private string m_KeyName = "";
        
        [JsonIgnore]
        public string KeyName
        {
            get
            {
                if (string.IsNullOrEmpty(m_KeyName))
                    return DefaultKeyName;
                else
                    return m_KeyName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    m_KeyName = DefaultKeyName;
                else
                    m_KeyName = value;
            }
        }

        [JsonIgnore]
        public abstract object KeyValue { get;set;}

        #endregion
    }
}
