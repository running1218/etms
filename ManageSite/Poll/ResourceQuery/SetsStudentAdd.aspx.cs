using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using System.Text;
public partial class Poll_ResourceQuery_SetsStudentAdd : BasePage
{
    private static Poll_QueryAreaLogic QueryAreaLogic = new Poll_QueryAreaLogic();
    private static OrganizationLogic OrgLogic = new OrganizationLogic();
    private static DepartmentLogic DepamentLogic = new DepartmentLogic();
    private static Dic_PostLogic PostLogic = new Dic_PostLogic();
    private static Site_StudentLogic StudentLogic = new Site_StudentLogic();
    private static Poll_QueryAreaDetailLogic QueryAreaDetailLogic = new Poll_QueryAreaDetailLogic();
    private static int totalCount = 0;
    #region 页面参数
    /// <summary>
    /// QueryAreaID
    /// </summary>
    public int QueryAreaID
    {
        get
        {
            return int.Parse(Request.QueryString["QueryAreaID"]);
        }
    }

    public Poll_QueryArea QueryArea
    {
        get
        {
            return (Poll_QueryArea)ViewState["QueryArea"];
        }
        set
        {
            ViewState["QueryArea"] = value;
        }
    }

    public Poll_QueryPublishObject PublishObject
    {
        get
        {
            return (Poll_QueryPublishObject)ViewState["PublishObject"];
        }
        set
        {
            ViewState["PublishObject"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            this.QueryArea = QueryAreaLogic.GetById(this.QueryAreaID);

            Poll_QueryPublishObjectLogic publishObjectLogic = new Poll_QueryPublishObjectLogic();
            this.PublishObject = publishObjectLogic.GetById(this.QueryArea.QueryPublishID);

            Node parent = new Organization()
            {
                OrganizationID = int.Parse(this.QueryArea.AreaCode),
                OrganizationName = OrgLogic.GetNodeByID(int.Parse(this.QueryArea.AreaCode)).NodeName,
            };
            if (this.QueryArea.AreaType == EnumQueryAreaType.CurrentOrg.ToString())//仅本机构
            {
                this.ddlOrg.Items.Add(new ListItem(parent.NodeName, parent.NodeID.ToString()));
            }
            else if (this.QueryArea.AreaType == EnumQueryAreaType.SubOrg.ToString())//仅下级机构
            {
                OrgLogic.GetNodeTree(parent, true);
                foreach (Node child in parent.ChildNodes)
                {
                    LoadOrgTree(child, this.ddlOrg);
                }
                //#bug，如果没有下级机构，则提示
                if (this.ddlOrg.Items.Count == 0)
                {
                    this.ddlOrg.Items.Insert(0, new ListItem("当前机构没有下级机构！", "-1"));
                }
            }
            else if (this.QueryArea.AreaType == EnumQueryAreaType.AllOrg.ToString())//本机构及下属机构
            {
                OrgLogic.GetNodeTree(parent, true);
                LoadOrgTree(parent, this.ddlOrg);
            }

            ddlOrg.SelectedIndex = 0;
            ddlOrg_SelectedIndexChanged(sender, e);//触发Selected事件

            this.PageSet1.QueryChange();
        }
    }

    private void LoadOrgTree(Node node, DropDownList ddl)
    {
        string nodeID = node.NodeID.ToString();
        string nodeName = "|--" + "".PadLeft(node.NodeCode.Length, '-') + node.NodeName;
        this.ddlOrg.Items.Add(new ListItem(nodeName, nodeID));
        foreach (Node child in node.ChildNodes)
        {
            LoadOrgTree(child, ddl);
        }
    }

    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        //载入选中机构下部门，岗位数据
        int orgID = int.Parse(this.ddlOrg.SelectedValue);
        DataTable dt = DepamentLogic.GetAllEnableDepartmentsByOrganizationID(orgID);
        this.ddlDepartment.DataSource = dt;
        this.ddlDepartment.DataTextField = "ColumnNameValue";
        this.ddlDepartment.DataValueField = "ColumnCodeValue";
        this.ddlDepartment.DataBind();
        this.ddlDepartment.Items.Insert(0, new ListItem("全部", "-1"));
        this.ddlDepartment.SelectedIndex = 0;

        dt = PostLogic.GetAllEnablePostByOrgID(orgID);
        this.ddlPost.DataSource = dt;
        this.ddlPost.DataTextField = "ColumnNameValue";
        this.ddlPost.DataValueField = "ColumnCodeValue";
        this.ddlPost.DataBind();
        this.ddlPost.Items.Insert(0, new ListItem("全部", "-1"));
        this.ddlPost.SelectedIndex = 0;
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
        int orgID = int.Parse(this.ddlOrg.SelectedValue);
        //int totalRecords;
        //string detailFilter = string.Format(" AND QueryAreaID={0}", this.QueryAreaID);
        //IList<Poll_QueryAreaDetail> details = QueryAreaDetailLogic.GetNoStudentList(1, 999999, "", detailFilter, out totalRecords);
        //查询机构下学员
        string filter = string.Format(" and users.OrganizationID={0} and users.status=1", orgID);//机构下启用学员
        ////排除已选学员
        //if (details.Count > 0)
        //{
        //    StringBuilder writer = new StringBuilder();
        //    writer.Append("(");
        //    foreach (Poll_QueryAreaDetail detailItem in details)
        //    {
        //        writer.AppendFormat("{0},", detailItem.DetailCode);//Student类型
        //    }
        //    writer.Append("0)");
        //    filter += " AND users.userID not in" + writer.ToString();
        //}
        if (this.ddlDepartment.SelectedValue != "-1")
        {
            filter += string.Format(" and users.DepartmentID={0}", this.ddlDepartment.SelectedValue);
        }
        if (this.ddlRank.SelectedValue != "-1")
        {
            filter += string.Format(" and students.RankID={0}", this.ddlRank.SelectedValue);
        }
        if (this.ddlPost.SelectedValue != "-1")
        {
            filter += string.Format(" and students.PostID={0}", this.ddlPost.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.txtRealName.Text))
        {
            filter += string.Format(" and users.RealName like '%{0}%'", this.txtRealName.Text.ToSafeSQLValue());
        }
        if (!string.IsNullOrEmpty(this.txtWorkerNo.Text))
        {
            filter += string.Format(" and students.WorkerNo like '%{0}%'", this.txtWorkerNo.Text.ToSafeSQLValue());
        }

        DataTable dt = QueryAreaDetailLogic.GetNoStudentList(pageIndex, pageSize, " users.DepartmentID,students.RankID,students.PostID,users.RealName ", filter, this.QueryAreaID, out totalRecordCount);
        totalCount = dt.Rows.Count;
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的学员！");
            return;
        }
        else
        {
            try
            {
                //循环插入
                foreach (int userID in selectedValues)
                {
                    QueryAreaDetailLogic.Add(new Poll_QueryAreaDetail()
                    {
                        CreateTime = DateTime.Now,
                        Creator = ETMS.AppContext.UserContext.Current.RealName,
                        QueryAreaID = this.QueryAreaID,
                        DetailCode = userID.ToString(),
                        DetailType = EnumQueryAreaDetailType.Student.ToString(),
                    });
                }
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "学员添加成功！", "function(){window.location = '" + this.ActionHref("SetArea_R2.aspx?QueryID=" + this.PublishObject.QueryID.ToString()) + "'}");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    protected void btnAddAll_Click(object sender, EventArgs e)
    {
        try
        {
            int orgID = int.Parse(this.ddlOrg.SelectedValue);
            //int totalRecords;
            //string detailFilter = string.Format(" AND QueryAreaID={0}", this.QueryAreaID);
            //IList<Poll_QueryAreaDetail> details = QueryAreaDetailLogic.GetNoStudentList(1, 999999, "", detailFilter, out totalRecords);
            //查询机构下学员
            string filter = string.Format(" and users.OrganizationID={0} and users.status=1", orgID);//机构下启用学员
            ////排除已选学员
            if (this.ddlDepartment.SelectedValue != "-1")
            {
                filter += string.Format(" and users.DepartmentID={0}", this.ddlDepartment.SelectedValue);
            }
            if (this.ddlRank.SelectedValue != "-1")
            {
                filter += string.Format(" and students.RankID={0}", this.ddlRank.SelectedValue);
            }
            if (this.ddlPost.SelectedValue != "-1")
            {
                filter += string.Format(" and students.PostID={0}", this.ddlPost.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.txtRealName.Text))
            {
                filter += string.Format(" and users.RealName like '%{0}%'", this.txtRealName.Text.ToSafeSQLValue());
            }
            if (!string.IsNullOrEmpty(this.txtWorkerNo.Text))
            {
                filter += string.Format(" and students.WorkerNo like '%{0}%'", this.txtWorkerNo.Text.ToSafeSQLValue());
            }
            if (totalCount == 0)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("没有添加的学员！");
                return;
            }

            QueryAreaDetailLogic.AddAllStudent("", filter, QueryAreaID, ETMS.AppContext.UserContext.Current.RealName);
            ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "学员添加成功！", "function(){window.location = '" + this.ActionHref("SetArea_R2.aspx?QueryID=" + this.PublishObject.QueryID.ToString()) + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(bizEx.Message);
            return;
        }
    }

}