using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class TraningImplement_TraningProjectQuery_TraningProjectView : System.Web.UI.Page
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
                ViewState["Crieria"] = "";
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件  按课程名称、组织机构、姓名排序
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " e.CourseName,u.OrganizationID,u.RealName";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID =BasePage.getSafeRequest(this.Page, "TrainingItemID").ToGuid();
            bind();
            this.PageSet1.QueryChange();
        }
        lbtnBack.PostBackUrl = this.ActionHref("TraningProjectList.aspx");

        TraningProjectView1.TrainingItemID = TrainingItemID;
        TraningProjectView1.IsAuditVisible = true;
        TraningProjectView1.IsIssueEndVisible = true;
    }


    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
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
            Lbl_ItemCode.Text = item.ItemCode;
            Lbl_ItemName.Text = item.ItemName;
            lbl_ItemDate.Text = string.Format("{0} 至 {1}", item.ItemBeginTime.ToDate(), item.ItemEndTime.ToDate());
        }
        //项目课程
        int total = 0;
        ddl_d999TrainingItemCourseID.DataSource = new Tr_ItemCourseLogic().GetItemCourseListByTrainingItemID(TrainingItemID, 1, int.MaxValue - 1, out total);
        ddl_d999TrainingItemCourseID.DataTextField = "CourseName";
        ddl_d999TrainingItemCourseID.DataValueField = "TrainingItemCourseID";
        ddl_d999TrainingItemCourseID.DataBind();
        ddl_d999TrainingItemCourseID.Items.Insert(0, new ListItem("全部", ""));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));

        DataTable dt = new Sty_StudentSignupLogic().GetStudentCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
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
            e.Row.Cells[4].Visible = false;
        }
    }

    /// <summary>
    /// 导出
    /// </summary>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //Crieria = string.Format(" {0}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));
        int total = 0;
        GridViewExport.DataSource = new Sty_StudentSignupLogic().GetStudentCourseListByTrainingItemID(TrainingItemID, 1, int.MaxValue - 1, SortExpression, Crieria, out total);
        GridViewExport.DataBind();

        #region 表头数据
        string content = @"<table width='100%' border='1'>
                              <tr>
                                 <td colspan='6' align='center' style='font-size:14; font-weight:bold'>项目课程学员信息</td>
                              </tr>
                              <tr>
                                <td width='20%' style='font-size:12;font-weight:bold'>项目编码</td>
                                <td colspan='1'>{0}</td>
                                <td width='20%' style='font-size:12;font-weight:bold'>项目名称</td>
                                <td colspan='3'>{1}</td>
                              </tr>
                              <tr>
                                <td width='20%' style='font-size:12;font-weight:bold'>项目周期</td>
                                <td colspan='5'>{2}</td>
                              </tr>
                        </table>";
        content = string.Format(content
            , Lbl_ItemCode.Text
            , Lbl_ItemName.Text
            , lbl_ItemDate.Text);
        #endregion

        #region 读取列表数据
        //System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.CurrentCulture);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        GridViewExport.RenderControl(oHtmlTextWriter);//将服务器控件的内容输出  
        content += oStringWriter.ToString();
        if (GridViewExport is GridView)
            content = content.Replace("border=\"0\"", "border=\"1\"");
        #endregion
        //输出
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("项目课程学员信息.xls", content);
    }
        
    /// <summary>
    /// 行邦定
    /// </summary>
    protected void GridViewExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[5].Visible = false;
        }
    }

    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(GridViewExport.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
}