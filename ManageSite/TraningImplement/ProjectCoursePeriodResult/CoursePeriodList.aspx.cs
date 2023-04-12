using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

public partial class TraningImplement_ProjectCoursePeriodResult_CoursePeriodList : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 项目课程ID 
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
                ViewState["TrainingItemCourseID"] = Guid.Empty;
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
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
    /// 排序条件
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = "";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }

        btnSearch.Attributes.Add("onclick", string.Format("return CheckSelectData('{0}')", ddl_f999TrainingItemID.ClientID));
    }
    
    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind() { 
        Tr_ItemLogic item = new Tr_ItemLogic();
        //对本组织机构创建的已发布未归档的启用的培训项目
        string crieriaItem = string.Format(" AND Tr_Item.ItemStatus in (10,20,40) AND IsIssue=1 AND IsUse=1 AND OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        int total = 0;
        ddl_f999TrainingItemID.DataSource = item.GetPagedList(1, int.MaxValue - 1, "", crieriaItem, out total);
        ddl_f999TrainingItemID.DataTextField = "ItemName";
        ddl_f999TrainingItemID.DataValueField = "TrainingItemID";
        ddl_f999TrainingItemID.DataBind();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0} AND f.OrgID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList), ETMS.AppContext.UserContext.Current.OrganizationID);
        Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
        DataTable dt = hoursLogic.GetItemCourseHoursALLInfoList( pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid itemCourseHoursID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
           
            #region 获取控件
            LinkButton lbtnSetResult = (LinkButton)e.Row.FindControl("lbtnSetResult");
            lbtnSetResult = lbtnSetResult == null ? new LinkButton() : lbtnSetResult;

            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;

            HiddenField hfPayStatus = (HiddenField)e.Row.FindControl("hfPayStatus");
            hfPayStatus = hfPayStatus == null ? new HiddenField() : hfPayStatus;
            #endregion

            //如果已支付不可修改结果信息
            switch (hfPayStatus.Value.Trim())
            {
                case "0":
                    lbtnSetResult.Enabled = true;
                    lbtnSetResult.Attributes["onclick"] = string.Format("javascript:showWindow('设置执行结果','{0}');javascript:return false;"
                                        , this.ActionHref(string.Format("CoursePeriodResultEdit.aspx?ItemCourseHoursID={0}", itemCourseHoursID)));
                    break;
                default:
                    lbtnSetResult.Enabled = false;
                    lbtnSetResult.CssClass = "link_colorGray";
                    break;
            }

            lbtnView.Attributes["onclick"] = string.Format("javascript:showWindow('查看课时信息','{0}');javascript:return false;"
                , this.ActionHref(string.Format("CoursePeriodView.aspx?ItemCourseHoursID={0}&TrainingItemCourseID={1}", itemCourseHoursID,lbtnView.CommandArgument)));
        }
    }
}