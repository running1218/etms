using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
using ETMS.Utility;
using System.Data;

public partial class TraningImplement_CourseTeacherManager_Controls_TeacherSelect : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 项目课程ID
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }

    #endregion
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
        Tr_ItemCourseTeacherLogic ItemCourseTeacherLogic = new Tr_ItemCourseTeacherLogic();

        DataTable dt = ItemCourseTeacherLogic.GetSelectTeacherListByItemCourseIDOrgID(TrainingItemCourseID,ETMS.AppContext.UserContext.Current.OrganizationID, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 删除
    /// </summary>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        Guid[] ItemCourseTeacherIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (ItemCourseTeacherIDs.Length == 0)
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要删除的讲师！");
            return;
        }
        else
        {
            try
            {
                Tr_ItemCourseTeacherLogic ItemCourseTeacherLogic = new Tr_ItemCourseTeacherLogic();
                ItemCourseTeacherLogic.BatchRemoveItemCourseTeacher(ItemCourseTeacherIDs);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "讲师删除成功！", "function(){window.location = window.location;}");
                //this.PageSet1.DataBind(); 
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[4].Visible = false;
        }
    }
}