using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class TraningImplement_ProjectStudentAdjustment_ProjectStudentAdjustment :BasePage
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
            {
                ViewState["Crieria"] = "";
            }
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID").ToGuid();
            bind();
            //单机构版本隐藏机构查询条件
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.trOrg.Visible = false;
                ddl_Site_User999OrganizationID.SelectedValue = ETMS.AppContext.UserContext.Current.OrganizationID.ToString();
            }

            this.PageSet1.QueryChange();
        }
        btn_Add.PostBackUrl = this.ActionHref(string.Format("StudentAdd.aspx?TrainingItemID={0}", TrainingItemID));
        
        //跟据查询条件来显示启用与停用
        if (dddl_Sty_StudentSignup999IsUse.SelectedValue.Trim() == "1")
        {
            btn_Open.Visible = false;
            btn_Close.Visible = true;
        }
        else
        {
            btn_Open.Visible = true;
            btn_Close.Visible = false;
        }
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            lblItemCode.Text = item.ItemCode;
            lblItemName.Text = item.ItemName;
                        
            //邦定组织机构信息
            ddl_Site_User999OrganizationID.DataSource = new Sty_StudentSignupLogic().GetTrainingItemOrganizationList(TrainingItemID);
            ddl_Site_User999OrganizationID.DataTextField = "DisplayPath";
            ddl_Site_User999OrganizationID.DataValueField = "OrganizationID";
            ddl_Site_User999OrganizationID.DataBind();
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string orgs = " AND Site_User.OrganizationID in (";
        foreach (ListItem item in ddl_Site_User999OrganizationID.Items)
        {
            if (item.Value.Trim() != "" && item.Value.Trim() != "-1")
                orgs += item.Value + ",";
        }
        orgs += "-1)";

        Crieria = string.Format(" {0} {1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), orgs);
      
        Sty_StudentSignupLogic StudentSignupLogic = new Sty_StudentSignupLogic();
        DataTable tab = StudentSignupLogic.GetStudentListALLByTrainingItemID(TrainingItemID, pageIndex, pageSize, Crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(tab, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    
    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[3].Visible = false;
        }
    }

    /// <summary>
    /// 启用
    /// </summary>
    protected void btn_Open_Click(object sender, EventArgs e)
    {
        SetStudentIsUse(1);
    }

    /// <summary>
    /// 停用
    /// </summary>
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        SetStudentIsUse(0);
    }

    /// <summary>
    /// 设置学员启用与停用
    /// </summary>
    /// <param name="isUse">1：启用，0：停用</param>
    private void SetStudentIsUse(int isUse) { 
    
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(string.Format("请选择要{0}的学员！", isUse == 1 ? "启用" : "停用"));
            return;
        }
        else
        {
            try
            {
                Sty_StudentSignupLogic logic = new Sty_StudentSignupLogic();
                logic.UpdateIsUseByStudentSignupID(selectedValues, isUse);
                this.PageSet1.DataBind();
                ETMS.Utility.JsUtility.SuccessMessageBox(string.Format("学员{0}成功！", isUse == 1 ? "启用" : "停用"));
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}