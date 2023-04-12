using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization.Course;
using ETMS.Components.Basic.API.Entity.TraningOrgnization.Course;

public partial class TraningOrgManager_TraningOrgManager_Controls_CourseInfo : System.Web.UI.UserControl
{
    private static readonly Tr_OuterOrgCourseLogic outerOrgCourseLogic = new Tr_OuterOrgCourseLogic();   
    /// <summary>
        /// 操作动作
        /// </summary>
        public OperationAction Action
        {
            get;
            set;
        }

        /// <summary>
        /// 课程ID
        /// </summary>
        public Guid OuterOrgCourseID
        {
            get;
            set;
        }
        /// <summary>
        /// 外部培训机构ID
        /// </summary>
        public Guid OuterOrgID
        {
            get;
            set;
        }
        /// <summary>
        /// 实例
        /// </summary>
        public Tr_OuterOrgCourse Source
        {
            set;
            get;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Action == OperationAction.Edit)
                    InitControl();               
            }
        }

        private void InitControl()
        {
            Source = outerOrgCourseLogic.GetById(OuterOrgCourseID);
            txtCourseCode.Text = Source.CourseCode;
            txtCourseName.Text = Source.CourseName;
            FCKeditorCourseIntroduction.Text = Source.CourseIntroduction;
            FCKeditorCourseOutline.Text = Source.CourseOutline;
            FCKeditorForObject.Text = Source.ForObject;
            dropCourseTypeCode.SelectedValue = Source.CourseTypeID.ToString();
            txtAdress.Text = Source.AddrURL;
            txtRemark.Text = Source.Remark;
        }

        /// <summary>
        /// 新增、修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                InitialEntity();
                outerOrgCourseLogic.Save(Source);
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("课程信息保存成功！");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }

        private void InitialEntity()
        {           
            if (Action == OperationAction.Add)
            {
                Source = new Tr_OuterOrgCourse()
                {
                    CreateUser = UserContext.Current.RealName,
                    CreateUserID = UserContext.Current.UserID,
                    ModifyUser = UserContext.Current.RealName,
                    ModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };
            }
            else if (Action == OperationAction.Edit)
            {
                Source = outerOrgCourseLogic.GetById(OuterOrgCourseID);
                Source.ModifyUser = UserContext.Current.RealName;
                Source.ModifyTime = DateTime.Now;
            }
            Source.CourseCode = txtCourseCode.Text.Trim();
            Source.CourseName = txtCourseName.Text.Trim();
            Source.CourseIntroduction = FCKeditorCourseIntroduction.Text;
            Source.CourseOutline = FCKeditorCourseOutline.Text;
            Source.ForObject = FCKeditorForObject.Text;
            Source.CourseTypeID = dropCourseTypeCode.SelectedValue.ToInt();
            Source.OuterOrgID = OuterOrgID;
            Source.AddrURL = txtAdress.Text;
            Source.Remark = txtRemark.Text;
        }   
}