using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.AppContext;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API;

public partial class QuestionDB_QuJudge_QuestionView : System.Web.UI.Page
{
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
    public JudgementQuestion eQuestion
    {
        //get
        //{
        //    return (JudgementQuestion)ViewState["eQuestion"];
        //}
        //set
        //{
        //    ViewState["eQuestion"] = value;
        //}
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
        IQuestionServiceLogic questionLogic = ServiceRepository.JudgementQuestionService;

        eQuestion = (JudgementQuestion)questionLogic.GetByID(QuestionID);

        lblDifficulty.FieldIDValue = eQuestion.CommonQuestion.Question.Difficulty.ToString();

        ltlQuestionTitle.Text = eQuestion.CommonQuestion.Question.QuestionTitle;

        if (eQuestion.Answer.OptionAnswer.OptionCode == "A")
        {
            rbtnOptionA.Checked = true;
        }
        else
        {
            rbtnOptionB.Checked = true;
        }

        QuestionBankID = eQuestion.CommonQuestion.Question.QuestionBankID;

        //获取题库实体
        QuestionBank questionBank = new QuestionBank();
        IQuestionBankLogic questionBankLogic = ServiceRepository.QuestionBankService;

        questionBank = questionBankLogic.GetByID(QuestionBankID);

        ltlQuestionBankName.Text = questionBank.QuestionBankName;

    }
}