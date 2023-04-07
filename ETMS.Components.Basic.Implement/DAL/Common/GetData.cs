using System.Data;

using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.Common
{
    public partial class GetData
    {



        /// <summary>
        /// 根据某个查询语句，获取其记录数，并返回按指定排序方式的某一页的数据列表
        /// </summary>
        /// <param name="sqlNoOrderby">不包含order by 的查询语句</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="orderBy">指定排序方式，不能为空, 比如：order by Tr_Item.CreateTime DESC</param>
        /// <param name="totalRecords">返回总的记录数</param>
        /// <returns>满足条件的指定页的数据列表</returns>
        public static DataTable GetPagedListFromSQL(string sqlNoOrderby, int pageIndex, int pageSize, string orderBy, out int totalRecords)
        {
            totalRecords = 0;
            //去掉SQL语句中以select开头的
            sqlNoOrderby = sqlNoOrderby.Trim();
            if (sqlNoOrderby.ToLower().StartsWith("select"))
                sqlNoOrderby = sqlNoOrderby.Substring(6);
            orderBy = orderBy.Trim();
            if (orderBy == "")
                throw new System.Exception("必须指定排序方式！");
            if (sqlNoOrderby == "")
                throw new System.Exception("必须有查询语句！");
            //组合查询条件
            string sqlModal = @"select 
                    ROW_NUMBER() over({0}) AS rowHZF,
                    {1}";
            string sql = string.Format(sqlModal, orderBy, sqlNoOrderby);
            //获取记录数的查询语句
            string sqlCount = string.Format("select COUNT(*) num from ({0}) hzf", sql);
            DataTable dtCount = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sqlCount).Tables[0];
            if (dtCount.Rows.Count == 1)
            {
                totalRecords = int.Parse(dtCount.Rows[0][0].ToString());
            }
            //获取数据列表的查询语句
            int start = (pageIndex - 1) * pageSize +1;//起始记录数
            if (start < 1)
                start = 1;
            int end = pageIndex * pageSize;//截止记录数
            string sqlList = string.Format("select * from ({0}) hzf where (rowHZF >={1} and rowHZF <={2}) ", sql, start, end);
            // and Tr_ItemCourse.CourseAttrID=2
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sqlList).Tables[0];
            return dt;
        }





    }
}
