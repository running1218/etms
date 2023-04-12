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

public partial class Poll_QuestionManage_Controls_QuSelectionEdit : System.Web.UI.UserControl
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
    private static Poll_OptionLogic OptionLogic = new Poll_OptionLogic();
    private static Poll_TitleLogic TitleLogic = new Poll_TitleLogic();
    private static Poll_ColumnLogic ColumnLogic = new Poll_ColumnLogic();

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
                int totalRecordCount = 0;
                TitleLogic.GetPagedList(1, 0, "", string.Format(" AND ColumnID={0}", this.ColumnID), out totalRecordCount);
                if (totalRecordCount > 0)
                {
                    this.ddlTitleNo.SelectedValue = (totalRecordCount + 1).ToString();
                }
                ddl_TitleType.SelectedValue = QuestionType.ToString();
                Poll_OptionCollection options = new Poll_OptionCollection();
                options.TitleType = 1;
                options.ExpandParm = 0;
                for (int i = 0; i < OptionCount; i++)
                {
                    options.Options.Add(new Poll_Option() { OptionID = Guid.NewGuid().GetHashCode() });
                }
                this.QuestionOptions = options;
            }
            //修改
            if (Action == OperationAction.Edit)
            {
                //题目
                Poll_Title question = TitleLogic.GetById(this.QuestionID);
                RichTextTitle.Text = question.TitleName;
                ddl_TitleType.SelectedValue = question.TitleTypeID.ToString();
                this.ddlTitleNo.SelectedValue = question.TitleNo.ToString();
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
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        LoadUserInputData();

        int optionID = e.CommandArgument.ToInt();
        //查找
        Poll_Option findItem = this.QuestionOptions.Options.Find((item) =>
          {
              return (item.OptionID == optionID);
          }
          );
        //删除
        this.QuestionOptions.Options.Remove(findItem);

        //重新绑定
        this.rpOptions.DataSource = this.QuestionOptions.Options;
        this.rpOptions.DataBind();
    }
    private void LoadUserInputData()
    {
        foreach (RepeaterItem row in this.rpOptions.Items)
        {
            TextBox txtOptionName = (TextBox)row.FindControl("txtOptionName");
            this.QuestionOptions.Options[row.ItemIndex].OptionName = txtOptionName.Text;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        LoadUserInputData();
        //添加
        this.QuestionOptions.Options.Add(new Poll_Option() { OptionID = Guid.NewGuid().GetHashCode() });

        //重新绑定
        this.rpOptions.DataSource = this.QuestionOptions.Options;
        this.rpOptions.DataBind();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
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
            question.TitleTypeID = int.Parse(ddl_TitleType.SelectedValue);
            //保存试题信息
            TitleLogic.Save(question);

            //清除旧选项
            int totalRecords;
            IList<Poll_Option> options = OptionLogic.GetEntityList(1, 9000, "", string.Format(" AND [TitleID]={0} ", question.TitleID), out totalRecords);
            foreach (Poll_Option item in options)
            {
                OptionLogic.Remove(item.OptionID);
            }

            //1、保存选项信息
            foreach (RepeaterItem row in rpOptions.Items)
            {
                TextBox txtOptionName = (TextBox)row.FindControl("txtOptionName");
                this.QuestionOptions.Options[row.ItemIndex].OptionName = txtOptionName.Text;

                this.QuestionOptions.Options[row.ItemIndex].OptionID = 0;
                this.QuestionOptions.Options[row.ItemIndex].TitleID = question.TitleID;
                OptionLogic.Save(this.QuestionOptions.Options[row.ItemIndex]);
            }
            //2、保存其他选项信息
            if (this.rblOther.SelectedValue == "1")//添加其他选项
            {
                Poll_Option otherOption = new Poll_Option() { TitleID = question.TitleID, OtherLength = 100, OptionName = "其他" };
                OptionLogic.Save(otherOption);
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("题目保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }
}