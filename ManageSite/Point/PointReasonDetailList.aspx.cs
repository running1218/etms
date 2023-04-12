using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Point.Implement.BLL;
using System.Data;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

public partial class Point_PointReasonDetailList : ETMS.Controls.BasePage
{
    private static StudentCoursePointLogic studentCoursePointLogic = new StudentCoursePointLogic();
    private static Sty_ClassSubgroupLogic classSubgroupLogic = new Sty_ClassSubgroupLogic();
    public static Point_Student_PointReasonDetailLogic pointReasonDetailLogic = new Point_Student_PointReasonDetailLogic();
        
    public DataTable PointTable
    {
        set { ViewState["PointTable"] = value; }
        get
        {
            return (DataTable)ViewState["PointTable"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[2].Visible = false;
        }
    }
    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        try
        {            
            //Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            //本组织机构已发布的启用的未归档的培训项目名称列表
            //string Crieria = string.Format(" AND ItemStatus!=90 AND IsUse=1 AND IsIssue=1  AND OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
            //int total = 0;itemLogic.GetPagedList(1, int.MaxValue - 1, " CreateTime DESC", Crieria, out total);

            DataTable dtlist = pointReasonDetailLogic.GetCanInputPointTrainingItemListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID);
            ddl_ItemName.DataSource = dtlist.DefaultView;
            ddl_ItemName.DataTextField = "ItemName";
            ddl_ItemName.DataValueField = "TrainingItemID";
            ddl_ItemName.DataBind();
            ///班级与项目级联
            classChangeWithItem();
            ///群组与班级级联
            groupChangeWithClass();            
        }
        catch{}
    }
    /// <summary>
    ///班级与项目级联
    /// </summary>
    protected void classChangeWithItem()
    {
        this.ddlClassName.Items.Clear();
        if (!string.IsNullOrEmpty(ddl_ItemName.SelectedValue))
        {
            //班级
            Sty_ClassLogic classLogic = new Sty_ClassLogic();
            string Crieria = string.Format(" AND TrainingItemID='{0}'", ddl_ItemName.SelectedValue.ToGuid());
            int total = 0;
            ddlClassName.DataSource = classLogic.GetPagedList(1, int.MaxValue - 1, " ClassName", Crieria, out total);
            ddlClassName.DataTextField = "ClassName";
            ddlClassName.DataValueField = "ClassID";
            ddlClassName.DataBind();
            
        }
        ddlClassName.Items.Insert(0, new ListItem("全部", ""));
    }
    /// <summary>
    ///群组与班级级联
    /// </summary>
    protected void groupChangeWithClass()
    {
        this.ddlGroupName.Items.Clear();
        //学习群组           
        if (!string.IsNullOrEmpty(ddlClassName.SelectedValue))
        {
            int total = 0;
            Sty_ClassSubgroupLogic classSubgroupLogic = new Sty_ClassSubgroupLogic();
            string Crieria = string.Format(" AND ClassID='{0}'", ddlClassName.SelectedValue.ToGuid());
            ddlGroupName.DataSource = classSubgroupLogic.GetPagedList(1, int.MaxValue - 1, " ClassSubgroupName", Crieria, out total);
            ddlGroupName.DataTextField = "ClassSubgroupName";
            ddlGroupName.DataValueField = "ClassSubgroupID";
            ddlGroupName.DataBind();
            
        }
        ddlGroupName.Items.Insert(0, new ListItem("全部", ""));
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        
        string crieria = string.Empty; //BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        if (!string.IsNullOrEmpty(this.ddlClassName.SelectedValue))
        {
            crieria += string.Format(" AND h.ClassID='{0}'",this.ddlClassName.SelectedValue.ToGuid());
        }
        if (!string.IsNullOrEmpty(this.ddlGroupName.SelectedValue))
        {
            crieria += string.Format(" AND j.ClassSubgroupID='{0}'", this.ddlGroupName.SelectedValue.ToGuid());
        }
        if (!string.IsNullOrEmpty(this.txtRealName.Text.Trim()))
        {
            crieria += string.Format(" AND u.RealName like '%{0}%'", this.txtRealName.Text.Trim().ToSafeSQLValue());
        }
        if (!string.IsNullOrEmpty(this.txtWorkerNo.Text.Trim()))
        {
            crieria += string.Format(" AND s.WorkerNo like '%{0}%'", this.txtWorkerNo.Text.Trim().ToSafeSQLValue());
        }

        //DataTable dt = pointReasonDetailLogic.GetCanInputPointStudentListByTrainingItemID(this.ddl_ItemName.SelectedValue.ToGuid(), pageIndex, pageSize, " u.OrganizationID,u.RealName,u.DepartmentID", crieria, out totalRecordCount);
        PointTable = pointReasonDetailLogic.GetCanInputPointStudentListByTrainingItemID(this.ddl_ItemName.SelectedValue.ToGuid(), pageIndex, pageSize, " u.OrganizationID,u.RealName,u.DepartmentID", crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(PointTable, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !CustomGridView1.IsEmpty)
        {
            //DataRowView drv = e.Row.DataItem as DataRowView;
            //ShortTextLabel lblGroupName = e.Row.FindControl("lblGroupName") as ShortTextLabel;
            //if (drv["ClassSubgroupID"].ToGuid() != Guid.Empty)
            //{
            //    lblGroupName.Text = classSubgroupLogic.GetById(drv["ClassSubgroupID"].ToGuid()).ClassSubgroupName;
            //}
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }

    protected void ddl_ItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ///班级与项目级联
        classChangeWithItem();
        
    }
    protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ///群组与班级级联
        groupChangeWithClass();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Guid[] selectNum = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectNum.Length < 1)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择学员！");
            return;
        }
        string IDs=string.Empty;
        for(int i=0;i<selectNum.Length;i++)
        {
            IDs+=selectNum[i];
            if(i!=selectNum.Length-1)
            {
                IDs+=",";
            }
        }
        Guid TrainingItemIDTemp=  PointTable.Rows[0]["TrainingItemID"].ToGuid();
        string title = "批量增加";
        Response.Redirect(string.Format("javascript:showCodeWindow('" + title.Escape() + "','{0}')", this.ActionHref(string.Format("PointReasonDetailListAdd.aspx?StudentSignupID={0}&TrainingItemID={1}&ClassID={2}&ClassSubgroupID={3}", IDs, TrainingItemIDTemp, string.IsNullOrEmpty(this.ddlClassName.SelectedValue) ? Guid.Empty.ToString() : ddlClassName.SelectedValue, string.IsNullOrEmpty(this.ddlGroupName.SelectedValue) ? Guid.Empty.ToString() : ddlGroupName.SelectedValue))));
    }
}