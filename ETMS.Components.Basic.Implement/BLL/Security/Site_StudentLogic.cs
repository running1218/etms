//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-30 14:32:19.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 学员信息(用户扩展表)业务逻辑
    /// </summary>
    public partial class Site_StudentLogic
    {
        private static readonly UserLogic UserLogic = new UserLogic();


        /// <summary>
        /// 验证用户名是否存在(供WebService方法使用)
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="userName">用户名</param>
        /// <returns>true:存在，false:不存在</returns>
        public bool IsUserExist(string connectionString, string userName)
        {
            return DAL.IsUserExist(connectionString,userName);
        }



        /// <summary>
        /// 注册用户信息
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <returns></returns>
        public bool UserRegister(string connectionString, Site_Student site_Student)
        {
            try
            {
                //注册一个用户
                DAL.UserRegister(connectionString, site_Student);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_Site_Student_WorkerNo", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Site_Student.WorkerNoExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }

            return true;
        }










        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(Site_Student site_Student)
        {
            try
            {
                if (site_Student.UserID.IsEmpty())
                {
                    //1、添加基本用户信息
                    UserLogic.Save(site_Student);
                    //2、添加学员扩展信息
                    Add(site_Student);
                }
                else
                {  //1、修改基本用户信息
                    UserLogic.Save(site_Student);
                    //2、修改学员扩展信息
                    Update(site_Student);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_Site_Student_WorkerNo", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Site_Student.WorkerNoExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Int32 userID)
        {
            try
            {
                //删除学员基本信息
                DAL.Remove(userID);
                //删除学员扩展信息
                UserLogic.Remove(new User() { UserID = userID });
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(userID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_Sty_StudentSignup_Site_Student", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("Security.Site_Student.IsUsing");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Site_Student GetById(Int32 userID)
        {
            //查询用户基本信息
            User userInfo = (User)UserLogic.GetUserByID(userID);
            //查询用户扩展信息
            Site_Student site_Student = DAL.GetById(userID);
            if (site_Student == null)
            {
                throw new ETMS.AppContext.BusinessException("Security.Site_Student.NotFoundException", new object[] { userID });
            }
            //合成用户完整信息
            site_Student.CopyUserBaseInfo(userInfo);
            return site_Student;
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Site_Student GetStudentById(Int32 userID)
        {
            //查询用户扩展信息
            Site_Student site_Student = DAL.GetById(userID);
            return site_Student;
        }

        /// <summary>
        /// 获取当前机构下启用学员列表，用于前端选择学员
        /// </summary>
        /// <param name="loginName">学员账户</param>
        /// <param name="realName">学员姓名</param>
        /// <param name="workerNo">工号</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面尺寸</param>
        /// <param name="totalRecords">输出总记录数</param>
        /// <returns>users.LoginName,users.RealName,users.MobilePhone,users.DepartmentID,users.[Status],students.WorkerNo,students.RankID,students.PostID</returns>
        public DataTable GetCurrentOrgEnablePagedList(string loginName, string realName, string workerNo, int pageIndex, int pageSize, out int totalRecords)
        {
            //设置查询用户为当前所在机构，启用
            string criteria = string.Format(" and users.OrganizationID={0} and users.[status]=1", ETMS.AppContext.UserContext.Current.OrganizationID);
            if (!string.IsNullOrEmpty(loginName))
            {
                criteria += string.Format(" and users.LoginName like '{0}%'", loginName.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(realName))
            {
                criteria += string.Format(" and users.realName like '{0}%'", realName.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(workerNo))
            {
                criteria += string.Format(" and students.workerNo like '{0}%'", workerNo.ToSafeSQLValue());
            }
            string order = " users.realname ";
            return DAL.GetManagePagedList(pageIndex, pageSize, order, criteria, out totalRecords);
        }

        /// <summary>
        /// 获取当前机构下管理学员列表（包括停用学员列表）
        /// </summary>
        /// <param name="loginName">学员账户</param>
        /// <param name="realName">学员姓名</param>
        /// <param name="workerNo">工号</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面尺寸</param>
        /// <param name="totalRecords">输出总记录数</param>
        /// <returns>users.LoginName,users.RealName,users.MobilePhone,users.DepartmentID,users.[Status],students.WorkerNo,students.RankID,students.PostID</returns>
        public DataTable GetCurrentOrgManagePagedList(string loginName, string realName, string workerNo, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetCurrentOrgManagePagedList(loginName, realName, workerNo, -1, -1, -1, -1, pageIndex, pageSize, out totalRecords);
        }
        /// <summary>
        /// 获取当前机构下管理学员列表（包括停用学员列表）
        /// </summary>
        /// <param name="loginName">学员账户</param>
        /// <param name="realName">学员姓名</param>
        /// <param name="workerNo">工号</param>
        /// <param name="departmentId">部门</param>
        /// <param name="rankId">职级</param>
        /// <param name="postId">岗位</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面尺寸</param>
        /// <param name="totalRecords">输出总记录数</param>
        /// <returns>users.LoginName,users.RealName,users.MobilePhone,users.DepartmentID,users.[Status],students.WorkerNo,students.RankID,students.PostID</returns>
        public DataTable GetCurrentOrgManagePagedList(string loginName, string realName, string workerNo, int departmentId, int rankId, int postId, int userStatus, int pageIndex, int pageSize, out int totalRecords)
        {
            //设置查询用户为当前所在机构
            string criteria = string.Format(" and users.OrganizationID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
            if (!string.IsNullOrEmpty(loginName))
            {
                criteria += string.Format(" and users.LoginName like '%{0}%'", loginName.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(realName))
            {
                criteria += string.Format(" and users.realName like '%{0}%'", realName.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(workerNo))
            {
                criteria += string.Format(" and students.workerNo like '%{0}%'", workerNo.ToSafeSQLValue());
            }
            if (departmentId != -1)
            {
                criteria += string.Format(" and users.departmentid = '{0}'", departmentId);
            }
            if (rankId != -1)
            {
                criteria += string.Format(" and students.RankID = '{0}'", rankId);
            }
            if (postId != -1)
            {
                criteria += string.Format(" and students.ResettlementWayID = '{0}'", postId);
            }

            if (userStatus != -1)
            {
                criteria += string.Format(" and users.[Status] = '{0}'", userStatus);
            }
            string order = " users.LoginName ";
            return DAL.GetManagePagedList(pageIndex, pageSize, order, criteria, out totalRecords);
        }

        /// <summary>
        /// 高级版本获取管理需要信息列表
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面尺寸</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">过滤条件，注意：表别名：users(site_user),students(site_student)</param>
        /// <param name="totalRecords">输出总记录数</param>
        /// <returns>users.LoginName,users.RealName,users.MobilePhone,users.DepartmentID,users.[Status],students.WorkerNo,students.RankID,students.PostID</returns>
        public DataTable GetManagePagedListAdv(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            //设置查询用户为当前所在机构
            sortExpression = sortExpression ?? " users.realname ";//设置默认排序条件
            return DAL.GetManagePagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetUserStudentPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords) {
            return DAL.GetUserStudentPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }
    }


}

