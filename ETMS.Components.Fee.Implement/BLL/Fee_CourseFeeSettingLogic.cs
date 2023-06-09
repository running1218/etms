//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-20 21:06:45.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Fee.API.Entity;
namespace ETMS.Components.Fee.Implement.BLL
{
    /// <summary>
    /// 课酬标准表业务逻辑
    /// </summary>
    public partial class Fee_CourseFeeSettingLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Fee_CourseFeeSetting fee_CourseFeeSetting)
		{
            try
            {
			    if(fee_CourseFeeSetting.CourseFeeSettingID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    fee_CourseFeeSetting.CourseFeeSettingID=fee_CourseFeeSetting.CourseFeeSettingID.NewID();;
                    Add(fee_CourseFeeSetting);
                }
                else
                {
                    Update(fee_CourseFeeSetting);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Fee_CourseFeeSettingCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("CourseFeeSetting.Fee_CourseFeeSetting.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Fee_CourseFeeSettingName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("CourseFeeSetting.Fee_CourseFeeSetting.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid courseFeeSettingID)
		{
            try
            {
			     DAL.Remove(courseFeeSettingID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(courseFeeSettingID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("CourseFeeSetting.Fee_CourseFeeSetting.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}

        /// <summary>
        /// 获取课酬标准
        /// </summary>
        /// <param name="trainingTimeDescID"></param>
        /// <param name="teacherLevelID"></param>
        /// <returns></returns>
        public string getTrainingTimeDesc(int trainingTimeDescID, int teacherLevelID)
        {
            int totalRecords = 0;
            string Criteria = string.Format(" and TrainingTimeDescID={0} and TeacherLevelID={1} And OrgID={2} ", trainingTimeDescID, teacherLevelID,ETMS.AppContext.UserContext.Current.OrganizationID);
            DataTable dt = GetPagedList(1, 1, "",Criteria, out totalRecords);
            if (dt.Rows.Count>0)
            {
                return dt.Rows[0]["CourseFee"].ToString();
            }
            
            return "";
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void AddByOrgID()
        {
            try
            {
                DAL.Add(ETMS.AppContext.UserContext.Current.OrganizationID, ETMS.AppContext.UserContext.Current.RealName);
                //记录日志
                
            }
            catch (System.Data.SqlClient.SqlException ex)
            {  
                //如果仍未处理，则抛出
                throw ex;
            }
        }
	}
	
	
}

