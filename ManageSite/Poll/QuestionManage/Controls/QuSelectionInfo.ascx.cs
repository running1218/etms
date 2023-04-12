using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.AppContext;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_QuestionManage_Controls_QuSelectionInfo : System.Web.UI.UserControl
{
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


    public Poll_OptionCollection QuestionOptions
    {
        get
        {
            return (Poll_OptionCollection)ViewState["Poll_OptionCollection"];
        }
        set
        {
            ViewState["Poll_OptionCollection"] = value;
        }
    }

    #endregion
    /// <summary>
    /// 答案选项默认4个ABCD
    /// </summary>
    protected int OptionCount = 4;

    protected string[] letterlist = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

    private static Poll_OptionLogic OptionLogic = new Poll_OptionLogic();
    private static Poll_TitleLogic TitleLogic = new Poll_TitleLogic();
    private static Poll_ColumnLogic ColumnLogic = new Poll_ColumnLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //修改
            if (Action == OperationAction.View)//查看
            {
                //题目
                Poll_Title question = TitleLogic.GetById(this.QuestionID);
                RichTextTitle.Text = question.TitleName;
                lblTitleNo.Text = question.TitleNo.ToString();
                //基本选项
                int totalRecords;
                IList<Poll_Option> options = OptionLogic.GetEntityList(1, 100, "", string.Format(" AND TitleID={0} AND [OtherLength]=0", question.TitleID), out totalRecords);
                this.QuestionOptions = new Poll_OptionCollection();
                this.QuestionOptions.Options = options as List<Poll_Option>;
                OptionCount = totalRecords;

                //其他选项
                OptionLogic.GetEntityList(1, 0, "", string.Format(" AND TitleID={0} AND [OtherLength]!=0", question.TitleID), out totalRecords);
                if (totalRecords > 0)
                {
                    this.QuestionOptions.ExpandParm = 1;
                    this.rblOther.SelectedValue = "1";
                }
            }

            this.rpOptions.DataSource = this.QuestionOptions.Options;
            this.rpOptions.DataBind();
        }
    }
}