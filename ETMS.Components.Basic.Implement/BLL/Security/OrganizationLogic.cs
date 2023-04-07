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
    /// ��֯��������
    /// </summary>
    public class OrganizationLogic : NodeLogic
    {
        private IDataAccess DAL = new OrganizationDataAccess();

        /// <summary>
        /// �л�����״̬
        /// </summary>
        /// <param name="organizationID">����ID</param>
        /// <param name="modifier">�޸���</param>
        public void SwitchOrganizationStatus(int organizationID, string modifier)
        {
            Node entity = GetNodeByID(organizationID);
            if (entity.IsStateOpen)//������-->ͣ��
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
                    //���ݿ��ṩĬ������
                    string password = strategy.Security_PassWord_Default;
                    //�����ļ��ṩĬ������
                    //string password = ETMS.AppContext.ApplicationContext.Current.AppSettings["Security.DefaultPassword"] ?? "888888";
                    /*�Զ��������������˻�*/
                    User orgAdminUser = new User()
                    {
                        LoginName = string.Format("Org{0}", organization.OrganizationCode),//����������Ϊ�˻���׺
                        PassWord = password,//����Ĭ������
                        IsSysAccount = true,
                        Status = 1,
                        RealName = organization.OrganizationName + "-��������Ա",//����������
                        MobilePhone = organization.MobilePhone,//���������˵绰
                        Email = organization.Email,  //���������˵绰                  
                        OrganizationID = organization.OrganizationID,
                        CreateTime = DateTime.Now,
                        Creator = AppContext.UserContext.Current.RealName,
                        Modifier = AppContext.UserContext.Current.RealName,
                        ModifyTime = DateTime.Now,
                    };
                    UserLogic userLogic = new UserLogic();
                    //1����ӻ�������Ա-�û���Ϣ
                    userLogic.Save(orgAdminUser);
                    //2����ӻ�������Ա-��ɫ
                    RoleLogic roleLogic = new RoleLogic();
                    //2.1��ȡ��������Ա��ɫ
                    Role role = (Role)roleLogic.GetNodeByNodeCode(RoleLogic.OrganizationAdminRoleCode);
                    UserRoleRelationLogic userRoleLogic = new UserRoleRelationLogic();
                    //2.2�û���ɫ������ϵ
                    userRoleLogic.Save(orgAdminUser.UserID, role.NodeID.ToString(), AppContext.UserContext.Current.RealName);
                    //3���ʼ���������
                    //ETMS.Utility.NotifyUtility.Notify("", null, null);
                }
                else
                {
                    string oldParentDisplayPath = organization.DisplayPath;
                    //1�����µ�ǰ�ڵ�DisplayPath
                    organization.DisplayPath = organization.DisplayPath.Substring(0, organization.DisplayPath.LastIndexOf('/')) + "/" + organization.OrganizationName;
                    DAL.Update(organization);
                    //2�������¼��ڵ�DisplayPath
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
                //ö������Լ���쳣��ת��Ϊҵ���쳣
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
                //�����δ�������׳�
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
        /// ��ȡ����֯������������
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
        /// ��ȡ��֯�����������ġ�����״̬�������б�
        /// </summary>
        /// <param name="organizationID">����ID</param>
        /// <returns>�������б�</returns>
        public System.Data.DataTable GetAllEnableOrganizationByID(int organizationID)
        {
            Node node = this.GetNodeByID(organizationID);
            string filter = string.Format(" AND Path like '{0}%' AND [State]=1 ", node.NodeCode);
            return (DAL as OrganizationDataAccess).QueryDataList(filter);
        }

        /// <summary>
        /// ��ȡ��֯�������ϼ������ġ�����״̬�������б�
        /// </summary>
        /// <param name="organizationID">����ID</param>
        /// <returns>�������б�</returns>
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
                DAL.Update(nodes[i]);//����
            }
        }


        //���ݸ�id��ѯ��֯����
        public DataTable GetPageListByParentID(int ParentID)
        {
            return new OrganizationDataAccess().GetPageListByParentID(ParentID);
        }

        /// <summary>
        /// ��֤������ѧԱ�ǲ����ڻ������Ƶķ�Χ��
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
