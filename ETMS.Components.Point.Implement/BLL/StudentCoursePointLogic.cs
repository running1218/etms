using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Point.Implement.DAL;
using ETMS.Components.Point.API.Entity;
using ETMS.AppContext;

namespace ETMS.Components.Point.Implement.BLL
{
    /// <summary>
    /// 学生课程积分业务扩展类
    /// 黄中福：2012－05－08
    /// </summary>
    public partial class StudentCoursePointLogic
    {

        private static readonly StudentCoursePointDataAccess DAL = new StudentCoursePointDataAccess();
        
        
        #region 业务操作方法(计算积分、录入积分、删除积分、发布积分等）


        /// <summary>
        /// 计算某个培训项目课程的课程积分
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="passLine">分数线：能获取积分的分数线</param>
        /// <param name="accessPointsUser">操作员</param>
        /// <returns></returns>
        public int ComputeCoursePointByTrainingItemCourseID(Guid trainingItemCourseID, int accessPointsMode, int passLine, string accessPointsUser)
        {
            return DAL.ComputeCoursePointByTrainingItemCourseID(trainingItemCourseID,accessPointsMode, passLine, accessPointsUser);
        }

        /// <summary>
        /// 统计某个培训项目课程的积分总和
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int StatCoursePointByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.StatCoursePointByTrainingItemCourseID(trainingItemCourseID);
        }

        /// <summary>
        /// 批量计算培训项目课程的课程积分
        /// </summary>
        /// <param name="trainingItemCourseIDArray">培训项目课程ID数组</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="passLine">分数线：能获取积分的分数线</param>
        /// <param name="accessPointsUser">操作员</param>
        /// <returns></returns>
        public void BatchComputeCoursePointByTrainingItemCourseID(Guid[] trainingItemCourseIDArray, int accessPointsMode, int passLine, string accessPointsUser)
        {
            BatchComputeCoursePointByTrainingItemCourseID(trainingItemCourseIDArray, accessPointsMode,passLine, accessPointsUser, false);
        }


        /// <summary>
        /// 批量计算培训项目课程的课程积分
        /// </summary>
        /// <param name="trainingItemCourseIDArray">培训项目课程ID数组</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="passLine">分数线：能获取积分的分数线</param>
        /// <param name="accessPointsUser">操作员</param>
        /// <param name="isShowDetailErrMsg">是否显示详细的错误信息</param>
        /// <returns></returns>
        public void BatchComputeCoursePointByTrainingItemCourseID(Guid[] trainingItemCourseIDArray, int accessPointsMode, int passLine, string accessPointsUser, bool isShowDetailErrMsg)
        {
            //如果只选择一个课程，则直接调用单个课程计算方法进行计算
            if (trainingItemCourseIDArray.Length == 1)
            {
                try
                {
                    ComputeCoursePointByTrainingItemCourseID(trainingItemCourseIDArray[0],accessPointsMode, passLine, accessPointsUser);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new ETMS.AppContext.BusinessException(ex.Message);
                }

                return;
            }
            string errorMsgALL = "";
            int noSuccessNum = 0;
            foreach (Guid trainingItemCourseID in trainingItemCourseIDArray)
            {
                try
                {
                    int count = ComputeCoursePointByTrainingItemCourseID(trainingItemCourseID, accessPointsMode,passLine, accessPointsUser);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    errorMsgALL += ex.Message;
                    noSuccessNum++;
                }
            }

            if (noSuccessNum > 0)
            {
                string errorMsg = "本组织机构的课程积分计算规则设置不完整，请检查课程积分计算规则，确保不同课程属性的课程课时均有对应的积分值！";
                if (isShowDetailErrMsg)
                {
                    errorMsg = "批量计算培训项目课程的课程积分：当前要计算的课程数为“{0}”个，有“{1}”个没有计算！原因是：" + errorMsgALL;
                    errorMsg = string.Format(errorMsg, trainingItemCourseIDArray.Length, noSuccessNum);
                }
                throw new ETMS.AppContext.BusinessException(errorMsg);
            }
        }



        /// <summary>
        /// 批量设置（手动录入/计算）某个培训项目下的学员选课的积分
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="studentCourseIDArray">要批量设置的学员选课ID数组</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="accessPoints">要设置的积分，应该大于0</param>
        /// <param name="accessPointsUser">操作员姓名</param>
        public void BatchSetStudentCoursePoints(Guid trainingItemCourseID, Guid[] studentCourseIDArray, int accessPointsMode, int accessPoints, string accessPointsUser)
        {
            foreach (Guid  studentCourseID in studentCourseIDArray)
            {
                DAL.ComputeCoursePointByStudentCourseID(trainingItemCourseID, studentCourseID,accessPointsMode, accessPoints, accessPointsUser);
            }
        }


 

        /// <summary>
        /// 批量删除（手动录入）学员选课的积分
        /// </summary>
        /// <param name="studentCourseIDArray">要批量删除积分的学员选课ID数组</param>
        /// <param name="accessPointsUser">操作员姓名</param>
        public int BatchDeleteStudentCoursePoints(Guid[] studentCourseIDArray, string accessPointsUser)
        {
            return DAL.BatchDeleteStudentCoursePoints(studentCourseIDArray, accessPointsUser);
        }


        /// <summary>
        /// 删除某个培训项目课程的（手动录入/计算）的未发布积分
        /// </summary>
        /// <param name="trainingItemCourseID">要除积分的培训项目课程OD</param>
        /// <param name="accessPointsMode">获得积分方式:1计算,2手动</param>
        /// <param name="accessPointsUser">操作员姓名</param>
        public int DeleteStudentCoursePointsByTrainingItemCourseID(Guid trainingItemCourseID, int accessPointsMode, string accessPointsUser)
        {
            return DAL.DeleteStudentCoursePointsByTrainingItemCourseID(trainingItemCourseID, accessPointsMode, accessPointsUser);
        }




        /// <summary>
        /// 发布满足指定查询条件的学员选课的课程积分
        /// </summary>
        /// <param name="criteria">制定的查询条件（参考GetStudentCoursePointAllInfoList方法）</param>
        /// <param name="scoreIssueUser">操作员</param>
        /// <param name="scoreIssueUserID">操作员ID</param>
        /// <returns>本次发布的学员数</returns>
        public int IssueCoursePointByConditionSQL(string criteria, string scoreIssueUser, int scoreIssueUserID)
        {
            return DAL.IssueCoursePointByConditionSQL(criteria, scoreIssueUser, scoreIssueUserID);
        }



        /// <summary>
        /// 发布某个培训项目课程的课程积分
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="scoreIssueUser">操作员</param>
        /// <returns>本次发布的学员数</returns>
        public int IssueCoursePointByTrainingItemCourseID(Guid trainingItemCourseID, string scoreIssueUser, int scoreIssueUserID)
        {
            string criteria = string.Format(" AND a.TrainingItemCourseID='{0}'", trainingItemCourseID);
            return IssueCoursePointByConditionSQL(criteria, scoreIssueUser, scoreIssueUserID);
        }



        /// <summary>
        /// 发布某个学员选课的课程积分
        /// </summary>
        /// <param name="studentCourseID">学员选课ID</param>
        /// <param name="scoreIssueUser">操作员</param>
        /// <param name="scoreIssueUserID">操作员ID</param>
        /// <returns>本次发布的学员数</returns>
        public int IssueCoursePointByStudentCourseID(Guid studentCourseID, string scoreIssueUser, int scoreIssueUserID)
        {
            string criteria = string.Format(" AND a.StudentCourse='{0}'", studentCourseID);
            return IssueCoursePointByConditionSQL(criteria, scoreIssueUser, scoreIssueUserID);
        }


        /// <summary>
        /// 批量发布学员选课的课程积分
        /// </summary>
        /// <param name="studentCourseIDArray">学员选课ID数组</param>
        /// <param name="scoreIssueUser">操作员</param>
        /// <param name="scoreIssueUserID">操作员ID</param>
        /// <returns></returns>
        public int BatchIssueCoursePointByStudentCourseID(Guid[] studentCourseIDArray, string scoreIssueUser, int scoreIssueUserID)
        {
            //拼条件
            string guid = "";
            for (int i = 0; i < studentCourseIDArray.Length; i++)
            {
                guid += "'" + studentCourseIDArray[i].ToString() + "'";
                if (i < studentCourseIDArray.Length - 1)
                    guid += ",";
            }
            if (guid.Trim().Length > 0)
            {
                string criteria = string.Format(" AND a.StudentCourse IN ({0})", guid);
                return IssueCoursePointByConditionSQL(criteria, scoreIssueUser, scoreIssueUserID);
            }
            return 0;

        }






        /// <summary>
        /// 批量发布培训项目课程的课程积分
        /// </summary>
        /// <param name="trainingItemCourseIDArray">培训项目课程ID数组</param>
        /// <param name="scoreIssueUser">操作员</param>
        /// <returns></returns>
        public void BatchIssueCoursePointByTrainingItemCourseID(Guid[] trainingItemCourseIDArray, string scoreIssueUser, int scoreIssueUserID)
        {
            //如果只选择一个课程，则直接调用单个课程计算方法进行计算
            if (trainingItemCourseIDArray.Length == 1)
            {
                IssueCoursePointByTrainingItemCourseID(trainingItemCourseIDArray[0], scoreIssueUser, scoreIssueUserID);
                return;
            }
            string errorMsgALL = "";
            int noSuccessNum = 0;
            foreach (Guid trainingItemCourseID in trainingItemCourseIDArray)
            {
                try
                {
                    IssueCoursePointByTrainingItemCourseID(trainingItemCourseID, scoreIssueUser, scoreIssueUserID);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    errorMsgALL += ex.Message;
                    noSuccessNum++;
                }
            }
            if (noSuccessNum > 0)
            {
                string errorMsg = "批量发培训项目课程的课程积分：当前要发布的课程数为“{0}”个，有“{1}”个没有发布！原因是：" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsg, trainingItemCourseIDArray.Length, noSuccessNum));
            }
        }


        
        #endregion



        #region 与学员积分相关的查询方法





        /// <summary>
        /// 获取某个培训项目课程已经发布积分的学员数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetIssuePointsStudentNumByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetIssuePointsStudentNumByTrainingItemCourseID(trainingItemCourseID);
        }

        /// <summary>
        /// 获取某个培训项目课程可以发布积分的学员数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetCanIssuePointsStudentNumByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetCanIssuePointsStudentNumByTrainingItemCourseID(trainingItemCourseID);
        }


        /// <summary>
        /// 获取某个培训项目课程未发布积分且没有积分（积分为零）的学员数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetNoPointsStudentNumByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetNoPointsStudentNumByTrainingItemCourseID(trainingItemCourseID);
        }


        /// <summary>
        /// 获取选课的所有学生的获得积分的列表
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
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetStudentCoursePointAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取选课的所有学生的未发布的积分列表（已经计算但未发布的课程学习积分）
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">以 AND 打头的查询条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        public DataTable GetStudentNotIssueCoursePointAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND d.IsIssueScore='0' and d.IsComputeScore='1' ";
            return GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取某个培训项目的已经选课的所有学生的获得积分的列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCoursePointAllInfoListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND c.TrainingItemID='{0}'", trainingItemID);
            return GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 从“学员选课表”获取某个学员的所有课程获得积分的列表（“个人积分查询”）
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCoursePointAllInfoListByStudentID(int studentID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND u.UserID='{0}'", studentID); //学员ID
            criteria += " AND d.IsIssueScore='1'";                       //课程的积分必须已经发布
            if ((sortExpression == "") || (sortExpression == null))
                sortExpression = " c.ItemName,a.AccessPointsTime desc "; //按项目名称和获取积分的时间倒序排序
            return GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取某个培训项目课程的所有学生的列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetStudentCoursePointAllInfoListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND d.TrainingItemCourseID='{0}'", trainingItemCourseID);
            return GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目课程的所有未发布的已经进行系统计算积分的列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoIssueStudentCourseComputePointListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND a.AccessPointsMode=1 AND a.IsIssueScore=0 AND a.AccessPoints> 0";//
            return GetStudentCoursePointAllInfoListByTrainingItemCourseID(trainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }

        /// <summary>
        /// 获取某个培训项目课程的所有未发布的已经进行手动录入积分的列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoIssueStudentCourseInputPointListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND a.AccessPointsMode=2 AND a.IsIssueScore=0 AND a.AccessPoints> 0";//
            return GetStudentCoursePointAllInfoListByTrainingItemCourseID(trainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取所有“待发布”（未发布的已经进行系统计算积分或设置有积分）的课程积分列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanIssueStudentCoursePointList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND a.IsIssueScore=0 AND a.AccessPoints> 0"; //待发布的积分
            return GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }

        /// <summary>
        /// 获取所有“已发布”（已发布的已经进行系统计算积分或设置有积分）的课程积分列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetAlreadyIssueStudentCoursePointList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND a.IsIssueScore=1 AND a.AccessPoints> 0"; //已发布的积分
            return GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }
        /// <summary>
        /// 获取所有没有课程积分的学员列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoStudentCoursePointList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND a.IsIssueScore=0 AND (a.AccessPoints = 0 OR a.AccessPoints IS NULL)"; 
            return GetStudentCoursePointAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }

        /// <summary>
        /// 获取某个培训项目课程的所有没有课程积分的学员列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoStudentCoursePointListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND d.TrainingItemCourseID='{0}'", trainingItemCourseID);
            return GetNoStudentCoursePointList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取所有未发布的课程积分列表（返回待发布的积分数据）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoIssueStudentCoursePointAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return GetCanIssueStudentCoursePointList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取某个组织机构的所有“待发布”（未发布的已经进行系统计算积分或设置有积分）的课程积分列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanIssueStudentCoursePointListByOrgID(int orgID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND c.OrgID='{0}'",orgID);     //该组织机构
            return GetCanIssueStudentCoursePointList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取某个培训项目课程的所有“待发布”（未发布的已经进行系统计算积分或设置有积分）的课程积分列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanIssueStudentCoursePointListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND a.TrainingItemCourseID='{0}'", trainingItemCourseID);     //该培训项目课程
            return GetCanIssueStudentCoursePointList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }







        #endregion



        #region 积分排行榜的查询方法


        /// <summary>
        /// 学员积分排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<StudentPointRanking> GetStudentRanking(int top)
        {
            int totalRecords = 0;
            return GetStudentRanking().PageList<StudentPointRanking>(1, top, out totalRecords);
        }

        /// <summary>
        /// 学员积分排行
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<StudentPointRanking> GetStudentRanking()
        { 
            var source = (List<StudentPointRanking>)CacheHelper.Get(string.Format("{0}_{1}", "StudentPointRanking", UserContext.Current.OrganizationID));
            if (null == source)
            { 
                source = DAL.GetStudentRanking(UserContext.Current.OrganizationID).ToList<StudentPointRanking>();
                CacheHelper.Add(string.Format("{0}_{1}", "StudentPointRanking", UserContext.Current.OrganizationID), source, TimeSpan.FromMinutes(0));//测试时去掉
            }
            return source;
        }

        /// <summary>
        /// 获取个人积分
        /// </summary>
        /// <returns></returns>
        public Int64 GetStudentPoint()
        {
            var entity = GetStudentRanking().SingleOrDefault(f => f.StudentID.Equals(UserContext.Current.UserID));
            if (null != entity)
            {
                return entity.CurrentTotalPoint;
            }

            return 0;
        }

        #endregion



        #region 与培训项目及其课程相关的查询方法



        /// <summary>
        /// 获取某个培训项目课程的课时对应设置的课程积分数，如果返回-100,说明没有设置对应的积分
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetTrainingItemCourseHourseGivePoint(Guid trainingItemCourseID)
        {
            return DAL.GetTrainingItemCourseHourseGivePoint(trainingItemCourseID);
        }


        /// <summary>
        /// 判断某个培训项目课程的课时是否设置有对应的积分
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public bool CheckTrainingItemCourseIsHourseGivePoint(Guid trainingItemCourseID)
        {
            return DAL.CheckTrainingItemCourseIsHourseGivePoint(trainingItemCourseID);
        }



        /// <summary>
        /// 获取某个培训项目所有可以计算积分的培训项目课程列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanComputePointCourseListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND t.TrainingItemID='{0}'", trainingItemID);
            return GetCanComputePointCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }




        /// <summary>
        /// 获取所有可以发布课程积分的培训项目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCanIssueCoursePointTrainingItemList()
        {
            return DAL.GetCanIssueCoursePointTrainingItemList();
        }

        /// <summary>
        /// 获取所有可以发布积分的培训项目课程列表
        /// <returns></returns>
        public DataTable GetCanIssuePointCourseList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetCanIssuePointCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }

        /// <summary>
        /// 获取某个组织机构所有可以发布积分的培训项目课程列表
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public DataTable GetCanIssuePointCourseListByOrgID(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return GetCanIssuePointCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }

        /// <summary>
        /// 获取某个培训项目所有可以发布积分的培训项目课程列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanIssuePointCourseListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += string.Format(" AND t.TrainingItemID='{0}'", trainingItemID);
            return GetCanIssuePointCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }

        /// <summary>
        /// 获取某个组织机构下的所有可以发布积分的培训项目列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public DataTable GetCanIssueCoursePointTrainingItemListByOrgID(int orgID)
        {
            return DAL.GetCanIssueCoursePointTrainingItemListByOrgID(orgID);
        }



        /// <summary>
        /// 获取所有可以计算积分的培训项目课程列表
        /// from Tr_ItemCourse tc
        ///     inner join Tr_Item t on t.TrainingItemID = tc.TrainingItemID
        ///     inner join Res_Course c on c.CourseID = tc.CourseID
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCanComputePointCourseList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetCanComputePointCourseList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }

        /// <summary>
        /// 获取所有可以计算积分的培训项目列表
        /// 按创建时间排序
        /// </summary>
        /// <returns></returns>
        public DataTable GetCanComputeCoursePointTrainingItemList()
        {
            return DAL.GetCanComputeCoursePointTrainingItemList();
        }

        /// <summary>
        /// 获取某个组织机构下的所有可以计算积分的培训项目列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <returns></returns>
        public DataTable GetCanComputeCoursePointTrainingItemListByOrgID(int orgID)
        {
            return DAL.GetCanComputeCoursePointTrainingItemListByOrgID(orgID);

        }




        #endregion






    }
}
