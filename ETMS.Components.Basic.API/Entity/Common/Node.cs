using System;
using System.Collections.Generic;
using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.API.Entity.Common
{
    /// <summary>
    /// �ڵ�����
    /// �ڵ����;�����ǰ�ڵ��ܷ����ӽڵ�
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// ���ڵ�
        /// </summary>
        Root = 0,
        /// <summary>
        /// ֦�ڵ�
        /// </summary>
        Branch = 1,
        /// <summary>
        /// Ҷ�ڵ�
        /// </summary>
        Leaf = 2
    }

    /// <summary>
    /// �ڵ㶨��
    /// </summary>
    /// <typeparam name="Node">���ͽڵ�����</typeparam>
    [Serializable]
    public abstract class Node : ETMS.AppContext.AbstractObject
    {
        private Int32 m_NodeID = 0;
        /// <summary>
        /// �ڵ�ID
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
        /// �ڵ�����
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
        /// ���ڵ�ID
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
        /// ���ڵ�
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
                //���ø��ڵ��Ǹ��¸��ڵ�ID
                this.m_ParentNodeID = value.NodeID;
            }
        }

        private String m_NodeCode = string.Empty;
        /// <summary>
        /// �ڵ����
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
        /// �Ƿ�����ӽڵ�
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
        /// �ӽڵ㼯
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
        ///// ��ǰ�ڵ�����
        ///// </summary>
        //public NodeType CurrentNodeType
        //{
        //    get
        //    {
        //        if (this.ParentNodeID == 0)//���ڵ�����
        //        {
        //            return NodeType.Root;
        //        }
        //        if (!this.HasChildNode)//Ҷ�ڵ�����
        //        {
        //            return NodeType.Leaf;
        //        }
        //        else//֦�ڵ�����
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
        /// ��ȡ��һ���ӽڵ�ı��봮
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

                //���밴��NoeCode˳������
                Node[] sortNodes = new Node[this.ChildNodes.Count];
                this.ChildNodes.CopyTo(sortNodes, 0);
                Array.Sort<Node>(sortNodes, new Comparison<Node>(delegate(Node A, Node B)
                {
                    return A.NodeCode.CompareTo(B.NodeCode);
                }
                ));

                string lastNodeCode = sortNodes[sortNodes.Length - 1].NodeCode;
                nextNodeCode = lastNodeCode.Substring(0, lastNodeCode.Length - 2) + (Convert.ToInt32(lastNodeCode.Substring(lastNodeCode.Length - 2)) + 1).ToString().PadLeft(2, '0');

                #region ��������ı��벻��3���������� ��ȥ��ǰ���ȡ����������� �磺0100 ���� 100
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
        /// ����
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
        /// ״̬
        /// ״̬˵����0�����ã�1������
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
        /// �������Ƿ���
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
        /// ������ID
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
        /// ����ʱ��
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
        /// ����ʱ��
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
        /// �޸���ID
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
        /// ˳��Ĭ��ֵ���Զ�ȡ���ֵ��
        /// ˵����Խ��Խ����
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
        /// ȫ·������
        /// </summary>
        public String DisplayPath { get; set; }
    }

}
