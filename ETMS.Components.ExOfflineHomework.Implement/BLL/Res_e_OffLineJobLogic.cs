using System;
using System.Data;

using ETMS.Utility.Logging;
using ETMS.Components.ExOfflineHomework.API.Entity;


namespace ETMS.Components.ExOfflineHomework.Implement.BLL
{
    /// <summary>
    /// 离线作业：黄中福
    /// </summary>
    public partial class Res_e_OffLineJobLogic
    {
        /// <summary>
        /// 添加一个离线作业，并与指定的项目课程关联
        /// </summary>
        /// <param name="res_e_OffLineJob">离线作业实体</param>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int AddItemCourseOffLineJob(Res_e_OffLineJob res_e_OffLineJob, Guid trainingItemCourseID)
        {
            //增加离线作业
            Res_e_OffLineJobLogic jobLogic = new Res_e_OffLineJobLogic();
            jobLogic.Add(res_e_OffLineJob);
            BizLogHelper.AddOperate(res_e_OffLineJob);

            //初始化一个项目课程离线作业实体
            Res_ItemCourseOffLineJob itemCourseOffLineJobEntity = new Res_ItemCourseOffLineJob();
            itemCourseOffLineJobEntity.ItemCourseOffLineJobID = System.Guid.NewGuid();
            itemCourseOffLineJobEntity.JobID = res_e_OffLineJob.JobID;
            itemCourseOffLineJobEntity.TrainingItemCourseID = trainingItemCourseID;//项目课程ID
            itemCourseOffLineJobEntity.IsUse = res_e_OffLineJob.IsUse;
            itemCourseOffLineJobEntity.BeginTime = res_e_OffLineJob.BeginTime;
            itemCourseOffLineJobEntity.EndTime = res_e_OffLineJob.EndTime;
            itemCourseOffLineJobEntity.CreateTime = res_e_OffLineJob.CreateTime;

            //添加到项目课程离线作业表
            Res_ItemCourseOffLineJobLogic action = new Res_ItemCourseOffLineJobLogic();
            action.Add(itemCourseOffLineJobEntity);
            return 0;

        }

        /// <summary>
        /// 添加一个离线作业，并与指定的项目关联
        /// </summary>
        /// <param name="res_e_OffLineJob">离线作业实体</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public int AddItemOffLineJob(Res_e_OffLineJob res_e_OffLineJob, Guid trainingItemID)
        {
            //增加离线作业
            Res_e_OffLineJobLogic jobLogic = new Res_e_OffLineJobLogic();
            jobLogic.Add(res_e_OffLineJob);
            BizLogHelper.AddOperate(res_e_OffLineJob);

            //初始化一个项目课程离线作业实体
            Res_ItemCourseOffLineJob itemCourseOffLineJobEntity = new Res_ItemCourseOffLineJob();
            itemCourseOffLineJobEntity.ItemCourseOffLineJobID = System.Guid.NewGuid();
            itemCourseOffLineJobEntity.JobID = res_e_OffLineJob.JobID;   
            itemCourseOffLineJobEntity.TrainingItemID = trainingItemID;
            itemCourseOffLineJobEntity.IsUse = res_e_OffLineJob.IsUse;
            itemCourseOffLineJobEntity.BeginTime = res_e_OffLineJob.BeginTime;
            itemCourseOffLineJobEntity.EndTime = res_e_OffLineJob.EndTime;
            itemCourseOffLineJobEntity.CreateTime = res_e_OffLineJob.CreateTime;

            //添加到项目课程离线作业表
            Res_ItemCourseOffLineJobLogic action = new Res_ItemCourseOffLineJobLogic();
            action.Add(itemCourseOffLineJobEntity);
            return 0;

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void RemoveItemCourseOffLineJob(Guid jobID)
        {
            DAL.Remove(jobID);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void RemoveItemCourseOffLineJob(Res_e_OffLineJob res_e_OffLineJob, Guid trainingItemCourseID)
        {
            //查询“项目课程离线作业”
            string whereSQL = " AND JobID='" + res_e_OffLineJob.JobID.ToString() + "' AND TrainingItemCourseID='" + trainingItemCourseID.ToString() + "'";
            int reccount = -1;
            Res_ItemCourseOffLineJobLogic action = new Res_ItemCourseOffLineJobLogic();
            DataTable dt = action.GetPagedList(1, 1, "", whereSQL, out reccount);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    try
                    {
                        string itemCourseOffLineJobID = dt.Rows[0]["ItemCourseOffLineJobID"].ToString();
                        //删除“项目课程离线作业”
                        action.Remove(new Guid(itemCourseOffLineJobID));
                        //删除离线作业
                        Res_e_OffLineJobLogic jobLogic = new Res_e_OffLineJobLogic();
                        jobLogic.Remove(res_e_OffLineJob);
                        BizLogHelper.DeleteOperate(res_e_OffLineJob);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string errorMsg = ex.Message.ToUpper();
                        if (errorMsg.IndexOf("FK_STY_STUD_REFERENCE_RES_ITEM", StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            throw new ETMS.AppContext.BusinessException("该离线作业已经有学生提交作业，不能删除！");
                        }
                        throw ex;
                    }

                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void RemoveItemOffLineJob(Res_e_OffLineJob res_e_OffLineJob, Guid trainingItemID)
        {
            //查询“项目课程离线作业”
            string whereSQL = " AND JobID='" + res_e_OffLineJob.JobID.ToString() + "' AND TrainingItemID='" + trainingItemID.ToString() + "'";
            int reccount = -1;
            Res_ItemCourseOffLineJobLogic action = new Res_ItemCourseOffLineJobLogic();
            DataTable dt = action.GetPagedList(1, 1, "", whereSQL, out reccount);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    try
                    {
                        string itemCourseOffLineJobID = dt.Rows[0]["ItemCourseOffLineJobID"].ToString();
                        //删除“项目课程离线作业”
                        action.Remove(new Guid(itemCourseOffLineJobID));
                        //删除离线作业
                        Res_e_OffLineJobLogic jobLogic = new Res_e_OffLineJobLogic();
                        jobLogic.Remove(res_e_OffLineJob);
                        BizLogHelper.DeleteOperate(res_e_OffLineJob);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string errorMsg = ex.Message.ToUpper();
                        if (errorMsg.IndexOf("FK_STY_STUD_REFERENCE_RES_ITEM", StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            throw new ETMS.AppContext.BusinessException("该离线作业已经有学生提交作业，不能删除！");
                        }
                        throw ex;
                    }

                }
            }
        }

        public  DataTable GetUserOfflineJobs(int userID, Guid trainingItemCourseID)
        {
            return DAL.GetUserOffLineJobs(userID, trainingItemCourseID);
        }

        public DataTable GetUserCourseOffLineJobs(int userID, Guid trainingItemCourseID)
        {
            return DAL.GetUserCourseOffLineJobs(userID, trainingItemCourseID);
        }

        /// <summary>
        /// 根据离线作业编号获取未批阅离线作业
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <returns></returns>
        public DataTable GetUnEvaluationStudentListbyItemCourseOffLineJobID(Guid itemCourseOffLineJobID)
        {
            return GetStudentListbyItemCourseOffLineJobID(itemCourseOffLineJobID, 0);
        }
        /// <summary>
        /// 根据离线作业编号获取未批阅离线作业
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <returns></returns>
        public DataTable GetUnEvaluationStuList(string JobName, string ItemName)
        {
            return GetStuList(JobName,ItemName,0);
        }
        /// <summary>
        /// 根据离线作业编号获取已批阅离线作业
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <returns></returns>
        public DataTable GetEvaluationStudentListbyItemCourseOffLineJobID(Guid itemCourseOffLineJobID)
        {
            return GetStudentListbyItemCourseOffLineJobID(itemCourseOffLineJobID, 1);
        }
        /// <summary>
        /// 根据离线作业编号获取已批阅离线作业
        /// </summary>
        /// <returns></returns>
        public DataTable GetEvaluationStuList(string JobName, string ItemName)
        {
            return GetStuList(JobName,ItemName,1);
        }
        /// <summary>
        /// 根据离线作业编号获取未提交离线作业
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <returns></returns>
        public DataTable GetUnSubmitStudentListbyItemCourseOffLineJobID(Guid itemCourseOffLineJobID)
        {
            return GetStudentListbyItemCourseOffLineJobID(itemCourseOffLineJobID, -1);
        }
        /// <summary>
        /// 根据离线作业编号获取未提交离线作业
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoSumbitStuList()
        {
            return DAL.GetNoSumbitStuList();
        }

        /// <summary>
        /// 根据离线作业编号和批阅状态，获取位提交作业的学员或者离线作业作答记录
        /// </summary>
        /// <param name="itemCourseOffLineJobID">课程离线作业ID</param>
        /// <param name="studentJobStatus">作业批阅状态0：未批阅 1：已批阅，小于0代表没有提交:</param>
        /// <returns></returns>
        public DataTable GetStudentListbyItemCourseOffLineJobID(Guid itemCourseOffLineJobID, int studentJobStatus)
        {
            return DAL.GetStudentListbyItemCourseOffLineJobID(itemCourseOffLineJobID, studentJobStatus);
        }
        /// <summary>
        /// 根据离线作业编号和批阅状态，获取位提交作业的学员或者离线作业作答记录
        /// </summary>   
        /// <param name="studentJobStatus">作业批阅状态0：未批阅 1：已批阅，小于0代表没有提交:</param>
        /// <returns></returns>
        public DataTable GetStuList(string JobName, string ItemName, int studentJobStatus)
        {
            return DAL.GetStuList(JobName,ItemName,studentJobStatus);
        }
        /// <summary>
        /// 批阅离线作业
        /// </summary>
        /// <param name="studentJobID">学员离线作业作答记录编号</param>
        /// <param name="markFilePath">离线作业存放路径</param>
        /// <param name="markFileName">离线作业名称</param>
        /// <param name="evaluationUser">批阅人员</param>
        /// <param name="evaluation">评语</param>
        public void SetEvaluationOffLineJob(Guid studentJobID, string markFilePath, string markFileName, string evaluationUser, string evaluation)
        {
            SetEvaluationOffLineJob(studentJobID, markFilePath, markFileName, evaluationUser, evaluation, 0);
        }

         /// <summary>
        /// 批阅离线作业
        /// </summary>
        /// <param name="studentJobID">学员离线作业作答记录编号</param>
        /// <param name="markFilePath">离线作业存放路径</param>
        /// <param name="markFileName">离线作业名称</param>
        /// <param name="evaluationUser">批阅人员</param>
        /// <param name="evaluation">评语</param>
        /// <param name="score">分数</param>
        public void SetEvaluationOffLineJob(Guid studentJobID, string markFilePath, string markFileName, string evaluationUser, string evaluation, decimal? score)
        {
            DAL.SetEvaluationOffLineJob(studentJobID, markFilePath, markFileName, evaluationUser, evaluation, score);
        }


        /// <summary>
        /// 获取某个培训项目课程的可用离线作业总数（状态为“启用”）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public Int32 GetOfflineJobTotalByTrainingItemCourseID(Guid trainingItemCourseID)
        {
            return DAL.GetOfflineJobTotalByTrainingItemCourseID(trainingItemCourseID);
        }




    }
}
