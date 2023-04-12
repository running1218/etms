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

public partial class QuestionDB_QuSingleSelection_Controls_QuestionInfo : System.Web.UI.UserControl
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
    public SingleChoiceQuestion eQuestion
    {
        //get
        //{
        //    return (SingleChoiceQuestion)ViewState["eQuestion"];
        //}
        //set
        //{
        //    ViewState["eQuestion"] = value;
        //}
        get;
        set;
    }

    #endregion

    /// <summary>
    /// 答案选项默认4个ABCD
    /// </summary>
    protected int OptionCount =4;

    protected string[] letterlist = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

    private static readonly IQuestionServiceLogic questionLogic = ServiceRepository.SingleChoiceQuestionService;

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
                //rbtnOption1.Checked = true;
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
        //题目
        eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);

        ddlDifficulty.SelectedValue = eQuestion.CommonQuestion.Question.Difficulty.ToString();
        RichTextTitle.Text = eQuestion.CommonQuestion.Question.QuestionTitle;

        OptionCount = eQuestion.Options.Count;
    }

    /// <summary>
    /// 是否checked
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    protected string GetQuestionChecked(int i)
    {
        string strChecked = "";
        if (Action == OperationAction.Add)
        {
            if (i == 0) { strChecked = "checked"; }
        }
        if (Action == OperationAction.Edit)
        {
            eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);

            if (eQuestion.Answer.AnswerOption.OptionCode == letterlist[i])
            {
                strChecked = "checked";
            }
        }

        return strChecked;
    }

    /// <summary>
    /// 获取选项ID值
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    protected string GetQuestionOptionID(int i)
    {
        string strOptionID = Guid.NewGuid().ToString();

        if (Action == OperationAction.Edit)
        {
            eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);
            strOptionID = eQuestion.Options[i].OptionID.ToString();
        }

        return strOptionID;
    }

    /// <summary>
    /// 获取选项内容
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    protected string GetQuestionOptionContent(int i)
    {
        string strOptionContent = "";

        if (Action == OperationAction.Edit)
        {
            eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);
            strOptionContent = eQuestion.Options[i].OptionContent;
        }

        return strOptionContent;
    }

    /// <summary>
    /// 给实体赋值
    /// </summary>
    private bool InitialEntity()
    {
        if (Action == OperationAction.Add)
        {
            eQuestion = new SingleChoiceQuestion();
            eQuestion.CommonQuestion = new CommonQuestion();
            eQuestion.CommonQuestion.Question = new Question();

            eQuestion.CommonQuestion.Question.QuestionBankID = QuestionBankID;
            eQuestion.CommonQuestion.Question.QuestionType = QuestionType.SingleChoice;
            eQuestion.CommonQuestion.Question.ParentQuestionID = Guid.Empty;
            eQuestion.CommonQuestion.Question.ObjectID = (short)ETMS.AppContext.UserContext.Current.OrganizationID;

            eQuestion.CommonQuestion.QuestionType = QuestionType.SingleChoice;
        }
        else if (Action == OperationAction.Edit)
        {
            eQuestion = (SingleChoiceQuestion)questionLogic.GetByID(QuestionID);
        }
        
        eQuestion.CommonQuestion.Question.QuestionTitle = RichTextTitle.Text;
        eQuestion.CommonQuestion.Question.Difficulty = (short)(int.Parse(ddlDifficulty.SelectedValue));

        eQuestion.Options = new List<QuestionOption>();

        //test
        //hfldOptionList.Value = "trueΩ2b9caa3a-0566-4afb-862e-effa1b1a08daΩ北京ΦfalseΩ2b9caa3a-0566-4afb-863e-effa1b1a08daΩ广州ΦfalseΩ2b9caa4a-0566-4afb-862e-effa1b1a08daΩ长沙ΦfalseΩ2b9caa4a-0566-4afb-852e-effa1b1a08daΩ上海"; 

        string[] optionList = hfldOptionList.Value.Split('Φ');
                
        int i =0;

        foreach (string item in optionList)
        {
            string[] strs = item.Split('Ω');

            string strOptionAnswer = strs[0];
            string strOptionID = strs[1];
            string strOptionContent = strs[2];

            if (string.IsNullOrEmpty(strOptionContent))
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请填写至少4个选项的信息！");
                return false;
            }

            QuestionOption option = new QuestionOption();
            option.QuestionID = eQuestion.CommonQuestion.Question.QuestionID;

            if (string.IsNullOrEmpty(strOptionID))
            {
                strOptionID = Guid.NewGuid().ToString();
            }

            option.OptionID = new Guid(strOptionID);
            option.OptionContent = strOptionContent;
            option.OptionCode = letterlist[i];

            eQuestion.Options.Add(option);

            if (strOptionAnswer == "true")
            {
                eQuestion.Answer = new SingleChoiceAnswer(eQuestion.Options[i]);
            }

            i++;
        }
        return true;
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(RichTextTitle.Text.Trim()))
        //{
        //    ETMS.Utility.JsUtility.FailedMessageBox("请填写题目。");
        //    return;
        //}
        try
        {
            if (!InitialEntity())
            {
                return;
            }

            IQuestionServiceLogic questionLogic = ServiceRepository.SingleChoiceQuestionService;
            if (Action == OperationAction.Add)
            {
                questionLogic.AddQuestion(eQuestion);
            }
            else if (Action == OperationAction.Edit)
            {
                questionLogic.Update(QuestionID, eQuestion);
                
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("单选题保存成功，点击“确定”后，重新刷新当前页数据！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

}