using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;


public partial class LearningManagement_ClassManager_ClassList : ETMS.Controls.BasePage
{
    private static Sty_ClassLogic classLogic = new Sty_ClassLogic();
    private static Sty_Class sty_Class = new Sty_Class();
    public Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }   

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            InitialControl();
        }
        aBack.HRef = "ProjectList.aspx";
    }

    private void InitialControl()
    {
        int total=0;
        DataTable dt=classLogic.GetClassItemList(1,1,string.Empty,string.Format(" and TrainingItemID='{0}'",TrainingItemID),out total);
        this.lblItemName.Text = dt.Rows[0]["ItemName"].ToString();
        this.lblTime.Text = dt.Rows[0]["ItemBeginTime"].ToDate()+ "至" + dt.Rows[0]["ItemEndTime"].ToDate();
        this.lblSingnNum.Text = dt.Rows[0]["SingnNum"].ToString();
        this.lblAssignNum.Text = dt.Rows[0]["AssignNum"].ToString();
        this.lblUnAssignNum.Text = (dt.Rows[0]["SingnNum"].ToInt() - dt.Rows[0]["AssignNum"].ToInt()).ToString();
    }
    
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        List<Sty_Class> list = classLogic.GetClassListByTrainingItemID(TrainingItemID);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(list, pageIndex, pageSize);
        totalRecordCount = list.Count;
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
        {
            Sty_Class drv = (Sty_Class)e.Row.DataItem;
            CustomLinkButton lbnDel = (CustomLinkButton)e.Row.FindControl("lbtn_Del");
            if (drv.AssignCount > 0)
            {
                lbnDel.Enabled = false;
                lbnDel.EnableConfirm = false;
                lbnDel.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }

    //单个删除班级信息
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            CustomLinkButton lbnDel = e.CommandSource as CustomLinkButton;             
           
            if (e.CommandName == "Del" && lbnDel.Enabled)
            {
                classLogic.Remove(e.CommandArgument.ToGuid());
                this.PageSet1.DataBind();
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}