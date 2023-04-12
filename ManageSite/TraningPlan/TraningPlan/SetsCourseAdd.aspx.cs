using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan.Course;

namespace ETMS.WebApp.Manage.TraningPlan.TraningPlan
{
    public partial class SetsCourseAdd : System.Web.UI.Page
    {
        #region 页面参数

        /// <summary>
        /// 计划ID
        /// </summary>
        private Guid PlanID
        {
            get
            {
                if (ViewState["PlanID"] == null)
                    ViewState["PlanID"] = Guid.Empty;
                return ViewState["PlanID"].ToGuid();
            }
            set
            {
                ViewState["PlanID"] = value;
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.CustomGridView1, PageDataSource);
            if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "PlanID")))
            {
                PlanID = BasePage.getSafeRequest(this.Page, "PlanID").ToGuid();
                this.PageSet1.QueryChange();
            }
            aBack.HRef = this.ActionHref("SetsCourse.aspx?PlanID=" + PlanID);
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            Tr_PlanCourseLogic logic = new Tr_PlanCourseLogic();
            Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
            DataTable dt = logic.GetNoSelectCourseListByPlanID(ETMS.AppContext.UserContext.Current.OrganizationID
                , PlanID
                , pageIndex
                , pageSize
                , SortExpression
                , Crieria                
                , out totalRecordCount);

            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            this.PageSet1.QueryChange();
        }
        /// <summary>
        /// 添加
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            if (selectedValues.Length == 0)
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的课程！");
                return;
            }
            else
            {
                try
                {
                    Tr_PlanCourseLogic courseLogic = new Tr_PlanCourseLogic();
                    courseLogic.BatchAdd(PlanID
                        , selectedValues
                        , ddlTrainingModel.SelectedValue.ToInt()
                        , ddlTeachModelID.SelectedValue.ToInt()
                        , txtBudgetFee.Text.ToDecimal()
                        , ETMS.AppContext.UserContext.Current.UserID
                        , ETMS.AppContext.UserContext.Current.RealName);
                    ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "课程添加成功！", "function(){window.location = '" + this.ActionHref("SetsCourse.aspx?PlanID=" + PlanID) + "'}");
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
        }
}
}