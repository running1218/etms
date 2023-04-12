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
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.API;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Utility.Service.FileUpload;

namespace ETMS.WebApp.Manage
{
    public partial class Resource_CourseResource : BasePage
    {
        #region 页面参数
        /// <summary>
        /// 查询条件 
        /// </summary>
        private string criteria;
        protected string Crieria
        {
            get
            {
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
        private string sortExpression;
        protected string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                {
                    ViewState["SortExpression"] = " CreateTime DESC ";
                }
                return (string)ViewState["SortExpression"];
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }
        #endregion

        private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.rptCourseList, PageDataSource);

            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
            }
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
            Crieria += string.Format(" And OrgID = {0} ", UserContext.Current.OrganizationID);
            DataTable dt = courseLogic.GetQueryList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

            divMsg.Visible = totalRecordCount == 0;
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
        }

        protected void rptCourseList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblCourseID = (Label)e.Item.FindControl("lblCourseID");
            DataList dltResource = (DataList)e.Item.FindControl("dltJobList");
            Image imgLogo = (Image)e.Item.FindControl("imgLogo");
            CourseResourceInfo info = null;
          
            if (null != lblCourseID && null != dltResource)
            {
                info = new CourseResourceInfo(lblCourseID.Text.ToGuid());
                dltResource.DataSource = info.CourseResourceList;
                dltResource.DataBind();
            }

            if (null != imgLogo)
            {
                imgLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(imgLogo.ImageUrl) ? "default.jpg" : imgLogo.ImageUrl);
            }
        }
    }

    public class CourseResourceInfo
    {
        public Guid CourseID
        {
            get;
            set;
        }

        public CourseResourceInfo(Guid courseID)
        {
            CourseID = courseID;
        }

        public List<CourseResourceDetail> CourseResourceList
        {
            get
            {
                List<CourseResourceDetail> list = new List<CourseResourceDetail>();

                foreach (ICourseResourcesFacade courseResourceFacade in ETMS.AppContext.ApplicationContext.Current.ComponentRepository.GetBizComponentsByGroupID<ICourseResourcesFacade>())
                {
                    list.Add(new CourseResourceDetail()
                    {
                        ResourceName = string.Format("{0}：", courseResourceFacade.Name),
                        ResourceNum = courseResourceFacade.GetResourcesTotal(CourseID),
                        ResourceTotalNum = courseResourceFacade.GetALLResourcesTotal(CourseID),
                        FunctionUrl =ETMS.Utility.HrefUtility.ActionHref(string.Format(courseResourceFacade.ManageAppHome, CourseID))
                    });
                }

                return list;
            }
        }
    }
}