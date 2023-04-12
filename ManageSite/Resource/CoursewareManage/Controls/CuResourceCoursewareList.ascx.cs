using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Components.Courseware.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.WebApp.Manage
{
    public partial class CuResourceCoursewareList : System.Web.UI.UserControl
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
                {
                    ViewState["Crieria"] = "";
                }
                return (string)ViewState["Crieria"];
            }
            set
            {
                ViewState["Crieria"] = value;
            }
        }

        public Guid CourseID
        {
            get;
            set;
        }

        /// <summary>
        /// 排序条件 " CoursewareName "
        /// </summary>
        
        protected string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = " CoursewareName ";
                }
                return (string)ViewState["SortExpression"];
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.CustomGridView1, PageDataSource);

            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
                InitialControl();
            }

            btnAddScorm.Attributes.Add("onclick", string.Format("javascript:showWindow('新增SCORM标准','{0}')", this.ActionHref(string.Format("~/Resource/CoursewareManage/CoursewareAddScorm.aspx?CourseID={0}", CourseID))));
            btnAddNoScorm.Attributes.Add("onclick", string.Format("javascript:showWindow('新增非SCORM标准','{0}')", this.ActionHref(string.Format("~/Resource/CoursewareManage/CoursewareAdd.aspx?op=add&CourseID={0}", CourseID))));
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Crieria = string.Format(" And CourseID='{0}' ", CourseID);
            DataTable dt = new Res_CoursewareLogic().Res_CoursewareGetPagedList(pageIndex, pageSize, " CreateTime desc", Crieria, out totalRecordCount);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        //查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
            upList.Update();
        }

        private void InitialControl()
        {
            Res_Course entity = new Res_CourseLogic().GetById(CourseID);
            if (null != entity)
            {
                lblCourseCode.Text = entity.CourseCode;
                lblCourseName.Text = entity.CourseName;
            }
        }
        
        //单个删除课程信息
        protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                try
                {
                    Res_CoursewareLogic coursewareLogic = new Res_CoursewareLogic();                   
                    coursewareLogic.RemoveCourseCourseware(e.CommandArgument.ToString().ToGuid());
                    this.PageSet1.QueryChange();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
        }
        protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && !this.CustomGridView1.IsEmpty)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                LinkButton lblScormEdit = e.Row.FindControl("lblScormEdit") as LinkButton;
                LinkButton lblScormView = e.Row.FindControl("lblScormView") as LinkButton;
                LinkButton lblEdited = e.Row.FindControl("lblEdited") as LinkButton;
                LinkButton lblViewed = e.Row.FindControl("lblViewed") as LinkButton;
                if (drv["CoursewareTypeID"].ToString() == "1")
                {
                    lblEdited.Visible = false;
                    lblViewed.Visible = false;
                    lblScormEdit.Attributes.Add("onclick", string.Format("javascript:showWindow('编辑SCORM标准课件','{0}');javascript: return false;", this.ActionHref(string.Format("{0}/Resource/CoursewareManage/CoursewareEditScorm.aspx?CoursewareID={1}", WebUtility.AppPath, drv["CoursewareID"].ToString()))));
                    lblScormView.Attributes.Add("onclick", string.Format("javascript:showWindow('查看SCORM标准','{0}');javascript: return false;", this.ActionHref(string.Format("{0}/Resource/CoursewareManage/CoursewareViewScorm.aspx?CoursewareID={1}", WebUtility.AppPath, drv["CoursewareID"].ToString()))));
                }
                else
                {
                    lblScormEdit.Visible = false;
                    lblScormView.Visible = false;
                    lblEdited.Attributes.Add("onclick", string.Format("javascript:showWindow('编辑非SCORM标准课件','{0}');javascript: return false;", this.ActionHref(string.Format("{0}/Resource/CoursewareManage/CoursewareEdit.aspx?CoursewareID={1}&op={2}&CourseID={3}", WebUtility.AppPath, drv["CoursewareID"].ToString(), "edit", drv["CourseID"].ToString()))));
                    lblViewed.Attributes.Add("onclick", string.Format("javascript:showWindow('查看非SCORM标准','{0}');javascript: return false;", this.ActionHref(string.Format("{0}/Resource/CoursewareManage/CoursewareView.aspx?CoursewareID={1}", WebUtility.AppPath, drv["CoursewareID"].ToString()))));

                }
            }
        }
    }
}