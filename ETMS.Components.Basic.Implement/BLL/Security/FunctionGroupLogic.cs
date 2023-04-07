using System;

using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 功能组管理
    /// </summary>
    public class FunctionGroupLogic : NodeLogic
    {
        private IDataAccess DAL = new FunctionGroupDataAccess();


        /// <summary>
        /// 切换功能组状态
        /// </summary>
        /// <param name="functionGroupID">功能组ID</param>
        /// <param name="modifier">修改人</param>
        public void SwitchFunctionGroupStatus(int functionGroupID, string modifier)
        {
            Node entity = GetNodeByID(functionGroupID);
            entity.State = 1 - entity.State;
            entity.Modifier = modifier;
            entity.ModifyTime = DateTime.Now;
            Save(entity);
        }


        public void Save(FunctionGroup functionGroup)
        {
            base.Save(functionGroup);
        }

        public void Remove(FunctionGroup functionGroup)
        {
            base.Remove(functionGroup);
        }

        protected override void doSave(Node node)
        {
            FunctionGroup functionGroup = (FunctionGroup)node;
            if (functionGroup.GroupID == 0)
            {

                DAL.Add(functionGroup);


            }
            else
            {

                DAL.Update(functionGroup);


            }
        }

        protected override void doRemove(Node node)
        {

            DAL.Delete((FunctionGroup)node);

        }

        protected override Node[] doGetSubNodeIDList(int parentID, Int32[] stateFilter)
        {
            if (stateFilter == null)
                stateFilter = NodeLogic.DefaultStateFilter;

            string filterScriptFormat = " AND parentID={0} AND state in ({1}) ";
            string filter = "";
            if (stateFilter.Length > 0)
            {
                foreach (Int32 state in stateFilter)
                {
                    filter += string.Format("{0},", state);
                }
                filter = filter.Substring(0, filter.Length - 1);
            }
            filter = string.Format(filterScriptFormat, parentID, filter);
            return (Node[])DAL.Query(filter);
        }

        public override Node GetNodeByID(Int32 nodeID)
        {
            if (nodeID == 0)
                return new FunctionGroup();
            Node findRole = (Node)DAL.Query(nodeID);
            if (findRole == null)
            {
                throw new ETMS.AppContext.BusinessException("Security.FunctionGroup.NotFoundFunctionGroupByID", new object[] { nodeID });
            }
            return findRole;
        }

        public override Node GetNodeByNodeCode(string nodeCode)
        {
            Node[] findRoles = (Node[])DAL.Query(string.Format(" AND GroupCode='{0}'", nodeCode));
            if (findRoles.Length == 0)
            {
                throw new ETMS.AppContext.BusinessException("Security.FunctionGroup.NotFoundFunctionGroupByGroupCode", new object[] { nodeCode }); 
            }
            return findRoles[0];
        }

        public override void doReplaceNodePath(string oldNodePath, string newNodePath, string oldNodeDispalyPath, string newNodeDispalyPath, int parentID)
        {
            FunctionGroup[] nodes = (FunctionGroup[])DAL.Query(string.Format(" AND [GroupCode] like '{0}%'", oldNodePath));

            string rootNodeCode = string.Empty;
            string rootDisplayPath = string.Empty;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].NodeCode.Equals(oldNodePath))
                {
                    nodes[i].ParentNodeID = parentID;
                    rootNodeCode = newNodePath;
                    rootDisplayPath = newNodeDispalyPath + "/" + nodes[i].NodeName;
                    nodes[i].NodeCode = rootNodeCode;
                    nodes[i].DisplayPath = rootDisplayPath;
                }
                else
                {
                    nodes[i].NodeCode = rootNodeCode + nodes[i].NodeCode.Substring(oldNodePath.Length);
                    nodes[i].DisplayPath = rootDisplayPath + nodes[i].DisplayPath.Substring(oldNodeDispalyPath.Length);
                }
                DAL.Update(nodes[i]);//保存
            } 
        }
    }
}
