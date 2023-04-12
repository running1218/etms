using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Course;
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

public partial class Living_LivingAdd : System.Web.UI.Page
{
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
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        OnlinePlayingLogic logic = new OnlinePlayingLogic();
        Res_Living entity = new Res_Living();

        try
        {
            var teacher = new UserLogic().GetUserByID(ddlTeacher.SelectedValue.ToInt());
            var course = new Res_CourseLogic().GetById(Request.ToparamValue<Guid>("CourseID"));

            if (null != teacher)
            {
                entity.CourseID = Request.ToparamValue<Guid>("CourseID");
                entity.LivingType = course.LivingType;
                entity.LivingName = txtName.Text.Trim();
                entity.StartTime = txtStartTime.Text.ToDateTime();
                entity.EndTime = txtEndTime.Text.ToDateTime();
                entity.TeacherID = ddlTeacher.SelectedValue.ToInt();
                entity.Account = teacher.LoginName;
                entity.NikeName = teacher.RealName;
                entity.OrgID = UserContext.Current.OrganizationID;
                entity.CreateTime = DateTime.Now;
                entity.CreateUser = UserContext.Current.UserName;
                entity.CreateUserID = UserContext.Current.UserID;
                entity.ModifyTime = DateTime.Now;
                entity.ModifyUser = UserContext.Current.UserName;

                logic.CreateLiving(entity);

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