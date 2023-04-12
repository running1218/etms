using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.LearningManagement.LearnProcessControl;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Utility;

public partial class LearningManagement_LearnProcessControl_LearnProcessControlList :BasePage
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
        
        if (!Page.IsPostBack)
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
        //对本组织机构创建的已发布的培训项目中的课程学习过程进行监控
        Crieria = string.Format(" {0} AND Tr_Item.OrgID={1} AND Tr_Item.IsIssue=1 ", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);

        ItemCourseControlLogic logic = new ItemCourseControlLogic();
        DataTable dt = logic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
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
            Guid TrainingItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
            #region 获得控件 学员人数、开始学习人数、平均学习时长
            //学员人数
            LinkButton lbtnStudentTotal = (LinkButton)e.Row.FindControl("lbtnStudentTotal");
            lbtnStudentTotal = lbtnStudentTotal == null ? new LinkButton() : lbtnStudentTotal;
            //开始学习人数
            LinkButton lbtnOpenLearnStudentTotal = (LinkButton)e.Row.FindControl("lbtnOpenLearnStudentTotal");
            lbtnOpenLearnStudentTotal = lbtnOpenLearnStudentTotal == null ? new LinkButton() : lbtnOpenLearnStudentTotal;
            //平均学习时长
            Label lblAverageSessionTime = (Label)e.Row.FindControl("lblAverageSessionTime");
            lblAverageSessionTime = lblAverageSessionTime == null ? new Label() : lblAverageSessionTime;
            #endregion

            //获得 学员人数、开始学习人数、平均学习时长
            ItemCourseControlLogic logic = new ItemCourseControlLogic();
            DataTable tab = logic.GetStudentNumSessionTime(TrainingItemCourseID);
            if (tab.Rows.Count > 0) {
                int StudentTotal=tab.Rows[0]["TotalRecords"].ToString().ToInt();
                int SessionTime=tab.Rows[0]["SessionTime"].ToString().ToInt();

                lbtnStudentTotal.Text =StudentTotal.ToString();
                lbtnStudentTotal.PostBackUrl = this.ActionHref("StudentAllList.aspx?TrainingItemCourseID=" + TrainingItemCourseID.ToString());
                
                lbtnOpenLearnStudentTotal.Text = tab.Rows[0]["OpenLearnStudentNum"].ToString();
                lbtnOpenLearnStudentTotal.PostBackUrl = this.ActionHref("StudentOpenLearnList.aspx?TrainingItemCourseID=" + TrainingItemCourseID.ToString());

                lblAverageSessionTime.Text = AverageSessionTime(StudentTotal, SessionTime);
            }
        }
    }

    /// <summary>
    /// 处理时间 00:00:00 
    /// </summary>
    private string AverageSessionTime(int StudentTotal, int SessionTime)
    {
        string str = "{0}:{1}:{2}";
        int h = 0;
        int m = 0;
        int s = 0;
        if (StudentTotal != 0 && SessionTime != 0)
        {
            int averageTiem = SessionTime / StudentTotal;
            h = averageTiem / 3600;
            m = (averageTiem - h * 3600) / 60;
            s = (averageTiem - h * 3600) % 60;
        }
        return string.Format(str, string.Format("{0:D2}", h), string.Format("{0:D2}", m), string.Format("{0:D2}", s));
    }
}