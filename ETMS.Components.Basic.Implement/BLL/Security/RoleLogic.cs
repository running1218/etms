using System;
using System.Collections.Generic;

using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.Common;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public class RoleLogic : NodeLogic
    {

        private PageUrlLogic PageUrlService = new PageUrlLogic();

        #region ���ý�ɫ���壨��Ϊ���ý�ɫ��ϵͳ��ʼ��ʱ�����ɫ�ģ���organizationID=0
        /// <summary>
        /// ϵͳ����Ա��ɫ
        /// </summary>
        public static string SystemAdminRoleCode = "0001";

        /// <summary>
        /// ��������Ա��ɫ
        /// </summary>
        public static string OrganizationAdminRoleCode = "0002";

        /// <summary>
        /// ��ʦ��ɫ
        /// </summary>
        public static string LecturerdAminRoleCode = "0003";

        #endregion


        private IDataAccess DAL = new RoleDataAccess();

        public void Save(Role role)
        {
            base.Save(role);
        }

        public void Remove(Role role)
        {
            base.Remove(role);
        }

        protected override void doSave(Node node)
        {
            try
            {
                Role role = (Role)node;
                if (role.RoleID == 0)
                {
                    DAL.Add(role);
                }
                else
                {
                    DAL.Update(role);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Message.IndexOf("IX_U_Site_Role_Org_Name", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    //�Ѵ��ڽ�ɫ����
                    throw new ETMS.AppContext.BusinessException("Security.Role.ExistsRoleName");
                }
            }
        }

        protected override void doRemove(Node node)
        {
            DAL.Delete((Role)node);
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
            Node findRole = (Node)DAL.Query(nodeID);
            if (findRole == null)
            {
                throw new ETMS.AppContext.BusinessException("Security.Role.NotFoundRoleByRoleID", new object[] { nodeID });
            }
            return findRole;
        }

        public override Node GetNodeByNodeCode(string nodeCode)
        {
            Node[] findRoles = (Node[])DAL.Query(string.Format(" AND RoleCode='{0}'", nodeCode));
            if (findRoles.Length == 0)
            {
                throw new ETMS.AppContext.BusinessException("Security.Role.NotFoundRoleByRoleCode", new object[] { nodeCode });
            }
            return findRoles[0];
        }

        /// <summary>
        /// ��ȡ�������Խ���ɫ
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public Role[] GetOrganizationRoles(Int32 organizationID)
        {
            return (Role[])new RoleDataAccess().Query(" AND organizationID=" + organizationID.ToString());
        }
        /// <summary>
        /// ���ݽ�ʦ��ɫ��ѯ��ʦ�����б������ʦ��
        /// </summary>
        /// <param name="roleCode">��ʦ��ɫ����</param>
        /// <param name="functionType"> 1:ҵ������;2:�γ������ѯ;3:��ҵ�����ѯ;4:������Ϊ��ѯ</param>
        /// <returns>�����б�</returns>
        public System.Data.DataTable GetFunctionListByRoleCode(string roleCode, int functionType)
        {
            return ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<System.Data.DataTable>(roleCode, functionType.ToString(), () =>
            {
                return ((RoleDataAccess)DAL).GetFunctionListByRoleCode(roleCode, functionType);
            });
        }

        /// <summary>
        /// ��ȡ��ɫ����Ĺ���URL����
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public String[] GetPageUrlByRoleCode(string roleCode)
        {
            //config/BizCache.config�ж��建����ڲ���
            string key = "RolePageUrls";
            return ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<String[]>(key, roleCode, () =>
                       {
                           List<string> urls = new List<string>();
                           foreach (System.Data.DataRow row in ((RoleDataAccess)DAL).GetPageUrlByRoleCode(roleCode).Rows)
                           {
                               urls.Add(row["PageURL"].ToString());
                           }
                           return urls.ToArray();
                       });
        }


        /// <summary>
        /// ��ȡ�û�����Ĺ���URL����
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public String[] GetPageUrlByUserID(int userID)
        {
            //config/BizCache.config�ж��建����ڲ���
            string key = "UserPageUrls";
            return ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<String[]>(key, userID.ToString(), () =>
            {
                List<string> urls = new List<string>();
                foreach (System.Data.DataRow row in ((RoleDataAccess)DAL).GetPageUrlByUserID(userID).Rows)
                {
                    urls.Add(row["PageURL"].ToString());
                }
                return urls.ToArray();
            });
        }


        /// <summary>
        /// �жϵ�ǰ�����û��Ƿ��������URL���û��������û�������ɫ��Ȩ���û�������Ȩ��
        /// </summary>
        /// <param name="roleCodes">�����û���ɫ���봮�����֮��ͨ���������ָ�</param>
        /// <param name="userID">�û�ID</param>
        /// <param name="requestUrl">������ʵ�URL</param>
        /// <returns>true false</returns>
        public bool IsAllowManageUserAccessURL(string roleCodes, int userID, string requestUrl)
        {
            bool isAllow = false;
            string[] allowUrls = (userID == 0 ? new string[0] : this.GetPageUrlByUserID(userID));
            isAllow = Array.Exists<string>(allowUrls, new Predicate<string>(delegate(string item) { return UrlMatch(item, requestUrl); }));
            if (!isAllow)//���δ�����û����ܷ��䣬������û���ɫ��֤��
            {
                foreach (string roleCode in roleCodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (IsAllowAccessURL(roleCode, requestUrl))
                    {
                        return true;
                    }
                }
                isAllow = false;
            } 
            if (!isAllow)//�����������urlδע�ᵽϵͳ�У���Ĭ������
            {
                string[] allUrls = PageUrlService.GetAllRegisterUrls();
                isAllow = !Array.Exists<string>(allUrls, new Predicate<string>(delegate(string item) { return UrlMatch(item, requestUrl, false); }));
            }
            return isAllow;
        }
        #region helper

        /// <summary>
        /// �жϵ�ǰ��ɫ�Ƿ��������URL
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="requestUrl">�����Root·��</param>
        /// <returns></returns>
        private bool IsAllowAccessURL(string roleCode, string requestUrl)
        {
            string[] allowUrls = GetPageUrlByRoleCode(roleCode);
            return Array.Exists<string>(allowUrls, new Predicate<string>(delegate(string item) { return UrlMatch(item, requestUrl); }));
        }

        private static bool UrlMatch(string patternUrl, string requestUrl, bool isRegularMatch = true)
        {
            //���ƥ��URL������**��ģʽƥ�䣬���˹��ܲ�������ģʽ����ƥ�䣬ֱ�ӷ���
            if (patternUrl.EndsWith("**") && !isRegularMatch)
            {
                return false;
            }

            if (!patternUrl.EndsWith("**"))//������ƶ���url����ֱ��ͨ��url�ַ������бȽϡ�
            {
                return requestUrl.StartsWith(patternUrl, StringComparison.InvariantCultureIgnoreCase);
            }
            else//url:/admin/**
            {
                patternUrl = patternUrl.Replace("**", "");
                return requestUrl.StartsWith(patternUrl, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        #endregion

    }
}
