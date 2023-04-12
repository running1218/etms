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
public partial class Poll_QuestionManage_Controls_QuFillBlanksEdit : System.Web.UI.UserControl
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
    public string TitleID
    {
        get
        {
            return Request.QueryString["TitleID"];
        }
    }
    /// <summary>
    /// 试卷ID
    /// </summary>
    public string QueryID
    {
        get { return Request.QueryString["QueryID"]; }
    }

    /// <summary>
    /// 选项列表
    /// </summary>
    public QS_QueryTitle queryTitleEntity
    {
        get
        {
            return (QS_QueryTitle)ViewState["queryTitleEntity"];
        }
        set
        {
            ViewState["queryTitleEntity"] = value;
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


    private QS_QueryTitleOption queryTitleOptionEntity = new QS_QueryTitleOption();

    private QS_QueryTitleLogic queryTitleLogicBiz = new QS_QueryTitleLogic();

    private QS_QueryTitleOptionLogic queryTitleLogicOptionBiz = new QS_QueryTitleOptionLogic();

    /// <summary>
    /// 答案选项默认4个ABCD
    /// </summary>
    protected int OptionCount = 4;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ddl_OrganizationID.Items.Add(new ListItem() { Text = "单选", Value = "1" });
            ddl_OrganizationID.Items.Add(new ListItem() { Text = "多选", Value = "2" });
            ddl_OrganizationID.Items.Add(new ListItem() { Text = "简答", Value = "4" });
            queryTitleEntity = new QS_QueryTitle();

            int totalRecords;
            options = queryTitleLogicOptionBiz.GetEntityList(1, 100, "OptionNo asc", " and TitleID='" + TitleID + "' AND [OtherLength]=0", out totalRecords).ToList<QS_QueryTitleOption>();
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

            //修改
            if (Action == OperationAction.Edit)
            {
                //题目
                queryTitleEntity = queryTitleLogicBiz.GetById(new Guid(TitleID));
                ddl_OrganizationID.SelectedValue = queryTitleEntity.TitleTypeID.ToString();
                RichTextTitle.Text = queryTitleEntity.TitleName;

                showOption(queryTitleEntity.TitleTypeID, queryTitleEntity.MinSelectNum.ToString(), queryTitleEntity.MaxSelectNum.ToString());
                if (ddl_OrganizationID.SelectedValue == "1" || ddl_OrganizationID.SelectedValue == "2")
                {
                    ddl_OrganizationID.Items.RemoveAt(2);
                }

                var p = from i in options where i.OtherLength == 1000 select i;
                if (p.Count() > 0)
                {
                    rblOther.SelectedValue = "0";
                }
            }
            //DropDownList1_SelectedIndexChanged(sender, e);
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            queryTitleEntity.TitleID = new Guid(TitleID);

            if (Action == OperationAction.Add)
            {
                queryTitleEntity.QueryID = new Guid(QueryID);
                queryTitleEntity.CreateTime = DateTime.Now;
                queryTitleEntity.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                queryTitleEntity.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                queryTitleEntity.CreateTime = DateTime.Now;
            }
            else
            {
                queryTitleEntity = queryTitleLogicBiz.GetById(new Guid(TitleID));
                queryTitleEntity.ModifyTime = DateTime.Now;
                queryTitleEntity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;

            }
            queryTitleEntity.TitleName = this.RichTextTitle.Text;
            queryTitleEntity.TitleTypeID = int.Parse(ddl_OrganizationID.SelectedValue);
            if (ddl_OrganizationID.SelectedValue == "1")
            {
                queryTitleEntity.MaxSelectNum = int.Parse(txtSelect2.SelectedValue);
                queryTitleEntity.MinSelectNum = int.Parse(DropDownListDingle.SelectedValue);
            }
            else if (ddl_OrganizationID.SelectedValue == "2")
            {
                queryTitleEntity.MaxSelectNum = int.Parse(DropDownList2.SelectedValue);
                queryTitleEntity.MinSelectNum = int.Parse(DropDownListDingle1.SelectedValue);
            }
            //保存试题信息
            queryTitleLogicBiz.Save(queryTitleEntity, Action);
            if (ddl_OrganizationID.SelectedValue == "1" || ddl_OrganizationID.SelectedValue == "2")
            {
                queryTitleLogicOptionBiz.RemoveByTitleID(queryTitleEntity.TitleID);

                foreach (RepeaterItem row in rpOptions.Items)
                {
                    TextBox txtOptionName = (TextBox)row.FindControl("txtOptionName");
                    this.options[row.ItemIndex].OptionName = txtOptionName.Text;
                    this.options[row.ItemIndex].TitleID = new Guid(TitleID);
                    this.options[row.ItemIndex].OptionID = Guid.NewGuid();
                    queryTitleLogicOptionBiz.Save(this.options[row.ItemIndex], OperationAction.Add);
                }
                ////2、保存其他选项信息
                if (this.rblOther.SelectedValue == "1")//添加其他选项
                {
                    QS_QueryTitleOption otherOption = new QS_QueryTitleOption() { OptionID = Guid.NewGuid(), OptionNo = 0, TitleID = new Guid(TitleID), OtherLength = 1000, OptionName = "其他" };
                    queryTitleLogicOptionBiz.Save(otherOption, OperationAction.Add);
                }
            }

            //this.QuSelectionEdit.

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("题目保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        showOption(int.Parse(ddl_OrganizationID.SelectedValue), queryTitleEntity.MinSelectNum.ToString(), queryTitleEntity.MaxSelectNum.ToString());

    }

    private void showOption(int TitleTypeID, string minValue, string maxValue)
    {
        if (TitleTypeID == 2)
        {
            DropDownListDingle.Visible = false;
            DropDownListDingle1.Visible = true;
            txtSelect2.Visible = false;
            DropDownList2.Visible = true;
            DropDownListDingle1.SelectedValue = minValue;
            DropDownList2.SelectedValue = maxValue;
            pannel1.Visible = true;
        }
        else if (TitleTypeID == 1)
        {
            DropDownListDingle.Visible = true;
            DropDownListDingle1.Visible = false;
            txtSelect2.Visible = true;
            DropDownList2.Visible = false;
            DropDownListDingle.SelectedValue = "1";
            txtSelect2.SelectedValue = "1";
            pannel1.Visible = true;
        }
        else if (TitleTypeID == 4)
        {
            DropDownList2.Visible = false;
            DropDownListDingle1.Visible = false;
            DropDownListDingle.Visible = false;
            txtSelect2.Visible = false;
            pannel1.Visible = false;
            //DropDownListDingle.Attributes[""] = false;
            //txtSelect2.Enabled = true;
            //
        }
    }

    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        LoadUserInputData();
        Guid optionID = new Guid(e.CommandArgument.ToString());
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
}