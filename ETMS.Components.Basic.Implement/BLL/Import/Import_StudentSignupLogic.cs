//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-19 11:09:46.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Logging;

using ETMS.Components.Basic.API.Entity.Import;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

using System.Data.OleDb;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.BLL.Import
{
    /// <summary>
    /// 项目学员导入明细表业务逻辑
    /// </summary>
    public partial class Import_StudentSignupLogic
	{
 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Import_StudentSignup import_StudentSignup)
		{
            try
            {
			    if(import_StudentSignup.DetailID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    import_StudentSignup.DetailID=import_StudentSignup.DetailID.NewID();;
                    Add(import_StudentSignup);
                }
                else
                {
                    Update(import_StudentSignup);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Import_StudentSignupCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Import.Import_StudentSignup.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Import_StudentSignupName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Import.Import_StudentSignup.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            } 
		} 

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Int32 detailID)
		{
            try
            {
			     DAL.Remove(detailID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(detailID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Import.Import_StudentSignup.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}



        #region 项目学员导入


        ExcelDataAccess EDA = new ExcelDataAccess();
        Import_TaskLogic TaskLogic = new Import_TaskLogic();
        string SheetName = "";




        /// <summary>
        /// 验证Excel文件是否合法
        /// </summary>
        /// <param name="importTask"></param>
        /// <param name="xlsFile"></param>
        /// <returns></returns>
        private bool ValidateExcel(Import_Task importTask, string xlsFile)
        {
            //1、读取sheet，验证excel文件是否符合
            List<string> sheets = EDA.getSheetName(xlsFile);
            if (sheets.Count == 0)
            {
                importTask.Status = 1;
                importTask.Remark = "excel文件不符合！";
                TaskLogic.Save(importTask);
                return false;
            }
            this.SheetName = sheets[0];//默认取第一个sheet
            using (OleDbDataReader reader = EDA.ReadExcelData(xlsFile, SheetName))
            {
                DataTable dt = reader.GetSchemaTable();

                //学员账号，学员姓名，部门，岗位，职级
                if (
                    (dt.Select(" ColumnName='学员账号'").Length > 0)
                    && (dt.Select(" ColumnName='学员姓名'").Length > 0)
                    )
                {
                    return true;
                }
                else
                {
                    importTask.Status = 1;
                    importTask.Remark = "excel文件不合法，文件必须包括如下字段：学员账号，学员姓名";
                    TaskLogic.Save(importTask);
                    return false;
                }
            }
        }


        
        /// <summary>
        /// 验证Excel数据是否合法，病保存到中间表“项目学员导入明细表”
        /// 字段：学员账号，学员姓名，部门，岗位，职级
        /// </summary>
        /// <param name="importTask">导入任务实体</param>
        /// <param name="xlsFile"></param>
        /// <returns></returns>
        private bool ValidateData(Import_Task importTask, string xlsFile, Guid trainingItemID)
        {
            //取对应的培训项目信息
            Tr_ItemLogic logic = new Tr_ItemLogic();
            Tr_Item entityItem = logic.GetById(trainingItemID);
            //1、excel入临时表
            DataTable dt = EDA.ImportDataTable(xlsFile, this.SheetName);
            foreach (DataRow row in dt.Rows)
            {
                Import_StudentSignup studentInfo = new Import_StudentSignup()
                {
                    TaskID = importTask.TaskID,//外键关联
                    TrainingItemID = trainingItemID,
                    OrgID = entityItem.OrgID,
                    ItemCode = entityItem.ItemCode,
                    ItemName = entityItem.ItemName,
                    LoginName = row["学员账号"].Equals(DBNull.Value) ? string.Empty : Convert.ToString(row["学员账号"]).Trim(),
                    RealName = row["学员姓名"].Equals(DBNull.Value) ? string.Empty : Convert.ToString(row["学员姓名"]).Trim(),
                    //DepartmentName = row["部门"].Equals(DBNull.Value) ? string.Empty : Convert.ToString(row["部门"]).Trim(),
                    //PostName = row["岗位"].Equals(DBNull.Value) ? string.Empty : Convert.ToString(row["岗位"]).Trim(),
                    //RankName = row["职级"].Equals(DBNull.Value) ? string.Empty : Convert.ToString(row["职级"]).Trim(),
                };
                this.Save(studentInfo);
            }
            //2、核对临时表数据
            int errorCount = DAL.DoValid(importTask);
            if (errorCount > 0)
            {
                importTask.Status = 2;
                importTask.Remark = "共" + dt.Rows.Count.ToString() + "条，其中" + errorCount.ToString() + "条数据校验出错，请修改后再重新导入！";
                //更新
                TaskLogic.Save(importTask);
                return false;
            }
            return true;
        }



        /// <summary>
        /// 导入学员信息到培训项目下
        /// </summary>
        /// <param name="importTask">导入任务</param>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="xlsFile">学员信息excel文件</param>
        /// <param name="createUserID">导入人ID</param>
        /// <param name="createUser">导入人</param>
        /// <param name="ErrorNum">返回不能导入的学员数</param>
        /// <returns>成功或失败状态，如果失败，请从importTask中获取错误信息！</returns>
        public bool ImportStudentInfo(Import_Task importTask, string xlsFile,  Guid trainingItemID, int createUserID, string createUser, out int errorNum)
        {
            errorNum = 0;
            try
            {
                //1、验证excel是否符合标准
                if (!ValidateExcel(importTask, xlsFile))
                {
                    return false;
                }

                //2、验证业务数据（临时表）
                if (!ValidateData(importTask, xlsFile, trainingItemID))
                {
                    return false;
                }

                //3、临时表数据==》正式表（学员报名表）
                int dataCount = DAL.DoImport(importTask, trainingItemID, createUserID, createUser, out errorNum);

                if(errorNum > 0)
                    importTask.Status = 2;//有部分不成功
                else
                    importTask.Status = 3;//成功

                importTask.Remark = "共" + dataCount.ToString() + "学员信息导入成功";
                return true;
            }
            catch (Exception ex)
            {
                importTask.Status = 1;//导入失败
                importTask.Remark = ex.Message;
                return false;
            }
            finally
            {
                //更新任务状态
                TaskLogic.Save(importTask);
            }
        }



        #endregion






	}
	
	
}

