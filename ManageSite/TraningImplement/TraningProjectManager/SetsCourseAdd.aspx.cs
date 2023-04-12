using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem;

public partial class TraningImplement_TraningProjectManager_SetsCourseAdd : BasePage
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    /// <summary>
    /// 计划ID
    /// </summary>
    public Guid PlanID
    {
        get
        {
            return ViewState["PlanID"].ToGuid();
        }
        set
        {
            ViewState["PlanID"] = value;
        }
    }
    
    /// <summary>
    /// 是否来自计划
    /// </summary>
    public bool IsPlanItem
    {
        get
        {
            if (ViewState["IsPlanItem"] == null)
                ViewState["IsPlanItem"] = false;
            return (bool)ViewState["IsPlanItem"];
        }
        set
        {
            ViewState["IsPlanItem"] = value;
        }
    }

    protected string TabDisplay =string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID").ToGuid();
            bind();
        }
        SetsCourseAdd1.TrainingItemID = TrainingItemID;
        SetsCourseAdd1.IsPlanItem = false;

        SetsPlanCourseAdd1.TrainingItemID = TrainingItemID;
        SetsPlanCourseAdd1.PlanID = PlanID;

        //如果不是计划内 隐藏页卡
        //if (!IsPlanItem)
        //{
            TabDisplay = "style='display:none'";
            Page.ClientScript.RegisterStartupScript(Page.GetType()
                , "showTab"
                , "<script type='text/javascript'>setCookie('Tab_0');</script>");
        //}

        aBack.HRef = this.ActionHref(string.Format("../ProjectCourseResource/CourseList.aspx?TrainingItemID={0}", TrainingItemID.ToString()));
    }

    private void bind()
    {
        Tr_Item item =  new Tr_ItemLogic().GetById(TrainingItemID);
        if (item != null)
        {
            IsPlanItem = item.IsPlanItem;
            PlanID = item.PlanID;
        }
    }
}