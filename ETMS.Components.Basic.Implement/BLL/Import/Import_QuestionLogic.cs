using System;
using System.Collections.Generic;
using ETMS.Components.Basic.API.Entity.Import;
using System.Data;
using ETMS.Utility.Data;
using ETMS.Components.Exam.API.Entity.ImportQuestion;
using ETMS.Components.Exam.API.Entity.ItemBank;

namespace ETMS.Components.Basic.Implement.BLL.Import
{
    public class Import_QuestionLogic : Import_AQuestion
    {
        public Import_QuestionLogic(QuestionType qsType, string ExcelName, string ExcelPath, Import_Task importTask, Guid QuestionBankID)
        {
            base.QSType = qsType;
            base.ExcelName = ExcelName;
            base.ExcelPath = ExcelPath;
            base.ImportTask = importTask;
            base.QuestionBankID = QuestionBankID;
        }

        ExcelDataAccess EDA = new ExcelDataAccess();

        public override bool GetImportData(out List<QuestionBasic> _qbList, out string errMsg)
        {
            bool temp = false;
            _qbList = new List<QuestionBasic>();
            errMsg = "";
            DataTable dt = new DataTable();
            try
            {
                CheckExcelFeild();

                if (Check)
                {
                    dt = EDA.ImportDataTable(ExcelPath, sheets[0]);
                    temp = Check;
                }

                DataTable dtNew = new DataTable();
                dtNew = dt.Copy();

                _qbList = ConvertToQuestionList(dtNew);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                temp = false;
            }

            return temp;
        }
    }
}
