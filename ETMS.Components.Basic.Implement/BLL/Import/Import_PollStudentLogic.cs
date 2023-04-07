using System;
using System.Collections.Generic;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.Utility.Data;
using System.Data.OleDb;
using System.Data;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.Basic.Implement.DAL.Import;
using ETMS.Components.QS.API.Entity;

namespace ETMS.Components.Basic.Implement.BLL.Import
{
    public partial class Import_PollStudentLogic
    {

        ExcelDataAccess EDA = new ExcelDataAccess();
        Import_TaskLogic TaskLogic = new Import_TaskLogic();
        string SheetName = "";
        private Import_Task importTask;

        public Import_Task ImportTask
        {
            get { return importTask; }
            set { importTask = value; }
        }

        private string excelPath;

        public string ExcelPath
        {
            get { return excelPath; }
            set { excelPath = value; }
        }

        private int queryID;

        public int QueryID
        {
            get { return queryID; }
            set { queryID = value; }
        }

        private string creater;

        public string Creater
        {
            get { return creater; }
            set { creater = value; }
        }

        public int QueryPublishID { get; set; }

        /// <summary>
        ///校验excel
        /// </summary>
        /// <param name="ValidateExcel"></param>
        /// <returns></returns>
        private bool ValidateExcel(Func<string, bool> ValidateExcel)
        {
            bool temp = false;
            temp = ValidateExcel(excelPath);
            if (!temp)
            {
                importTask.Status = 1;
                importTask.Remark = "excel文件不符合！";
                TaskLogic.Save(importTask);
            }
            return temp;
        }
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="CheckColumnXCount"></param>
        /// <returns></returns>
        private bool CheckColumnXCount(Func<string, string, bool> CheckColumnXCount)
        {
            bool temp = false;
            temp = CheckColumnXCount(excelPath, this.SheetName);
            if (!temp)
            {
                importTask.Status = 1;
                importTask.Remark = "excel文件不合法，文件必须包括如下字段：学员账号，学员姓名";
                TaskLogic.Save(importTask);
            }
            return temp;
        }

        private bool CheckData()
        {
            Poll_QueryLogic Logic = new Poll_QueryLogic();
            ETMS.Components.Poll.API.Entity.Poll_Query pl = Logic.GetById(queryID);
            DataTable drs = EDA.ImportDataTable(excelPath, this.SheetName);
            foreach (DataRow p in drs.Rows)
            {
                Import_SurveyArea import_SurveyArea = new Import_SurveyArea()
                {
                    TaskID = importTask.TaskID,
                    //QueryID = queryID,
                    //QueryPublishID=this.QueryPublishID,
                    OrganizationID = pl.OrganizationID,
                    RealName = p["姓名"].ToString(),
                    DisplayPath = p["组织机构"].ToString(),
                    LoginName = p["用户名"].ToString(),
                    WorkNo = p["工号"].ToString(),
                    DepartmentName = p["部门"].ToString(),
                    PostName = p["岗位"].ToString(),
                    RankName = p["职级"].ToString(),
                    Email = p["邮箱"].ToString()
                };
                if (string.IsNullOrEmpty(import_SurveyArea.DetailID.ToString()))
                {
                    import_SurveyArea.DetailID = 0;
                }
                import_SurveyArea.DetailID = 0;
                //this.AddPollStudent(import_SurveyArea);
            }
            int errorCount = new Import_PollStudentDataAccess().DoValid(importTask);




            if (errorCount > 0)
            {
                importTask.Status = 2;
                importTask.Remark = "共" + drs.Rows.Count + "条，其中" + errorCount.ToString() + "条数据校验出错！";
                //更新
                TaskLogic.Save(importTask);
                return false;
            }
            return true;
        }

        public bool CheckOutImportData(int queryAreaID, out int errCount, out int succssCount)
        {
            errCount = 0;
            succssCount = 0;
            try
            {

                var p = ValidateExcel((string excelName) => { List<string> sheets = EDA.getSheetName(excelName); this.SheetName = sheets[0]; return sheets.Count > 0; });
                if (!p)
                {
                    return false;
                }
                p = CheckColumnXCount((string excelName, string sheetName) =>
                {
                    using (OleDbDataReader reader = EDA.ReadExcelData(excelName, SheetName))
                    {
                        var dt = reader.GetSchemaTable();
                        return ((dt.Select(" ColumnName='姓名'").Length > 0) && (dt.Select(" ColumnName='组织机构'").Length > 0) && (dt.Select(" ColumnName='用户名'").Length > 0));
                    }
                });
                if (!p)
                {
                    return false;
                }
                p = CheckData();
                succssCount = new Import_PollStudentDataAccess().ImportStudentSurveyArea(importTask.TaskID, QueryPublishID, creater, out errCount);
                if (p)
                {
                    importTask.Status = 3;//成功

                    importTask.Remark = "共" + succssCount.ToString() + "学员信息导入成功";
                }

                return p;
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

    }
}
