using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Utility;

public partial class Notice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var entity = new Inf_BulletinLogic().GetById(Request.ToparamValue<int>("BulletinID"));
            if (null != entity)
            {
                ltlTitle.Text = entity.MainHead;
                ltlContent.Text = entity.ArticleContent;
                ltlTime.Text = entity.BeginDate.ToDate();

                new Inf_BulletinReadLogic().Add(new Inf_BulletinRead() { ArticleID = entity.ArticleID, UserID = 0, CreateTime = DateTime.Now });

                ltlReadNum.Text = new Inf_BulletinReadLogic().GetReadNum(entity.ArticleID).ToString();
            }
        }
    }
}