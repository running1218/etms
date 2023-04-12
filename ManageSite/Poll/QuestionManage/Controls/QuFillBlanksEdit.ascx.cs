using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ETMS.AppContext;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_QuestionManage_Controls_QuFillBlanksEdit : System.Web.UI.UserControl
{
    private static Poll_OptionLogic OptionLogic = new Poll_OptionLogic();
    private static Poll_TitleLogic TitleLogic = new Poll_TitleLogic();
    private static Poll_ColumnLogic ColumnLogic = new Poll_ColumnLogic();

    #region 页面条件参数存放

    /// <summary>
    /// 操作
    /// </summary>
    public OperationAction Action
    {
        get;
        set;
    }

    /// <summary>
    /// 试题ID
    /// </summary>
    public int QuestionID
    {
        get
        {
            return int.Parse(Request.QueryString["QuestionID"]);
        }
    }

    /// <summary>
    /// 题所属默认栏目
    /// </summary>
    public int ColumnID
    {
        get
        {
            return (int)ViewState["ColumnID"];
        }
        set
        {
            ViewState["ColumnID"] = value;
        }
    }
    /// <summary>
    /// 题型
    /// </summary>
    public int QuestionType
    {
        get
        {
            return (int)ViewState["QuestionType"];
        }
        set
        {
            ViewState["QuestionType"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int maxLength = 50;
            for (int i = 1; i <= maxLength; i++)
            {
                this.ddlTitleNo.Items.Add(i.ToString());
            }

            if (Action == OperationAction.Add)
            {
                //题序号自动累加
                int totalRecordCount = TitleLogic.GetMaxTitleNo(this.ColumnID);
                if (totalRecordCount > 0)
                {
                    this.ddlTitleNo.SelectedValue = (totalRecordCount + 1).ToString();
                }
            }
            //修改
            if (Action == OperationAction.Edit)
            {
                //题目
                Poll_Title question = TitleLogic.GetById(this.QuestionID);
                RichTextTitle.Text = question.TitleName;
                this.ddlTitleNo.SelectedValue = question.TitleNo.ToString();
            }

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            Poll_Title question = null;
            if (Action == OperationAction.Add)
            {
                question = new Poll_Title();
                question.TitleTypeID = this.QuestionType;
                question.ColumnID = this.ColumnID;
                question.CreateTime = DateTime.Now;
            }
            else
            {
                question = TitleLogic.GetById(this.QuestionID);
            }
            question.TitleName = this.RichTextTitle.Text;
            question.TitleNo = int.Parse(this.ddlTitleNo.SelectedValue);
            //保存试题信息
            TitleLogic.Save(question);

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("题目保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}