using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.IO;

namespace ETMS.Utility.Data
{
    public class ExcelDataAccess
    {

        public DataTable ImportDataTable(string filePath, string sheetName)
        {
            System.Data.DataTable rs = new System.Data.DataTable();
            bool canOpen = false;
            string myConnstring = getConstring(filePath);
            OleDbConnection conn = new OleDbConnection(myConnstring);

            try//尝试数据连接是否可用
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    conn.Close();
                }
                canOpen = true;
            }
            catch (Exception ex)
            {

            }

            if (canOpen)
            {
                try//如果数据连接可以打开则尝试读入数据
                {

                    OleDbCommand myOleDbCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "]", conn);
                    OleDbDataAdapter myData = new OleDbDataAdapter(myOleDbCommand);
                    myData.Fill(rs);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    rs = null;
                }
            }
            else
            {
                rs = null;
            }
            return rs;
        }

        /// <summary>
        /// 读取Excel数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="sheetName">文件名</param>
        /// <returns></returns>
        public OleDbDataReader ReadExcelData(string filePath, string sheetName)
        {
            OleDbDataReader reader = null;
            string myConnstring = getConstring(filePath);

            try
            {
                OleDbConnection conn = new OleDbConnection(myConnstring);
                conn.Open();

                OleDbCommand command = new OleDbCommand("SELECT * FROM [" + sheetName + "]", conn);
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw new Exception("读取数据错误！" + ex.Message);
            }

            return reader;
        }

        /// <summary>
        /// 获取Excel表名
        /// </summary>
        /// <param name="filePath">文件路径名</param>
        /// <returns>List类</returns>
        public List<string> getSheetName(string filePath)
        {
            string myConnstring = getConstring(filePath);
            OleDbConnection conn = new OleDbConnection(myConnstring);
            conn.Open();
            List<string> tableList = new List<string>();
            string sheetName = "";
            DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                sheetName = schemaTable.Rows[i].ItemArray[2].ToString();
                sheetName = sheetName.Replace("'", "");
                if (!sheetName.EndsWith("_"))
                {
                    tableList.Add(sheetName);
                }
            }
            return tableList;
        }

        /// <summary>
        /// 获取连接字串
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private string getConstring(string filePath)
        {
            //excel 2007以上版本，要求服务器需安装office2007及以上环境
            if (filePath.EndsWith(".xlsx", StringComparison.InvariantCultureIgnoreCase))
            {
                string myConnstring = "Provider=Microsoft.Ace.OLEDB.12.0;" +
              "Data Source=" + filePath + ";" +
              "Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2;\";";
                return myConnstring;
            }
            //excel 2003及以下版本，windows2003系统天然默认支持！
            else
            {
                string myConnstring = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + filePath + ";" +
                "Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2;\";";
                return myConnstring;
            }
        }

        #region DataTable==>Excel
        public void SaveExcel(DataTable dt, string Filter, string FileName, string SheetName)
        {
            OleDbConnection conn_excel = new OleDbConnection();
            conn_excel.ConnectionString = getConstring(FileName);

            OleDbDataAdapter da_excel = new OleDbDataAdapter("Select * From [" + SheetName + "$]", conn_excel);
            DataTable dt_excel = new DataTable();
            da_excel.Fill(dt_excel);

            da_excel.InsertCommand = SqlInsert(SheetName, dt, conn_excel);

            DataRow dr_excel;
            string ColumnName;

            foreach (DataRow dr in dt.Select(Filter))
            {
                dr_excel = dt_excel.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    ColumnName = dc.ColumnName;
                    dr_excel[ColumnName] = dr[ColumnName];
                }
                dt_excel.Rows.Add(dr_excel);
            }

            //取原来文件的属性
            FileAttributes oldAttr = FileAttributes.Normal;
            try
            {
                //huangzhf:2012-05-31修改
                if (System.IO.File.Exists(FileName))
                {
                    //取原来文件的属性
                    oldAttr = System.IO.File.GetAttributes(FileName);
                    //设置文件的属性为正常
                    System.IO.File.SetAttributes(FileName, FileAttributes.Normal);
                }
            }
            catch { }

            //保存Excel文件
            da_excel.Update(dt_excel);

            try
            {
                //huangzhf:2012-05-31修改
                if (System.IO.File.Exists(FileName))
                {
                    //恢复文件的属性
                    System.IO.File.SetAttributes(FileName, oldAttr);
                }
            }
            catch { }

            conn_excel.Close();
        }

        private void CheckColumn(DataTable dt, DataTable dt_v)
        {
            foreach (DataRow dr in dt_v.Select())
            {
                if (!dt.Columns.Contains(dr["列名"].ToString()))
                {
                    dr.Delete();
                }
            }
            dt_v.AcceptChanges();
        }

        private string GetDataType(Type i)
        {
            string s;

            switch (i.Name)
            {
                case "String":
                    s = "Char";
                    break;
                case "Int32":
                    s = "Int";
                    break;
                case "Int64":
                    s = "Int";
                    break;
                case "Int16":
                    s = "Int";
                    break;
                case "Double":
                    s = "Double";
                    break;
                case "Decimal":
                    s = "Double";
                    break;
                default:
                    s = "Char";
                    break;

            }
            return s;
        }

        private OleDbType StringToOleDbType(Type i)
        {
            OleDbType s;

            switch (i.Name)
            {
                case "String":
                    s = OleDbType.Char;
                    break;
                case "Int32":
                    s = OleDbType.Integer;
                    break;
                case "Int64":
                    s = OleDbType.Integer;
                    break;
                case "Int16":
                    s = OleDbType.Integer;
                    break;
                case "Double":
                    s = OleDbType.Double;
                    break;
                case "Decimal":
                    s = OleDbType.Decimal;
                    break;
                default:
                    s = OleDbType.Char;
                    break;

            }
            return s;

        }

        private string SqlCreateSheet(DataTable dt, string SheetName)
        {
            string sql;

            sql = "CREATE TABLE " + SheetName + " (";

            foreach (DataColumn dc in dt.Columns)
            {
                sql += "[" + dc.ColumnName + "] " + GetDataType(dc.DataType) + " ,";
            }

            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";

            return sql;
        }

        // 生成 InsertCommand 并设置参数
        private OleDbCommand SqlInsert(string SheetName, DataTable dt, OleDbConnection conn_excel)
        {
            OleDbCommand i;
            string sql;

            sql = "INSERT INTO [" + SheetName + "$] (";
            foreach (DataColumn dc in dt.Columns)
            {
                sql += "[" + dc.ColumnName + "] ";
                sql += ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ") VALUES (";
            foreach (DataColumn dc in dt.Columns)
            {
                sql += "?,";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";

            i = new OleDbCommand(sql, conn_excel);

            foreach (DataColumn dc in dt.Columns)
            {
                i.Parameters.Add("@" + dc.Caption, StringToOleDbType(dc.DataType), 0, dc.Caption);
            }

            return i;
        }

        #endregion
    }
}
