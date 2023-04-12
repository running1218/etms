using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.QuestionDB;
using ETMS.AppContext;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;

public partial class QuestionDB_Controls_ExamQuestionBank : System.Web.UI.UserControl
{
    private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();
    //private static readonly QuestionInfo questionLogic = new QuestionInfo();
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
    /// <summary>
    /// 试题类型 1 单选题 2 多选题 3 判断题 4 填空题 5 简答题
    /// </summary>
    public QuestionType QuestionType
    {
        get
        {
            if (ViewState["QuestionType"] == null)
            {
                ViewState["QuestionType"] = QuestionType.Null;
            }
            return (QuestionType)ViewState["QuestionType"];
        }
        set
        {
            ViewState["QuestionType"] = value;
        }
    }
    #endregion

    protected Int32 TextLength = 40;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        
        if (!Page.IsPostBack)
        {
            if (QuestionType == QuestionType.Null)
            {
                TextLength = 25;
            }
            this.PageSet1.QueryChange();
        }

    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        Crieria = string.Format("{0} AND OrgID={1} And CourseStatus=1 ", Crieria, UserContext.Current.OrganizationID);
        DataTable dt = courseLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
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


    protected string getQuestionCountByCourseIDAndQuestionType(Guid courseID, QuestionType questionType)
    {
        int totalRecordCount = 0;
        IQuestionBankLogic bankService = ServiceRepository.QuestionBankService;

        bankService.GetQuestionByCourseID(courseID, new QuestionSearch() { Type = questionType }, 1, 1, out totalRecordCount);

        return totalRecordCount.ToString();

    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        switch (QuestionType)
        {
            case QuestionType.Null:
                break;
            case QuestionType.SingleChoice:
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                break;
            case QuestionType.MultipleChoice:
                 e.Row.Cells[2].Visible = false;
                e.Row.Cells[4].Visible = false;
                break;
            case QuestionType.TextEntry:
                break;
            case QuestionType.Judgement:
                 e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                break;
            case QuestionType.ExtendedText:
                break;
            case QuestionType.Match:
                break;
            case QuestionType.Group:
                break;
            default:
                break;
        }


    }
}