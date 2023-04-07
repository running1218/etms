using System;
using ETMS.Components.Basic.API.Entity.Common;

namespace ETMS.Components.Basic.Implement.BLL.Common
{
    /// <summary>
    /// �ڵ���������
    /// </summary>
    public abstract class NodeLogic
    {
        protected static Int32[] DefaultStateFilter = { 1 };

        /// <summary>
        /// ����ڵ�
        /// ˵�����������͵Ľڵ��֧��
        /// </summary>
        /// <param name="node">�ڵ�</param>
        public void Save(Node node)
        {
#if EnableDTC
            using (TransactionScope ts = new TransactionScope())
            {
#endif
            this.SaveImpl(node);
#if EnableDTC
            ts.Complete();
            }
#endif
        }
        private void SaveImpl(Node node)
        {
            foreach (Node item in node.ChildNodes)
            {
                SaveImpl(item);
            }
            doSave(node);
        }
        /// <summary>
        /// ����ڵ�
        /// ��������������д�˷���
        /// </summary>
        /// <param name="node">�ڵ�</param>
        protected abstract void doSave(Node node);

        //2��ɾ����Ҷ�ͽڵ�
        public void Remove(Node node)
        {
#if EnableDTC
            using (TransactionScope ts = new TransactionScope())
            {
#endif
            this.RemoveImpl(node);
#if EnableDTC
            ts.Complete();
            }
#endif
        }
        private void RemoveImpl(Node node)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (Node item in node.ChildNodes)
                {
                    RemoveImpl(item);
                }
            }
            else
            {
                Node[] nodes = this.doGetSubNodeIDList(node.NodeID, new int[] { 0, 1 });
                foreach (Node item in nodes)
                {
                    this.RemoveImpl(item);
                }
                doRemove(node);
            }
        }
        /// <summary>
        /// ɾ���ڵ�
        /// ��������������д�˷���
        /// </summary>
        /// <param name="node">�ڵ�</param>
        protected abstract void doRemove(Node node);


        /// <summary>
        /// ���ɽڵ���
        /// </summary>
        /// <param name="parent">���ڵ�</param>
        /// <param name="stateFilter">״̬��������</param>
        /// <param name="degreeSearch">�Ƿ��������</param>
        /// <returns></returns>
        public Node GetNodeTree(Node parent, Int32[] stateFilter, bool degreeSearch)
        {
            Node[] nodes = doGetSubNodeIDList(parent.NodeID, stateFilter);
            for (int i = 0; i < nodes.Length; i++)
            {
                Node node = nodes[i];
                if (degreeSearch)
                {
                    node = GetNodeTree(node, stateFilter, degreeSearch);
                }
                parent.ChildNodes.Add(node);
            }
            return parent;
        }

        /// <summary>
        /// ��ѯ�ڵ����б����ڹ���������״̬���ã�
        /// ˵���������ؽڵ㼰���ӽڵ�������б�
        /// </summary>
        /// <param name="parent">���ڵ�</param>
        /// <param name="degreeSearch">�Ƿ��������</param>
        /// <returns>�õ����ڵ��������ӽڵ����Ϣ</returns>
        public Node GetNodeTreeForManager(Node parent, bool degreeSearch)
        {
            return this.GetNodeTree(parent, new int[] { 1, 0 }, degreeSearch);
        }

        /// <summary>
        /// ��ѯ�ڵ����б�����ҵ�񣨽�����״̬���ã�
        /// ˵���������ؽڵ㼰���ӽڵ�������б�
        /// </summary>
        /// <param name="parent">���ڵ�</param> 
        /// <param name="degreeSearch">�Ƿ��������</param>
        /// <returns>�õ����ڵ��������ӽڵ����Ϣ</returns>
        public Node GetNodeTree(Node parent, bool degreeSearch)
        {
            return this.GetNodeTree(parent, new int[] { 1 }, degreeSearch);
        }
        /// <summary>
        /// ��ȡ�ӽڵ㼯��
        /// </summary>
        /// <param name="parentID">���ڵ�ID</param>
        /// <param name="stateFilter">״̬��������</param>
        /// <returns>���ڵ��°������ӽڵ㼯��</returns>
        protected abstract Node[] doGetSubNodeIDList(Int32 parentID, Int32[] stateFilter);

        /// <summary>
        /// ��ȡ�����ڵ���Ϣ
        /// ע�⣺������ȡ�ڵ�Ļ�����Ϣ���������ӽڵ���Ϣ��
        /// </summary>
        /// <param name="nodeID">�ڵ�ID</param>
        /// <returns></returns>
        public abstract Node GetNodeByID(Int32 nodeID);

        /// <summary>
        /// ���ݽڵ���������ҽڵ���Ϣ
        /// </summary>
        /// <param name="nodeCode">�ڵ����</param>
        /// <returns>�ڵ���Ϣ</returns>
        public abstract Node GetNodeByNodeCode(string nodeCode);

        /// <summary>
        /// ���ݽڵ�����ȡ�ڵ���
        /// </summary>
        /// <param name="nodeCode">�ڵ����</param>
        /// <param name="degreeSearch">�Ƿ��������</param>
        /// <returns>�ڵ�����������ӽڵ����Ϣ</returns>
        public Node GetNodeTree(string nodeCode, bool degreeSearch)
        {
            Node parentNode = GetNodeByNodeCode(nodeCode);
            return GetNodeTree(parentNode, degreeSearch);
        }

        /// <summary>
        /// �ƶ��ڵ�
        /// </summary>
        /// <param name="parentNodeID">���ڵ�ID</param>
        /// <param name="nodeID">�ڵ�</param>
        public virtual void MoveNode(int parentNodeID, int nodeID)
        {
            Node parentNode = GetNodeByID(parentNodeID);
            Node node = GetNodeByID(nodeID);
            //�������ӽڵ����
            string newNodeCode = GetNodeTreeForManager(parentNode, false).NextChildCode;
            string oldNodeCode = node.NodeCode;
            string newNodeDisplayPath = parentNode.DisplayPath;
            string oldNodeDisplayPath = node.DisplayPath;
            doReplaceNodePath(oldNodeCode, newNodeCode, oldNodeDisplayPath, newNodeDisplayPath, parentNode.NodeID);         

        }
        /// <summary>
        /// ��ɽڵ�Path�滻
        /// </summary>
        /// <param name="oldNodePath"></param>
        /// <param name="newNodePath"></param>
        /// <param name="oldNodeDispalyPath"></param>
        /// <param name="newNodeDispalyPath"></param>
        public virtual void doReplaceNodePath(string oldNodePath, string newNodePath, string oldNodeDispalyPath, string newNodeDispalyPath, int parentId) { }
    }
}
