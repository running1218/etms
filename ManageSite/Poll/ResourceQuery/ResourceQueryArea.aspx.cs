using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.Collections;
using System.Text;
using System.IO;

public partial class Poll_ResourceQuery_ResourceQueryArea : System.Web.UI.Page
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

    private static OrganizationLogic OrgLogic = new OrganizationLogic();
    private Poll_QueryArea QueryArea = new Poll_QueryArea();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PageSet1.pageInit(this.GridViewList, new IPageDataSource(getDataSource1));
        this.PageSet2.pageInit(this.OrgGridView, new IPageDataSource(GetOrgDataSource));
        if (!Page.IsPostBack)
        {
            //问卷信息
            Poll_Query entity = QueryLogic.GetById(this.QueryID);
            //this.lblQueryName.Text = entity.QueryName;

            //1、载入页签1“默认配置”
            this.CurrentQueryArea = QueryAreaLogic.GetResourceQueryArea(QueryID, this.ResourceType, this.ResourceCode);
            //this.rblAreaType.SelectedValue = this.CurrentQueryArea.AreaType;//载入选择机构范围


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
            this.lbtnImport.Attributes["onclick"] = string.Format("javascript:showWindow('导入学员','{0}',500,350);javascript:return false;", this.ActionHref("ImportStudent.aspx?QueryID=" + QueryID));

            QueryArea = QueryAreaLogic.GetById(this.CurrentQueryArea.QueryAreaID);

            //initSelOrg(sender, e);9
            //2、载入页签2 “高级配置”
            this.PageSet1.QueryChange();
            this.PageSet2.QueryChange();
        }

        //add 2012-12-27 end
    }


    private IList GetOrgDataSource(int pageIndex, int pageSize, out int totalRecords)
    {
        totalRecords = 0;
        StringBuilder sb = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(txtOrgCode.Text))
        {
            sb.AppendFormat(" AND OrganizationCode='{0}'", txtOrgCode.Text);
        }
        if (!string.IsNullOrWhiteSpace(txtOrgName.Text))
        {
            sb.AppendFormat(" AND OrganizationName='{0}'", txtOrgName.Text);
        }
        System.Data.DataTable dt = QueryAreaLogic.GetSelectOrgInfoByQueryID(QueryID, 1, 10, "", sb.ToString(), out totalRecords);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void btnSearchOrgClick(object sender, EventArgs e)
    {
        this.PageSet2.QueryChange();
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

        string filter = "";
        if (!string.IsNullOrEmpty(this.txtRealName.Text))
        {
            filter += string.Format(" AND u.RealName like '%{0}%'", this.txtRealName.Text.ToSafeSQLValue());
        }
        if (!string.IsNullOrEmpty(this.txtWorkerNo.Text))
        {
            filter += string.Format(" AND s.WorkerNo like '%{0}%'", this.txtWorkerNo.Text.ToSafeSQLValue());
        }
        //add 2012-12-26 hjy begin
        if (ddl_OrganizationID.SelectedValue != "-1")
        {
            filter += string.Format(" AND u.OrganizationID={0}", this.ddl_OrganizationID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddl_PostID.SelectedValue))
        {
            filter += string.Format(" AND s.PostID={0}", this.ddl_PostID.SelectedValue);
        }
        if (!string.IsNullOrEmpty(ddl_DepartmentID.SelectedValue))
        {
            filter += string.Format(" AND u.DepartmentID={0}", this.ddl_DepartmentID.SelectedValue);
        }
        if (ddlRank.SelectedValue != "-1")
        {
            filter += string.Format(" AND s.RankID={0}", this.ddlRank.SelectedValue);
        }

        DataTable dt = QueryAreaLogic.GetSelectStudentInfoByQueryID(QueryID, 1, 10, "", filter, out totalRecords);

        //add 2012-12-26 hjy end 

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
        UpdatePanel3.Update();
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
            QueryAreaDetailLogic.Remove(queryDetailIDs, out execCount);
            this.PageSet1.DataBind();//刷新数据
            ETMS.Utility.JsUtility.SuccessMessageBox(execCount + "学员删除成功，" + (queryDetailIDs.Length - execCount) + "学员已经提交问卷无法删除！");
            return;
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }


    protected void btnDeleteOrgClick(object sender, EventArgs e)
    {
        int[] queryDetailIDs = CustomGridView.GetSelectedValues<int>(this.OrgGridView);
        if (queryDetailIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择机构后删除！");
            return;
        }
        try
        {
            var execCount = 0;
            foreach (var i in queryDetailIDs)
            {
                if (QueryAreaLogic.DeleteUnOrg(i) > 0)
                {
                    execCount++;
                }
            }
            this.PageSet2.DataBind();//刷新数据
            ETMS.Utility.JsUtility.SuccessMessageBox(execCount + "学员删除成功，" + (queryDetailIDs.Length - execCount) + "学员已经提交问卷无法删除！");
            return;
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //更新机构范围
            //this.CurrentQueryArea.AreaType = this.rblAreaType.SelectedValue;
            QueryAreaLogic.Save(this.CurrentQueryArea);
            ETMS.Utility.JsUtility.SuccessMessageBox("机构范围设置成功！");

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
        Node parent = new Organization()
        {
            OrganizationID = int.Parse(QueryArea.AreaCode),
            OrganizationName = OrgLogic.GetNodeByID(int.Parse(QueryArea.AreaCode)).NodeName,
        };

        OrgLogic.GetNodeTree(parent, true);
        LoadOrgTree(parent, this.ddl_OrganizationID);

        ddl_OrganizationID.SelectedIndex = 0;
        ddl_OrganizationID_SelectedIndexChanged(sender, e);//触发Selected事件
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int totalRecords = 0;
        this.GridViewList.Columns.RemoveAt(0);//移除全选
        this.GridViewList.DataSource = this.getDataSource1(1, int.MaxValue-1, out totalRecords);
        this.GridViewList.DataBind();
       
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("调查学员.xls", this.GridViewList);
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
    //    private void ToExcel( this.GridViewList, ByVal FileName As String)
    //{

    //}
    //        Response.Clear()
    //        Response.Buffer = True

    //        Response.Charset = "utf-8"

    //        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName + Date.Now.ToString() + ".xls")
    //        Response.ContentType = "application/ms-excel"
    //        Dim strWriter As New StringWriter()
    //        Dim htw As New HtmlTextWriter(strWriter)
    //        ctl.RenderControl(htw)
    //        Response.Write(strWriter.ToString)
    //        Response.End()



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
        this.ddl_DepartmentID.DataBind();




        this.ddl_DepartmentID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_DepartmentID.SelectedIndex = 0;

        dt = PostLogic.GetAllEnablePostByOrgID(orgID);
        this.ddl_PostID.DataSource = dt;
        this.ddl_PostID.DataTextField = "ColumnNameValue";
        this.ddl_PostID.DataValueField = "ColumnCodeValue";
        this.ddl_PostID.DataBind();




        this.ddl_PostID.Items.Insert(0, new ListItem("全部", ""));
        this.ddl_PostID.SelectedIndex = 0;
    }

}