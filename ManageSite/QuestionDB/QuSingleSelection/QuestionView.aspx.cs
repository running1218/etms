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

using ETMS.AppContext;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;

public partial class QuestionDB_QuSingleSelection_QuestionView : System.Web.UI.Page
{
    protected int OptionCount = 4;

    protected string[] letterlist = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

    private static readonly IQuestionServiceLogic questionLogic = ServiceRepository.SingleChoiceQuestionService;

    #region 页面条件参数存放

    /// <summary>
    /// 题库ID
    /// </summary>
    public Guid QuestionBankID
    {
        get
        {
            return (Guid)ViewState["QuestionBankID"];
        }
        set
        {
            ViewState["QuestionBankID"] = value;
        }
    }

    /// <summary>
    /// 试题ID
    /// </summary>
    public Guid QuestionID
    {
        get
        {
            return (Guid)ViewState["QuestionID"];
        }
        set
        {
            ViewState["QuestionID"] = value;
        }
    }

    /// <summary>
    /// 试题实体
    /// </summary>
    public SingleChoiceQuestion eQuestion
    {
        get;
        set;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QuestionID = new Guid(Request.QueryString["QuestionID"].ToString());

            InitControl();
        }
    }

    /// <summary>
    /// 实体赋值给控件
    /// </summary>
    private void InitControl()
    {
        IQuestionServiceLogic questionLogic = ServiceRepository.SingleChoiceQuestionService;

        eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);

        OptionCount = eQuestion.Options.Count;

        QuestionBankID = eQuestion.CommonQuestion.Question.QuestionBankID;

        lblDifficulty.FieldIDValue = eQuestion.CommonQuestion.Question.Difficulty.ToString();

        ltlQuestionTitle.Text = eQuestion.CommonQuestion.Question.QuestionTitle;

        //获取题库实体
        QuestionBank questionBank = new QuestionBank();
        IQuestionBankLogic questionBankLogic = ServiceRepository.QuestionBankService;

        questionBank = questionBankLogic.GetByID(QuestionBankID);

        ltlQuestionBankName.Text = questionBank.QuestionBankName;

    }

    /// <summary>
    /// 是否checked
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    protected string GetQuestionChecked(int i)
    {
        string strChecked = "";

        eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);

        if (eQuestion.Answer.AnswerOption.OptionCode == letterlist[i])
        {
            strChecked = "checked";
        }

        return strChecked;
    }

    /// <summary>
    /// 获取选项内容
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    protected string GetQuestionOptionContent(int i)
    {
        string strOptionContent = "";

        eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);
        strOptionContent =letterlist[i] + "、"+ eQuestion.Options[i].OptionContent;

        return strOptionContent;
    }

}