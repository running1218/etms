using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using System.Text;
using ETMS.Utility;

namespace ETMS.WebApp.Manage.Resource.ProfessorManage.Controls
{
    public partial class ProfessorInfoInner : System.Web.UI.UserControl
    {
        private static Site_TeacherLogic teacherLogic = new Site_TeacherLogic();
        #region 页面条件参数存放

        /// <summary>
        /// 操作类型 1 Add 2 Edit
        /// </summary>
        public Int32 Operation
        {
            get
            {
                if (ViewState["Operation"] == null)
                {
                    ViewState["Operation"] = 1;
                }
                return (Int32)ViewState["Operation"];
            }
            set
            {
                ViewState["Operation"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSet1.pageInit(this.GridViewList, PageDataSource);
            if (!Page.IsPostBack)
            {
                this.PageSet1.QueryChange();
            }           
        }

        private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
        {
            StringBuilder whereStr = new StringBuilder();
            if (!string.IsNullOrEmpty(this.txtTeacherName.Text.Trim()))
                whereStr.Append(string.Format(" and a.RealName like '%{0}%'", this.txtTeacherName.Text.Trim().ToSafeSQLValue()));
            if (!string.IsNullOrEmpty(this.txtWorkerNo.Text.Trim()))
                whereStr.Append(string.Format(" and a.WorkerNo like '%{0}%'", this.txtWorkerNo.Text.Trim().ToSafeSQLValue()));
            whereStr.Append(string.Format(" and A.OrganizationID={0}", ETMS.AppContext.UserContext.Current.OrganizationID));
            DataTable dt = teacherLogic.GetInnerChoseTeacherList(pageIndex, pageSize, string.Empty, whereStr.ToString(), out totalRecordCount);
            PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
            return pageDataSource.PageDataSource;
        }

        protected void btnSearch_Click(object sender,EventArgs e)
        {
            this.PageSet1.QueryChange();
        }

        protected string GetCheckUserID()
        {
            StringBuilder userStr = new StringBuilder();
            for (int i = 0; i <= GridViewList.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridViewList.Rows[i].FindControl("CheckBox1");
                if (cbox.Checked == true)
                {
                    userStr.Append(GridViewList.DataKeys[i].Value.ToString());
                    if (i != GridViewList.Rows.Count)
                    {
                        userStr.Append(",");
                    }
                }                
            }
            return userStr.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int[] selectedValues = CustomGridView.GetSelectedValues<int>(this.GridViewList);
            if (selectedValues.Length == 0)
            {
                ETMS.WebApp.Manage.Extention.AlertMessageBox("请选择要添加的讲师！");
                return;
            }
            else
            {
                try
                {
                    teacherLogic.SetInnerTeacher(GetCheckUserID());
                    ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "讲师添加成功！", "function (){window.location='" + this.ActionHref(string.Format("{0}/Security/ProfessorManage/ProfessorListInner.aspx", WebUtility.AppPath)) + "'}");
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
           
        }

    }
}