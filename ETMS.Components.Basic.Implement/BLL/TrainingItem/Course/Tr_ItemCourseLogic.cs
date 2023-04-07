using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Course
{

    /// <summary>
    /// 培训项目课程扩展类
    /// 黄中福
    /// </summary>
    public partial class Tr_ItemCourseLogic
    {
        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid trainingItemCourseID)
        {
            try
            {
                DAL.Remove(trainingItemCourseID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(trainingItemCourseID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message.ToUpper();
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC4", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目课程已经有“导学资料”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_RES_ITEM_REFERENCE_TR_ITEMC", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目课程已经有“离线作业”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC2", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目课程已经有“课程资源”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_STY_COUR_REFERENCE_TR_ITEMC", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目课程已经有“课程学习过程监控”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC3", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目课程已经有“课时安排”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_STY_STUD_REFERENCE_TR_ITEMC ", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目课程已经有“学员选课”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目课程已经有“讲师”，不能删除！");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }


        /// <summary>
        /// 批量添加课程到指定项目中,同时添加该课程已经启用的在线课件到培训项目课程的资源中
        /// hzf：2012-10-30
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="courseIDArray">要添加的课程ID数组（GUID）</param>
        /// <param name="TeachModelID">授课方式</param>
        /// <param name="CourseAttrID">课程属性</param>
        /// <param name="CourseBeginTime">培训开始时间</param>
        /// <param name="CourseEndTime">培训结束时间</param>
        /// <param name="CreateUserID">创建人ID</param>
        /// <param name="CreateUser">创建人</param>
        public void BatchAddItemCourseAndCourseware(Guid trainingItemID, Guid[] courseIDArray, int TeachModelID, int CourseAttrID, DateTime CourseBeginTime, DateTime CourseEndTime, decimal PassLine, int CreateUserID, string CreateUser)
        {
            int noSuccessNum = 0;
            string errorMsgALL = "";
            foreach (Guid courseID in courseIDArray)
            {

                Res_Course course = new Res_CourseLogic().GetById(courseID);

                Tr_ItemCourse entity = new Tr_ItemCourse();
                entity.TrainingItemCourseID = System.Guid.NewGuid();
                entity.CourseID = courseID;
                entity.TrainingItemID = trainingItemID;
                entity.CourseStatus = 1;

                entity.TeachModelID = TeachModelID;
                entity.CourseAttrID = CourseAttrID;
                entity.CourseBeginTime = CourseBeginTime;
                entity.CourseEndTime = CourseEndTime;
                entity.PassLine = PassLine;
                entity.CourseHours = course.CourseHours;

                //hzf2012-10-19加
                entity.CreateUserID = CreateUserID;
                entity.CreateUser = CreateUser;
                entity.CreateTime = System.DateTime.Now;
                entity.ModifyUser = CreateUser;
                entity.ModifyTime = System.DateTime.Now;

                try
                {
                    //如果添加不成功，继续添加下一条
                    DAL.AddItemCourseAndCourseware(entity);
                }
                catch (System.Exception ex)
                {
                    noSuccessNum++;
                    if (errorMsgALL.IndexOf(ex.Message) < 0)
                        errorMsgALL += ex.Message;
                }
            }
            if (noSuccessNum > 0)
            {
                errorMsgALL = "添加完毕：当前要添加的项目课程数为“{0}”个，有“{1}”个添加不成功，原因如下：" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsgALL, courseIDArray.Length, noSuccessNum));
            }
        }

        /// <summary>
        /// 添加课程到指定项目中,同时添加该课程已经启用的在线课件到培训项目课程的资源中
        /// </summary>
        /// <param name="List<Tr_ItemCourse>"></param>
        public void AddItemCourseAndCourseware(List<Tr_ItemCourse> list)
        {
            int noSuccessNum = 0;
            string errorMsgALL = "";
            foreach (Tr_ItemCourse itemCourse in list)
            {
                try
                {
                    //如果添加不成功，继续添加下一条
                    DAL.AddItemCourseAndCourseware(itemCourse);
                }
                catch (System.Exception ex)
                {
                    noSuccessNum++;
                    if (errorMsgALL.IndexOf(ex.Message) < 0)
                        errorMsgALL += ex.Message;
                }
            }
            if (noSuccessNum > 0)
            {
                errorMsgALL = "添加完毕：当前要添加的项目课程数为“{0}”个，有“{1}”个添加不成功，原因如下：" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsgALL, list.Count, noSuccessNum));
            }
        }

        /// <summary>
        /// 批量删除项目课程，同时添加该项目课程的资源
        /// </summary>
        public void BatchRemoveItemCourseAndCourseware(Guid[] trainingItemCourseIDArray)
        {
            int noSuccessNum = 0;
            string errorMsgALL = "";
            foreach (Guid trainingItemCourseID in trainingItemCourseIDArray)
            {
                try
                {
                    DAL.RemoveItemCourseAndCourseware(trainingItemCourseID);
                    //记录删除日志（根据ID删除）
                    BizLogHelper.Operate(trainingItemCourseID, "删除");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    noSuccessNum++;
                    string errorMsgOne = "";
                    string errorMsg = ex.Message.ToUpper();
                    //枚举数据约束异常并转换为业务异常，数据已经使用
                    if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC4", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程已经有“导学资料”，不能删除！";
                    }
                    else if (errorMsg.IndexOf("FK_RES_ITEM_REFERENCE_TR_ITEMC", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程已经有“离线作业”，不能删除！";
                    }
                    else if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC2", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程已经有“课程资源”，不能删除！";
                    }
                    else if (errorMsg.IndexOf("FK_STY_COUR_REFERENCE_TR_ITEMC", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程已经有“课程学习过程监控”，不能删除！";
                    }
                    else if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC3", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程已经有“课时安排”，不能删除！";
                    }
                    else if (errorMsg.IndexOf("FK_STY_STUD_REFERENCE_TR_ITEMC", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程已经有“学员选课”，不能删除！";
                    }
                    else if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEMC", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程已经有“讲师”，不能删除！";
                    }
                    else
                        errorMsgALL = ex.Message;
                    if (errorMsgALL.IndexOf(errorMsgOne) < 0)
                        errorMsgALL += errorMsgOne + "\r\n";
                }

            }
            if (noSuccessNum > 0)
            {
                errorMsgALL = "删除失败，原因如下：\r\n" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsgALL, trainingItemCourseIDArray.Length, noSuccessNum));
            }

        }


        /// <summary>
        /// 批量添加课程到指定项目中 (edit 添加5个字段 zhangsz 2012-04-08)
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="courseIDArray">要添加的课程ID数组（GUID）</param>
        /// <param name="TeachModelID">授课方式</param>
        /// <param name="CourseAttrID">课程属性</param>
        /// <param name="CourseHours">课程学时</param>
        /// <param name="CourseBeginTime">培训开始时间</param>
        /// <param name="CourseEndTime">培训结束时间</param>
        /// <param name="CreateUserID">创建人ID</param>
        /// <param name="CreateUser">创建人</param>
        public void BatchAdd(Guid trainingItemID, Guid[] courseIDArray, int TeachModelID, int CourseAttrID, decimal CourseHours, DateTime CourseBeginTime, DateTime CourseEndTime, decimal BudgetFee, decimal PassLine, int CreateUserID, string CreateUser)
        {
            int noSuccessNum = 0;
            string errorMsg = "";
            foreach (Guid courseID in courseIDArray)
            {
                Tr_ItemCourse entity = new Tr_ItemCourse();
                entity.TrainingItemCourseID = System.Guid.NewGuid();
                entity.CourseID = courseID;
                entity.TrainingItemID = trainingItemID;
                entity.CourseStatus = 1;

                entity.TeachModelID = TeachModelID;
                entity.CourseAttrID = CourseAttrID;
                entity.CourseHours = CourseHours;
                entity.CourseBeginTime = CourseBeginTime;
                entity.CourseEndTime = CourseEndTime;
                entity.BudgetFee = BudgetFee;
                entity.PassLine = PassLine;

                //hzf2012-10-19加
                entity.CreateUserID = CreateUserID;
                entity.CreateUser = CreateUser;
                entity.CreateTime = System.DateTime.Now;
                entity.ModifyUser = CreateUser;
                entity.ModifyTime = System.DateTime.Now;

                try
                {
                    //如果添加不成功，继续添加下一条
                    DAL.Add(entity);
                }
                catch (System.Exception ex)
                {
                    noSuccessNum++;
                    errorMsg = ex.Message;
                }
                if (noSuccessNum > 0)
                {
                    errorMsg = "添加完毕：当前要添加的项目课程数为“{0}”个，有“{1}”个添加不成功，原因如下：" + errorMsg;
                    throw new ETMS.AppContext.BusinessException(string.Format(errorMsg, courseIDArray.Length, noSuccessNum));
                }
            }
        }


        /// <summary>
        /// 修改课程状态信息 1启用 0停用 (zhangsz add)
        /// </summary>
        /// <param name="TrainingItemCourseID">培训项目课程ID</param>
        /// <param name="CourseStatus">课程状态  1启用 0停用</param>
        public void UpdateCourseState(Guid TrainingItemCourseID, int CourseStatus)
        {
            Tr_ItemCourse ItemCourse = GetById(TrainingItemCourseID);
            ItemCourse.CourseStatus = CourseStatus;
            Update(ItemCourse);
        }



        /// <summary>
        /// 获取某个在组织机构下的培训项目课程列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByOrgID(int orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetItemCourseListByOrgID(orgID, pageIndex, pageSize, "", out totalRecords);
        }

        /// <summary>
        /// 获取某个组织机构下的培训项目课程列表
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria">AND 打头的查询条件</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByOrgID(int orgID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            return DAL.GetItemCourseListByOrgID(orgID, pageIndex, pageSize, criteria, out totalRecords);
        }


        /// <summary>
        /// 获取某个讲师可维护的培训项目课程列表
        /// 默认满足条件：
        /// 1.培训项目必须是：审核通过
        /// 2.项目是“已经发布启用”
        /// 3.项目和课程状态是“启用”
        /// </summary>
        /// <param name="teacherID">讲师ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTeacherID(int teacherID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetItemCourseListByTeacherID(teacherID, pageIndex, pageSize, out  totalRecords);
        }



        /// <summary>
        /// 获取某个用户ID（讲师对应的用户ID）的培训项目课程列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByUserID(int userID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetItemCourseListByUserID(userID, pageIndex, pageSize, out  totalRecords);
        }
        /// <summary>
        /// 获取某个的培训项目课程列表
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetItemCourseListByTrainingItemCourseID(trainingItemCourseID);
        }


        /// <summary>
        /// 获取某个培训项目下的课程列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemID(Guid trainingItemID)
        {
            int totalRecords = 0;
            return DAL.GetItemCourseListByTrainingItemID(trainingItemID, 1, int.MaxValue - 1000, out totalRecords);
        }

        /// <summary>
        /// 获取某个培训项目下的课程列表，并区分必修、选修报名状态
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemID(int userID,Guid trainingItemID)
        {
            int totalRecords = 0;
            return DAL.GetItemCourseListByTrainingItemID(userID,trainingItemID, 1, int.MaxValue - 1000, out totalRecords);
        }

        /// <summary>
        /// 前台获取项目下启用的课程列表(报名）
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public List<Tr_ItemCourse> GetItemCoursesByTrainingItemID(Guid trainingItemID)
        {
            int totalRecords = 0;
            var list = DAL.GetItemCourseListByTrainingItemID(trainingItemID, 1, int.MaxValue - 1000, out totalRecords).ToList<Tr_ItemCourse>().Where(c => c.CourseStatus.Equals(1)).ToList();
            foreach (var entity in list)
            {
                var signList = GetSignupItemCoursesStatus(trainingItemID, entity.CourseID);
                entity.SignStatus = signList.Sum(f => f.SignStatus);
                var statusEntity = signList.SingleOrDefault(f => f.SignStatus.Equals(1));
                entity.StudentSignupID = statusEntity == null ? entity.StudentSignupID : statusEntity.StudentSignupID;
            }
            return list;
        }

        /// <summary>
        /// 前台获取项目下启用的课程列表(项目公告）
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public List<Tr_ItemCourse> GetItemNoticeCoursesByTrainingItemID(Guid trainingItemID)
        {
            int totalRecords = 0;
            var list = DAL.GetItemCourseListByTrainingItemID(trainingItemID, 1, int.MaxValue - 1000, out totalRecords).ToList<Tr_ItemCourse>().Where(c => c.CourseStatus.Equals(1)).ToList();
            foreach (var entity in list)
            {
                entity.TeacherNum = new Tr_ItemCourseTeacherLogic().GetTeacherListByItemCourseID(entity.TrainingItemCourseID).Count;
            }
            return list;
        }

        /// <summary>
        /// 前台获取项目下启用的报名课程是否已经选择或者其它项目报名过
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public List<Tr_ItemCourse> GetSignupItemCoursesStatus(Guid trainingItemID, Guid courseID)
        {
            return DAL.GetSignCourseStatus(trainingItemID, courseID, UserContext.Current.UserID).ToList<Tr_ItemCourse>();
        }


        /// <summary>
        /// 获取某个培训项目下的课程列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetItemCourseListByTrainingItemID(trainingItemID, pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// 获取培训项目下的课程列表
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="SortExpression">排序条件</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, string sortExpression, out int totalRecords)
        {
            return DAL.GetItemCourseListByTrainingItemID(trainingItemID, pageIndex, pageSize, sortExpression, out totalRecords);
        }

        /// <summary>
        /// 跟据项目课程ID修改排序号
        /// </summary>
        /// <param name="trainingItemCourseID">项目课程ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByItemCourseID(Guid trainingItemCourseID, int orderNum)
        {
            DAL.UpdateOrderNumByItemCourseID(trainingItemCourseID, orderNum);
        }

        /// <summary>
        /// 获取某个培训项目下的课程数
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public int GetItemCourseCountByTrainingItemID(Guid trainingItemID)
        {
            return DAL.GetItemCourseCountByTrainingItemID(trainingItemID);
        }



        /// <summary>
        /// 获取某个培训项目课程下的离线作业列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetOffLineJobListByTrainingItemCourseID(Guid trainingItemCourseID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetOffLineJobListByTrainingItemCourseID(trainingItemCourseID, pageIndex, pageSize, out totalRecords);
        }



        /// <summary>
        /// 获取某个培训项目课程下的离线作业数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetOffLineJobCountByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetOffLineJobCountByTrainingItemCourseID(trainingItemCourseID);
        }


        /// <summary>
        /// 获取某个培训项目课程下的离线作业列表
        /// </summary>
        /// <param name="trainingItemCourseID">项目课程ID</param>
        /// <param name="conditionSQL">以AND开头的查询条件</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="totalRecords">所有满足条件的记录数</param>
        /// <returns></returns>
        public DataTable GetOffLineJobListByTrainingItemCourseIDAndCondition(Guid trainingItemCourseID, string conditionSQL, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetOffLineJobListByTrainingItemCourseIDAndCondition(trainingItemCourseID, conditionSQL, pageIndex, pageSize, out  totalRecords);

        }

        /// <summary>
        /// 获取某个培训项目下的离线作业列表
        /// </summary>
        /// <param name="trainingItemID">项目ID</param>
        /// <returns></returns>
        public DataTable GetOffLineJobListByTrainingItemID(Guid trainingItemID, int UserID)
        {
            return DAL.GetOffLineJobListByTrainingItemID(trainingItemID, UserID);

        }
        /// <summary>
        /// 根据用户ID获取所有培训项目下的课程列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemCourseLisListByUserID(int UserID)
        {
            return DAL.GetTrainingItemCourseLisListByUserID(UserID);
        }
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="JobName">实践名称</param>
        /// <param name="ItemName">项目名称</param>
        /// <param name="OrganizationID">机构ID</param>   
        /// <returns>返回查询结果</returns>
        public DataTable GetPageList( string JobName, string ItemName, int OrganizationID)
        {
            return DAL.GetPageList(JobName, ItemName,OrganizationID);
        }


        /// <summary>
        /// 获取成绩发布列表，涉及的表有：Tr_Item、Res_Course、Tr_ItemCourse
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetGradeIssueList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetGradeIssueList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 发布某个培训项目课程成绩
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="isIssueGrade">是否发布课程成绩（0：不发布，1：发布）</param>
        /// <param name="gradeIssueUser">课程成绩发布人</param>
        public void Tr_ItemCourse_GradeIssue(Guid trainingItemCourseID, int isIssueGrade, string gradeIssueUser)
        {
            try
            {
                DAL.Tr_ItemCourse_GradeIssue(trainingItemCourseID, isIssueGrade, gradeIssueUser);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }


        /// <summary>
        /// 培训项目课程的学生成绩列表
        ///     INNER JOIN Sty_StudentSignup ss on ss.StudentSignupID = a.StudentSignupID
        ///     INNER JOIN Tr_ItemCourse c on c.TrainingItemCourseID = a.TrainingItemCourseID
        ///     INNER JOIN Res_Course b on b.CourseID = c.CourseID
        ///     INNER JOIN Site_User u on u.UserID = ss.UserID
        ///     INNER JOIN Site_Student s on s.UserID = u.UserID
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        /// <returns>DataTable</returns>
        public DataTable GetItemCourseStudentScoreList(Guid trainingItemCourseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetItemCourseStudentScoreList(trainingItemCourseID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 查询某个培训项目课程及其对应的课程报名与否等数据
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="trainingItemSignupNumber">整个项目已经报名的学员数</param>
        /// <param name="trainingItemCourseSignupNumber">该培训项目课程已经报名的学员数(已经选课的学员数)</param>
        /// <param name="courseSignupNumber">该项目学员中，已经在别的项培训项目课程对应该课程报名的学员数</param>
        /// <param name="courseReSignupNumber">当前该课程已经重复报名的学员数</param>
        public void Tr_ItemCourse_GetStudentSignupOrNotNumber(Guid trainingItemCourseID, out int trainingItemSignupNumber, out int trainingItemCourseSignupNumber, out int courseSignupNumber, out int courseReSignupNumber)
        {
            DAL.Tr_ItemCourse_GetStudentSignupOrNotNumber(trainingItemCourseID, out  trainingItemSignupNumber, out  trainingItemCourseSignupNumber, out  courseSignupNumber, out  courseReSignupNumber);
        }


        /// <summary>
        /// 跟据项目ID与讲师ID获得项目课程
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public DataTable GetItemCourseListByTrainingItemIDAndTeacherID(Guid trainingItemID, int teacherID)
        {
            return DAL.GetItemCourseListByTrainingItemIDAndTeacherID(trainingItemID, teacherID);
        }

        public void UpdateItemCourseBudgetFee(string itemCourseID, string budgetFee)
        {
            DAL.UpdateItemCourseBudgetFee(itemCourseID, budgetFee);
        }

        /// <summary>
        /// 获取学员未报名项目课程列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetNotBuyTraininItemCourseList(int userID, Guid itemID,int signupModeID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetNotBuyTraininItemCourseList(userID, itemID, signupModeID,pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// 项目课程完成情况统计
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public TrainItemCourseStudyAnalysis GetTrainItemCourseStudyAnalysis(Guid trainingItemCourseID)
        {
            TrainItemCourseStudyAnalysis ChooseCourseNumModle = DAL.GetChooseCourseNum(trainingItemCourseID).ToList<TrainItemCourseStudyAnalysis>()[0];
            TrainItemCourseStudyAnalysis CompletedNumModle = DAL.GetCompletedNum(trainingItemCourseID).ToList<TrainItemCourseStudyAnalysis>()[0];
            TrainItemCourseStudyAnalysis UnStudyNumModle = DAL.GetUnStudyNum(trainingItemCourseID).ToList<TrainItemCourseStudyAnalysis>()[0];
            TrainItemCourseStudyAnalysis ContentCompleteNumModle = DAL.GetContentCompleteNum(trainingItemCourseID).ToList<TrainItemCourseStudyAnalysis>()[0];
            TrainItemCourseStudyAnalysis JobCompleteNumModle = DAL.GetJobCompleteNum(trainingItemCourseID).ToList<TrainItemCourseStudyAnalysis>()[0];

            TrainItemCourseStudyAnalysis result = new TrainItemCourseStudyAnalysis();
            result.ChooseCourseNum = ChooseCourseNumModle.ChooseCourseNum;
            result.CompletedNum = CompletedNumModle.CompletedNum;
            result.UnStudyNum = UnStudyNumModle.UnStudyNum;
            result.ContentCompleteNum = ContentCompleteNumModle.ContentCompleteNum;
            result.JobCompleteNum = JobCompleteNumModle.JobCompleteNum;
            result.UnCompleteNum = result.ChooseCourseNum - result.UnStudyNum - result.CompletedNum;

            if (result.ChooseCourseNum == 0)
            {
                result.ContentCompleteRate = "--";
                result.JobCompleteRate = "--";
                result.CourseCompleteRate = "--";
            }
            else {
                TrainItemCourseStudyAnalysis standard = DAL.GetTop1Info(trainingItemCourseID).ToList<TrainItemCourseStudyAnalysis>()[0];
                if (standard != null && standard.CourseResourceNum != null && standard.CourseResourceNum != 0)
                {
                    result.ContentCompleteRate = string.Format("{0}%", (result.ContentCompleteNum * 100 / result.ChooseCourseNum));
                }
                else {
                    result.ContentCompleteRate = "--";
                }

                if (standard != null && standard.JobNum != null && standard.JobNum != 0)
                {
                    result.JobCompleteRate = string.Format("{0}%", (result.JobCompleteNum * 100 / result.JobCompleteNum));
                }
                else
                {
                    result.JobCompleteRate = "--";
                }

                if (standard != null && ((standard.JobNum != null && standard.JobNum != 0) || (standard.CourseResourceNum != null && standard.CourseResourceNum != 0)))
                {
                    result.CourseCompleteRate = string.Format("{0}%", (result.CompletedNum * 100 / result.ChooseCourseNum));
                }
            }

            return result;
        }
    }
}
