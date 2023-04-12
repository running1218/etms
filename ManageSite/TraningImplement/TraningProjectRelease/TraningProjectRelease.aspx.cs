using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Notify;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Notify;

public partial class TraningImplement_TraningProjectRelease_TraningProjectRelease : System.Web.UI.Page
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            bind();
            this.PageSet1.QueryChange();
        }
        ABack.HRef = this.ActionHref("TraningProjectList.aspx");
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            lbl_ItemName.Text = item.ItemName;
            lbl_IsAllowSignup.Text = item.IsAllowSignup ? "是" : "否";
            lbl_ItemBeginTime.Text = item.ItemBeginTime.ToDate();
            lbl_ItemEndTime.Text= item.ItemEndTime.ToDate();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid ItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
            #region 获取控件
            Label lblCourseStatus = (Label)e.Row.FindControl("lblCourseStatus");
            lblCourseStatus = lblCourseStatus == null ? new Label() : lblCourseStatus;

            LinkButton lbtn_Open = (LinkButton)e.Row.FindControl("lbtn_Open");
            lbtn_Open = lbtn_Open == null ? new LinkButton() : lbtn_Open;

            LinkButton lbtn_Close = (LinkButton)e.Row.FindControl("lbtn_Close");
            lbtn_Close = lbtn_Close == null ? new LinkButton() : lbtn_Close;
            #endregion

            //控制启用与停用按钮
            switch (lblCourseStatus.Text.Trim())
            {
                case "启用":
                    lbtn_Close.Visible = true;
                    lbtn_Open.Visible = false;
                    break;
                default:
                    lbtn_Close.Visible = false;
                    lbtn_Open.Visible = true;
                    break;
            }
            Label lblSignupNumbers = (Label)e.Row.FindControl("lblSignupNumbers");
            if (lblSignupNumbers != null) {
                lblSignupNumbers.Text = new Sty_StudentCourseLogic().GetItemCourseStudentNum(ItemCourseID).ToString();
            }
        }
    }
    
    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Open" || e.CommandName == "Close")
        {
            try
            {
                Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
                ItemCourseLogic.UpdateCourseState(e.CommandArgument.ToGuid(), e.CommandName == "Open" ? 1 : 0);
                ETMS.Utility.JsUtility.SuccessMessageBox("操作成功！");
                this.PageSet1.DataBind(); 
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    /// <summary>
    /// 发布
    /// </summary>
    protected void btn_Release_Click(object sender, EventArgs e)
    {
        try
        {
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            itemLogic.Tr_Item_Issue(TrainingItemID, 1, ETMS.AppContext.UserContext.Current.RealName);
            try
            {
                //项目发布按钮点击时触发消息提醒逻辑
                ETMS.Components.Basic.Implement.BLL.JobService.IJobService service = new ETMS.Components.Basic.Implement.BLL.JobService.TrainingItemPublishNotifyJob(TrainingItemID);
                service.DoJob();
                service = new ETMS.Components.Basic.Implement.BLL.JobService.TrainingItemPublishNotifyJobTeacher(TrainingItemID);
                service.DoJob();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                string errMsg = "内部原因为：" + bizEx.Message;
                ETMS.Utility.JsUtility.SuccessMessageBox("系统提示", "项目发布成功,但给项目下的学员发送消息提醒不成功！！", "function(){window.location = 'TraningProjectList.aspx'}");
            }
            ETMS.Utility.JsUtility.SuccessMessageBox("系统提示", "项目发布成功！", "function(){window.location = 'TraningProjectList.aspx'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    private void TeacherSendMail()
    {
        Notify_MessageLogic MessageLogic = new Notify_MessageLogic();

        //1、消息入库
        Notify_Message messageObject = new Notify_Message();
    }
}