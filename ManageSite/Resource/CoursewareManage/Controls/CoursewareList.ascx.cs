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
using ETMS.Components.Courseware.API.Entity;

namespace ETMS.WebApp.Manage
{
    public partial class CoursewareList : System.Web.UI.UserControl
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

        /// <summary>
        /// 是否启用SCORM课件支持
        /// </summary>
        protected bool IsEnableScormCourseWare
        {
            get
            {
                return ETMS.Product.ProductComponentStrategy.IsSupport(Product.ExtendComponentType.CourseWare_SCORM);
            }
        }
        /// <summary>
        /// 是否启用非SCORM课件支持
        /// </summary>
        protected bool IsEnableNotScormCourseWare
        {
            get
            {
                return ETMS.Product.ProductComponentStrategy.IsSupport(Product.ExtendComponentType.CourseWare_NotSCORM);
            }
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
            Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
            Crieria += string.Format(" and OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);

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
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    //JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
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
                LinkButton lblEdited = e.Row.FindControl("lblEdited") as LinkButton;
                
                if (drv["CoursewareTypeID"].ToString() == "1")
                {
                    lblEdited.Visible = false;
                    lblScormEdit.Attributes.Add("onclick", string.Format("javascript:showWindow('编辑SCORM标准课件','{0}');javascript: return false;", this.ActionHref(string.Format("{0}/Resource/CoursewareManage/CoursewareEditScorm.aspx?CoursewareID={1}",WebUtility.AppPath, drv["CoursewareID"].ToString()))));
                }
                else
                {
                    lblScormEdit.Visible = false;
                    lblEdited.Attributes.Add("onclick", string.Format("javascript:showWindow('编辑非SCORM标准课件','{0}');javascript: return false;", this.ActionHref(string.Format("{0}/Resource/CoursewareManage/CoursewareEdit.aspx?CoursewareID={1}&op={2}&CourseID={3}", WebUtility.AppPath,drv["CoursewareID"].ToString(), "edit", drv["CourseID"].ToString()))));
                }
                
            }
        }

    }
}