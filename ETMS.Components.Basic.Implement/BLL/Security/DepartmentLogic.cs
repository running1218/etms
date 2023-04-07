using System;


using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
using System.Data;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class DepartmentLogic : NodeLogic
    {
        private IDataAccess DAL = new DepartmentDataAccess();

        public void Save(Department department)
        {
            base.Save(department);
        }

        public void Remove(Department department)
        {
            Node test = GetNodeTreeForManager(department, false);
            if (test.HasChildNode)
            {
                throw new ETMS.AppContext.BusinessException("Security.Department.ForbiddenDeleteDepartmentWhenHasSubDepartment", new object[] { department.NodeID });
            }

            UserLogic userLogic = new UserLogic();
            int totalRecord = 0;
            userLogic.GetPagedList(0, 0, string.Format(" AND DepartmentID={0}", department.DepartmentID), "", out totalRecord);
            if (totalRecord > 0)
            {
                throw new ETMS.AppContext.BusinessException("Security.Department.ForbiddenDeleteDepartmentWhenHasUser", new object[] { department.NodeID });
            }
            base.Remove(department);
        }

        protected override void doSave(Node node)
        {
            try
            {
                Department department = (Department)node;
                if (department.DepartmentID == 0)
                {
                    DAL.Add(department);
                }
                else
                {
                    string oldParentDisplayPath = department.DisplayPath;
                    //1、更新当前节点DisplayPath
                    department.DisplayPath = department.DisplayPath.Substring(0, department.DisplayPath.LastIndexOf('/')) + "/" + department.NodeName;
                    DAL.Update(department);
                    //2、更新下级节点DisplayPath
                    Node[] nodes = (Node[])DAL.Query(string.Format(" AND Path like '{0}%' and Path!='{0}'", department.NodeCode));
                    foreach (Node item in nodes)
                    {
                        if (item.DisplayPath.StartsWith(oldParentDisplayPath))
                        {
                            item.DisplayPath = department.DisplayPath + item.DisplayPath.Substring(oldParentDisplayPath.Length);
                        }
                        Save(item);
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("IX_U_Site_Department_Name", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Department.NameExists");
                }
                if (ex.Message.IndexOf("IX_U_Site_Department_Code", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Department.CodeExists");
                }
                if (ex.Message.IndexOf("IX_U_Site_Department_Path", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Department.PathExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        protected override void doRemove(Node node)
        {
            DAL.Delete((Department)node);
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
                return new Department();
            Node findRole = (Node)DAL.Query(nodeID);
            if (findRole == null)
            {
                throw new ETMS.AppContext.BusinessException("Security.Department.NotFoundDepartmentByID", new object[] { nodeID });
            }
            return findRole;
        }

        public override Node GetNodeByNodeCode(string nodeCode)
        {
            Node[] findRoles = (Node[])DAL.Query(string.Format(" AND Path='{0}'", nodeCode));
            if (findRoles.Length == 0)
            {
                throw new ETMS.AppContext.BusinessException("Security.Department.NotFoundDepartmentByPath", new object[] { nodeCode });
            }
            return findRoles[0];
        }

        /// <summary>
        /// 获取组织机构下根部门列表
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <returns>根部门列表</returns>
        public Node[] GetRootDepartmentsByOrganizationID(int organizationID)
        {
            string filter = string.Format(" AND OrganizationID={0} AND ParentID=0 ", organizationID);
            return (Node[])DAL.Query(filter);
        }

        /// <summary>
        /// 获取组织机构下所有【启用状态】部门列表
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <returns>根部门列表</returns>
        public System.Data.DataTable GetAllEnableDepartmentsByOrganizationID(int organizationID)
        {
            string filter = string.Format(" AND OrganizationID={0} AND [State]=1 ", organizationID);
            return (DAL as DepartmentDataAccess).QueryDataList(filter);
        }

        /// <summary>
        /// 获取组织机构下所有【启用状态】部门列表
        /// </summary>
        /// <returns>根部门列表</returns>
        public System.Data.DataTable GetAllEnableDepartments()
        {
            string filter = " AND [State]=1 ";
            return (DAL as DepartmentDataAccess).QueryDataList(filter);
        }

        /// <summary>
        /// 切换部门状态
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <param name="modifier">修改人</param>
        public void SwitchDepartmentStatus(int departmentID, string modifier)
        {
            Node entity = GetNodeByID(departmentID);
            if (entity.IsStateOpen)//由启用-->停用
            {
                //下级部门有启用状态时，不允许停用当前部门
                if (DAL.Query(string.Format(" AND DepartmentID!={0} AND Path like '{1}%' AND State=1", entity.NodeID, entity.NodeCode)).Length > 0)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Deparetment.DisableDepartmentFaild", new object[] { departmentID });
                }
                //当前机构下有学员时，不允许停用当前部门
                Site_StudentLogic studentLogic = new Site_StudentLogic();
                int totalRecords = 0;
                studentLogic.GetCurrentOrgManagePagedList(null, null, null, departmentID, -1, -1, 1, 0, -1, out totalRecords);
                if (totalRecords > 0)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Deparetment.DisableDepartmentFaildHasStudent", new object[] { departmentID });
                }
            }
            entity.State = 1 - entity.State;
            entity.Modifier = modifier;
            entity.ModifyTime = DateTime.Now;
            Save(entity);
        }

        public override void doReplaceNodePath(string oldNodePath, string newNodePath, string oldNodeDispalyPath, string newNodeDispalyPath, int parentID)
        {
            Department[] nodes = (Department[])new DepartmentDataAccess().QueryMove(string.Format(" AND [Path] like '{0}%'", oldNodePath));

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

                    nodes[i].DepartmentCode = GetNewCodeMove(nodes[i].OrganizationID, nodes[i].ParentNodeID, oldNodePath);
                    nodes[i].Path = GetNewPathMove(nodes[i].OrganizationID, nodes[i].ParentNodeID, oldNodePath);
                }
                else
                {
                    nodes[i].NodeCode = rootNodeCode + nodes[i].NodeCode.Substring(oldNodePath.Length);
                    nodes[i].DisplayPath = rootDisplayPath + nodes[i].DisplayPath.Substring(oldNodeDispalyPath.Length);

                    nodes[i].DepartmentCode = GetNewCodeMove(nodes[i].OrganizationID, nodes[i].ParentNodeID, oldNodePath);
                    nodes[i].Path = nodes[i].NodeCode;// GetNewPathMove(nodes[i].OrganizationID, nodes[i].ParentNodeID, oldNodePath);
                }

                DAL.Update(nodes[i]);//保存
            }
        }

        public string GetDeptNameByID(object DeptID)
        {
            Department department = (Department)DAL.Query(DeptID);
            if (department == null)
                return "";
            return department.DepartmentName;
        }

        /// <summary>
        /// 生成新的部门编码
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        public string GetNewCode(int organizationID, int parentID)
        {
            string departmentCode = "";
            DepartmentDataAccess dal = new DepartmentDataAccess();
            DataTable tab = dal.GetNewCodePath(organizationID, parentID);
            if (tab.Rows.Count > 0 && !Convert.IsDBNull(tab.Rows[0]["DepartmentCode"]))
            {
                departmentCode = tab.Rows[0]["DepartmentCode"].ToString();
            }
            return departmentCode;
        }

        /// <summary>
        /// 生成新的Path编码
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        public string GetNewPath(int organizationID, int parentID)
        {
            string path = "";
            DepartmentDataAccess dal = new DepartmentDataAccess();
            DataTable tab = dal.GetNewCodePath(organizationID, parentID);
            if (tab.Rows.Count > 0 && !Convert.IsDBNull(tab.Rows[0]["Path"]))
            {
                path = tab.Rows[0]["Path"].ToString();
            }
            return path;
        }

        /// <summary>
        /// 移动部门时生成新的部门编码
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        public string GetNewCodeMove(int organizationID, int parentID, string oldNodePath)
        {
            string departmentCode = "";
            DepartmentDataAccess dal = new DepartmentDataAccess();
            DataTable tab = dal.GetNewCodePathMove(organizationID, parentID, oldNodePath);
            if (tab.Rows.Count > 0 && !Convert.IsDBNull(tab.Rows[0]["DepartmentCode"]))
            {
                departmentCode = tab.Rows[0]["DepartmentCode"].ToString();
            }
            return departmentCode;
        }

        /// <summary>
        /// 移动部门时生成新的Path编码
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <param name="parentID">上级ID</param>
        /// <returns></returns>
        public string GetNewPathMove(int organizationID, int parentID, string oldNodePath)
        {
            string path = "";
            DepartmentDataAccess dal = new DepartmentDataAccess();
            DataTable tab = dal.GetNewCodePathMove(organizationID, parentID, oldNodePath);
            if (tab.Rows.Count > 0 && !Convert.IsDBNull(tab.Rows[0]["Path"]))
            {
                path = tab.Rows[0]["Path"].ToString();
            }
            return path;
        }

    }
}
