using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Controls;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TraningOrgnization;
using ETMS.Components.Basic.Implement.BLL.Course.Teacher;

public partial class TraningImplement_TraningProjectManager_SetsCourseEdit : BasePage
{
    #region 页面参数

    /// <summary>
    /// 存对象信息
    /// </summary>
    public Tr_ItemCourse ItemCourse
    {
        get
        {
            if (ViewState["ItemCourse"] == null)
                ViewState["ItemCourse"] = new Tr_ItemCourse();

            return (Tr_ItemCourse)ViewState["ItemCourse"];
        }
        set
        {
            ViewState["ItemCourse"] = value;
        }
    }

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
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            ddlCourseStatus.SelectedValue = "1";
            TrainingItemCourseID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID"));
            bind();
        }
    }

    /// <summary>
    /// 邦定
    /// </summary>
    private void bind()
    {
        #region 基本信息


        //培训机构
        int total = 0;
        Tr_OuterOrgLogic outerOrgLogic = new Tr_OuterOrgLogic();
        DataTable dt = outerOrgLogic.GetPagedList(1, int.MaxValue - 1, string.Empty, string.Format(" and OuterOrgStatus=1 and OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID), out total);
        this.ddlOuterOrgID.DataSource = dt;
        this.ddlOuterOrgID.DataTextField = "OuterOrgName";
        this.ddlOuterOrgID.DataValueField = "OuterOrgID";
        this.ddlOuterOrgID.DataBind();
        this.ddlOuterOrgID.Items.Insert(0, new ListItem("请选择", ""));

        #endregion

        //获得项目课程信息
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            string ItemBeginTime = string.Empty;
            string ItemEndTime = string.Empty;
            #region 项目代码与名称
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            Tr_Item item = itemLogic.GetById(ItemCourse.TrainingItemID);
            if (item != null)
            {
                lblItemCode.Text = item.ItemCode;
                lblItemName.Text = item.ItemName;
                ItemBeginTime = item.ItemBeginTime.ToDate();
                ItemEndTime = item.ItemEndTime.ToDate();
            }
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
            {
                lblCourseCode.Text = Course.CourseCode;
                lblCourseName.Text = Course.CourseName;
                DictionaryLabelCourseType.FieldIDValue = Course.CourseTypeID.ToString();
                DictionaryLabelCourseLevel.FieldIDValue = Course.CourseLevelID.ToString();
            }
            #endregion

            ddlTeachModelID.SelectedValue = ItemCourse.TeachModelID.ToString();
            ddlCourseStatus.SelectedValue = ItemCourse.CourseStatus.ToString();
            ttbCourseBeginTime.Text = ItemCourse.CourseBeginTime.ToString("yyyy-MM-dd") == "0001-01-01" ? ItemBeginTime : ItemCourse.CourseBeginTime.ToDate();
            ttbCourseEndTime.Text = ItemCourse.CourseEndTime.ToString("yyyy-MM-dd") == "0001-01-01" ? ItemEndTime : ItemCourse.CourseEndTime.ToDate();
            ddlTrainingModelID.SelectedValue = ItemCourse.TrainingModelID.ToString();
            ddlOuterOrgID.SelectedValue = ItemCourse.OuterOrgID.ToString();
            txtCourseHours.Text = ItemCourse.CourseHours.ToString();
            txtCourseHours.Attributes.Add("t_value", txtCourseHours.Text);
            txtOuterOrgDutyUser.Text = ItemCourse.OuterOrgDutyUser;
            txtScore.Text = ItemCourse.Score.ToString();
            txtOuterOrgEMAIL.Text = ItemCourse.OuterOrgEMAIL;
            ddlCourseAttrID.SelectedValue = ItemCourse.CourseAttrID.ToString();
            txtBudgetFee.Text = ItemCourse.BudgetFee.ToString();
            txtBudgetFee.Attributes.Add("t_value", txtBudgetFee.Text);
            txtRemark.Text = ItemCourse.Remark;
            txtPassLine.Text = string.Format("{0:N0}", ItemCourse.PassLine);
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        ItemCourse.TeachModelID = ddlTeachModelID.SelectedValue.ToInt();
        ItemCourse.CourseStatus = ddlCourseStatus.SelectedValue.ToInt();
        ItemCourse.CourseBeginTime = ttbCourseBeginTime.Text.ToDateTime();
        ItemCourse.CourseEndTime = (ttbCourseEndTime.Text+" 23:59:59").ToDateTime();
        ItemCourse.TrainingModelID = ddlTrainingModelID.SelectedValue.ToInt();
        ItemCourse.OuterOrgID = ddlOuterOrgID.SelectedValue != "" ? new Guid(ddlOuterOrgID.SelectedValue) : Guid.Empty;
        ItemCourse.CourseHours = txtCourseHours.Text.ToDecimal();
        ItemCourse.OuterOrgDutyUser = txtOuterOrgDutyUser.Text;
        ItemCourse.Score = txtScore.Text.ToInt();
        ItemCourse.OuterOrgEMAIL = txtOuterOrgEMAIL.Text;
        ItemCourse.CourseAttrID = ddlCourseAttrID.SelectedValue.ToInt();
        ItemCourse.BudgetFee = txtBudgetFee.Text.ToDecimal();
        ItemCourse.Remark = txtRemark.Text;
        ItemCourse.PassLine = txtPassLine.Text.ToDecimal();
        ItemCourse.ModifyTime = DateTime.Now;
        ItemCourse.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;

        #region 验证信息
        //课程学时
        string[] str = ItemCourse.CourseHours.ToString().Split('.');
        if (str[0].Length > 6)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("您设置的课程学时“" + ItemCourse.CourseHours.ToString() + "”已超过最大课程学时999999.99，请重新设置。");
            return;
        }
        else if (ItemCourse.CourseHours <= (Decimal)0.0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("您设置的课程学时不能为空同时必须大于0，请重新设置。");
            return;
        }
        //预算
        if (ItemCourse.BudgetFee > (Decimal)999999999999.99) {
            ETMS.Utility.JsUtility.AlertMessageBox("您设置的预算超出最大值“999999999999.99”，请重新设置。");
            return;
        }
        //及格线
        if (ItemCourse.PassLine > (Decimal)9999.99)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("您设置的及格线超出最大值“9999.99”，请重新设置。");
            return;
        }
        #endregion

        try
        {
            Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
            ItemCourseLogic.Update(ItemCourse);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("信息修改成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}
