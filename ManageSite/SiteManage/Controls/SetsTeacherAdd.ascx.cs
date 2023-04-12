using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Utility;

public partial class SiteManage_Controls_SetsTeacherAdd : System.Web.UI.UserControl
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
                ViewState["SortExpression"] = " RealName ASC";
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
        Crieria = string.Format(" {0} AND OrganizationID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        DataTable dt = new Rec_TeacherLogic().GetNoRecommendTeacherPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.CustomGridView1);
        int[] selectedValues = null;
        List<int> ls = new List<int>();
        foreach (string p in CustomGridView1.AllCheckValues)
        {
            ls.Add(p.ToInt());
        }
        selectedValues = ls.ToArray();

        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要添加的课程！");
            return;
        }
        Rec_TeacherLogic recTeacherLogic = new Rec_TeacherLogic();
        //选中的课程放入集合中
        List<Rec_Teacher> list = new List<Rec_Teacher>();
        int MaxSort = recTeacherLogic.GetRecommendTeacherMaxSort();
        int counter = 0;
        foreach (int teacherID in selectedValues)
        {
            counter += 1;
            Rec_Teacher recTeacher = new Rec_Teacher();
            recTeacher.TeacherID = teacherID;
            recTeacher.IsTop = false;
            recTeacher.Sort = MaxSort + counter;
            recTeacher.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            recTeacher.CreateUser = ETMS.AppContext.UserContext.Current.UserName;
            recTeacher.CreateTime = System.DateTime.Now;
            recTeacher.ModifyUser = ETMS.AppContext.UserContext.Current.UserName;
            recTeacher.ModifyTime = System.DateTime.Now;
            list.Add(recTeacher);
        }
        try
        {
            recTeacherLogic.AddRecTeacher(list);
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