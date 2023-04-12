using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility.Service.FileUpload;

using ETMS.Utility;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Scrom.Implement.BLL;
using ETMS.AppContext;
using ETMS.WebApp.Manage;

public partial class Resource_CoursewareManage_Controls_CoursewareInfoScorm : System.Web.UI.UserControl
{
    private static readonly Res_CoursewareLogic res_CoursewareLogic = new Res_CoursewareLogic();
    private static Guid defaultGuidValue = new Guid();
    //private static Res_Courseware courseware = new Res_Courseware();

    #region 页面条件参数存放

    /// <summary>
    /// 操作类型 add  edit
    /// </summary>
    public OperationAction Action
    {
        get
        {
            return (OperationAction)ViewState["Action"];
        }
        set
        {
            ViewState["Action"] = value;
        }
    }

    //课件ID
    public Guid CoursewareID
    {
        get
        {
            if (ViewState["CoursewareID"] == null)
            {
                ViewState["CoursewareID"] = defaultGuidValue;
            }
            return (Guid)ViewState["CoursewareID"];
        }
        set
        {
            ViewState["CoursewareID"] = value;
        }
    }

    /// <summary>
    /// 课程ID
    /// </summary>
    public Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
            {
                ViewState["CourseID"] = defaultGuidValue;
            }
            return (Guid)ViewState["CourseID"];
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }

    private Res_Courseware courseware
    {
        get
        {
            if (ViewState["courseware"] == null)
            {
                ViewState["courseware"] = new Res_Courseware(); 
            }
            return (Res_Courseware)ViewState["courseware"];
        }
        set
        {
            ViewState["courseware"] = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            radlCoursewareStatus.SelectedValue = "1";
            fuCoverImage.FunctionType = FileUploadFunctionType.CourseLogo;
  
            if (Action == OperationAction.Edit)
            {
                InitControl();
                if (courseware.CoursewarePath != string.Empty)
                    lbtnSave.Text = "保存";
            }
            else
            {
                if (null != CourseID)
                {
                    ddlCoursewareID.CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
                }
                
            }
            
        }
    }

    //初始化控件值
    private void InitControl()
    {   
        courseware = res_CoursewareLogic.GetById(CoursewareID);
        txtCoursewareName.Text = courseware.CoursewareName;
        radlCoursewareStatus.SelectedValue = courseware.CoursewareStatus.ToString();
        txtShowHoures.Text = courseware.ShowHoures.ToString();
        txtCoursewareSource.Text = courseware.CoursewareSource;
        txtRemark.Text = courseware.Remark;
        CoursewareID = courseware.CoursewareID;
        CourseID = new ETMS.Components.Basic.Implement.BLL.Course.Resources.Res_CourseResLogic().getCourseIDByResID(CoursewareID.ToString(), ETMS.Components.Basic.API.Entity.EnumResourcesType.Courseware);
        ddlCoursewareID.CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
        ddlCoursewareID.isEnabled = false;
        imgCoverLogo.ImageUrl = string.IsNullOrEmpty(courseware.CoverImg) ? "" : StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, courseware.CoverImg);  
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveData();
            ETMS.WebApp.Manage.Extention.SuccessMessageBoxAndCloseWindow("课件信息保存成功!");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch
        {
            ETMS.WebApp.Manage.Extention.FailedMessageBox("课件信息保存失败！");
        }
    }

    private Guid SaveData()
    {
        if (ValidateCourse())
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("课程名称不能为空!");
        }
        else
        {
            courseware.OrgID = UserContext.Current.OrganizationID;
            courseware.CoursewareTypeID = 1;
            courseware.CoursewareName = txtCoursewareName.Text.Trim();
            courseware.CoursewareSource = txtCoursewareSource.Text.Trim();
            courseware.CoursewareStatus = radlCoursewareStatus.SelectedValue.ToInt();
            courseware.ShowHoures = txtShowHoures.Text.ToInt();
            courseware.Remark = txtRemark.Text.Trim();
            courseware.DelFlag = false;
            CourseID = ddlCoursewareID.getCourseID();
            courseware.CoverImg = fuCoverImage.UploadFileEntity().BizUrl ?? courseware.CoverImg;

            if (CoursewareID.ToString() == defaultGuidValue.ToString())
            {
                courseware.CoursewareID = Guid.NewGuid();
                courseware.CreateUser =UserContext.Current.RealName;
                courseware.CreateUserID = UserContext.Current.UserID;
                courseware.CreateTime = DateTime.Now;
                courseware.ModifyUser = UserContext.Current.RealName;
                courseware.ModifyTime = DateTime.Now;
            }
            else
            {
                courseware.CoursewareID = CoursewareID;
                courseware.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                courseware.ModifyTime = System.DateTime.Now;
            }

            res_CoursewareLogic.Save(courseware, CourseID, courseware.CoursewareID, Action);
        }

        return courseware.CoursewareID;
    }

    bool ValidateCourse()
    {
        return ddlCoursewareID.getCourseID() == Guid.Empty;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            var coursewareID = SaveData();
            Response.Redirect(this.ActionHref(string.Format("../ImportScormFiles.aspx?CoursewareID={0}", coursewareID)));
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch
        {
            ETMS.WebApp.Manage.Extention.FailedMessageBox("课件信息保存失败！");
        }
    }
}