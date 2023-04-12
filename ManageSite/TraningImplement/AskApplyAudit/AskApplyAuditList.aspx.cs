using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
public partial class TraningImplement_AskApplyAudit_AskApplyAuditList : ETMS.Controls.BasePage
{
    public static PublicFacade publicFacade = new PublicFacade();
    private static Tr_ItemCourseHoursStudentLogic itemCourseHoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
    private static Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();
    //private static Guid[] itemCourseHoursStudentIDs = new Guid[20];

    private string itemCourseHoursStudentIDStr = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.GridViewList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.ddl_a999AuditStatus.SelectedIndex = 1;
            this.PageSet1.QueryChange();
        }
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.GridViewList.Columns[5].Visible = false;
        }
    }
   
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string whereStr = string.Empty;
        whereStr = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        whereStr += string.Format(" and d.OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable dt = itemCourseHoursStudentLogic.GetItemCourseHoursLeaveStudent(pageIndex, pageSize, " d.OrgID,u.DepartmentID,u.RealName", whereStr, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !this.GridViewList.IsEmpty)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            LinkButton lbnCheck = e.Row.FindControl("lbnCheck") as LinkButton;
            LinkButton lbnView = e.Row.FindControl("lbnView") as LinkButton;
            lbnCheck.Visible = false;
            lbnView.Visible = false;
            string trainTime = drv["TrainingDate"].ToDate() + " " + drv["TrainingBeginTime"].ToDateTime().ToString("HH:mm") + "-" + drv["TrainingEndTime"].ToDateTime().ToString("HH:mm");

            if (drv["AuditStatus"].ToInt() == 20 || drv["AuditStatus"].ToInt() == 40)//20：审核通过  40：审核不通过
            {
                lbnView.Visible = true;
                lbnView.Attributes.Add("href", string.Format("javascript:showWindow('查看','{0}')", this.ActionHref(string.Format("AskApplyAuditView.aspx?ItemCourseHoursStudentID={0}&ItemName={1}&CourseName={2}&TrainingDate={3}&UserID={4}", drv["ItemCourseHoursStudentID"].ToGuid(), drv["ItemName"].ToString(), drv["CourseName"].ToString(), trainTime,drv["UserID"]))));
            }
            else
            {
                lbnCheck.Visible = true;
                lbnCheck.Attributes.Add("href", string.Format("javascript:showWindow('审核','{0}')", this.ActionHref(string.Format("AskApplyAudit.aspx?ItemCourseHoursStudentID={0}&ItemName={1}&CourseName={2}&TrainingDate={3}&UserID={4}", drv["ItemCourseHoursStudentID"].ToGuid(), drv["ItemName"].ToString(), drv["CourseName"].ToString(), trainTime, drv["UserID"]))));
            }           
        }
    }   

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        upList.Update();
    }

    protected void btnAgree_Click(object sender, EventArgs e)
    {
        Guid[] selectNum = CustomGridView.GetSelectedValues<Guid>(this.GridViewList);
        if (selectNum.Length < 1)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择学员");
            return;
        }
        string title = "审核";
        Response.Redirect(string.Format("javascript:showCodeWindow('" + title.Escape() + "','{0}',480,300)", this.ActionHref(string.Format("AskApplyOption.aspx?SelectNum={0}&itemCourseHoursStudentIDStr={1}&AuditStatus={2}", InitialGuidGroup(), itemCourseHoursStudentIDStr, 20))));

    }
    protected void btnDeny_Click(object sender, EventArgs e)
    {
        Guid[] selectNum = CustomGridView.GetSelectedValues<Guid>(this.GridViewList);
        if (selectNum.Length < 1)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择学员");
            return;
        }
        string title = "审核";
        Response.Redirect(string.Format("javascript:showCodeWindow('" + title.Escape() + "','{0}',480,300)", this.ActionHref(string.Format("AskApplyOption.aspx?SelectNum={0}&itemCourseHoursStudentIDStr={1}&AuditStatus={2}", InitialGuidGroup(), itemCourseHoursStudentIDStr, 40))));

    }

    /// <summary>
    /// 获取列表中选中
    /// </summary>
    public int InitialGuidGroup()
    {
        int j = 0;
        for (int i = 0; i < this.GridViewList.Rows.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)GridViewList.Rows[i].FindControl("CheckBox1");
            if (chkSelect.Checked)
            {
                //itemCourseHoursStudentIDs[j] = GridViewList.DataKeys[i].Values["ItemCourseHoursStudentID"].ToGuid();
                itemCourseHoursStudentIDStr += GridViewList.DataKeys[i].Values["ItemCourseHoursStudentID"].ToString();
                if (i < this.GridViewList.Rows.Count)
                {
                    itemCourseHoursStudentIDStr += "_";
                }
                j++;
            }
        }
        return j;
    }

}