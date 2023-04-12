using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;

public partial class SiteManage_Controls_SetsCourseAdd : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
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
                ViewState["SortExpression"] = "";
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
        if (!IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }
    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} AND OrgID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        DataTable dt = new Res_CourseLogic().GetCourseNotInListByObjectID(ObjectCourseRelation.RecommendCourse, "", pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        Guid[] selectedValues = null;
        List<Guid> ls = new List<Guid>();
        foreach (string p in CustomGridView1.AllCheckValues)
        {
            ls.Add(new Guid(p));
        }
        selectedValues = ls.ToArray();
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要添加的课程！");
            return;
        }
        Rec_CourseLogic recCourseLogic = new Rec_CourseLogic();
        //选中的课程放入集合中
        List<Rec_Course> list = new List<Rec_Course>();
        int MaxSort = recCourseLogic.GetRecommendCourseMaxSort();
        int counter = 0;
        foreach (Guid courseID in selectedValues)
        {
            counter += 1;
            Rec_Course recCourse = new Rec_Course();
            recCourse.CourseID = courseID;
            recCourse.IsTop = false;
            recCourse.Sort = MaxSort + counter;
            recCourse.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
            recCourse.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            recCourse.CreateUser = ETMS.AppContext.UserContext.Current.UserName;
            recCourse.CreateTime = System.DateTime.Now;
            recCourse.ModifyUser = ETMS.AppContext.UserContext.Current.UserName;
            recCourse.ModifyTime = System.DateTime.Now;
            list.Add(recCourse);
        }
        try
        {
            recCourseLogic.AddRecCourse(list);
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "推荐课程添加成功！", "function () { window.parent.location = window.parent.location }");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            this.PageSet1.DataBind();
            return;
        }
    }
}