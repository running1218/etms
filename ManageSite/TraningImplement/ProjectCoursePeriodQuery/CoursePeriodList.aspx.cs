using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.API.Entity.ClassRoom;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.Teacher;

public partial class TraningImplement_ProjectCoursePeriodQuery_CoursePeriodList : System.Web.UI.Page
{
    #region 页面参数
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
            this.PageSet1.QueryChange();
        }
    }
    
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //A. 	机构非讲师的后台管理员，拥有本功能的权限，则可对本组织机构中所有已发布的启用的培训项目进行课时安排的查询
        Crieria = string.Format(" {0} AND f.IsIssue=1 AND f.IsUse=1 AND f.OrgID={1}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList),ETMS.AppContext.UserContext.Current.OrganizationID);
        //B. 	机构讲师则只看到自已负责的、本组织机构已发布的启用的培训项目课程的课时安排信息
        try
        {
            //如果当前用户出现在讲师表中 就为机构讲师
            Site_Teacher teacher = new Site_TeacherLogic().GetById(ETMS.AppContext.UserContext.Current.UserID);
            Crieria = string.Format(" {0} AND c.TeacherID={1}", Crieria, ETMS.AppContext.UserContext.Current.UserID);
        }catch{}

        Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
        DataTable dt = hoursLogic.GetItemCourseHoursALLInfoList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

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
            HiddenField hfTrainingItemCourseID = (HiddenField)e.Row.FindControl("hfTrainingItemCourseID");
            hfTrainingItemCourseID = hfTrainingItemCourseID == null ? new HiddenField() : hfTrainingItemCourseID;

            //查看
            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;
           
            //报名表
            LinkButton lbtnSignInTable = (LinkButton)e.Row.FindControl("lbtnSignInTable");
            lbtnSignInTable = lbtnSignInTable == null ? new LinkButton() : lbtnSignInTable;
                    
            //学员数
            Label lblStudentTotal = (Label)e.Row.FindControl("lblStudentTotal");
            lblStudentTotal = lblStudentTotal == null ? new Label() : lblStudentTotal;
            #endregion
            //查看
            lbtnView.Attributes["onclick"] = string.Format("javascript:showWindow('查看课时信息','{0}',650,500);javascript:return false;"
                    , this.ActionHref(string.Format("../ProjectCoursePeriod/CoursePeriodView.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}", hfTrainingItemCourseID.Value, itemCourseHoursID)));
            //课时学员数
            lblStudentTotal.Text = new Tr_ItemCourseHoursStudentLogic().GetItemCourseHoursStudentNumByItemCourseHoursID(itemCourseHoursID).ToString();
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //签到表
        if (e.CommandName == "SignInTable")
        {
            ExportFile(e.CommandArgument.ToGuid());
        }
    }

    #region 导出数据相关操作
    /// <summary>
    /// 导出数据
    /// </summary>
    /// <param name="itemCourseHoursID">项目课程课时ID</param>
    private void ExportFile(Guid itemCourseHoursID)
    {
        //列表邦定数据
        int totalRecordCount = 0;
        this.CustomGridViewExport.DataSource = PageDataSourceExport(itemCourseHoursID, 1, int.MaxValue - 1, out totalRecordCount);
        this.CustomGridViewExport.DataBind();

        #region 表头数据
        string content = @"<table width='100%' border='1'>
                              <tr>
                                 <td colspan='8' align='center' style='font-size:14; font-weight:bold'>培训签到表</td>
                              </tr>
                              <tr>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训项目</td>
                                <td colspan='6'>{0}</td>
                              </tr>
                              <tr>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训课程</td>
                                <td width='30' colspan='2'>{1}</td>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训讲师</td>
                                <td width='30' colspan='2'>{2}</td>
                              </tr>
                              <tr>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训时间</td>
                                <td width='30' colspan='2'>{3}</td>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训地点</td>
                                <td width='30' colspan='2'>{4}</td>
                              </tr>
                        </table>";

        //填充数据
        int total = 0;
        DataTable dt = new Tr_ItemCourseHoursLogic().GetItemCourseHoursALLInfoList(1, 1, "", string.Format(" AND a.ItemCourseHoursID='{0}'", itemCourseHoursID), out total);
        if (dt.Rows.Count > 0)
        {
            content = string.Format(content
                , dt.Rows[0]["ItemName"].ToString()
                , dt.Rows[0]["CourseName"].ToString()
                , dt.Rows[0]["TeacherName"].ToString()
                , dt.Rows[0]["TrainingDate"].ToDate() + "（" + dt.Rows[0]["TrainingBeginTime"].ToDateTime().ToString("HH:mm") + " - " + dt.Rows[0]["TrainingEndTime"].ToDateTime().ToString("HH:mm") + "）"
                , dt.Rows[0]["ClassRoomName"].ToString() + "（" + dt.Rows[0]["Address"].ToString() + "）"
                );
        }
        #endregion

        #region 读取列表数据
        //System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.CurrentCulture);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        CustomGridViewExport.RenderControl(oHtmlTextWriter);//将服务器控件的内容输出  
        content += oStringWriter.ToString();
        if (CustomGridViewExport is GridView)
            content = content.Replace("border=\"0\"", "border=\"1\"");
        #endregion
        //输出
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("培训签到表.xls", content);
    }


    private System.Collections.IList PageDataSourceExport(Guid ItemCourseHoursID, int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseHoursStudentLogic hoursLogic = new Tr_ItemCourseHoursStudentLogic();
        DataTable dt = hoursLogic.GetItemCourseHoursStudentByItemCourseHoursID(ItemCourseHoursID, pageIndex, pageSize, " u.OrganizationID,u.DepartmentID,u.RealName", "", out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(CustomGridViewExport.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }
    #endregion
}