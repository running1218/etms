using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.Import;
using System.Data.OleDb;
using System.Data;
using ETMS.Components.Exam.API.Entity.ImportQuestion;
using ETMS.Components.Exam.API.Entity.ItemBank;
using System.Collections;

namespace ETMS.Components.Basic.Implement.BLL.Import
{
    /// <summary>
    /// 判断题答案
    /// </summary>
    public enum PdAnswer : byte
    {
        是 = 1, 否 = 0, Y = 1, N = 0, y = 1, n = 0
    }

    public enum Difficulty : short
    {
        易 = 1, 中 = 2, 难 = 3
    }

    public abstract class Import_AQuestion
    {
        public QuestionType QSType
        {
            get;
            set;
        }

        public string ExcelName
        {
            get;
            set;
        }

        public string ExcelPath
        {
            get;
            set;
        }

        public Import_Task ImportTask
        {
            get;
            set;
        }

        public Guid QuestionBankID
        {
            get;
            set;
        }
        private ArrayList colSingle;
        public bool Check = true;
        public List<string> sheets;
        ExcelDataAccess EDA = new ExcelDataAccess();
        Import_TaskLogic TaskLogic = new Import_TaskLogic();
        private void CheckExcelType()
        {
            sheets = EDA.getSheetName(ExcelPath);
            if (sheets.Count == 0)
            {
                ImportTask.Status = 1;
                ImportTask.Remark = "excel文件不符合！";
                TaskLogic.Save(ImportTask);
                Check = false;
            }
        }

        public bool CheckExcelFeild()
        {
            CheckExcelType();

            if (Check)
            {
                string sheetName = sheets[0];
                using (OleDbDataReader reader = EDA.ReadExcelData(ExcelPath, sheetName))
                {
                    var dt = reader.GetSchemaTable();
                    if (!validateSingle(dt))
                    {
                        throw new Exception("请您使用模版的列导入！");
                    }
                    switch (QSType)
                    {
                        case QuestionType.SingleChoice:
                            {
                                if (!(dt.Select(" ColumnName='题目'").Length > 0) && (dt.Select(" ColumnName='答案'").Length > 0) && (dt.Select(" ColumnName='选项'").Length > 0) && (dt.Select(" ColumnName='选项内容'").Length > 0))
                                {
                                    ImportTask.Status = 1;
                                    ImportTask.Remark = "excel文件不合法，文件必须包括如下字段：题目,答案,选项,选项内容";
                                    TaskLogic.Save(ImportTask);
                                    Check = false;
                                }
                                break;
                            }
                        case QuestionType.MultipleChoice:
                            {
                                if (!(dt.Select(" ColumnName='题目'").Length > 0) && (dt.Select(" ColumnName='答案'").Length > 0) && (dt.Select(" ColumnName='选项'").Length > 0) && (dt.Select(" ColumnName='选项内容'").Length > 0))
                                {
                                    ImportTask.Status = 1;
                                    ImportTask.Remark = "excel文件不合法，文件必须包括如下字段：题目,答案,选项,选项内容";
                                    TaskLogic.Save(ImportTask);
                                    Check = false;
                                }
                                break;
                            }
                        case QuestionType.Judgement:
                            {
                                if (!(dt.Select(" ColumnName='题目'").Length > 0) && (dt.Select(" ColumnName='答案'").Length > 0))
                                {
                                    ImportTask.Status = 1;
                                    ImportTask.Remark = "excel文件不合法，文件必须包括如下字段：题目,答案";
                                    TaskLogic.Save(ImportTask);
                                    Check = false;
                                }
                                break;
                            }
                        default:
                            if (!(dt.Select(" ColumnName='题目'").Length > 0))
                            {
                                ImportTask.Status = 1;
                                ImportTask.Remark = "excel文件不合法，文件必须包括如下字段：题目";
                                TaskLogic.Save(ImportTask);
                                Check = false;
                            }
                            break;
                    }
                }
            }
            return Check;
        }

        public List<QuestionBasic> ConvertToQuestionList(DataTable dtNew)
        {
            List<QuestionBasic> qbList = new List<QuestionBasic>();
            ArrayList ar = new ArrayList();
            string questionName = "";
            foreach (DataRow dr in dtNew.Rows)
            {
                if (!string.IsNullOrEmpty(dr["题目"].ToString()))
                {
                    questionName = dr["题目"].ToString();
                    if (ar.Contains(questionName))
                        throw new Exception("试题不能包含重复的题目！");
                    else
                        ar.Add(dr["题目"].ToString());
                    if (string.IsNullOrWhiteSpace(dr["难易度"].ToString()))
                        throw new Exception("试题难易度不能为空！");
                    else
                    {
                        if (!Enum.IsDefined(typeof(Difficulty), dr["难易度"]))
                        {
                            throw new Exception("试题的难易度填写错误！");
                        }
                    }
                }
                else
                {
                    dr["题目"] = questionName;
                }
                dtNew.AcceptChanges();
            }
            var listID = from p in dtNew.AsEnumerable() group p by p.Field<object>("题目") into g select new { name = g.Key };
            foreach (var q in listID)
            {
                string msg = "";
                QuestionBasic qb = new QuestionBasic();
                try
                {
                    DataRow[] drs = dtNew.Select("题目='" + q.name.ToString() + "'", "题号 desc");
                    qb.QuestionID = drs[0]["题号"].ToString();
                    qb.QuestionTitle = q.name.ToString().Trim();
                    qb.Difficult = getQSDifficult(drs[0]["难易度"].ToString());
                    List<QuestionExpansion> qeLS = new List<QuestionExpansion>();
                    foreach (DataRow dr in drs)
                    {
                        QuestionExpansion qe = new QuestionExpansion();
                        if (QSType == QuestionType.SingleChoice || QSType == QuestionType.MultipleChoice)
                        {
                            if (dr["答案"].ToString() == "√")
                                qe.Answer = true;
                            else if (string.IsNullOrEmpty(dr["答案"].ToString()))
                                qe.Answer = false;
                            else
                            {
                                msg = "试题答案填写不正确(应该为√)！";
                            }
                            qe.AnswerContent = dr["答案"].ToString();
                            qe.Option = dr["选项"].ToString();
                            qe.OptionContent = dr["选项内容"].ToString();
                            if (qe.OptionContent.Length > 500)
                                msg = "选项内容长度不能超过500个中文！";
                            if (string.IsNullOrWhiteSpace(qe.OptionContent))
                                msg = "选项内容不能空！";
                        }
                        else
                        {
                            qe.AnswerContent = dr["答案"].ToString();
                            if (!string.IsNullOrWhiteSpace(dr["答案"].ToString()))
                            {
                                if (Enum.IsDefined(typeof(PdAnswer), dr["答案"]))
                                    qe.Answer = Convert.ToBoolean(Enum.Parse(typeof(PdAnswer), dr["答案"].ToString()));
                                else
                                {
                                    if (string.IsNullOrEmpty(msg))
                                        msg = "试题答案填写不正确(应该为是,否,Y,N,y,n)！";
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(msg))
                                    msg = "试题答案填写不正确(应该为是,否,Y,N,y,n)！";
                            }
                        }
                        qeLS.Add(qe);
                    }
                    if (QSType == QuestionType.SingleChoice || QSType == QuestionType.MultipleChoice)
                    {
                        var x = from n in qeLS select new { Answer = n.Answer, OptionContent = n.OptionContent };
                        if (x.Count() < 2)
                        {
                            msg = "试题的选项至少为2个！";
                        }

                        var y = from s in x
                                group s by s.Answer into g
                                where g.Key == true
                                select new { aName = g.Key, aCount = g.Count() };
                        if (y.Count() == 0)
                            msg = "请您填写答案选项！";
                        if (QSType == QuestionType.SingleChoice)
                        {
                            foreach (var o in y)
                            {
                                if (o.aCount > 1)
                                {
                                    msg = "请您填写正确的试题答案选项！";
                                }
                            }
                        }

                        var z = from t in x group t by t.OptionContent into g select new { aName = g.Key, aCount = g.Count() };
                        foreach (var c in z)
                        {
                            if (c.aCount > 1)
                            {
                                msg = "选项内容不唯一";
                                break;
                            }
                        }
                    }
                    qb.Qreplenish = qeLS;
                    qb.Msg = msg;
                    if (!string.IsNullOrEmpty(msg))
                    {
                        qb.State = "失败";
                    }
                }
                catch (Exception ex)
                {
                    qb.State = "失败";
                    qb.Msg = ex.Message;
                }
                finally
                {
                    qbList.Add(qb);
                }
            }
            return qbList;
        }

        public bool GetValidateQuestion(List<QuestionBasic> qbList, out string errMsg)
        {
            errMsg = "";
            var x = from p in qbList group p by p.QuestionTitle into g select new { name = g.Key };
            if (x.Count() < qbList.Count)
            {
                errMsg = "试题不能包含重复的题目！";
                return false;
            }
            return true;
        }

        public abstract bool GetImportData(out List<QuestionBasic> _qbList, out string errMsg);

        private short getQSDifficult(string difficult)
        {
            short i = 0;
            switch (difficult)
            {
                case "易":
                    i = 1;
                    break;
                case "难":
                    i = 3;
                    break;
                default:
                    i = 2;
                    break;
            }
            return i;
        }

        private bool validateSingle(DataTable dt)
        {
            bool temp = true;
            colSingle = new ArrayList();
            if (QSType == QuestionType.Judgement)
                addColJ();
            else
                addCol();
            foreach (var a in colSingle)
            {
                if (dt.Select("ColumnName='" + a + "'").Count() == 0)
                {
                    temp = false;
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                if (!colSingle.Contains(dr["ColumnName"].ToString()))
                {
                    temp = false;
                }
            }
            return temp;
        }

        private void addCol()
        {
            colSingle.Add("题号");
            colSingle.Add("题目");
            colSingle.Add("难易度");
            colSingle.Add("答案");
            colSingle.Add("选项");
            colSingle.Add("选项内容");
        }

        private void addColJ()
        {
            colSingle.Add("题号");
            colSingle.Add("题目");
            colSingle.Add("难易度");
            colSingle.Add("答案");
        }
    }
}
