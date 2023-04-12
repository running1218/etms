using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.OnlinePlaying.API;
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Components.OnlinePlaying.API.Entity;
using ETMS.Components.Basic.Implement.BLL.Course;

public partial class Living_LivingEdit : System.Web.UI.Page
{
    public Res_Living Source
    {
        get
        {
            return (Res_Living)ViewState["Res_Living"];
        }
        set
        {
            ViewState["Res_Living"] = value;
        }
    }

    public string LivingID
    {
        get
        {
            return Request.ToparamValue<string>("LivingID");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    protected void Bind()
    {
        Site_TeacherLogic logic = new Site_TeacherLogic();
        var teachers = logic.GetTeachersByOrganization();
        ddlTeacher.DataSource = teachers;
        ddlTeacher.DataTextField = "RealName";
        ddlTeacher.DataValueField = "TeacherID";
        ddlTeacher.DataBind();

        Source = new OnlinePlayingLogic().GetLiving(LivingID);

        txtName.Text = Source.LivingName;
        txtStartTime.Text = Source.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
        txtEndTime.Text = Source.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
        ddlTeacher.SelectedValue = Source.TeacherID.ToString();
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        OnlinePlayingLogic logic = new OnlinePlayingLogic();

        try
        {
            var teacher = new UserLogic().GetUserByID(ddlTeacher.SelectedValue.ToInt());

            if (null != teacher)
            {
                Source.LivingName = txtName.Text.Trim();
                Source.StartTime = txtStartTime.Text.ToDateTime();
                Source.EndTime = txtEndTime.Text.ToDateTime();
                Source.TeacherID = ddlTeacher.SelectedValue.ToInt();
                Source.Account = teacher.LoginName;
                Source.NikeName = teacher.RealName;
                Source.ModifyTime = DateTime.Now;
                Source.ModifyUser = UserContext.Current.UserName;

                logic.UpdateLiving(Source);

                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功！");
            }
        }
        catch (BusinessException biz)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(biz));
        }
        catch
        {
            JsUtility.AlertMessageBox("保存失败，请与管理员联系！");
        }
    }
}