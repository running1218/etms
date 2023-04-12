using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.AppContext;

public partial class TraningImplement_ProjectCourseResourceBatch_Controls_ResourceAddList : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
                ViewState["Crieria"] = "";
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = "icr.CreateUser desc";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 20;
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        Crieria += string.Format(" And i.OrgID = '{0}' ", UserContext.Current.OrganizationID);
        Tr_ItemCourseResLogic itemCourseRes = new Tr_ItemCourseResLogic();
        DataTable dt = itemCourseRes.GeItemCourseResNoSelectInfo(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 跟据资源类型获得 各自浏览路径
    /// </summary>
    /// <param name="CourseResID">资源ID</param>
    /// <param name="CourseID">课件ID</param>
    /// <param name="CourseResTypeID">资源类型ID</param>
    /// <returns></returns>
    protected string getResUrl(string CourseResID, string CourseID, string CourseResTypeID)
    {
        string strUrl = string.Empty;
        switch (CourseResTypeID)
        {
            case "1": //在线课件
                strUrl = this.ActionHref(string.Format("~/Courseware/OpenCourseware.aspx?CourseWareID={0}&CourseID={1}", CourseResID, CourseID));
                break;
            case "2": //在线作业
                strUrl = this.ActionHref(string.Format("~/QuestionDB/Testpaper/TestpaperView.aspx?ExerciseType={0}&ExerciseID={1}", (int)ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExOnlineHomework, CourseResID));
                break;
            case "5": //在线测试
                strUrl = this.ActionHref(string.Format("~/QuestionDB/Testpaper/TestpaperView.aspx?ExerciseType={0}&ExerciseID={1}", (int)ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExOnlineTest, CourseResID));
                break;
            default:
                strUrl = "#";
                break;
        }
        return strUrl;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int selectCount = 0;
        for (int i = 0; i < CustomGridView1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)CustomGridView1.Rows[i].FindControl("CheckBox1");
            if (cb.Checked)
            {
                selectCount++;
            }
        }
        if (selectCount == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要添加的资源！");
        }
        else
        {
            try
            {
                for (int i = 0; i < CustomGridView1.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)CustomGridView1.Rows[i].FindControl("CheckBox1");
                    if (cb.Checked)
                    {
                        Tr_ItemCourseRes courseRes = new Tr_ItemCourseRes();
                        #region
                        courseRes.ItemCourseResID = Guid.NewGuid();
                        //项目课程ID
                        HiddenField hfTrainingItemCourseID = CustomGridView1.Rows[i].FindControl("hfTrainingItemCourseID") as HiddenField;
                        if (hfTrainingItemCourseID != null)
                            courseRes.TrainingItemCourseID = hfTrainingItemCourseID.Value.ToGuid();
                        //项目课程资源类型ID
                        HiddenField hfCourseResTypeID = CustomGridView1.Rows[i].FindControl("hfCourseResTypeID") as HiddenField;
                        if (hfCourseResTypeID != null)
                            courseRes.CourseResTypeID = hfCourseResTypeID.Value.ToInt();
                        //资源ID
                        HiddenField hfResID = CustomGridView1.Rows[i].FindControl("hfResID") as HiddenField;
                        if (hfResID != null)
                            courseRes.CourseResID = hfResID.Value;
                        //资源名称
                        ShortTextLabel lblResName = CustomGridView1.Rows[i].FindControl("lblResName") as ShortTextLabel;
                        if (lblResName != null)
                            courseRes.ResName = lblResName.Text;
                        courseRes.IsUse = 1;
                        //资源开始时间
                        ShortTextLabel lblCourseBeginTime = CustomGridView1.Rows[i].FindControl("lblCourseBeginTime") as ShortTextLabel;
                        if (lblCourseBeginTime != null)
                            courseRes.ResBeginTime = lblCourseBeginTime.Text.ToDateTime();
                        //资源开始时间
                        ShortTextLabel lblCourseEndTime = CustomGridView1.Rows[i].FindControl("lblCourseEndTime") as ShortTextLabel;
                        if (lblCourseEndTime != null)
                            courseRes.ResEndTime = lblCourseEndTime.Text.ToDateTime();

                        courseRes.CreateTime = DateTime.Now;
                        courseRes.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                        courseRes.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                        #endregion
                        Tr_ItemCourseResLogic resLogic = new Tr_ItemCourseResLogic();
                        resLogic.Save(courseRes, ETMS.AppContext.OperationAction.Add);
                    }
                }
                this.PageSet1.DataBind();
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", "资源添加成功！", "window.triggerRefreshEvent");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}