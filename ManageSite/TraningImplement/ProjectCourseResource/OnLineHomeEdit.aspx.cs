using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Courseware.Implement.BLL;
using System.Data;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using ETMS.Components.ExOnlineJob.Implement.BLL;

public partial class TraningImplement_ProjectCourseResource_OnLineHomeEdit : System.Web.UI.Page
{
    #region

    /// <summary>
    /// 培训项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }

    /// <summary>
    /// 培训项目课程资源ID
    /// </summary>
    public Guid ItemCourseResID
    {
        get
        {
            return ViewState["ItemCourseResID"].ToGuid();
        }
        set
        {
            ViewState["ItemCourseResID"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
                TrainingItemCourseID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID"));
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "ItemCourseResID")))
                ItemCourseResID = new Guid(BasePage.getSafeRequest(this.Page, "ItemCourseResID"));
            bind();
        }
        //dllIsUse.SelectedValue = "1";
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Res_ItemCourse_OnLineJobLogic Logic = new Res_ItemCourse_OnLineJobLogic();
        DataTable tab = Logic.GetTrainingItemGetOneResources(TrainingItemCourseID, ItemCourseResID);
        if (tab.Rows.Count > 0)
        {
            lblOnLineJobName.Text = tab.Rows[0]["OnLineJobName"].ToString();
            dllIsUse.SelectedValue = tab.Rows[0]["IsUse"].ToString();

            ResBeginTime.Text = tab.Rows[0]["ResBeginTime"].ToDate();
            ResEndTime.Text = tab.Rows[0]["ResEndTime"].ToDate();
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        Tr_ItemCourseRes tr_ItemCourseRes = new Tr_ItemCourseResLogic().GetById(ItemCourseResID);

        tr_ItemCourseRes.IsUse = dllIsUse.SelectedValue.ToInt();
        tr_ItemCourseRes.ResBeginTime = ResBeginTime.Text.ToDateTime();
        tr_ItemCourseRes.ResEndTime = (ResEndTime.Text+" 23:59:59").ToDateTime();

        try
        {
            Res_ItemCourse_OnLineJobLogic Logic = new Res_ItemCourse_OnLineJobLogic();
            Logic.SaveResourceToItemCourse(tr_ItemCourseRes);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("信息保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}