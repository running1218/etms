using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.OnlinePlaying.API;
using ETMS.Components.OnlinePlaying.Implement.BLL;
using ETMS.Controls;
using ETMS.AppContext;

public partial class Living_LivingList : System.Web.UI.Page
{
    public Guid CourseID
    {
        get
        {
            return this.Request.ToparamValue<Guid>("CourseID");
        }
    }

    public int LivingCount
    {
        get
        {
            return logic.GetLivingByCourseID(CourseID).Count;
        }
    }

    private static readonly OnlinePlayingLogic logic = new OnlinePlayingLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblCourseName.Text = new ETMS.Components.Basic.Implement.BLL.Course.Res_CourseLogic().GetById(CourseID).CourseName;
        //this.btnAdd.Attributes.Add("onclick", string.Format("javascript: showWindow('新增', '{0}', 480, 360)", this.ActionHref(string.Format("LivingAdd.aspx?CourseID={0}", CourseID))));
        PageSet1.pageInit(gvList, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
                
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        var data = logic.GetLivingPageList(CourseID, pageIndex, pageSize, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(data, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }
    /// <summary>
    /// 删除课程信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            try
            {
                logic.DeleteLiving(e.CommandArgument.ToString());
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            }
        }
    }
}