using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using ETMS.Components.StudyClass.API;
using ETMS.Components.Basic.API.Entity.Security;
namespace ETMS.Components.StudyClass.Implement.BLL.StudyClass
{
    /// <summary>
    /// 班级表业务逻辑
    /// </summary>
    public partial class Sty_ClassLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Sty_Class sty_Class)
		{
            try
            {
			    if(sty_Class.ClassID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    sty_Class.ClassID=sty_Class.ClassID.NewID();;
                    Add(sty_Class);
                }
                else
                {
                    Update(sty_Class);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(BizErrorDefine.StudyClass_Sty_Class_CodeExists);
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid classID)
		{
            try
            {
			     DAL.Remove(classID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(classID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException(BizErrorDefine.StudyClass_Sty_Class_DataUsed);
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}

        /// <summary>
        /// 查询项目列表 - 根据参数获取对象列表（分页，可排序）
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetClassItemList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetClassItemList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 获取培训项目班级列表
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public List<Sty_Class> GetClassListByTrainingItemID(Guid trainingItemID)
        {
            return DAL.GetClassList(trainingItemID).ToList<Sty_Class>();
        }

        /// <summary>
        /// 获取我的培训项目班级列表
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public List<Sty_Class> GetMyTrainingItemClass(Guid trainingItemID)
        {
            return DAL.GetMyTrainingItemClass(trainingItemID, UserContext.Current.UserID).ToList<Sty_Class>();
        }

        /// <summary>
        /// 获取培训项目班级列表(分页）
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        public List<Sty_Class> GetClassListByTrainingItemID(Guid trainingItemID, int pageIndex, int pageSize, out int totalRecords)
        {
            var list = DAL.GetClassList(trainingItemID).ToList<Sty_Class>();
            totalRecords = list.Count;
            return list.PageList<Sty_Class>(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取可增加到班级的学员列表
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="classStudent"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Sty_ClassStudent> ChoseClassStudentList(Guid trainingItemID, Sty_ClassStudent classStudent, int pageIndex, int pageSize, out int totalRecords)
        {
            var list = DAL.ChoseClassStudentList(trainingItemID, classStudent).ToList<Sty_ClassStudent>();
            totalRecords = list.Count;
            return list.PageList<Sty_ClassStudent>(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取班级的学员列表(分页）
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="classStudent"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Sty_ClassStudent> GetClassStudentList(Guid classID, Sty_ClassStudent classStudent, int pageIndex, int pageSize, out int totalRecords)
        {
            var list = DAL.GetClassStudentList(classID, classStudent).ToList<Sty_ClassStudent>(out totalRecords);
            var result = list.PageList<Sty_ClassStudent>(pageIndex, pageSize);
            result.ForEach(e => e.ClassPostion = GetClassPositions(classID, e.UserID));
            return result;
        }

        /// <summary>
        /// 获取班级的学员列表
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="classStudent"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Sty_ClassStudent> GetClassStudentList(Guid classID)
        {
            return DAL.GetClassStudentList(classID, new Sty_ClassStudent() { RealName = string.Empty, WorkerNo = string.Empty, StudentTypeID = -1 }).ToList<Sty_ClassStudent>();
        }

        /// <summary>
        /// 获取学员班级职务
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetClassPositions(Guid classID, int userID)
        {
            var list = DAL.GetClassPositions(classID, userID).ToList<Sty_ClassMonitor>();
            string position = string.Empty;

            foreach (var entity in list)
            {
                position += "、" + ETMS.Components.Basic.Implement.BLL.Dictionary.SysDict.GetDictionaryItemInfoByID("Dic_Sys_StudentType", entity.StudentTypeID.ToString());
            }

            return position.Length > 0 ? position.Substring(1, position.Length - 1) : "学员";
        }

        /// <summary>
        /// 获取学员班级职务列表
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<Sty_ClassMonitor> GetStudentClassPositions(Guid classID, int userID)
        {
            return DAL.GetClassPositions(classID, userID).ToList<Sty_ClassMonitor>();
        }

        /// <summary>
        /// 前台班级列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Sty_ItemInfo> GetMyClassList(int pageIndex, int pageSize, out int totalRecords)
        {
            var list = DAL.GetMyClassList(UserContext.Current.UserID).ToList<Sty_ItemInfo>(out totalRecords);
            //list.ForEach(c => c.StyClass = GetMyTrainingItemClass(c.TrainingItemID));
            //list.ForEach(c => c.ClassNum = GetClassListByTrainingItemID(c.TrainingItemID).Count);

            foreach (var entity in list)
            {
                var classEntity = GetMyTrainingItemClass(entity.TrainingItemID).SingleOrDefault();
                if (null != classEntity)
                {
                    entity.ClassID = classEntity.ClassID;
                    entity.ClassName = classEntity.ClassName;
                }

                entity.ClassNum = GetClassListByTrainingItemID(entity.TrainingItemID).Count;
            }
            return list;
        }

        /// <summary>
        /// 获取班级的学员列表(前台)
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <param name="classStudent"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Sty_ClassStudent> GetClassStudentList(Guid classID,int pageIndex, int pageSize, out int totalRecords)
        {
             Sty_ClassStudent student = new Sty_ClassStudent(){ 
                 StudentTypeID = -1, 
                 RealName = string.Empty, 
                 WorkerNo = string.Empty 
             };
            var list = DAL.GetClassStudentList(classID, student).ToList<Sty_ClassStudent>(out totalRecords);
            var result = list.PageList<Sty_ClassStudent>(pageIndex, pageSize);
            result.ForEach(e => e.ClassPostion = GetClassPositions(classID, e.UserID));

            foreach (Sty_ClassStudent cs in result)
            {
                var group = new Sty_ClassSubgroupStudentLogic().GetGroupByClassStudentID(cs.ClassStudentID);
                if (null != group)
                {
                    cs.ClassSubgroupName = group.ClassSubgroupName;
                    cs.IsLeader = group.IsLeader;
                }
                else
                {
                    cs.ClassSubgroupName = string.Empty;
                    cs.IsLeader = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取项目学员所在机构列表
        /// </summary>
        /// <param name="traningItemID"></param>
        /// <returns></returns>
        public List<Organization> GetTrainingItemOrganizationList(Guid traningItemID)
        {
            var source = DAL.GetTrainingItemOrganizationList(traningItemID).ToList<Organization>();
            source.Insert(0, new Organization(){OrganizationID = -1, DisplayPath = "全部"});
            return source;
        }
	}	
}

