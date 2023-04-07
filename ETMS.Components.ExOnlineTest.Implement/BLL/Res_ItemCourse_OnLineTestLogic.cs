using System;
using System.Data;
using ETMS.Utility.Logging;

using ETMS.Components.ExOnlineTest.Implement.DAL;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;

namespace ETMS.Components.ExOnlineTest.Implement.BLL
{
    public class Res_ItemCourse_OnLineTestLogic
    {
        Res_ItemCourse_OnLineTestDataAccess DAL = new Res_ItemCourse_OnLineTestDataAccess();


        #region 业务数据维护方法，比如：添加、修改、删除




        /// <summary>
        /// 增加
        /// </summary>
        public void AddResourceToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
        {
            tr_ItemCourseRes.CourseResTypeID = (Int32)Basic.API.Entity.EnumResourcesType.OnLineJob;//类型是在线作业
            DAL.AddResourceToItemCourse(tr_ItemCourseRes);
            BizLogHelper.AddOperate(tr_ItemCourseRes);
        }


        /// <summary>
        /// 批量添加课程资源(在线测试)到指定项目课程的资源中
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="courseResIDArray">要添加的在线测试ID数组（GUID）</param>
        public void BatchAdd(Guid trainingItemCourseID, Guid[] courseResIDArray, DateTime ResBeginTime, DateTime ResEndTime)
        {
            foreach (Guid courseResID in courseResIDArray)
            {
                Tr_ItemCourseRes entity = new Tr_ItemCourseRes();
                entity.ItemCourseResID = System.Guid.NewGuid();
                entity.TrainingItemCourseID = trainingItemCourseID;
                entity.CourseResTypeID = (Int32)Basic.API.Entity.EnumResourcesType.OnLineTest;//类型是在线测试
                entity.CourseResID = courseResID.ToString();
                entity.IsUse = 1;//默认为“启用”
                entity.CreateTime = System.DateTime.Now;
                entity.CreateUser = AppContext.UserContext.Current.RealName;
                entity.CreateUserID = AppContext.UserContext.Current.UserID;
                entity.ResBeginTime = ResBeginTime;
                entity.ResEndTime=ResEndTime;
                try
                {
                    //如果添加不成功，继续添加下一条
                    DAL.AddResourceToItemCourse(entity);
                }
                catch
                {
                }
            }

        }


        /// <summary>
        /// 从培训项目课程资源中，删除某个资源
        /// </summary>
        /// <param name="itemCourseResID">要删除的培训项目课程资源ID</param>
        public void RemoveResourceFromItemCourse(Guid itemCourseResID)
        {
            //先判断该资源是否被使用，如果被使用，则不能删除
            Tr_ItemCourseResLogic logic = new Tr_ItemCourseResLogic();
            Tr_ItemCourseRes entity = logic.GetById(itemCourseResID);
            if (DAL.CheckResourceIsUsed(new Guid(entity.CourseResID)))
            {
                throw new ETMS.AppContext.BusinessException("该培训项目课程资源已经被学习或使用，不能删除！");
            }

            DAL.RemoveResourceFromItemCourse(itemCourseResID);
        }


        /// <summary>
        /// 从培训项目课程资源中，批量删除其资源
        /// </summary>
        /// <param name="itemCourseResIDArray">要删除的培训项目课程资源ID数组</param>
        public void BatchRemoveResourceFromItemCourse(Guid[] itemCourseResIDArray)
        {
            int noDelNum = 0;
            foreach (Guid itemCourseResID in itemCourseResIDArray)
            {
                try
                {
                    RemoveResourceFromItemCourse(itemCourseResID);
                }
                catch
                {
                    noDelNum++;
                }
            }
            if (noDelNum > 0)
            {
                string errorMsg = "删除完毕：当前要删除的记录数为“{0}”个，有“{1}”个删除不成功，原因可能是这些培训项目课程资源已经被学习或使用！";
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsg,itemCourseResIDArray.Length,noDelNum));
            }
        }



        /// <summary>
        /// 保存
        /// </summary>
        public void SaveResourceToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
        {
            DAL.SaveResourceToItemCourse(tr_ItemCourseRes);
        }





        /// <summary>
        /// 批量删除学员的在线测试：主要是删除异常的在线测试
        /// </summary>
        /// <param name="ItemCourseResIDArray">要删除的学员的在线测试ID数组</param>
        public void BatchRemoveStudentOnlineTest(Guid[] studentOnlineTestIDArray)
        {
            int noSuccessNum = 0;
            string errorMsgALL = "";
            foreach (Guid studentOnlineTestID in studentOnlineTestIDArray)
            {
                try
                {
                    RemoveStudentOnlineTest(studentOnlineTestID);
                    //记录删除日志（根据ID删除）
                    BizLogHelper.Operate(studentOnlineTestID, "删除");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    noSuccessNum++;
                    string errorMsgOne = ex.Message;
                    if (errorMsgALL.IndexOf(errorMsgOne) < 0)
                        errorMsgALL += errorMsgOne + "\r\n";
                }

            }
            if (noSuccessNum > 0)
            {
                errorMsgALL = "删除完毕：当前要删除的学员的在线测试数为“{0}”个，有“{1}”个删除不成功，原因如下：\r\n" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsgALL, studentOnlineTestIDArray.Length, noSuccessNum));
            }

        }



        /// <summary>
        /// 删除学员的在线测试：主要是删除异常的在线测试
        /// </summary>
        /// <param name="studentOnlineTestID">要删除的学员的在线测试ID</param>
        public void RemoveStudentOnlineTest(Guid studentOnlineTestID)
        {
            try
            {
                DAL.RemoveStudentOnlineTest(studentOnlineTestID);
            }
            catch (Exception ex)
            {
                
                throw new ETMS.AppContext.BusinessException("删除在线测试不成功，原因是：\r\n" + ex.Message);
            }
        }


        /// <summary>
        /// 根据指定条件删除学员的在线测试：主要是删除异常的在线测试
        /// </summary>
        /// <param name="deleteSQLCondition">要删除的学员的在线测试的条件</param>
        public void RemoveStudentOnlineTestBySQLCondition(string deleteSQLCondition)
        {
            try
            {
                DAL.RemoveStudentOnlineTestBySQLCondition(deleteSQLCondition);
            }
            catch (Exception ex)
            {

                throw new ETMS.AppContext.BusinessException("删除在线测试不成功，原因是：\r\n" + ex.Message);
            }
        }



        #endregion

        #region 数据查询方法


        /// <summary>
        /// 根据培训项目课程ID获取其在线测试总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns>返回:在线测试总数</returns>
        public Int32 GetItemCourseOnLineTestTotal(Guid trainingItemCourseID)
        {
            return DAL.GetItemCourseOnLineTestTotal(trainingItemCourseID);
        }




        /// <summary>
        /// 获取某个培训项目课程未使用的在线测试资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            return DAL.GetTrainingItemNoSelectResourcesList(trainingItemCourseID, out totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目课程已使用的在线测试资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            return DAL.GetTrainingItemSelectResourcesList(trainingItemCourseID, out totalRecords);
        }


        /// <summary>
        /// 获取某个培训项目课程的某个在线测试资源信息
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="ItemCourseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemGetOneResources(Guid trainingItemCourseID, Guid ItemCourseResID)
        {
            return DAL.GetTrainingItemGetOneResources( trainingItemCourseID,  ItemCourseResID);
        }


        /// <summary>
        /// 获取所有学员的在线考试异常信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GetExceptionOnlineTestInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetExceptionOnlineTestInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }




        #endregion


    }
}
