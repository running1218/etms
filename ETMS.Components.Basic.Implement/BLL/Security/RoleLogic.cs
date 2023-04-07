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
    /// 角色管理
    /// </summary>
    public class RoleLogic : NodeLogic
    {

        private PageUrlLogic PageUrlService = new PageUrlLogic();

        #region 内置角色定义（何为内置角色：系统初始化时插入角色的，且organizationID=0
        /// <summary>
        /// 系统管理员角色
        /// </summary>
        public static string SystemAdminRoleCode = "0001";

        /// <summary>
        /// 机构管理员角色
        /// </summary>
        public static string OrganizationAdminRoleCode = "0002";

        /// <summary>
        /// 讲师角色
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
                    //已存在角色名称
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
        /// 获取机构下自建角色
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public Role[] GetOrganizationRoles(Int32 organizationID)
        {
            return (Role[])new RoleDataAccess().Query(" AND organizationID=" + organizationID.ToString());
        }
        /// <summary>
        /// 根据教师角色查询教师功能列表（管理教师）
        /// </summary>
        /// <param name="roleCode">教师角色编码</param>
        /// <param name="functionType"> 1:业务功能区;2:课程情况查询;3:作业情况查询;4:上网行为查询</param>
        /// <returns>功能列表</returns>
        public System.Data.DataTable GetFunctionListByRoleCode(string roleCode, int functionType)
        {
            return ETMS.Utility.BizCache.BizCacheHelper.GetOrInsertItem<System.Data.DataTable>(roleCode, functionType.ToString(), () =>
            {
                return ((RoleDataAccess)DAL).GetFunctionListByRoleCode(roleCode, functionType);
            });
        }

        /// <summary>
        /// 获取角色授予的功能URL集合
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public String[] GetPageUrlByRoleCode(string roleCode)
        {
            //config/BizCache.config中定义缓存过期策略
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
        /// 获取用户授予的功能URL集合
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public String[] GetPageUrlByUserID(int userID)
        {
            //config/BizCache.config中定义缓存过期策略
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
        /// 判断当前管理用户是否被允许访问URL（用户包含：用户所属角色授权，用户个人授权）
        /// </summary>
        /// <param name="roleCodes">管理用户角色编码串，多个之间通过“，”分割</param>
        /// <param name="userID">用户ID</param>
        /// <param name="requestUrl">请求访问的URL</param>
        /// <returns>true false</returns>
        public bool IsAllowManageUserAccessURL(string roleCodes, int userID, string requestUrl)
        {
            bool isAllow = false;
            string[] allowUrls = (userID == 0 ? new string[0] : this.GetPageUrlByUserID(userID));
            isAllow = Array.Exists<string>(allowUrls, new Predicate<string>(delegate(string item) { return UrlMatch(item, requestUrl); }));
            if (!isAllow)//如果未定义用户功能分配，则采用用户角色验证。
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
            if (!isAllow)//如果不允许，且url未注册到系统中，则默认允许
            {
                string[] allUrls = PageUrlService.GetAllRegisterUrls();
                isAllow = !Array.Exists<string>(allUrls, new Predicate<string>(delegate(string item) { return UrlMatch(item, requestUrl, false); }));
            }
            return isAllow;
        }
        #region helper

        /// <summary>
        /// 判断当前角色是否被允许访问URL
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="requestUrl">相对于Root路径</param>
        /// <returns></returns>
        private bool IsAllowAccessURL(string roleCode, string requestUrl)
        {
            string[] allowUrls = GetPageUrlByRoleCode(roleCode);
            return Array.Exists<string>(allowUrls, new Predicate<string>(delegate(string item) { return UrlMatch(item, requestUrl); }));
        }

        private static bool UrlMatch(string patternUrl, string requestUrl, bool isRegularMatch = true)
        {
            //如果匹配URL包含“**”模式匹配，但此功能不允许按照模式进行匹配，直接返回
            if (patternUrl.EndsWith("**") && !isRegularMatch)
            {
                return false;
            }

            if (!patternUrl.EndsWith("**"))//如果是制定的url，则直接通过url字符串进行比较。
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
