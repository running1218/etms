using System;

namespace ETMS.Components.Poll.API.Entity
{
    /// <summary>
    /// 矩阵题评分范围实体
    /// </summary>
    [Serializable]
    public class Poll_MatrixScope
    {
        #region Constructor
        /// <summary>
        /// 矩阵题评分范围构造函数--默认
        /// </summary>
        public Poll_MatrixScope()
        {
        }

        /// <summary>
        /// 矩阵题评分范围构造函数--所有属性
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
        /// 范围ID
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
        /// 范围名称
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
