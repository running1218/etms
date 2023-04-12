using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Utility;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;
using ETMS.Components.Exam.Implement.BLL.ItemBank;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Entity.Test;

public partial class QuestionDB_TestPaper_Controls_ItemDB2BeSelect : System.Web.UI.UserControl
{
    #region 页面条件参数存放
    /// <summary>
    /// TestPaperID
    /// </summary>
    public Guid TestPaperID
    {
        get
        {
            return (Guid)ViewState["TestPaperID"];
        }
        set
        {
            ViewState["TestPaperID"] = value;
        }
    }

    /// <summary>
    /// CourseID
    /// </summary>
    public Guid CourseID
    {
        get
        {
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
    /// <summary>
    /// 作业类型
    /// </summary>
    public BizExerciseType ExerciseType
    {
        get
        {
            return (BizExerciseType)ViewState["ExerciseType"];
        }
        set
        {
            ViewState["ExerciseType"] = value;
        }
    }

    /// <summary>
    /// 作业ID
    /// </summary>
    public string ExerciseID
    {
        get
        {
            return ViewState["ExerciseID"].ToString();
        }
        set
        {
            ViewState["ExerciseID"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {   
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            SetQuestionBank();

            this.PageSet1.QueryChange();
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
        if (ddlQuestionType.SelectedValue != "-1")
        {
            questionSearch.Type = (QuestionType)Enum.Parse(typeof(QuestionType), ddlQuestionType.SelectedValue);
        }
        questionSearch.Difficulty = (short)ddlDifficulty.SelectedValue.ToInt();
        questionSearch.QuestionTitle = txtQuestionTitle.Text.Trim();

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
        //upList.Update();
    }

    /// <summary>
    /// 批量建立关系
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnSave_Click(object sender, EventArgs e)
    {   
        object[] selectedValues = CustomGridView.GetSelectedValues<object>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            JsUtility.AlertMessageBox("请选择题目！");
            return;
        }
        try
        {
            IPaperQuestionLogic iPaperQuestionLogic = ServiceRepository.PaperQuestionService;
            IList<KeyValuePair<Guid,QuestionType>> selectedQuestions=new List<KeyValuePair<Guid,QuestionType>>();
             foreach (GridViewRow row in this.CustomGridView1.Rows)
            {
                CheckBox control = row.FindControl("CheckBox1") as CheckBox;
                if (control != null && control.Checked)
                {
                  System.Collections.Specialized.IOrderedDictionary values=  this.CustomGridView1.DataKeys[row.RowIndex].Values;
                  selectedQuestions.Add(new KeyValuePair<Guid, QuestionType>((Guid)values[0], (QuestionType)values[1]));
                }
            }
             iPaperQuestionLogic.AddQuestionToTestPaper(TestPaperID, selectedQuestions);
             string url = this.ActionHref(string.Format("~/QuestionDB/TestPaper/AddTestPaper.aspx?ExerciseType={0}&ExerciseID={1}", (int)ExerciseType, ExerciseID));

             //刷新数据
              
             JsUtility.SuccessMessageBox("提示", string.Format("成功选择[{0}]个题目！", selectedValues.Length), string.Format("function(){1}window.location = '{0}'{2}", url,"{","}"));

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}