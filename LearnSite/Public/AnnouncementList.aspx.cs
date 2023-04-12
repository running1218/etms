using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Utility;
using System;
using System.Data;
using System.Text;
using System.Web.UI;

namespace ETMS.Studying.Public
{
    public partial class AnnouncementList : System.Web.UI.Page
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
            strQuery.Append(string.Format(" and IsUse=1 and OrgID={0} and ArticleTypeID={1}", BaseUtility.SiteOrganizationID, BulletinTypeEnum.Builletin.ToEnumValue()));
            //日常公告 top 12
            DataTable dataList = Logic.GetPagedList(1, 5, " CreateTime desc ", strQuery.ToString(), out totalRecords);
            if (totalRecords <= 5)
            {
                this.div_LoadMore.InnerText = "加载完成";
            }
            //绑定公告数据
            this.rptAnnouncementList.DataSource = dataList;
            this.rptAnnouncementList.DataBind();
        }

    }
}