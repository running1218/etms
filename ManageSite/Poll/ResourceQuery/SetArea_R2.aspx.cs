using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.AppContext;
public partial class Poll_ResourceQuery_SetArea_R2 : System.Web.UI.Page
{
    private static Poll_QueryAreaLogic QueryAreaLogic = new Poll_QueryAreaLogic();
    private static Poll_QueryAreaDetailLogic QueryAreaDetailLogic = new Poll_QueryAreaDetailLogic();
    private static Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
    #region 页面条件参数存放
    public int QueryID
    {
        get
        {
            return int.Parse(Request.QueryString["QueryID"]);
        }
    }
    public string ResourceType
    {
        get
        {
            return "R2";
        }
    }
    public string ResourceCode
    {
        get
        {
            //case "R2":
            return "00000000-0000-0000-0000-000000000002";
        }
    }

    public Poll_QueryArea CurrentQueryArea
    {
        get
        {
            return (Poll_QueryArea)ViewState["CurrentQueryArea"];
        }
        set
        {
            ViewState["CurrentQueryArea"] = value;
        }
    }

    private static int totalCount = 0;

    private static OrganizationLogic OrgLogic = new OrganizationLogic();
    private Poll_QueryArea QueryArea = new Poll_QueryArea();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(getDataSource1));
        if (!Page.IsPostBack)
        {
            //问卷信息
            Poll_Query entity = QueryLogic.GetById(this.QueryID);
            this.lblQueryName.Text = entity.QueryName;
            this.lblBeginTime.DateTimeValue = entity.BeginTime;
            this.lblEndTime.DateTimeValue = entity.EndTime;


            //1、载入页签1“默认配置”
            this.CurrentQueryArea = QueryAreaLogic.GetResourceQueryArea(QueryID, this.ResourceType, this.ResourceCode);
            this.rblAreaType.SelectedValue = this.CurrentQueryArea.AreaType;//载入选择机构范围



            //返回按钮
            this.hylReturn.NavigateUrl = this.ActionHref("Default.aspx?ResourceType=" + this.ResourceType);

            //单机构版本隐藏“组织机构”列
            if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
            {
                this.GridViewList.Columns[3].Visible = false;
            }

            //add 2012-12-26 begin

            this.ddl_DepartmentID.Items.Insert(0, new ListItem("全部", ""));
            this.ddl_DepartmentID.SelectedIndex = 0;
            this.ddl_PostID.Items.Insert(0, new ListItem("全部", ""));
            this.ddl_PostID.SelectedIndex = 0;




            QueryArea = QueryAreaLogic.GetById(this.CurrentQueryArea.QueryAreaID);


            initSelOrg(sender, e);


            //2、载入页签2 “高级配置”
            this.PageSet1.QueryChange();
        }
        this.lbtnImport.Attributes["onclick"] = string.Format("javascript:showWindow('导入学员','{0}',500,350);javascript:return false;", this.ActionHref("ImportStudent.aspx?QueryID=" + QueryID + "&orgtype=" + rblAreaType.SelectedValue));

        //add 2012-12-27 end
    }

    private void LoadOrgTree(Node node, DropDownList ddl)
    {
        string nodeID = node.NodeID.ToString();
        string nodeName = "|--" + "".PadLeft(node.NodeCode.Length, '-') + node.NodeName;
        this.ddl_OrganizationID.Items.Add(new ListItem(nodeName, nodeID));
        foreach (Node child in node.ChildNodes)
        {
            LoadOrgTree(child, ddl);
        }
    }
    private IList getDataSource1(int pageIndex, int pageSize, out int totalRecords)
    {
        //1、载入所选(全部）学员列表
        IList<Poll_QueryAreaDetail> details = QueryAreaLogic.GetResourceQueryAreaDetail(this.CurrentQueryArea.QueryAreaID, 1, int.MaxValue - 1, out totalRecords);
        ETMS.Components.Basic.Implement.BLL.Security.Site_StudentLogic studentLogic = new ETMS.Components.Basic.Implement.BLL.Security.Site_StudentLogic();
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.Columns.Add("QueryAreaDetailID", typeof(int));
        dt.Columns.Add("UserID", typeof(int));
        dt.Columns.Add("WorkerNo", typeof(string));
        dt.Columns.Add("LoginName", typeof(string));
        dt.Columns.Add("RealName", typeof(string));
        dt.Columns.Add("OrganizationID", typeof(int));
        dt.Columns.Add("DepartmentID", typeof(int));
        dt.Columns.Add("RankID", typeof(int));
        dt.Columns.Add("PostID", typeof(int));
        dt.Columns.Add("Email", typeof(string));

        foreach (Poll_QueryAreaDetail item in details)
        {
            ETMS.Components.Basic.API.Entity.Security.Site_Student studentInfo = studentLogic.GetById(int.Parse(item.DetailCode));
            System.Data.DataRow row = dt.NewRow();
            row.ItemArray = new object[]
            {
               item.QueryAreaDetailID,
               studentInfo.UserID,
               studentInfo.WorkerNo,
               studentInfo.LoginName,
               studentInfo.RealName,
               studentInfo.OrganizationID,
               studentInfo.DepartmentID,
               studentInfo.RankID,
               studentInfo.PostID,
               studentInfo.Email
            };
            dt.Rows.Add(row);
        }
        //排序   
        System.Data.DataView dv = dt.DefaultView;
        dv.Sort = " OrganizationID,DepartmentID,RealName ";
        // modify 2012-12-06 hjy

        string filter = "1=1";
        if (!string.IsNullOrEmpty(this.txtRealName.Text))
        {
            filter += string.Format(" AND RealName like '%{0}%'", this.txtRealName.Text.ToSafeSQLValue());
        }
        if (!string.IsNullOrEmpty(this.txtWorkerNo.Text))
        {
            filter += string.Format(" AND WorkerNo like '%{0}%'", this.txtWorkerNo.Text.ToSafeSQLValue());
        }
        //add 2012-12-26 hjy begin
        if (ddl_OrganizationID.SelectedValue != "0")
        {
            filter += string.Format(" AND OrganizationID='{0}'", this.ddl_OrganizationID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddl_PostID.SelectedValue))
        {
            filter += string.Format(" AND PostID='{0}'", this.ddl_PostID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddl_DepartmentID.SelectedValue))
        {
            filter += string.Format(" AND DepartmentID='{0}'", this.ddl_DepartmentID.SelectedValue);
        }
        if (ddlRank.SelectedValue != "-1")
        {
            filter += string.Format(" AND RankID='{0}'", this.ddlRank.SelectedValue);
        }
        //add 2012-12-26 hjy end 
        dv.RowFilter = filter;
        totalRecords = dv.Count;
        totalCount = totalRecords;
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dv, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int execCount = 0;
        int[] queryDetailIDs = CustomGridView.GetSelectedValues<int>(this.GridViewList);
        if (queryDetailIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择学员后删除！");
            return;
        }
        try
        {
            QueryAreaDetailLogic.Remove(queryDetailIDs, QueryID, out execCount);
            this.PageSet1.DataBind();//刷新数据
            ETMS.Utility.JsUtility.SuccessMessageBox(execCount + "学员删除成功，" + (queryDetailIDs.Length - execCount) + "学员已经提交问卷无法删除！");
            return;
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {

        try
        {
            //string filter = "";
            //if (!string.IsNullOrEmpty(this.txtRealName.Text))
            //{
            //    filter += string.Format(" AND su.RealName like '%{0}%'", this.txtRealName.Text.ToSafeSQLValue());
            //}
            //if (!string.IsNullOrEmpty(this.txtWorkerNo.Text))
            //{
            //    filter += string.Format(" AND ss.WorkerNo like '%{0}%'", this.txtWorkerNo.Text.ToSafeSQLValue());
            //}
            ////add 2012-12-26 hjy begin
            //if (ddl_OrganizationID.SelectedValue != "-1")
            //{
            //    filter += string.Format(" AND su.OrganizationID='{0}'", this.ddl_OrganizationID.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddl_PostID.SelectedValue))
            //{
            //    filter += string.Format(" AND ss.PostID='{0}'", this.ddl_PostID.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddl_DepartmentID.SelectedValue))
            //{
            //    filter += string.Format(" AND su.DepartmentID='{0}'", this.ddl_DepartmentID.SelectedValue);
            //}
            //if (ddlRank.SelectedValue != "-1")
            //{
            //    filter += string.Format(" AND ss.RankID='{0}'", this.ddlRank.SelectedValue);
            //}

            if (totalCount == 0)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("没有删除的学员！");
                return;
            }
            int j = 0;
            int i = QueryAreaDetailLogic.DeleteAllStudent("", this.CurrentQueryArea.QueryAreaID, out j);
            this.PageSet1.DataBind();//刷新数据
            ETMS.Utility.JsUtility.SuccessMessageBox(i + "学员删除成功，" + (j - i) + "学员已经提交问卷无法删除！");
            return;
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(bizEx.Message);
        }
    }
    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(GridViewList.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //更新机构范围
            if (new Poll_UserResourceQueryResultLogic().GetQueryUserCount(this.QueryID) > 0)
            {
                int i = 0;
                if (this.CurrentQueryArea.AreaType == "SubOrg" && this.rblAreaType.SelectedValue == "CurrentOrg")
                {
                    i = QueryAreaLogic.GetStudentNumFromQueryArea(this.QueryID);
                    if (i > 0)
                    {
                        ETMS.Utility.JsUtility.AlertMessageBox("下级组织机构已经有" + i + "人选入调查，请手动删除后再变更机构范围！");
                        this.rblAreaType.SelectedValue = "SubOrg";
                        return;
                    }
                }
                else if (this.CurrentQueryArea.AreaType == "CurrentOrg" && this.rblAreaType.SelectedValue == "SubOrg")
                {
                    i = QueryAreaLogic.GetStudentNumFromQueryArea(this.QueryID);
                    if (i > 0)
                    {
                        ETMS.Utility.JsUtility.AlertMessageBox("本组织机构已经有" + i + "人选入调查，请手动删除后再变更机构范围！");
                        this.rblAreaType.SelectedValue = "CurrentOrg";
                        return;
                    }
                }
                else if (this.CurrentQueryArea.AreaType == "AllOrg" && this.rblAreaType.SelectedValue == "SubOrg")
                {
                    i = QueryAreaLogic.GetStudentNumFromQueryAreaByOrg(this.QueryID, UserContext.Current.OrganizationID);
                    if (i > 0)
                    {
                        ETMS.Utility.JsUtility.AlertMessageBox("本组织机构已经有" + i + "人选入调查，请手动删除后再变更机构范围！");
                        this.rblAreaType.SelectedValue = "AllOrg";
                        return;
                    }
                }
                else if (this.CurrentQueryArea.AreaType == "AllOrg" && this.rblAreaType.SelectedValue == "CurrentOrg")
                {
                    i = QueryAreaLogic.GetNoStudentNumFromQueryAreaByOrg(this.QueryID, UserContext.Current.OrganizationID);
                    if (i > 0)
                    {
                        ETMS.Utility.JsUtility.AlertMessageBox("下级组织机构已经有" + i + "人选入调查，请手动删除后再变更机构范围！");
                        this.rblAreaType.SelectedValue = "AllOrg";
                        return;
                    }
                }
            }
            this.CurrentQueryArea.AreaType = this.rblAreaType.SelectedValue;
            QueryAreaLogic.Save(this.CurrentQueryArea);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("提示", "机构范围设置成功！");

            return;
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    private void initSelOrg(object sender, EventArgs e)
    {
        this.ddl_OrganizationID.Items.Clear();
        ddl_OrganizationID.Items.Insert(0, new ListItem("全部", "0"));
        ddl_OrganizationID.SelectedIndex = 0;
        Node parent = new Organization()
        {
            OrganizationID = int.Parse(QueryArea.AreaCode),
            OrganizationName = OrgLogic.GetNodeByID(int.Parse(QueryArea.AreaCode)).NodeName,
        };
        if (this.CurrentQueryArea.AreaType == EnumQueryAreaType.CurrentOrg.ToString())//仅本机构
        {
            this.ddl_OrganizationID.Items.Add(new ListItem(parent.NodeName, parent.NodeID.ToString()));
        }
        else if (this.CurrentQueryArea.AreaType == EnumQueryAreaType.SubOrg.ToString())//仅下级机构
        {
            OrgLogic.GetNodeTree(parent, true);
            foreach (Node child in parent.ChildNodes)
            {
                LoadOrgTree(child, this.ddl_OrganizationID);
            }
            //#bug，如果没有下级机构，则提示
            if (this.ddl_OrganizationID.Items.Count == 0)
            {
                this.ddl_OrganizationID.Items.Insert(0, new ListItem("当前机构没有下级机构！", "-1"));
            }
        }
        else if (this.CurrentQueryArea.AreaType == EnumQueryAreaType.AllOrg.ToString())//本机构及下属机构
        {
            OrgLogic.GetNodeTree(parent, true);
            LoadOrgTree(parent, this.ddl_OrganizationID);
        }

        ddl_OrganizationID.SelectedIndex = 0;
        ddl_OrganizationID_SelectedIndexChanged(sender, e);//触发Selected事件
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecords = 0;
        this.GridViewList.Columns.RemoveAt(0);//移除全选
        this.GridViewList.DataSource = this.getDataSource1(1, int.MaxValue - 1, out totalRecords);
        this.GridViewList.DataBind();
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("调查学员.xls", this.GridViewList);
    }

    /// <summary>
    /// 机构选中事件
    /// </summary>
    protected void ddl_OrganizationID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DepartmentLogic DepamentLogic = new DepartmentLogic();
        Dic_PostLogic PostLogic = new Dic_PostLogic();
        //载入选中机构下部门，岗位数据
        int orgID = int.Parse(this.ddl_OrganizationID.SelectedValue);
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddl_DepartmentID.DataSource = dt;
        this.ddl_DepartmentID.DataTextField = "ColumnNameValue";
        this.ddl_DepartmentID.DataValueField = "ColumnCodeValue";
        if (dt.Rows.Count > 0)
        {
            this.ddl_DepartmentID.DataBind();
        }

        this.ddl_DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_DepartmentID.SelectedIndex = 0;

        dt = PostLogic.GetAllEnablePostByOrgID(orgID);
        this.ddl_PostID.DataSource = dt;
        this.ddl_PostID.DataTextField = "ColumnNameValue";
        this.ddl_PostID.DataValueField = "ColumnCodeValue";
        if (dt.Rows.Count > 0)
        {
            this.ddl_PostID.DataBind();
        }

        this.ddl_PostID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_PostID.SelectedIndex = 0;
    }
}