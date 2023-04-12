using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ETMS.AppContext;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.BLL;
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
    public string QuestionID
    {
        get
        {
            return Request.QueryString["QuestionID"];
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

    /// <summary>
    /// 答案选项默认4个ABCD
    /// </summary>
    protected int OptionCount = 4;
    #endregion

    private QS_QueryTitle queryTitleEntity = new QS_QueryTitle();
    private QS_QueryTitleOption queryTitleOptionEntity = new QS_QueryTitleOption();

    private QS_QueryTitleLogic queryTitleLogicBiz = new QS_QueryTitleLogic();
    private QS_QueryTitleOptionLogic queryTitleLogicOptionBiz = new QS_QueryTitleOptionLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //题目
            queryTitleEntity = queryTitleLogicBiz.GetById(new Guid(QuestionID));
            RichTextTitle.Text = queryTitleEntity.TitleName;
            this.lblTitleNo.Text = queryTitleEntity.TitleNo.ToString();

            //基本选项
            int totalRecords;
            IList<QS_QueryTitleOption> options = queryTitleLogicOptionBiz.GetEntityList(1, 100, "", string.Format(" AND TitleID='{0}' AND [OtherLength]=0", QuestionID), out totalRecords);

            OptionCount = totalRecords;

            //其他选项
            queryTitleLogicOptionBiz.GetEntityList(1, 0, "", string.Format(" AND TitleID='{0}' AND [OtherLength]!=0", QuestionID), out totalRecords);
            if (totalRecords > 0)
            {
                //this.QuestionOptions.ExpandParm = 1;
                this.rblOther.SelectedValue = "1";
            }


            this.rpOptions.DataSource = options;
            this.rpOptions.DataBind();
        }
    }
}