//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-10 9:15:05.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Teacher;
namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher
{
    /// <summary>
    /// 培训项目课程讲师表业务逻辑
    /// </summary>
    public partial class Tr_ItemCourseTeacherLogic
	{



        #region 业务数据维护方法，比如：添加、修改、删除




        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(Tr_ItemCourseTeacher tr_ItemCourseTeacher)
        {
            try
            {
                if (tr_ItemCourseTeacher.ItemCourseTeacherID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    tr_ItemCourseTeacher.ItemCourseTeacherID = tr_ItemCourseTeacher.ItemCourseTeacherID.NewID(); ;
                    Add(tr_ItemCourseTeacher);
                }
                else
                {
                    Update(tr_ItemCourseTeacher);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Tr_ItemCourseTeacherCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("TrainingItem.Course.Teacher.Tr_ItemCourseTeacher.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Tr_ItemCourseTeacherName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("TrainingItem.Course.Teacher.Tr_ItemCourseTeacher.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid itemCourseTeacherID)
        {
            try
            {
                DAL.Remove(itemCourseTeacherID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(itemCourseTeacherID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("TrainingItem.Course.Teacher.Tr_ItemCourseTeacher.DataUsed");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }  





        /// <summary>
        /// 批量添加讲师到指定培训项目课程讲师中
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="teacherIDArray">要添加的培训项目课程的讲师ID数组</param>
        public void BatchAdd(Guid trainingItemCourseID, int[] teacherIDArray)
        {
            foreach (int itemCourseTeacherID in teacherIDArray)
            {
                Tr_ItemCourseTeacher entity = new Tr_ItemCourseTeacher();
                entity.ItemCourseTeacherID = System.Guid.NewGuid();
                entity.TrainingItemCourseID = trainingItemCourseID;
                entity.TeacherID =itemCourseTeacherID;
                entity.IsUse = 1;//默认为“启用”
                entity.CreateTime = System.DateTime.Now;
                entity.CreateUser = AppContext.UserContext.Current.RealName;
                entity.CreateUserID = AppContext.UserContext.Current.UserID;
                try
                {
                    //如果添加不成功，继续添加下一条
                    DAL.Add(entity);
                }
                catch
                {
                }
            }

        }




        /// <summary>
        /// 从培训项目课程讲师，批量删除讲师
        /// </summary>
        /// <param name="itemCourseTeacherIDArray">要删除的培训项目课程讲师ID数组</param>
        public void BatchRemoveItemCourseTeacher(Guid[] itemCourseTeacherIDArray)
        {
            foreach (Guid itemCourseTeacherID in itemCourseTeacherIDArray)
            {
                try
                {
                    DAL.Remove(itemCourseTeacherID);
                }
                catch
                {
                }
            }
        }





        #endregion






        /// <summary>
        /// 获取某培训项目课程下的讲师数量
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public int GetTeacherTotal(Guid trainingItemCourseID)
        {
            return DAL.GetTeacherTotal( trainingItemCourseID);
        }





        /// <summary>
        /// 获取培训项目课程的讲师列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTeacherListByItemCourseID(Guid trainingItemCourseID, out int totalRecords)
        {
            return DAL.GetTeacherListByItemCourseID(trainingItemCourseID, out  totalRecords);
        }

        /// <summary>
        /// 获取培训项目课程的讲师列表(启用状态）
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        public List<Tr_ItemCourseTeacher> GetTeacherListByItemCourseID(Guid trainingItemCourseID)
        { 
            int totalRecords = 0;
            return GetTeacherListByItemCourseID(trainingItemCourseID, out totalRecords).ToList<Tr_ItemCourseTeacher>().Where(f => f.Status.Equals(1) & f.IsUse.Equals(1)).ToList();
        }

        /// <summary>
        /// 获取培训项目课程的未选择的讲师列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectTeacherListByItemCourseID(Guid trainingItemCourseID, out int totalRecords)
        {
            return DAL.GetNoSelectTeacherListByItemCourseID(trainingItemCourseID, out  totalRecords);
        }

        /// <summary>
        /// 获取培训项目课程的未选择的讲师列表（如果课程不是本机构的 本机构的讲师也会查出）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNoSelectTeacherListByItemCourseIDOrgID(Guid trainingItemCourseID, int orgID, out int totalRecords)
        {
            return DAL.GetNoSelectTeacherListByItemCourseIDOrgID(trainingItemCourseID, orgID, out  totalRecords);
        }

        /// <summary>
        /// 获取培训项目课程的已选择的讲师列表（如果课程不是本机构的 本机构的讲师也会查出）
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetSelectTeacherListByItemCourseIDOrgID(Guid trainingItemCourseID, int orgID, out int totalRecords)
        {
            return DAL.GetSelectTeacherListByItemCourseIDOrgID(trainingItemCourseID, orgID, out  totalRecords);
        }

        
        /// <summary>
        /// 跟据项目ID获得项目课程选择的讲师列表
        /// </summary>
        /// <param name="TrainingItemID">培训项目ID</param>
        /// <returns></returns>
        public DataTable GetItemCourseTeacherList(Guid TrainingItemID) {
            return DAL.GetItemCourseTeacherList(TrainingItemID);
        }
	}	
}

