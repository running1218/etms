using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;

using ETMS.Components.Exam.API.Interface.ItemBank;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API;
using ETMS.Components.Basic.Implement.BLL;

using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.Implement.BLL.NewTestPaper;

public partial class QuestionDB_TestPaper_Controls_ItemDBSelected : System.Web.UI.UserControl
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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitQuestionSelectList();


        }
    }

    /// <summary>
    /// 获取已选题目
    /// </summary>
    private void InitQuestionSelectList()
    {   
        int totalSize = 0;
        IPaperQuestionLogic iPaperQuestionLogic = ServiceRepository.PaperQuestionService;
        CustomGridView1.DataSource = iPaperQuestionLogic.FindQuestionView(TestPaperID, ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.Null, int.MaxValue - 1, 1, out totalSize);
        CustomGridView1.DataBind();
        ltlItemCount.Text = totalSize.ToString();
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
                IPaperQuestionLogic iPaperQuestionLogic = ServiceRepository.PaperQuestionService;
                iPaperQuestionLogic.Delete(TestPaperID, new Guid(e.CommandArgument.ToString()));
                InitQuestionSelectList();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    /// <summary>
    /// 批量update分数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        AExamHomework examLogic = ExamHomeworkFactory.Create("ExamLogic");
        try
        {
            IPaperQuestionLogic iPaperQuestionLogic = ServiceRepository.PaperQuestionService;
            
            foreach (GridViewRow row in this.CustomGridView1.Rows)
            {
                TextBox control = row.FindControl("txtScore") as TextBox;
                if (control != null && !string.IsNullOrEmpty(control.Text))
                {
                    Guid questionID = CustomGridView1.DataKeys[row.RowIndex].Value.ToGuid();

                    iPaperQuestionLogic.UpdateQuestionScore(TestPaperID,questionID, (decimal)control.Text.ToDecimal());
                }
            }
            examLogic.CopyTestPaperQuestionInfo(TestPaperID);
            InitQuestionSelectList();
            JsUtility.SuccessMessageBox("分数设置成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    protected string getQuestionTypeName(string questionType)
    {
        switch (questionType)
        {
            case "SingleChoice":
                return "单选题分数";
            case "MultipleChoice":
                return "多选题分数";
            case "Judgement":
                return "判断题分数";
            default:
                return "";
        }
    }
}