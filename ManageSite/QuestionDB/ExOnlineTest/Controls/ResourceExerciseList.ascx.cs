using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ETMS.Components.ExOnlineTest.Implement.BLL;
using ETMS.Components.ExOnlineTest.API.Entity;
using ETMS.Controls;

using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;

public partial class ExOnlineTestResourceExerciseList : System.Web.UI.UserControl
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
                ViewState["SortExpression"] = " CreateTime DESC ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }

    public Guid CourseID
    {
        get;
        set;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        CourseID = this.Request.ToparamValue<Guid>("CourseID");

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            InitialControl();
        }

        btnAdd.Attributes.Add("onclick", string.Format("javascript:showWindow('新增在线测试','{0}');javascript:return false;", this.ActionHref(string.Format("~/QuestionDB/ExOnlineTest/ExerciseAdd.aspx?CourseID={0}", CourseID))));
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria += string.Format(" AND OrgID={0} And CourseID='{1}'", UserContext.Current.OrganizationID, CourseID);
        Ex_OnLineTestLogic onlineTestLogic = new Ex_OnLineTestLogic();

        DataTable dt = onlineTestLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    private void InitialControl()
    {
        Res_Course entity = new Res_CourseLogic().GetById(CourseID);
        if (null != entity)
        {
            lblCourseCode.Text = entity.CourseCode;
            lblCourseName.Text = entity.CourseName;
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 单个删除课程信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            try
            {
                Ex_OnLineTestLogic onlineTestLogic = new Ex_OnLineTestLogic();
                onlineTestLogic.Remove(new Guid(e.CommandArgument.ToString()));
                //this.PageSet1.QueryChange();
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    protected int getExerciseType()
    {
        return (int)ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExOnlineTest;
    }
}