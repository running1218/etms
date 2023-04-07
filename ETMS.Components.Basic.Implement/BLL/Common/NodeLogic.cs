using System;
using ETMS.Components.Basic.API.Entity.Common;

namespace ETMS.Components.Basic.Implement.BLL.Common
{
    /// <summary>
    /// 节点管理基础类
    /// </summary>
    public abstract class NodeLogic
    {
        protected static Int32[] DefaultStateFilter = { 1 };

        /// <summary>
        /// 保存节点
        /// 说明：三种类型的节点均支持
        /// </summary>
        /// <param name="node">节点</param>
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
        /// 保存节点
        /// 子类需根据情况重写此方法
        /// </summary>
        /// <param name="node">节点</param>
        protected abstract void doSave(Node node);

        //2、删除树叶型节点
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
        /// 删除节点
        /// 子类需根据情况重写此方法
        /// </summary>
        /// <param name="node">节点</param>
        protected abstract void doRemove(Node node);


        /// <summary>
        /// 生成节点树
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="stateFilter">状态过滤条件</param>
        /// <param name="degreeSearch">是否深度搜索</param>
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
        /// 查询节点树列表，用于管理（仅返回状态启用）
        /// 说明：即返回节点及其子节点的树型列表
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="degreeSearch">是否深度搜索</param>
        /// <returns>得到父节点下所有子节点的信息</returns>
        public Node GetNodeTreeForManager(Node parent, bool degreeSearch)
        {
            return this.GetNodeTree(parent, new int[] { 1, 0 }, degreeSearch);
        }

        /// <summary>
        /// 查询节点树列表，用于业务（仅返回状态启用）
        /// 说明：即返回节点及其子节点的树型列表
        /// </summary>
        /// <param name="parent">父节点</param> 
        /// <param name="degreeSearch">是否深度搜索</param>
        /// <returns>得到父节点下所有子节点的信息</returns>
        public Node GetNodeTree(Node parent, bool degreeSearch)
        {
            return this.GetNodeTree(parent, new int[] { 1 }, degreeSearch);
        }
        /// <summary>
        /// 获取子节点集合
        /// </summary>
        /// <param name="parentID">父节点ID</param>
        /// <param name="stateFilter">状态过滤条件</param>
        /// <returns>父节点下包含的子节点集合</returns>
        protected abstract Node[] doGetSubNodeIDList(Int32 parentID, Int32[] stateFilter);

        /// <summary>
        /// 提取单个节点信息
        /// 注意：仅仅提取节点的基本信息，不包含子节点信息。
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <returns></returns>
        public abstract Node GetNodeByID(Int32 nodeID);

        /// <summary>
        /// 根据节点编码来查找节点信息
        /// </summary>
        /// <param name="nodeCode">节点编码</param>
        /// <returns>节点信息</returns>
        public abstract Node GetNodeByNodeCode(string nodeCode);

        /// <summary>
        /// 根据节点编码获取节点树
        /// </summary>
        /// <param name="nodeCode">节点编码</param>
        /// <param name="degreeSearch">是否深度搜索</param>
        /// <returns>节点编码下所有子节点的信息</returns>
        public Node GetNodeTree(string nodeCode, bool degreeSearch)
        {
            Node parentNode = GetNodeByNodeCode(nodeCode);
            return GetNodeTree(parentNode, degreeSearch);
        }

        /// <summary>
        /// 移动节点
        /// </summary>
        /// <param name="parentNodeID">父节点ID</param>
        /// <param name="nodeID">节点</param>
        public virtual void MoveNode(int parentNodeID, int nodeID)
        {
            Node parentNode = GetNodeByID(parentNodeID);
            Node node = GetNodeByID(nodeID);
            //生成新子节点编码
            string newNodeCode = GetNodeTreeForManager(parentNode, false).NextChildCode;
            string oldNodeCode = node.NodeCode;
            string newNodeDisplayPath = parentNode.DisplayPath;
            string oldNodeDisplayPath = node.DisplayPath;
            doReplaceNodePath(oldNodeCode, newNodeCode, oldNodeDisplayPath, newNodeDisplayPath, parentNode.NodeID);         

        }
        /// <summary>
        /// 完成节点Path替换
        /// </summary>
        /// <param name="oldNodePath"></param>
        /// <param name="newNodePath"></param>
        /// <param name="oldNodeDispalyPath"></param>
        /// <param name="newNodeDispalyPath"></param>
        public virtual void doReplaceNodePath(string oldNodePath, string newNodePath, string oldNodeDispalyPath, string newNodeDispalyPath, int parentId) { }
    }
}
