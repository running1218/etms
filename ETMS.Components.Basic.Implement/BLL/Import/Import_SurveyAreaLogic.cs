//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013/2/1 9:27:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.Utility.Data;
using System.Data.OleDb;
namespace ETMS.Components.Basic.Implement.BLL.Import
{
    /// <summary>
    /// ҵ���߼�
    /// </summary>
    public partial class Import_SurveyAreaLogic
    {

        /// <summary>
        /// �������
        /// </summary>
        public void Save(Import_SurveyArea import_SurveyArea)
        {
            try
            {
                if (import_SurveyArea.DetailID == 0)
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������;
                    Add(import_SurveyArea);
                }
                else
                {
                    Update(import_SurveyArea);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("Index_U_Import_SurveyAreaCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Import_SurveyArea.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Import_SurveyAreaName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Import_SurveyArea.NameExists");
                }
                //�����δ���������׳�
                throw ex;
            }
        }


        public DataSet GetPollImportStudentList(int taskID)
        {
            return DAL.GetPollImportStudentList(taskID);
        }
        ///// <summary>
        ///// ɾ��
        ///// </summary>
        //protected void doRemove()
        //{
        //    try
        //    {
        //         DAL.Remove();
        //         //��¼ɾ����־������IDɾ����
        //         BizLogHelper.Operate(,"ɾ��");
        //    }
        //   catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
        //        if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
        //        {
        //            throw new ETMS.AppContext.BusinessException(".Import_SurveyArea.DataUsed");
        //        } 
        //        //�����δ���������׳�
        //        throw ex;
        //    }  
        //}  

        public bool CheckOutImportData(int queryID, int queryAreaID, string excelName, Import_Task importTask, string excelPath, int orgID, string creater, int createrUserID, string orgType, out int errorCount)
        {
            Import_TaskLogic TaskLogic = new Import_TaskLogic();
            int errCount = 0;
            errorCount = 0;
            try
            {
                int succssCount = 0;
                ExcelDataAccess EDA = new ExcelDataAccess();
                List<string> sheets = EDA.getSheetName(excelPath);

                if (sheets.Count == 0)
                {
                    importTask.Status = 1;
                    importTask.Remark = "excel�ļ������ϣ�";
                    TaskLogic.Save(importTask);
                    return false;
                }
                string sheetName = sheets[0];
                using (OleDbDataReader reader = EDA.ReadExcelData(excelPath, sheetName))
                {
                    var dt = reader.GetSchemaTable();
                    if (!(dt.Select(" ColumnName='����'").Length > 0) && (dt.Select(" ColumnName='��֯����'").Length > 0) && (dt.Select(" ColumnName='�û���'").Length > 0))
                    {
                        importTask.Status = 1;
                        importTask.Remark = "excel�ļ����Ϸ����ļ�������������ֶΣ�ѧԱ�˺ţ�ѧԱ����";
                        TaskLogic.Save(importTask);
                        return false;
                    }
                }


                DataTable drs = EDA.ImportDataTable(excelPath, sheetName);
                foreach (DataRow p in drs.Rows)
                {
                    Import_SurveyArea import_SurveyArea = new Import_SurveyArea()
                    {
                        TaskID = importTask.TaskID,
                        QueryAreaID = queryAreaID,
                        QueryID = queryID,
                        OrganizationID = orgID,
                        OrgType = orgType,
                        RealName = p["����"].ToString(),
                        DisplayPath = p["��֯����"].ToString(),
                        LoginName = p["�û���"].ToString(),
                        WorkNo = p["����"].ToString(),
                        DepartmentName = p["����"].ToString(),
                        PostName = p["��λ"].ToString(),
                        RankName = p["ְ��"].ToString(),
                        Email = p["����"].ToString()
                    };
                    if (string.IsNullOrEmpty(import_SurveyArea.DetailID.ToString()))
                    {
                        import_SurveyArea.DetailID = 0;
                    }
                    import_SurveyArea.DetailID = 0;
                    this.Save(import_SurveyArea);
                }
                errorCount = DAL.DoValid(importTask.TaskID);

                succssCount = DAL.ImportStudentSurveyArea(queryAreaID, importTask.TaskID, creater, out errCount);

                if (errorCount > 0)
                {
                    importTask.Status = 2;
                    importTask.Remark = "��" + drs.Rows.Count + "�����ݣ�����" + errorCount.ToString() + "������У�������" + succssCount + "�����ݵ���ɹ���";
                    //����
                    TaskLogic.Save(importTask);
                }
                else
                {
                    importTask.Status = 3;//�ɹ�
                    importTask.Remark = "��" + succssCount.ToString() + "���ݵ���ɹ���";
                }
                return true;
            }

            catch (Exception ex)
            {
                importTask.Status = 1;//����ʧ��
                importTask.Remark = ex.Message;
                return false;
            }
            finally
            {
                //��������״̬
                TaskLogic.Save(importTask);
            }

        }
    }
}

