using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.QuestionDB;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Utility;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;
using ETMS.Components.Exam.Implement.BLL.ItemBank;
using ETMS.Components.Exam.API.Entity.ItemBank;

public partial class QuestionDB_Controls_QuestionList : System.Web.UI.UserControl
{

    #region 页面条件参数存放

    private static QuestionInfo info = new QuestionInfo();

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

    /// <summary>
    /// 课程ID
    /// </summary>
    public Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
            {
                ViewState["CourseID"] = new Guid();
            }
            return (Guid)ViewState["CourseID"];
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }

    /// <summary>
    /// 题库ID
    /// </summary>
    public QuestionBank eQuestionBank
    {
        get
        {
            return (QuestionBank)ViewState["eQuestionBank"];
        }
        set
        {
            ViewState["eQuestionBank"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            SetQuestionBank();
            ltlBtnAdd.Text = GetAddUrl();
            this.PageSet1.QueryChange();
           
            this.lbtnImport.Attributes["onclick"] = string.Format("javascript:showWindow('导入试题','{0}',500,350);javascript:return false;", this.ActionHref("../ImportStudent.aspx?CourseID=" + CourseID + "&QuestionType=" + QuestionType.ToEnumValue() + "&QuestionBankID=" + eQuestionBank.QuestionBankID));
        }
    }

    /// <summary>
    /// 获取课程的题库实体，如果没有，插入一条课程题库关联数据和题库信息。
    /// </summary>
    private void SetQuestionBank()
    {
        string CourseName = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(CourseID);
        int OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;

        IQuestionBankLogic bankService = ServiceRepository.QuestionBankService;

        eQuestionBank = bankService.GetQuestionBankByCourseID(CourseID, CourseName, OrgID);

        //初始化信息
        ltlCourseName.Text = CourseName;
    }

    /// <summary>
    /// 列表中操作列：编辑 删除 查看
    /// </summary>
    /// <param name="questionType"></param>
    /// <returns></returns>
    protected string GetUrl(string QuestionID)
    {
        string reUrl = "";
        //题型名称
        string QuestionTypeName = info.QuestionTypeName(QuestionType);

        string strfile = string.Empty;
        switch (QuestionType)
        {
            case QuestionType.Null:
                break;
            case QuestionType.SingleChoice:
                strfile = "~/QuestionDB/QuSingleSelection";
                break;
            case QuestionType.MultipleChoice:
                strfile = "~/QuestionDB/QuMultipleChoice";
                break;
            case QuestionType.TextEntry:
                break;
            case QuestionType.Judgement:
                strfile = "~/QuestionDB/QuJudge";
                break;
            case QuestionType.ExtendedText:
                strfile = "~/QuestionDB/QuQuestionAndAnswer";
                break;
            case QuestionType.Match:
                break;
            case QuestionType.Group:
                break;
            default:
                break;
        }

        string str1 = this.ActionHref(string.Format("{0}/QuestionEdit.aspx?QuestionID={1}&QuestionBankID={2}", strfile, QuestionID, eQuestionBank.QuestionBankID));
        string str2 = this.ActionHref(string.Format("{0}/QuestionView.aspx?QuestionID={1}&QuestionBankID={2}", strfile, QuestionID, eQuestionBank.QuestionBankID));

        reUrl = "<a href=\"javascript:showWindow('编辑{0}','{1}')\">编辑</a>"
              + "<a href=\"javascript:showWindow('查看{0}','{2}')\">查看</a>";

        return string.Format(reUrl, QuestionTypeName, str1, str2);
    }

    /// <summary>
    /// 新增按钮地址
    /// </summary>
    /// <param name="iType"></param>
    /// <returns></returns>
    protected string GetAddUrl()
    {
        string QuestionTypeName = info.QuestionTypeName(QuestionType);
        string strfile = string.Empty;
        switch (QuestionType)
        {
            case QuestionType.Null:
                break;
            case QuestionType.SingleChoice:
                strfile = "~/QuestionDB/QuSingleSelection";
                break;
            case QuestionType.MultipleChoice:
                strfile = "~/QuestionDB/QuMultipleChoice";
                break;
            case QuestionType.TextEntry:
                break;
            case QuestionType.Judgement:
                strfile = "~/QuestionDB/QuJudge";
                break;
            case QuestionType.ExtendedText:
                strfile = "~/QuestionDB/QuQuestionAndAnswer";
                break;
            case QuestionType.Match:
                break;
            case QuestionType.Group:
                break;
            default:
                break;
        }
        string str1 = this.ActionHref(string.Format("{0}/QuestionAdd.aspx?QuestionBankID={1}", strfile, eQuestionBank.QuestionBankID));
        return string.Format("<input type=\"button\" class=\"btn_Add\" value=\"新增\" onclick=\"javascript:showWindow('新增{0}','{1}')\" />", QuestionTypeName, str1);
    }

    /// <summary>
    /// 查询课程的题库下的各个题型的题目列表：CourseID 关联 QuestionBankID ，分QuestionType 的QuestionID 列表
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        IQuestionBankLogic bankService = ServiceRepository.QuestionBankService;

        QuestionSearch questionSearch = new QuestionSearch();

        questionSearch.QuestionBankID = eQuestionBank.QuestionBankID;
        questionSearch.Type = QuestionType;
        questionSearch.Difficulty = (short)ddl_Difficulty.SelectedValue.ToInt();
        questionSearch.QuestionTitle = txt_QuestionTitle.Text.Trim();

        return (System.Collections.IList)bankService.GetQuestionByCourseID(CourseID, questionSearch, pageIndex, pageSize, out totalRecordCount);

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
                if (QuestionType == ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.Judgement)
                {
                    IQuestionServiceLogic questionLogic = ServiceRepository.JudgementQuestionService;
                    questionLogic.Delete(new Guid(e.CommandArgument.ToString()));
                }
                if (QuestionType == ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.SingleChoice)
                {
                    IQuestionServiceLogic questionLogic = ServiceRepository.SingleChoiceQuestionService;
                    questionLogic.Delete(new Guid(e.CommandArgument.ToString()));
                }
                if (QuestionType == ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.MultipleChoice)
                {
                    IQuestionServiceLogic questionLogic = ServiceRepository.MultipleChoiceQuestionService;
                    questionLogic.Delete(new Guid(e.CommandArgument.ToString()));
                }
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


}