using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement;
using ETMS.Utility.Service.FileUpload;

public partial class Valuation_TeacherEvaluation : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 讲师ID
    /// </summary>
    public int UserID
    {
        get
        {
            return (int)ViewState["UserID"];
        }
        set
        {
            ViewState["UserID"] = value;
        }
    }

    #endregion
    private Site_Student student = new Site_Student();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserID = Request.QueryString["UserID"].ToInt();
            initTeacherInfo();

            EvaluationMark1.ObjectType = ETMS.Components.Basic.Implement.BLL.BizEvaluationObjectType.Teacher;
            EvaluationMark1.ObjectID = UserID.ToString();

            PlateResult1.ObjectType = ETMS.Components.Basic.Implement.BLL.BizEvaluationObjectType.Teacher;
            PlateResult1.ObjectID = UserID.ToString();

            getTeacherLevel();
        }
    }


    #region 被评价教师

    /// <summary>
    /// 被评价教师的信息
    /// </summary>
    private void initTeacherInfo()
    {   
        student = new PublicFacade().GetStudentInfo(UserID);
    }

    /// <summary>
    /// 教师照片
    /// </summary>
    /// <returns></returns>
    protected string getUserImg()
    {
        return StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.UserIcon, string.IsNullOrEmpty(student.PhotoUrl) ? "default.gif" : student.PhotoUrl);
    }

    /// <summary>
    /// 教师姓名
    /// </summary>
    /// <returns></returns>
    protected string getRealName()
    {
        return student.RealName;
    }

    /// <summary>
    /// 教师等级
    /// </summary>
    protected void getTeacherLevel()
    {
        Site_TeacherLogic teacherlogic = new Site_TeacherLogic();

        lblTeacherLevel.FieldIDValue = teacherlogic.GetById(UserID).TeacherLevelID.ToString();
    }

    #endregion
}