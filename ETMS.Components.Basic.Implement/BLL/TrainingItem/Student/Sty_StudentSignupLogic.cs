using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Utility.Logging;

using ETMS.Components.Basic.Implement.DAL.TrainingItem.Student;
using ETMS.Components.Basic.API.Entity.TrainingItem.Student;
using ETMS.Components.Basic.API.Entity.Security;


namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Student
{
    public partial class Sty_StudentSignupLogic
    {

        #region 业务操作方法，如：添加、修改、删除、审核等

        /// <summary>
        /// 培训项目学员保存
        /// </summary>
        /// <param name="entity">培训项目学员实体</param>
        /// <param name="action">操作方法：添加或者修改</param>
        public void Save(Sty_StudentSignup entity, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                    Add(entity);
                else if (action == OperationAction.Edit)
                    Save(entity);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(Sty_StudentSignup sty_StudentSignup)
        {
            try
            {
                if (sty_StudentSignup.StudentSignupID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    sty_StudentSignup.StudentSignupID = sty_StudentSignup.StudentSignupID.NewID(); ;
                    Add(sty_StudentSignup);
                }
                else
                {
                    Update(sty_StudentSignup);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Sty_StudentSignupCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("TrainingItem.Student.Sty_StudentSignup.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Sty_StudentSignupName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("TrainingItem.Student.Sty_StudentSignup.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        } 



        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid studentSignupID)
        {
            try
            {
                DAL.Remove(studentSignupID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(studentSignupID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errMsg = ex.Message.ToUpper(); 
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (errMsg.IndexOf("FK_POINT_ST_REFERENCE_STY_STUD", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该学员已经获得学习过程获得积分，不能删除！");
                }
                else if (errMsg.IndexOf("FK_STY_CLAS_REFERENCE_STY_STUD", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该学员已经被分配到班级，不能删除！");
                }
                else if (errMsg.IndexOf("FK_STY_STUD_REFERENCE_STY_STUD", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该学员已经选课，不能删除！");
                }

                //如果仍未处理，则抛出
                throw ex;
            }
        }  




        /// <summary>
        /// 获取培训项目的学员总数
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns>学员总数</returns>
        public Int32 GetTrainingItemStudentTotal(Guid trainingItemID)
        {
            return DAL.GetTrainingItemStudentTotal(trainingItemID);
        }



        /// <summary>
        /// 项目学员调整 启用或者停用学员状态(zhangsz Add 2012-05-14)
        /// </summary>
        /// <param name="studentSignupID">学员报名ID</param>
        /// <param name="isUse">启用或者停用</param>
        public void UpdateIsUseByStudentSignupID(Guid[] studentSignupIDs, int isUse)
        {
            Sty_StudentSignupDataAccess dal = new Sty_StudentSignupDataAccess();
            for (int i = 0; i < studentSignupIDs.Length; i++)
            {
                try
                {
                    dal.UpdateIsUseByStudentSignupID(studentSignupIDs[i], isUse);
                }
                catch { }
            }
        }



  



        #endregion


   

        #region 数据查询
        


        /// <summary>
        /// 获取某个培训项目的学员列表
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            return DAL.GetStudentListByTrainingItemID( trainingItemID,  pageIndex,  pageSize,  criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目的学员列表,返回学员的基本信息
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentListALLByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            return DAL.GetStudentListALLByTrainingItemID( trainingItemID,  pageIndex,  pageSize,  criteria, out  totalRecords);
        }

        /// <summary>
        /// 方法 GetNoSelectStudentList中，where条件模板
        /// </summary>
        string sqlGetNoSelectStudentlistWhere = @"and WorkerNo like '%{0}%' 
                                                  and RealName like '%{1}%' 
                                                  and (OrganizationID={2} or {2}=0) 
                                                  and (DepartmentID={3} or {3}=0)
                                                  and (PostID={4} or {4}=0)
                                                  and (RankID={5} or {5}=0)
                                                  and UserID not in (select userID from dbo.Sty_StudentSignup where TrainingItemID='{6}')";

        /// <summary>
        /// 获取学员列表，用于项目学员添加 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="workerNo">工号</param>
        /// <param name="realName">姓名</param>
        /// <param name="organizationID">组织机构编号</param>
        /// <param name="departmentID">部门编号</param>
        /// <param name="postID">岗位编号</param>
        /// <param name="rankID">职级编号</param>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectStudentList(int pageIndex, int pageSize, string sortExpression, string workerNo, string realName, int organizationID, int departmentID, int postID, int rankID, Guid trainingItemID, out int totalRecords)
        {
            string criteria = string.Format(sqlGetNoSelectStudentlistWhere, workerNo.ToSafeSQLValue(), realName.ToSafeSQLValue(), organizationID, departmentID, postID, rankID, trainingItemID);
            return GetNoSelectStudentList(pageIndex, pageSize,sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 获取学员列表，用于项目学员添加 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectStudentList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetNoSelectStudentList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目未添加的学员列表，用于项目学员添加 
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectStudentListByTrainingItemID(Guid trainingItemID,int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string sqlModal = @" and UserID not in (select userID from dbo.Sty_StudentSignup where TrainingItemID='{0}')";
            criteria += string.Format(sqlModal,trainingItemID);
            return DAL.GetNoSelectStudentList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }



        /// <summary>
        /// 培训项目集中报名
        /// </summary>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <param name="userIDList">培训学员列表</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户</param>
        /// <param name="modifyTime">修改时间</param>
        /// <param name="modifyUser">修改用户</param>
        /// <param name="reMark">备注</param>
        public void TrainingItemCenterSignUp(Guid trainingItemID, int[] userIDs, DateTime createTime, int createUserID, string createUser, DateTime modifyTime, string modifyUser, string reMark)
        {
            SignupModeEnum signUpModeID = SignupModeEnum.CenterSignUp;
            AddStudentListToTrainingItem(signUpModeID, trainingItemID, userIDs, createTime, createUserID, createUser, modifyTime, modifyUser, reMark);
        }

        /// <summary>
        /// 培训项目自主报名(前台)
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public Guid TrainingItemSelfSignUp(Guid trainingItemID)
        {
            var entity = GetStudentSignupByTrainingItemAndUser(trainingItemID, UserContext.Current.UserID);
            if (null == entity || entity.StudentSignupID.Equals(Guid.Empty))
            {
                entity = new Sty_StudentSignup()
                {
                    StudentSignupID = Guid.NewGuid(),
                    SignupModeID = (int)SignupModeEnum.SelfSignUp,
                    TrainingItemID = trainingItemID,
                    UserID = UserContext.Current.UserID,
                    CreateUserID = UserContext.Current.UserID,
                    CreateTime = DateTime.Now,
                    CreateUser = UserContext.Current.RealName,
                    ModifyTime = DateTime.Now,
                    ModifyUser = UserContext.Current.RealName
                };
                DAL.Add(entity);
            }
            return entity.StudentSignupID;
        }        

        /// <summary>
        /// 项目报名&获取学员报名ID（前台报名）
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Sty_StudentSignup GetStudentSignupByTrainingItemAndUser(Guid trainingItemID, int userID)
        {
            int totalRecords = 0;
            var list = GetStudentListByTrainingItemID(trainingItemID, 1, int.MaxValue - 1000, string.Empty, out totalRecords).ToList<Sty_StudentSignup>();
            return list.Where(f => f.UserID.Equals(userID)).SingleOrDefault();
        }

        /// <summary>
        /// 培训项目报名
        /// </summary>
        /// <param name="signUpModeID">报名模式：</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="userIDList">培训学员列表</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户</param>
        /// <param name="modifyTime">修改时间</param>
        /// <param name="modifyUser">修改用户</param>
        /// <param name="reMark">备注</param>
        public void AddStudentListToTrainingItem(SignupModeEnum signUpModeID, Guid trainingItemID, int[] userIDs, DateTime createTime, int createUserID, string createUser, DateTime modifyTime, string modifyUser, string reMark)
        {
            DataTable userTab = new DataTable();
            userTab.Columns.Add("UserID");
            string userIDList = string.Empty;
            for (int i = 0; i < userIDs.Length; i++)
            {
                if (string.IsNullOrEmpty(userIDList))
                {
                    userIDList = userIDs[i].ToString();
                }
                else
                {
                    userIDList = string.Format("{0},{1}", userIDList,userIDs[i].ToString());
                }
                DataRow newRow = userTab.NewRow();
                newRow["UserID"] = userIDs[i].ToString();
                userTab.Rows.Add(newRow);
            }
            
            AddStudentListToTrainingItem(signUpModeID, trainingItemID, userIDList, createTime, createUserID, createUser, modifyTime, modifyUser, reMark);

            new Tr_ItemLogic().GenerateOrder(trainingItemID, userTab);
        }


        /// <summary>
        /// 培训项目报名
        /// </summary>
        /// <param name="signUpModeID">报名模式：</param>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <param name="userIDList">培训学员列表</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户</param>
        /// <param name="modifyTime">修改时间</param>
        /// <param name="modifyUser">修改用户</param>
        /// <param name="reMark">备注</param>
        public void AddStudentListToTrainingItem(SignupModeEnum signUpModeID, Guid trainingItemID, string userIDList, DateTime createTime, int createUserID, string createUser, DateTime modifyTime, string modifyUser, string reMark)
        {
            DAL.AddStudentListToTrainingItem(signUpModeID, trainingItemID, userIDList, createTime, createUserID, createUser, modifyTime, modifyUser, reMark);
        }



        /// <summary>
        /// 培训项目报名:按查询条件报名
        /// </summary>
        /// <param name="signUpModeID">报名模式：</param>
        /// <param name="trainingItemID">培训项目编号</param>
        /// <param name="criteria">要添加的满足条件的学员，与AND 开头的查询条件</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="createUserID">创建用户编号</param>
        /// <param name="createUser">创建用户</param>
        /// <param name="reMark">备注</param>
        /// <returns>返回成功插入的学员数</returns>
        public int AddStudentListToTrainingItemBySQLCondition(SignupModeEnum signUpModeID, Guid trainingItemID, string criteria, DateTime createTime, int createUserID, string createUser, string reMark)
        {
            return DAL.AddStudentListToTrainingItemBySQLCondition(signUpModeID, trainingItemID, criteria, createTime, createUserID, createUser, reMark);
        }




        /// <summary>
        /// 获取学员ID，获取其报名的所有项目列表
        /// [Sty_StudentSignup] a
        /// Tr_Item b
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemListByStudentID(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetTrainingItemListByStudentID(studentID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取学员ID，获取其报名的已经发布而且是启用状态的项目列表，给“学习归档”使用
        /// [Sty_StudentSignup] a
        /// Tr_Item b
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemListToGradeAchiveByStudentID(int studentID)
        {
            int totalRecords = 0;
            string criteria = " AND c.PayStatus=1 AND b.IsIssue=1";
            return GetTrainingItemListByStudentID(studentID, 1, int.MaxValue-1000, "", criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取学员ID，获取其报名的已经发布而且是启用状态的项目列表，给“考试归档”使用
        /// [Sty_StudentSignup] a
        /// Tr_Item b
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemListToExamAchiveByStudentID(int studentID)
        {
            int totalRecords = 0;
            string criteria = " AND b.IsIssue='1' AND b.IsUse='1' ";
            return GetTrainingItemListByStudentID(studentID, 1, int.MaxValue - 1000, "", criteria, out  totalRecords);
        }



        /// <summary>
        /// 根据学员ID，获取其报名的所有项目的已经考试的课程列表
        /// FROM Sty_StudentCourse a
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Tr_ItemCourse d on d.TrainingItemCourseID =a.TrainingItemCourseID
        /// INNER JOIN Res_Course e on e.CourseID = d.CourseID
        /// INNER JOIN vw_ValidStudent f on f.UserID=b.UserID
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseExamList(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentCourseExamList(studentID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        
        /// <summary>
        /// 根据学员ID，获取其报名的某个项目的已经考试的课程列表
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseExamListByTrainingItemID(int studentID, Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, out int totalRecords)
        {
            string criteria = "";
            if(trainingItemID != Guid.Empty)
                criteria += string.Format(" AND c.TrainingItemID='{0}'", trainingItemID);
            return GetStudentCourseExamList(studentID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取报名学员的所有选课列表
        /// FROM Sty_StudentCourse a
        /// INNER JOIN Sty_StudentSignup b on b.StudentSignupID =a.StudentSignupID
        /// INNER JOIN Tr_Item c on c.TrainingItemID =b.TrainingItemID
        /// INNER JOIN Tr_ItemCourse d on d.TrainingItemCourseID =a.TrainingItemCourseID
        /// INNER JOIN Res_Course e on e.CourseID = d.CourseID
        /// INNER JOIN Site_User u on u.UserID=b.UserID
        /// INNER JOIN Site_Student s on s.UserID=u.UserID
        /// LEFT JOIN Sty_ClassStudent g on g.StudentSignupID = b.StudentSignupID
        /// LEFT JOIN Sty_Class h on h.ClassID = g.ClassID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取某个培训项目课程的学员列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND d.[TrainingItemCourseID]='{0}' ", trainingItemCourseID);
            return GetStudentCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }





        /// <summary>
        /// 根据学员ID，获取其报名的所有项目的课程列表
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseListByStudentID(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND b.[UserID]='{0}' ", studentID);
            return GetStudentCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个学员的某个培训项目的课程信息，包括成绩等
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public DataTable GetStudentCourseListByTrainingItemID(int studentID, Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND c.TrainingItemID='{0}' ", trainingItemID);
            return GetStudentCourseListByStudentID(studentID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目课程的学员信息，包括成绩等
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentCourseListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND a.TrainingItemCourseID='{0}' ", trainingItemCourseID);
            return GetStudentCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目课程的学员信息，包括成绩等
        /// </summary>
        /// <param name="trainingItemID">培训项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentCourseListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND c.TrainingItemID='{0}' ", trainingItemID);
            return GetStudentCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }






        /// <summary>
        /// 获取某个学员的某个培训项目的课程信息，包括成绩等
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public DataTable GetStudentCourseList(int studentID, Guid trainingItemID)
        {
            int totalRecords = 0;
            return GetStudentCourseListByTrainingItemID(studentID, trainingItemID, 1, int.MaxValue - 1000, "", "", out  totalRecords);
        }


        /// <summary>
        /// 获取某个学员的某个培训项目的课程数
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public int GetStudentCourseNumByTrainingItemID(int studentID, Guid trainingItemID)
        {
            int totalRecords = 0;
            DataTable dt = GetStudentCourseListByTrainingItemID(studentID, trainingItemID, 1, 0, "", "", out  totalRecords);
            return totalRecords;
        }




        /// <summary>
        /// 根据学员ID，获取学生某门选课的在线考试列表
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="studentCourseID">学员选课ID</param>
        /// <returns></returns>
        public DataTable GetStudentCourseOnLineTestLisByStudentID(int studentID, Guid studentCourseID)
        {
            return DAL.GetStudentCourseOnLineTestLisByStudentID(studentID, studentCourseID);
        }
        /// <summary>
        /// 根据学员ID，获取学生某门选课的在线考试列表
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="studentCourseID"></param>
        /// <param name="OnLineTestID"></param>
        /// <returns></returns>
        public DataTable GetStudentCourseOnLineTestLisByTestID(int studentID, Guid studentCourseID, Guid OnLineTestID)
        {
            return DAL.GetStudentCourseOnLineTestLisByTestID(studentID, studentCourseID, OnLineTestID);
        }

        /// <summary>
        /// 获取某个学员某个培训项目下，尚未选择的课程，且该课程的状态为“启用”
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentNoSelectItemCourseListByTrainingItemID(int studentID, Guid trainingItemID, int pageIndex, int pageSize, string Crieria, out int totalRecords)
        {
            return DAL.GetStudentNoSelectItemCourseListByTrainingItemID(studentID, trainingItemID, pageIndex, pageSize, Crieria, out  totalRecords);
        }



        /// <summary>
        /// 获取所有学员项目报名的所有基本信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetStudentSignupAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentSignupAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取某个培训项目的所有学员项目报名的所有基本信息
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetStudentSignupAllInfoListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND c.TrainingItemID='{0}'", trainingItemID);
            return GetStudentSignupAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目课程未选课的所有学生列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectCourseAllInfoListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetNoSelectCourseAllInfoListByTrainingItemCourseID(trainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取项目学员所在机构列表
        /// </summary>
        /// <param name="traningItemID"></param>
        /// <returns></returns>
        public List<Organization> GetTrainingItemOrganizationList(Guid traningItemID)
        {
            var source = DAL.GetTrainingItemOrganizationList(traningItemID).ToList<Organization>();
            source.Insert(0, new Organization() { OrganizationID = -1, DisplayPath = "全部" });
            return source;
        }

        #endregion

        /// <summary>
        /// 课程中心学员选购课程时，自动创建学员报名数据
        /// </summary>
        /// <param name="signUpModeID"></param>
        /// <param name="trainingItemID"></param>
        /// <param name="userID"></param>
        /// <param name="createTime"></param>
        /// <param name="createUserID"></param>
        /// <param name="createUser"></param>
        /// <param name="modifyTime"></param>
        /// <param name="modifyUser"></param>
        /// <param name="reMark"></param>
        public void AddStyStudentSignup(int signUpModeID, Guid trainingItemID,string[] trainingItemCourseIDs, int userID, DateTime createTime, int createUserID, string createUser, DateTime modifyTime, string modifyUser, string reMark)
        {
            string trainingItemCourseIDList = string.Empty;
            for (int i = 0; i < trainingItemCourseIDs.Length; i++)
            {
                if (trainingItemCourseIDs[i]!= null)
                {
                    if (string.IsNullOrEmpty(trainingItemCourseIDList))
                    {
                        trainingItemCourseIDList = trainingItemCourseIDs[i].ToString();
                    }
                    else
                    {
                        trainingItemCourseIDList = string.Format("{0},{1}", trainingItemCourseIDList, trainingItemCourseIDs[i].ToString());
                    }
                }
            }
            DAL.AddStyStudentSignup(signUpModeID, trainingItemID, trainingItemCourseIDList, userID, createTime, createUserID, createUser, modifyTime, modifyUser, reMark);
        }

        /// <summary>
        /// 学生学习档案
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetUserStudentArchives(int userID) {
            return DAL.GetUserStudentArchives(userID);
        }
    }
}
