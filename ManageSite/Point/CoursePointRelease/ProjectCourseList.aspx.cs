using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Point.Implement.BLL;
using System.Data;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Utility;
using System.Collections;

public partial class Point_CoursePointRelease_ProjectCourseList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }
    }
    private StudentCoursePointLogic pointLogic = new StudentCoursePointLogic();
    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        ddl_ItemName.DataSource = pointLogic.GetCanIssueCoursePointTrainingItemListByOrgID(ETMS.AppContext.UserContext.Current.OrganizationID);
        ddl_ItemName.DataTextField = "ItemName";
        ddl_ItemName.DataValueField = "TrainingItemID";
        ddl_ItemName.DataBind();
        ddl_ItemName.Items.Insert(0, new ListItem("全部", ""));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        //当前组织机构下的数据
        string criteria = string.Format(" AND c.OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        string sort = "c.ItemName,e.CourseName, u.OrganizationID,u.DepartmentID,u.RealName";//项目名称、课程名称、组织机构、部门、学员姓名排序
        DataTable dt = null;
        if (ddl_ItemName.SelectedValue != "")
        {
            criteria += string.Format(" And c.TrainingItemID='{0}'", ddl_ItemName.SelectedValue);
        }
        if (txt_CourseName.Text != "")
        {
            criteria += string.Format(" And e.CourseName like '%{0}%'", txt_CourseName.Text);
        }
        dt = pointLogic.GetNoIssueStudentCoursePointAllInfoList(pageIndex, pageSize, sort, criteria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 发布积分
    /// </summary>
    protected void btnPoint_Click(object sender, EventArgs e)
    {
        Guid[] StudentCourseIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (StudentCourseIDs.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要发布积分的课程！");
            return;
        }
        else
        {
            try
            {
                 int resultCount=pointLogic.BatchIssueCoursePointByStudentCourseID(StudentCourseIDs
                   , ETMS.AppContext.UserContext.Current.RealName
                   , ETMS.AppContext.UserContext.Current.UserID);
                ETMS.Utility.JsUtility.SuccessMessageBox("提示", string.Format("共{0}条积分发布成功！",resultCount), "function(){window.location =window.location}");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
    /// <summary>
    /// 全部发布
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllPoint_Click(object sender, EventArgs e)
    {
        //当前组织机构下的数据
        string criteria = string.Format(" AND c.OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);

        if (ddl_ItemName.SelectedValue != "")
        {
            criteria += string.Format(" And c.TrainingItemID='{0}'", ddl_ItemName.SelectedValue);
        }
        if (txt_CourseName.Text != "")
        {
            criteria += string.Format(" And e.CourseName like '%{0}%'", txt_CourseName.Text);
        }
        try
        {
          int resultCount= pointLogic.IssueCoursePointByConditionSQL(criteria
                , ETMS.AppContext.UserContext.Current.RealName
                , ETMS.AppContext.UserContext.Current.UserID);
          if (resultCount > 0)
          {
              ETMS.Utility.JsUtility.SuccessMessageBox("提示", string.Format("共{0}条积分发布成功！", resultCount), "function(){window.location =window.location}");
          }
          else
          {
              ETMS.Utility.JsUtility.AlertMessageBox("提示", "抱歉，没有课发布的积分列表！", "function(){window.location =window.location}");
          }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
}