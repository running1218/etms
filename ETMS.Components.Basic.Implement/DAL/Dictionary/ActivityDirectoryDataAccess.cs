using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Basic.Implement.DAL.Dictionary
{
    public partial class ActivityDirectoryDataAccess
    {
        public DataTable GetAreaList(int level)
        {
            string sql = @"SELECT AreaID, AreaCode, AreaName, ParentCode, AreaLevel,Latitude, Longitude
                            FROM Dic_Sys_Area
                            WHERE AreaLevel = @AreaLevel
                            ORDER BY AreaCode";
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@AreaLevel", SqlDbType.Int)                    
                };
            parms[0].Value = level;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        public DataTable GetAreaListByParent(string parentCode)
        {
            string sql = @"SELECT AreaID, AreaCode, AreaName, ParentCode, AreaLevel,Latitude, Longitude
                            FROM Dic_Sys_Area
                            WHERE ParentCode = @ParentCode
                            ORDER BY AreaCode";
            SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter("@ParentCode", SqlDbType.NVarChar, 50)
                };
            parms[0].Value = parentCode;
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, parms).Tables[0];
        }

        /// <summary>
        /// OuterOrgID as ColumnCodeValue,OuterOrgName as ColumnNameValue
        /// </summary>
        /// <returns></returns>
        public DataTable GetGroupList()
        {
            string sql = @"SELECT GroupID as ColumnCodeValue, GroupName as ColumnNameValue, OrderNo, [Status]
                            FROM Activity_Dic_Group
                            WHERE [Status] = 1
                            ORDER BY OrderNo";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetPrizeList()
        {
            string sql = @"SELECT PrizeID as ColumnCodeValue, PrizeName as ColumnNameValue, OrderNo, [Status]
                            FROM Activity_Dic_Prize
                            WHERE [Status] = 1
                            ORDER BY OrderNo";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetProductTypeList()
        {
            string sql = @"SELECT ProductTypeID as ColumnCodeValue, TypeName as ColumnNameValue, OrderNo, [Status]
                            FROM Activity_Dic_ProductType
                            WHERE [Status] = 1
                            ORDER BY OrderNo";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetRegionList()
        {
            string sql = @"SELECT RegionID as ColumnCodeValue, RegionName as ColumnNameValue, OrderNo, [Status]
                            FROM Activity_Dic_Region
                            WHERE [Status] = 1
                            ORDER BY OrderNo";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetShapeList()
        {
            string sql = @"SELECT ShapeID as ColumnCodeValue, ShapeName as ColumnNameValue, OrderNo, [Status]
                            FROM Activity_Dic_Shape
                            WHERE [Status] = 1
                            ORDER BY OrderNo";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }

        public DataTable GetActivityTypeList()
        {
            string sql = @"SELECT TypeID as ColumnCodeValue, TypeName as ColumnNameValue, OrderNo, [Status]
                            FROM Activity_Dic_Type
                            WHERE [Status] = 1
                            ORDER BY OrderNo";

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.Text, sql, null).Tables[0];
        }
    }
}
