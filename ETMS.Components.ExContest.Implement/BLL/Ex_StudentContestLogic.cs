//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-5-19 9:14:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.ExContest.API.Entity.StudentContest;
namespace ETMS.Components.ExContest.Implement.BLL.StudentContest
{
    /// <summary>
    /// 学生闯关竞赛结果表业务逻辑
    /// </summary>
    public partial class Ex_StudentContestLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Ex_StudentContest ex_StudentContest)
		{
            try
            {
			    if(ex_StudentContest.StudentContestID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    ex_StudentContest.StudentContestID=ex_StudentContest.StudentContestID.NewID();;
                    Add(ex_StudentContest);
                }
                else
                {
                    Update(ex_StudentContest);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Ex_StudentContestCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudentContest.Ex_StudentContest.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Ex_StudentContestName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudentContest.Ex_StudentContest.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid studentContestID)
		{
            try
            {
			     DAL.Remove(studentContestID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(studentContestID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudentContest.Ex_StudentContest.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}

        /// <summary>
        /// 获得UserExamID
        /// </summary>
        /// <param name="OnlineTestID"></param>
        /// <returns></returns>
        public Guid getUserExamID(Guid contestID)
        {
            Ex_StudentContest ex_StudentContest = GetByStudentContest(contestID, ETMS.AppContext.UserContext.Current.UserID);

            if (ex_StudentContest == null)
            {
                return Guid.Empty;
            }
            else
            {
                return ex_StudentContest.UserExamID;
            }
        }

        /// <summary>
        /// 是否闯关通过
        /// </summary>
        /// <param name="contestID"></param>
        /// <returns></returns>
        public string GetScore(Guid contestID)
        {
            string returnValue = "";
            Ex_StudentContest ex_StudentContest = GetByStudentContest(contestID, ETMS.AppContext.UserContext.Current.UserID);

            if (ex_StudentContest == null)
            {
                returnValue= "未参加";
            }
            else
            {
                if (ex_StudentContest.Score == 100)
                {
                    returnValue = "已通过";
                }
                else
                {
                    returnValue = "未通过";
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Ex_StudentContest GetByStudentContest(Guid contestID, int userID)
        {
            Ex_StudentContest ex_StudentContest = DAL.GetByStudentContest(contestID, userID);

            return ex_StudentContest;
        }

	}
	
	
}

