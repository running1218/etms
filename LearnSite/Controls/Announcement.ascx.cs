using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Utility;
using System;
using System.Data;
using System.Text;
using System.Web.UI;

namespace ETMS.Studying.Controls
{
    public partial class Announcement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AnnouncementDataBind();
            }
        }
        /// <summary>
        /// 日程公告列表
        /// </summary>
        private void AnnouncementDataBind()
        {
            Inf_BulletinLogic Logic = new Inf_BulletinLogic();
            int totalRecords = 0;
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(string.Format(" and IsUse=1 and OrgID={0} and ArticleTypeID={1}", BaseUtility.SiteOrganizationID,BulletinTypeEnum.Builletin.ToEnumValue()));
            //日常公告 top 12
            DataTable dataList1 = Logic.GetPagedList(1, 12, " CreateTime desc ", strQuery.ToString(), out totalRecords);
            //拆分两个结果集，依次为2和10行数据
            DataTable[] tableSlice = new DataTable[2];
            tableSlice[0] = new DataTable();
            tableSlice[1] = new DataTable();
            foreach (DataColumn dc in dataList1.Columns)
            {

                tableSlice[0].Columns.Add(dc.ColumnName, dc.DataType);
                tableSlice[1].Columns.Add(dc.ColumnName, dc.DataType);
            }
            for (int j = 0; j < dataList1.Rows.Count; j++)
            {
                if (j < 2)
                {
                    
                    tableSlice[0].ImportRow(dataList1.Rows[j]);
                }
                else
                {
                    tableSlice[1].ImportRow(dataList1.Rows[j]);
                }
               
            }
            //绑定2条公告数据
            this.rptAnnouncementList1.DataSource = tableSlice[0];
            this.rptAnnouncementList1.DataBind();
            //绑定10条公告数据
            this.rptAnnouncementList2.DataSource = tableSlice[1];
            this.rptAnnouncementList2.DataBind();
        }
    }
}