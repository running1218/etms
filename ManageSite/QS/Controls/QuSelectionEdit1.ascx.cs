using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.AppContext;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Utility;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Controls;

public partial class Poll_QuestionManage_Controls_QuSelectionEdit1 : System.Web.UI.UserControl
{
    #region 页面条件参数存放



    /// <summary>
    /// 试题ID
    /// </summary>
    public string TitleID
    {
        get
        {
            return (string)ViewState["TitleID"];
        }
        set
        {
            ViewState["TitleID"] = value;
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
    /// 选项列表
    /// </summary>
    public List<QS_QueryTitleOption> options
    {
        get
        {
            return (List<QS_QueryTitleOption>)ViewState["options"];
        }
        set
        {
            ViewState["options"] = value;
        }
    }

    #endregion
    /// <summary>
    /// 答案选项默认4个ABCD
    /// </summary>
    protected int OptionCount = 4;

    private QS_QueryTitle queryTitleEntity = new QS_QueryTitle();
    private QS_QueryTitleOption queryTitleOptionEntity = new QS_QueryTitleOption();

    private QS_QueryTitleLogic queryTitleLogicBiz = new QS_QueryTitleLogic();
    private QS_QueryTitleOptionLogic queryTitleLogicOptionBiz = new QS_QueryTitleOptionLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TitleID = Request.QueryString["TitleID"]; ;
            int totalRecords;
            options = queryTitleLogicOptionBiz.GetEntityList(1, 100, "", " and TitleID='" + TitleID + "'", out totalRecords).ToList<QS_QueryTitleOption>();
            if (options.Count == 0)
            {
                options = new List<QS_QueryTitleOption>();
                QS_QueryTitleOption queryOption = new QS_QueryTitleOption();
                for (int i = 0; i < OptionCount; i++)
                {
                    options.Add(new QS_QueryTitleOption() { OptionID = Guid.NewGuid(), OptionNo = 0 });
                }
            }
            this.rpOptions.DataSource = options;
            this.rpOptions.DataBind();
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        LoadUserInputData();
        Guid optionID = e.CommandArgument.ToGuid();
        QS_QueryTitleOption findItem = options.Find(item => { return (item.OptionID == optionID); });
        //删除
        options.Remove(findItem);
        //queryTitleLogicOptionBiz.Remove(optionID);
        ////重新绑定
        this.rpOptions.DataSource = options;
        this.rpOptions.DataBind();
    }
    private void LoadUserInputData()
    {
        foreach (RepeaterItem row in this.rpOptions.Items)
        {
            TextBox txtOptionName = (TextBox)row.FindControl("txtOptionName");
            options[row.ItemIndex].OptionName = txtOptionName.Text;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        LoadUserInputData();
        ////添加
        options.Add(new QS_QueryTitleOption() { OptionID = Guid.NewGuid() });

        ////重新绑定
        this.rpOptions.DataSource = options;
        this.rpOptions.DataBind();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {

            ////1、保存选项信息
            foreach (RepeaterItem row in rpOptions.Items)
            {
                TextBox txtOptionName = (TextBox)row.FindControl("txtOptionName");
                this.options[row.ItemIndex].OptionName = txtOptionName.Text;
                this.options[row.ItemIndex].TitleID = new Guid(TitleID);
                if (this.options[row.ItemIndex].OptionNo == 0)
                {
                    queryTitleLogicOptionBiz.Save(this.options[row.ItemIndex], OperationAction.Add);
                }
                else
                {
                    queryTitleLogicOptionBiz.Save(this.options[row.ItemIndex], OperationAction.Edit);
                }

            }
            ////2、保存其他选项信息
            if (this.rblOther.SelectedValue == "1")//添加其他选项
            {
                QS_QueryTitleOption otherOption = new QS_QueryTitleOption() { OptionID = new Guid().ToGuid().NewID(), OptionNo = 0, TitleID = new Guid(TitleID), OtherLength = 1000, OptionName = "其他" };
                queryTitleLogicOptionBiz.Save(otherOption, OperationAction.Add);
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("题目保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}