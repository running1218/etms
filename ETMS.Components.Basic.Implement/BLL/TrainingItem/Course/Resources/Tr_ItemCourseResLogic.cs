using System;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility.Logging;

using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;

namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources
{


    /// <summary>
    /// 培训项目课程资源扩展类
    /// 黄中福
    /// </summary>
    public partial class Tr_ItemCourseResLogic
    {
        /// <summary>
        /// 培训项目课程资源保存
        /// </summary>
        /// <param name="entity">培训项目课程资源实体</param>
        /// <param name="action">操作方法：添加或者删除</param>
        public void Save(Tr_ItemCourseRes entity, OperationAction action)
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
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }



        /// <summary>
        /// 获取所有培训项目课程资源信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GeItemCourseResAllInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GeItemCourseResAllInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 获取所有培训项目课程未添加的资源信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GeItemCourseResNoSelectInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GeItemCourseResNoSelectInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        
        
        /// <summary>
        /// 获取所有培训项目课程已经启用的资源信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GeItemCourseResIsUseInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND icr.IsUse=1 ";
            return GeItemCourseResAllInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }



        /// <summary>
        /// 获取所有培训项目课程已经停用的资源信息
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GeItemCourseResNoUseInfo(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            criteria += " AND icr.IsUse=0 ";
            return GeItemCourseResAllInfo(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 批量设置项目课程资源的使用停用状态
        /// </summary>
        /// <param name="itemCourseResIDArray">项目课程资源ID数组</param>
        /// <param name="isUse">是否启用：0=停用，1=启用</param>
        public void BatchSetResUseStatus(Guid[] itemCourseResIDArray ,int isUse)
        {
            foreach (Guid itemCourseResID in itemCourseResIDArray)
            {
                Tr_ItemCourseRes entity = DAL.GetById(itemCourseResID);
                entity.IsUse = isUse;
                DAL.Save(entity);
            }

        }


        /// <summary>
        /// 批量启用项目课程资源
        /// </summary>
        /// <param name="ItemCourseResIDArray">要启用项目课程资源ID数组</param>
        public void BatchSetResIsUse(Guid[] itemCourseResIDArray)
        {
            BatchSetResUseStatus(itemCourseResIDArray, 1);
        }


        /// <summary>
        /// 批量停用项目课程资源
        /// </summary>
        /// <param name="ItemCourseResIDArray">要停用项目课程资源ID数组</param>
        public void BatchSetResIsNoUse(Guid[] itemCourseResIDArray)
        {
            BatchSetResUseStatus(itemCourseResIDArray, 0);
        }



        /// <summary>
        /// 批量删除项目课程资源
        /// </summary>
        /// <param name="ItemCourseResIDArray">要删除的项目课程资源ID数组</param>
        public void BatchRemove(Guid[] itemCourseResIDArray)
        {
            int noSuccessNum = 0;
            string errorMsgALL = "";
            foreach (Guid itemCourseResID in itemCourseResIDArray)
            {
                try
                {
                    DAL.Remove(itemCourseResID);
                    //记录删除日志（根据ID删除）
                    BizLogHelper.Operate(itemCourseResID, "删除");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    noSuccessNum++;
                    string errorMsgOne = "";
                    string errorMsg = ex.Message.ToUpper();
                    //枚举数据约束异常并转换为业务异常，数据已经使用 
                    //原外键错误 FK_TR_ITEMC_REFERENCE_TR_ITEMC5  FK_SCO_CMI__REFERENCE_TR_ITEMC
                    if (errorMsg.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        errorMsgOne = "该项目课程资源已经有学员学习！”，不能删除！";
                    }
                    else
                    {
                        throw ex;
                    }
                    if (errorMsgALL.IndexOf(errorMsgOne) < 0)
                        errorMsgALL += errorMsgOne + "\r\n";
                }

            }
            if (noSuccessNum > 0)
            {
                errorMsgALL = "删除完毕：当前要删除的项目课程资源数为“{0}”个，有“{1}”个删除不成功，原因如下：\r\n" + errorMsgALL;
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsgALL, itemCourseResIDArray.Length, noSuccessNum));
            }

        }



        /// <summary>
        /// 批量设置项目课程资源的学习时间范围
        /// </summary>
        /// <param name="ItemCourseResIDArray">要设置学习时间的项目课程资源ID数组</param>
        /// <param name="beginTime">开始学习时间</param>
        /// <param name="endTime">结束学习时间</param>
        public void BatchSetResStudyTime(Guid[] itemCourseResIDArray, DateTime beginTime, DateTime endTime)
        {
            foreach (Guid itemCourseResID in itemCourseResIDArray)
            {
                Tr_ItemCourseRes entity = DAL.GetById(itemCourseResID);
                entity.ResBeginTime = beginTime;
                entity.ResEndTime = endTime;
                DAL.Save(entity);
            }
        }



        /// <summary>
        ///  根据指定的条件，设置项目课程资源的学习时间为培训项目课程的学习时间
        /// </summary>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        public void SetItemCourseResStudyTimeToItemCourse(string criteria)
        {
            DAL.SetItemCourseResStudyTimeToItemCourse(criteria);
        }


        /// <summary>
        /// 批量设置项目课程资源的学习时间为培训项目课程的学习时间
        /// </summary>
        /// <param name="ItemCourseResIDArray">要设置学习时间的项目课程资源ID数组</param>
        public void BatchSetResStudyTimeToItemCourse(Guid[] itemCourseResIDArray)
        {
            string itemCourseResALL = "";
            int count = 0;
            foreach (Guid itemCourseResID in itemCourseResIDArray)
            {
                itemCourseResALL +=  string.Format("'{0}'", itemCourseResID.ToString());
                count++;
                if (count < itemCourseResIDArray.Length)
                    itemCourseResALL += ",";
            }
            if(itemCourseResALL == "")
                return;
            string sqlCondition = string.Format(" and icr.ItemCourseResID in ({0}) ",itemCourseResALL);
            SetItemCourseResStudyTimeToItemCourse(sqlCondition);

        }




        /// <summary>
        /// 获取所有学生的培训项目资源的学习进度（主要是针对有在线课件的）
        /// “用户ID，培训项目课程ID”唯一
        /// </summary>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortExpression">排序方式</param>
        /// <param name="criteria">与 AND 开头的查询条件</param>
        /// <param name="totalRecords">返回总的满足条件的记录数</param>
        public DataTable GetStudentCoursewareStudyProgressAllInfoList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetStudentCoursewareStudyProgressAllInfoList(pageIndex, pageSize, sortExpression, criteria, out  totalRecords);
        }


        /// <summary>
        /// 从DataTable计算进度,返回:学习完成的完成章节数 * 100/总的完成章节数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Double ComputeStudyProgressFromDataTable(DataTable dtStudyProgressList)
        {
            int CompleteChapterNumber = 0, ChapterNumber = 0;
            foreach (DataRow row in dtStudyProgressList.Rows)
            {
                try
                {
                    CompleteChapterNumber += int.Parse(row["CompleteChapterNumber"].ToString());
                    ChapterNumber += int.Parse(row["ChapterNumber"].ToString());
                }
                catch
                {
                }
            }
            Double studyProgress = 0.00;
            if (ChapterNumber > 0)
            {
                studyProgress = CompleteChapterNumber * 100.00 / ChapterNumber;
                studyProgress = Math.Round(studyProgress, 2);
            }
            return studyProgress;
        }

        /// <summary>
        /// 获取所有学生的培训项目资源的学习进度（主要是针对有在线课件的）
        /// “用户ID，培训项目课程ID”唯一
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        public Double GetStudentCoursewareStudyProgressByUserID_TrainingItemCourseID(int userID, Guid trainingItemCourseID)
        {
            DataTable dt = GetStudentCoursewareStudyProgressListByUserID_TrainingItemCourseID( userID,  trainingItemCourseID);
            return ComputeStudyProgressFromDataTable(dt);
        }

        /// <summary>
        /// 获取所有学生的培训项目资源的学习进度（主要是针对有在线课件的）
        /// “用户ID，培训项目课程ID”唯一
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        public DataTable GetStudentCoursewareStudyProgressListByUserID_TrainingItemCourseID(int userID, Guid trainingItemCourseID)
        {
            int totalRecords = -1;
            string criteria = string.Format(" AND ss.UserID='{0}' AND icr.TrainingItemCourseID='{1}'",userID,trainingItemCourseID);
            DataTable dt = GetStudentCoursewareStudyProgressAllInfoList(1, int.MaxValue - 10000, "", criteria, out totalRecords);
            return dt;
        }






    }
}
