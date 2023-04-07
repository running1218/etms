using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Scrom.Implement.DAL
{
    /// <summary>
    /// �½� ��Դ
    /// </summary>
    public class ItemResourceDataAccess
    {
        /// <summary>
        /// ��ȡ�μ��½���
        /// </summary>
        /// <param name="CoursewareID"></param>
        /// <returns></returns>
        public DataTable GetLessonTree(Guid CoursewareID,Guid ItemCourseResID,int UserID)
        {
            string commandName = "dbo.Pr_sco_e_Item_GetAll";

            SqlParameter[] parms ={ 
                new SqlParameter("@CourseWareID",SqlDbType.UniqueIdentifier),
                new SqlParameter("@UserID",SqlDbType.Int),
                new SqlParameter("@ItemCourseResID",SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = CoursewareID;
            parms[1].Value = UserID;
            parms[2].Value = ItemCourseResID;

            return SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

        /// <summary>
        /// ��ȡ��Դ���ڿγ̵���Ϣ
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>returnv
        public DataTable GetInfoByRescourceID(Guid ResourceID)
        {
            string commandName = "dbo.Pr_sco_GetInfoByResourceID";
            SqlParameter[] parms ={ 
                new SqlParameter("@ResourceID",SqlDbType.UniqueIdentifier)
            };

            parms[0].Value = ResourceID;

            return SqlHelper.ExecuteDataset(ConnectionString.ScormRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
    }
}
