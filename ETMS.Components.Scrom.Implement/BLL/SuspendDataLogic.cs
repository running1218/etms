using System;
using ETMS.Components.Scrom.Implement.DAL;
using System.Data;

namespace ETMS.Components.Scrom.Implement.BLL
{
    public class SuspendDataLogic
    {
        /// <summary>
        /// 设置暂停数据值
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="UserID"></param>
        /// <param name="val"></param>
        public void SetSuspendData(Guid ResourceID, int UserID, string val)
        {
            new SuspendDataDataAccess().SetSuspendData(ResourceID, UserID, val);
        }

        /// <summary>
        /// 根据资源编号、用户编号 获得暂停数据
        /// </summary>
        /// <param name="ResourceID">资源编号</param>
        /// <param name="UserID">用户编号</param>
        /// <returns></returns>
        public string GetSuspendData(Guid ResourceID, int UserID)
        {
            string SuspendData = "";
            DataTable tab = new SuspendDataDataAccess().GetSuspendData(ResourceID, UserID);
            if (tab.Rows.Count > 0 && !Convert.IsDBNull(tab.Rows[0]["SuspendData"]))
            {
                SuspendData = tab.Rows[0]["SuspendData"].ToString();
            }
            return SuspendData;
        }
    }
}
