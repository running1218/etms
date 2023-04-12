using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace ETMS.WebApp
{
    /// <summary>
    ///ReadExcel 的摘要说明
    /// </summary>
    public class ReadExcel
    {
        public static string ExcelPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath(@"~/Excel/DB.xlsx");
            }
        }

        public ReadExcel()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static DataTable GetData(ETMS.Components.Basic.Implement.BLL.BizDataSourceEnum bizDataSourceEnum)
        {
            return GetData(bizDataSourceEnum.ToString());
        }

        public static DataTable GetData(string sheetName)
        {
            DataTable source = new DataTable();
            source.TableName = sheetName;
            //string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 8.0; HDR=NO; IMEX=1'"; //此连接只能操作Excel2007之前(.xls)文件
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + ExcelPath + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                OleDbDataAdapter odda = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", string.Format("{0}$", sheetName)), conn);
                odda.Fill(source);
                conn.Close();
            }
            return Transfer(source);
        }

        protected static DataTable Transfer(DataTable source)
        {
            if (source.Rows.Count > 0)
            {
                foreach (DataColumn column in source.Columns)
                {
                    column.ColumnName = source.Rows[0][column.ColumnName].ToString();
                }

                source.Rows.Remove(source.Rows[0]);
            }

            return source;
        }
    }
}