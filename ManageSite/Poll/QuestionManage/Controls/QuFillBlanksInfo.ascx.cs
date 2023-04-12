using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ETMS.AppContext;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_QuestionManage_Controls_QuFillBlanksInfo : System.Web.UI.UserControl
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
            //浏览
            if (Action == OperationAction.View)
            {
                //题目
                Poll_Title question = TitleLogic.GetById(this.QuestionID);
                RichTextTitle.Text = question.TitleName;
                this.lblTitleNo.Text = question.TitleNo.ToString();
            }
        }
    }
}