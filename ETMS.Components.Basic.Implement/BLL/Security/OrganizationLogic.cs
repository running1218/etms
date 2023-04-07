using System;
using System.Collections.Generic;

using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
using System.Data;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 组织机构管理
    /// </summary>
    public class OrganizationLogic : NodeLogic
    {
        private IDataAccess DAL = new OrganizationDataAccess();

        /// <summary>
        /// 切换机构状态
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <param name="modifier">修改人</param>
        public void SwitchOrganizationStatus(int organizationID, string modifier)
        {
            Node entity = GetNodeByID(organizationID);
            if (entity.IsStateOpen)//由启用-->停用
            {
                if (DAL.Query(string.Format(" AND OrganizationID!={0} AND Path like '{1}%' AND State=1", entity.NodeID, entity.NodeCode)).Length > 0)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Organization.DisableOrganizationFaild", new object[] { organizationID });
                }
            }
            entity.State = 1 - entity.State;
            entity.Modifier = modifier;
            entity.ModifyTime = DateTime.Now;
            Save(entity);
        }


        public void Save(Organization organization)
        {
            base.Save(organization);
        }

        public void Remove(Organization organization)
        {
            Node test = GetNodeTreeForManager(organization, false);
            if (test.HasChildNode)
            {
                throw new ETMS.AppContext.BusinessException("Security.Organization.ForbiddenDeleteOrganizationWhenHasSubOrganization", new object[] { organization.NodeID });
            }
            base.Remove(organization);
        }

        protected override void doSave(Node node)
        {
            try
            {
                Organization organization = (Organization)node;
                if (organization.OrganizationID == 0)
                {

                    DAL.Add(organization);

                    AccountStrategyLogic accountStrategyLogic = new AccountStrategyLogic();
                    AccountStrategy strategy = accountStrategyLogic.GetAccountStrategy(organization.OrganizationID);
                    //数据库提供默认密码
                    string password = strategy.Security_PassWord_Default;
                    //配置文件提供默认密码
                    //string password = ETMS.AppContext.ApplicationContext.Current.AppSettings["Security.DefaultPassword"] ?? "888888";
                    /*自动创建机构管理账户*/
                    User orgAdminUser = new User()
                    {
                        LoginName = string.Format("Org{0}", organization.OrganizationCode),//机构编码做为账户后缀
                        PassWord = password,//设置默认密码
                        IsSysAccount = true,
                        Status = 1,
                        RealName = organization.OrganizationName + "-机构管理员",//机构负责人
                        MobilePhone = organization.MobilePhone,//机构负责人电话
                        Email = organization.Email,  //机构负责人电话                  
                        OrganizationID = organization.OrganizationID,
                        CreateTime = DateTime.Now,
                        Creator = AppContext.UserContext.Current.RealName,
                        Modifier = AppContext.UserContext.Current.RealName,
                        ModifyTime = DateTime.Now,
                    };
                    UserLogic userLogic = new UserLogic();
                    //1、添加机构管理员-用户信息
                    userLogic.Save(orgAdminUser);
                    //2、添加机构管理员-角色
                    RoleLogic roleLogic = new RoleLogic();
                    //2.1提取机构管理员角色
                    Role role = (Role)roleLogic.GetNodeByNodeCode(RoleLogic.OrganizationAdminRoleCode);
                    UserRoleRelationLogic userRoleLogic = new UserRoleRelationLogic();
                    //2.2用户角色建立关系
                    userRoleLogic.Save(orgAdminUser.UserID, role.NodeID.ToString(), AppContext.UserContext.Current.RealName);
                    //3、邮件短信提醒
                    //ETMS.Utility.NotifyUtility.Notify("", null, null);
                }
                else
                {
                    string oldParentDisplayPath = organization.DisplayPath;
                    //1、更新当前节点DisplayPath
                    organization.DisplayPath = organization.DisplayPath.Substring(0, organization.DisplayPath.LastIndexOf('/')) + "/" + organization.OrganizationName;
                    DAL.Update(organization);
                    //2、更新下级节点DisplayPath
                    Node[] nodes = (Node[])DAL.Query(string.Format(" AND Path like '{0}%' and Path!='{0}'", organization.NodeCode));
                    foreach (Node item in nodes)
                    {
                        if (item.DisplayPath.StartsWith(oldParentDisplayPath))
                        {
                            item.DisplayPath = organization.DisplayPath + item.DisplayPath.Substring(oldParentDisplayPath.Length);
                        }
                        Save(item);
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("IX_U_Site_Organization_Name", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Organization.NameExists");
                }
                if (ex.Message.IndexOf("IX_U_Site_Organization_Code", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Organization.CodeExists");
                }
                if (ex.Message.IndexOf("IX_U_Site_Organization_Path", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Organization.PathExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        protected override void doRemove(Node node)
        {
            DAL.Delete((Organization)node);

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
                return new Organization() { State = 1 };
            Node findRole = (Node)DAL.Query(nodeID);
            if (findRole == null)
            {
                throw new ETMS.AppContext.BusinessException("Security.Organization.NotFoundOrganizationByID", new object[] { nodeID });
            }
            return findRole;
        }

        public Organization GetNodeByDomain(string domain)
        {
            var organization = new OrganizationDataAccess().QueryByDomain(domain);
            return organization;
        }

        public override Node GetNodeByNodeCode(string nodeCode)
        {
            Node[] findRoles = (Node[])DAL.Query(string.Format(" AND Path='{0}'", nodeCode));
            if (findRoles.Length == 0)
            {
                throw new ETMS.AppContext.BusinessException("Security.Organization.NotFoundOrganizationByPath", new object[] { nodeCode });
            }
            return findRoles[0];
        }

        /// <summary>
        /// 获取本组织及其下属机构
        /// </summary>
        /// <param name="selfOrganizationPath"></param>
        /// <returns></returns>
        public List<Organization> GetSelfAndSubOrganization(string selfOrganizationPath)
        {
            Node[] orgs = (Node[])DAL.Query(string.Format(" And Path like '{0}%' ", selfOrganizationPath));

            List<Organization> entities = new List<Organization>();

            foreach (Node node in orgs)
            {
                entities.Add(new Organization() { OrganizationID = node.NodeID });
            }

            return entities;
        }

        /// <summary>
        /// 获取组织机构及下属的【启用状态】机构列表
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <returns>根部门列表</returns>
        public System.Data.DataTable GetAllEnableOrganizationByID(int organizationID)
        {
            Node node = this.GetNodeByID(organizationID);
            string filter = string.Format(" AND Path like '{0}%' AND [State]=1 ", node.NodeCode);
            return (DAL as OrganizationDataAccess).QueryDataList(filter);
        }

        /// <summary>
        /// 获取组织机构及上级机构的【启用状态】机构列表
        /// </summary>
        /// <param name="organizationID">机构ID</param>
        /// <returns>根部门列表</returns>
        public System.Data.DataTable GetAllEnableOrganizationByID()
        {
            string filter = " AND [State]=1 ";
            return (DAL as OrganizationDataAccess).QueryDataList(filter);
        }

        public override void doReplaceNodePath(string oldNodePath, string newNodePath, string oldNodeDispalyPath, string newNodeDispalyPath, int parentID)
        {
            Organization[] nodes = (Organization[])DAL.Query(string.Format(" AND [Path] like '{0}%'", oldNodePath));
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


        //根据父id查询组织机构
        public DataTable GetPageListByParentID(int ParentID)
        {
            return new OrganizationDataAccess().GetPageListByParentID(ParentID);
        }

        /// <summary>
        /// 验证机构下学员是不是在机构限制的范围内
        /// </summary>
        /// <param name="organizationID"></param>
        public void OrganizationCheckStudentNum(int organizationID)
        {
            try
            {
                new OrganizationDataAccess().OrganizationCheckStudentNum(organizationID);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
              throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }

        public void Setting(int orgID, string title, string limit, string footer)
        {
            new OrganizationDataAccess().Setting(orgID, title, limit, footer);
        }

        public Organization QueryByID(int orgID)
        {
            DataTable result = new OrganizationDataAccess().QueryByID(orgID);
            if (null == result)
                return null;
            else
            {
                DataRow row = result.Rows[0];
                return new Organization() {
                    OrganizationID = orgID,
                    Title = row["Title"].ToString(),
                    MenuLimit = row["MenuLimit"].ToString(),
                    FooterInfo = row["FooterInfo"].ToString()
                };
            }
        }
    }
}
