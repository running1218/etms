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

public partial class QuestionDB_QuJudge_Controls_QuestionInfo : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    /// <summary>
    /// 操作
    /// </summary>
    public OperationAction Action
    {
        get
        {
            return (OperationAction)ViewState["Action"];
        }
        set
        {
            ViewState["Action"] = value;
        }
    }

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
            //获取题库实体
            QuestionBank questionBank = new QuestionBank();
            IQuestionBankLogic questionBankLogic = ServiceRepository.QuestionBankService;

            questionBank = questionBankLogic.GetByID(QuestionBankID);

            ltlQuestionBankName.Text = questionBank.QuestionBankName;

            if (Action == OperationAction.Add)
            {
                rbtnOptionA.Checked = true;
                txtOptionA.Value = Guid.NewGuid().ToString();
                txtOptionB.Value = Guid.NewGuid().ToString();
            }
            //修改
            if (Action == OperationAction.Edit)
            {
                InitControl();
            }
        }
    }

    /// <summary>
    /// 实体赋值给控件
    /// </summary>
    private void InitControl()
    {
        IQuestionServiceLogic questionLogic = ServiceRepository.JudgementQuestionService;

        eQuestion = (JudgementQuestion)questionLogic.GetByID(QuestionID);

        ddlDifficulty.SelectedValue = eQuestion.CommonQuestion.Question.Difficulty.ToString();
        RichTextTitle.Text = eQuestion.CommonQuestion.Question.QuestionTitle;

        txtOptionA.Value = eQuestion.Options[0].OptionID.ToString();
        txtOptionB.Value = eQuestion.Options[1].OptionID.ToString();

        if (eQuestion.Answer.OptionAnswer.OptionCode == "A")
        {
            rbtnOptionA.Checked = true;
        }
        else
        {
            rbtnOptionB.Checked = true;
        }

    }

    /// <summary>
    /// 给实体赋值
    /// </summary>
    private void InitialEntity()
    {
        if (Action == OperationAction.Add)
        {
            eQuestion = new JudgementQuestion();
            eQuestion.CommonQuestion = new CommonQuestion();
            eQuestion.CommonQuestion.Question = new Question();

            //eQuestion.CommonQuestion.Question.QuestionID = Guid.NewGuid();
            eQuestion.CommonQuestion.Question.QuestionBankID = QuestionBankID;
            eQuestion.CommonQuestion.Question.QuestionType = QuestionType.Judgement;
            eQuestion.CommonQuestion.Question.ParentQuestionID = Guid.Empty;
            
            eQuestion.CommonQuestion.Question.ObjectID = (short)ETMS.AppContext.UserContext.Current.OrganizationID;

            eQuestion.CommonQuestion.QuestionType = QuestionType.Judgement;
        }
        if (Action == OperationAction.Edit)
        {
            IQuestionServiceLogic questionLogic = ServiceRepository.JudgementQuestionService;

            eQuestion = (JudgementQuestion)questionLogic.GetByID(QuestionID);
        }

        eQuestion.CommonQuestion.Question.Difficulty = (short)(int.Parse(ddlDifficulty.SelectedValue));
        eQuestion.CommonQuestion.Question.QuestionTitle = RichTextTitle.Text;

        eQuestion.Options = new List<QuestionOption>();

        QuestionOption option = new QuestionOption();

        option.QuestionID = eQuestion.CommonQuestion.Question.QuestionID;
        option.OptionID =new Guid(txtOptionA.Value);
        option.OptionContent = "是";
        option.OptionCode = "A";

        eQuestion.Options.Add(option);

        option = new QuestionOption();

        option.QuestionID = eQuestion.CommonQuestion.Question.QuestionID;
        option.OptionID = new Guid(txtOptionB.Value);
        option.OptionContent = "否";
        option.OptionCode = "B";

        eQuestion.Options.Add(option);

        if (rbtnOptionA.Checked)
        {
            //eQuestion.CommonQuestion.Question.Answers = JudgementAnswer.Serialize(new JudgementAnswer(eQuestion.Options[0]));
            eQuestion.Answer = new JudgementAnswer(eQuestion.Options[0]);
        }
        else
        {
            //eQuestion.CommonQuestion.Question.Answers = JudgementAnswer.Serialize(new JudgementAnswer(eQuestion.Options[1]));
            eQuestion.Answer = new JudgementAnswer(eQuestion.Options[1]);
        }
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(RichTextTitle.Text.Trim()))
        {
            ETMS.Utility.JsUtility.FailedMessageBox("请填写题目。");
            return;
        }

        InitialEntity();

        IQuestionServiceLogic questionLogic = ServiceRepository.JudgementQuestionService;
        if (Action == OperationAction.Add)
        {
            questionLogic.AddQuestion(eQuestion);
        }
        else if (Action == OperationAction.Edit)
        {
            questionLogic.Update(QuestionID,eQuestion);
        }

        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("判断题保存成功，点击“确定”后，重新刷新当前页数据！");
    }

}