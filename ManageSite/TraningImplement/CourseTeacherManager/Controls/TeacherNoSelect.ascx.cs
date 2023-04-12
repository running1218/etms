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

public partial class TraningImplement_CourseTeacherManager_Controls_TeacherNoSelect : System.Web.UI.UserControl
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

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange(); 
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseTeacherLogic ItemCourseTeacherLogic = new Tr_ItemCourseTeacherLogic();
        DataTable dt = ItemCourseTeacherLogic.GetNoSelectTeacherListByItemCourseIDOrgID(TrainingItemCourseID,ETMS.AppContext.UserContext.Current.OrganizationID, out totalRecordCount);

        //如果查询条件不为空时
        if (txt_RealName.Text.Trim() != "") { 
            DataTable tab = dt.Clone();
            DataRow[] rows = dt.Select("RealName like '%"+txt_RealName.Text+"%'");
            //列赋值
            foreach (DataRow row in rows)
            {
                DataRow r = tab.NewRow();
                r.ItemArray = row.ItemArray;
                tab.Rows.Add(r);
            }
            totalRecordCount = tab.Rows.Count;
            dt = tab.Copy();
        }
        
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
            ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的讲师！");
            return;
        }
        else
        {
            try
            {
                Tr_ItemCourseTeacherLogic ItemCourseTeacherLogic = new Tr_ItemCourseTeacherLogic();
                ItemCourseTeacherLogic.BatchAdd(TrainingItemCourseID, selectedValues);
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "讲师添加成功！", "function(){window.location = window.location;}");
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