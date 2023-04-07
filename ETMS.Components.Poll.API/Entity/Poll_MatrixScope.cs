using System;

namespace ETMS.Components.Poll.API.Entity
{
    /// <summary>
    /// ���������ַ�Χʵ��
    /// </summary>
    [Serializable]
    public class Poll_MatrixScope
    {
        #region Constructor
        /// <summary>
        /// ���������ַ�Χ���캯��--Ĭ��
        /// </summary>
        public Poll_MatrixScope()
        {
        }

        /// <summary>
        /// ���������ַ�Χ���캯��--��������
        /// </summary>
        public Poll_MatrixScope(Int32 id, string name)
        {
            this.m_ID = id;
            this.m_Name = name;
        }

        #endregion Constructor

        #region Fields, Properties
        private Int32 m_ID;
        /// <summary>
        /// ��ΧID
        /// </summary>
        public Int32 ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        private string m_Name;
        /// <summary>
        /// ��Χ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }
        #endregion Fields, Properties
    }
}
