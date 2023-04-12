using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace University.Mooc.AppContext
{
    /// <summary>
    /// �����˶���
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// ������Ϣ����ֵ�ԣ�
        /// </summary>
        KeyValuePair<string, object> PK { get;}
        
        /// <summary>
        /// ������
        /// </summary>
        String KeyName { get;set;}
        
        /// <summary>
        /// ����ֵ
        /// </summary>
        Object KeyValue { get;set;}
    }

    /// <summary>
    /// �������ʵ�֣���������̳�
    /// </summary>
    [Serializable]
    public abstract class AbstractObject : IObject
    {
        #region IObject ��Ա

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
