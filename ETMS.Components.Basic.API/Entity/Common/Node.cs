using System;
using System.Collections.Generic;
using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.API.Entity.Common
{
    /// <summary>
    /// 节点类型
    /// 节点类型决定当前节点能否有子节点
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 根节点
        /// </summary>
        Root = 0,
        /// <summary>
        /// 枝节点
        /// </summary>
        Branch = 1,
        /// <summary>
        /// 叶节点
        /// </summary>
        Leaf = 2
    }

    /// <summary>
    /// 节点定义
    /// </summary>
    /// <typeparam name="Node">范型节点类型</typeparam>
    [Serializable]
    public abstract class Node : ETMS.AppContext.AbstractObject
    {
        private Int32 m_NodeID = 0;
        /// <summary>
        /// 节点ID
        /// </summary>
        public Int32 NodeID
        {
            get
            {
                return m_NodeID;
            }
            set
            {
                m_NodeID = value;
            }
        }

        private string m_NodeName = string.Empty;
        /// <summary>
        /// 节点名称
        /// </summary>
        public String NodeName
        {
            get
            {
                return m_NodeName;
            }
            set
            {
                m_NodeName = value;
            }
        }

        private Int32 m_ParentNodeID = 0;
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Int32 ParentNodeID
        {
            get
            {
                if (this.ParentNode != null)
                    return this.ParentNode.NodeID;
                else
                    return m_ParentNodeID;
            }
            set
            {
                m_ParentNodeID = value;
            }
        }
        private Node m_parentNode;
        /// <summary>
        /// 父节点
        /// </summary>
        public Node ParentNode
        {
            get
            {
                return m_parentNode;
            }
            set
            {
                m_parentNode = value;
                //设置父节点是更新父节点ID
                this.m_ParentNodeID = value.NodeID;
            }
        }

        private String m_NodeCode = string.Empty;
        /// <summary>
        /// 节点编码
        /// </summary>
        public String NodeCode
        {
            get
            {
                return m_NodeCode;
            }
            set
            {
                m_NodeCode = value;
            }
        }

        /// <summary>
        /// 是否包含子节点
        /// </summary>
        public bool HasChildNode
        {
            get
            {
                return (this.ChildNodes.Count > 0);
            }
        }

        private IList<Node> m_childNodes = null;
        /// <summary>
        /// 子节点集
        /// </summary>
        public IList<Node> ChildNodes
        {
            get
            {
                if (m_childNodes == null)
                    m_childNodes = new List<Node>();
                return m_childNodes;
            }
            set
            {
                m_childNodes = value;
            }
        }

        public IList<Function> Functions
        {
            get;
            set;
        }

        ///// <summary>
        ///// 当前节点类型
        ///// </summary>
        //public NodeType CurrentNodeType
        //{
        //    get
        //    {
        //        if (this.ParentNodeID == 0)//根节点类型
        //        {
        //            return NodeType.Root;
        //        }
        //        if (!this.HasChildNode)//叶节点类型
        //        {
        //            return NodeType.Leaf;
        //        }
        //        else//枝节点类型
        //        {
        //            return NodeType.Branch;
        //        }
        //    }
        //}

        protected virtual string StartNodeCode
        {
            get
            {
                return "00";
            }

        }
        /// <summary>
        /// 获取下一个子节点的编码串
        /// </summary>
        public virtual String NextChildCode
        {
            get
            {
                string nextNodeCode = this.NodeCode + "1".PadLeft(this.StartNodeCode.Length, '0');
                if (this.ChildNodes.Count == 0)
                {
                    return nextNodeCode;
                }

                //必须按照NoeCode顺序排列
                Node[] sortNodes = new Node[this.ChildNodes.Count];
                this.ChildNodes.CopyTo(sortNodes, 0);
                Array.Sort<Node>(sortNodes, new Comparison<Node>(delegate(Node A, Node B)
                {
                    return A.NodeCode.CompareTo(B.NodeCode);
                }
                ));

                string lastNodeCode = sortNodes[sortNodes.Length - 1].NodeCode;
                nextNodeCode = lastNodeCode.Substring(0, lastNodeCode.Length - 2) + (Convert.ToInt32(lastNodeCode.Substring(lastNodeCode.Length - 2)) + 1).ToString().PadLeft(2, '0');

                #region 如果产生的编码不是3的整数倍数 就去掉前面的取最大整数倍数 如：0100 返回 100
                if (nextNodeCode.Length % 3 >= 1)
                {
                    int subLength = nextNodeCode.Length / 3 * 3;
                    nextNodeCode = nextNodeCode.Substring(nextNodeCode.Length - subLength, subLength);
                }
                #endregion
                return nextNodeCode;
            }
        }

        private String m_Description = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public String Description
        {
            get
            {
                if (m_Description == null)
                    m_Description = string.Empty;
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        private Int32 m_State = 0;
        /// <summary>
        /// 状态
        /// 状态说明：0：禁用，1：启用
        /// </summary>
        public Int32 State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }

        /// <summary>
        /// 功能组是否开启
        /// </summary>
        public bool IsStateOpen
        {
            get
            {
                return (this.State == 1);
            }
        }

        private Object m_Creator = "";
        /// <summary>
        /// 创建者ID
        /// </summary>
        public Object Creator
        {
            get
            {
                return m_Creator;
            }
            set
            {
                m_Creator = value;
            }
        }

        private DateTime m_CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return m_CreateTime;
            }
            set
            {
                m_CreateTime = value;
            }
        }

        private DateTime m_ModifyTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime ModifyTime
        {
            get
            {
                return m_ModifyTime;
            }
            set
            {
                m_ModifyTime = value;
            }
        }

        private Object m_Modifier = "";
        /// <summary>
        /// 修改者ID
        /// </summary>
        public Object Modifier
        {
            get
            {
                return m_Modifier;
            }
            set
            {
                m_Modifier = value;
            }
        }
        public override object KeyValue
        {
            get
            {
                return this.NodeID;
            }
            set
            {
                this.NodeID = (int)value;
            }
        }

        private Int32 m_OrderNo = 0;
        /// <summary>
        /// 顺序：默认值（自动取最大值）
        /// 说明：越大越靠后
        /// </summary>
        public Int32 OrderNo
        {
            get
            {
                return m_OrderNo;
            }
            set
            {
                m_OrderNo = value;
            }
        }

        /// <summary>
        /// 全路径描述
        /// </summary>
        public String DisplayPath { get; set; }
    }

}
