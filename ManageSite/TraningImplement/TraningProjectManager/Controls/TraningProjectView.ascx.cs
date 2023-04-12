using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan;

public partial class TraningImplement_TraningProjectManager_Controls_TraningProjectView : System.Web.UI.UserControl
{
    #region 页面参数
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    /// <summary>
    /// 是否显示审核信息
    /// </summary>
    public bool IsAuditVisible
    {
        get
        {
            if (ViewState["IsAuditVisible"] == null)
            {
                ViewState["IsAuditVisible"] = true;
            }
            return (bool)ViewState["IsAuditVisible"];
        }
        set { ViewState["IsAuditVisible"] = value; }
    }

    /// <summary>
    /// 是否显示发布结束信息
    /// </summary>
    public bool IsIssueEndVisible
    {
        get
        {
            if (ViewState["IsIssueEndVisible"] == null)
            {
                ViewState["IsIssueEndVisible"] = false;
            }
            return (bool)ViewState["IsIssueEndVisible"];
        }
        set { ViewState["IsIssueEndVisible"] = value; }
    }

    /// <summary>
    /// 是否显示归档信息
    /// </summary>
    public bool IsItemEndModeVisible {
        get
        {
            if (ViewState["IsItemEndModeVisible"] == null)
            {
                ViewState["IsItemEndModeVisible"] = true;
            }
            return (bool)ViewState["IsItemEndModeVisible"];
        }
        set { ViewState["IsItemEndModeVisible"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
        //是否显示审核信息
        tabAudit.Visible = IsAuditVisible;
        tabAudit1.Visible = IsAuditVisible;
        //是否显示发布结束信息
        tabIsIssueEnd.Visible = IsIssueEndVisible;
        //是否显示归档信息
        tabItemEndMode1.Visible = IsItemEndModeVisible;
        tabItemEndMode2.Visible = IsItemEndModeVisible;
        tabItemEndMode3.Visible = IsItemEndModeVisible;
    }

    /// <summary>
    /// 邦定信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            Lbl_ItemCode.Text = item.ItemCode;
            Lbl_ItemName.Text = item.ItemName;
            DictionaryLabel1.FieldIDValue = item.SpecialtyTypeCode;
            dlblIsPlanItem.FieldIDValue = item.IsPlanItem.ToString();
            //计划
            if (item.IsPlanItem)
            {
                trPlan.Visible = true;
                Lbl_Plan.Text = new Tr_PlanLogic().GetById(item.PlanID).PlanName;
            }
            //培训级别
            DictionaryLabel2.FieldIDValue = item.TrainingLevelID.ToString();
            if (item.TrainingLevelID == 2)
            {
                trTrainingLevel.Visible = true;
                lblDept.FieldIDValue = item.DutyDeptID.ToString();
            }
            lblItemBeginTime.Text = item.ItemBeginTime.ToDate();
            lblItemEndTime.Text = item.ItemEndTime.ToDate();
            lblIsUse.Text = item.IsUse == 1 ? "是" : "否";
            //
            dlabSignupMode.FieldIDValue = item.SignupModeID.ToString();
            //if (item.IsAllowSignup)
            //{
            //    trIsAllowSignup.Visible = true;
            //    lblSignupBeginTime.Text = item.SignupBeginTime.ToDate();
            //    lblSignupEndTime.Text = item.SignupEndTime.ToDate();
            //}
            Lbl_DutyUser.Text = item.DutyUser;
            Lbl_BudgetFee.Text = item.BudgetFee.ToString();
            Lbl_Mobile.Text = item.Mobile;
            Lbl_EMAIL.Text = item.EMAIL;
            Lbl_ItemTarget.Text = item.ItemTarget;
            Lbl_ItemObjectStudent.Text = item.ItemObjectStudent;
            Lbl_Remark.Text = item.Remark;
            //学员数与课程数
            Lbtn_StudentNumber.Text = new Sty_StudentSignupLogic().GetTrainingItemStudentTotal(TrainingItemID).ToString();
            Lbtn_CourseNumber.Text = new Tr_ItemCourseLogic().GetItemCourseCountByTrainingItemID(TrainingItemID).ToString();
            
            lbl_CreateUser.Text = item.CreateUser;
            lbl_CreateTime.Text = item.CreateTime.ToDate();
            lbl_ModifyUser.Text = item.ModifyUser;
            lbl_ModifyTime.Text = item.ModifyTime.ToDate();

            dlabItemStatus.FieldIDValue = item.ItemStatus.ToString();
            if (item.ItemStatus != 10)
            {
                labAuditUser.Text = item.AuditUser;
                labAuditTime.Text = item.AuditTime.ToDate();
                labAuditOpinion.Text = item.AuditOpinion;
            }
            if (item.IsIssue)
            {
                lblIssueUser.Text = item.IssueUser;
                lblIssueTime.Text = item.IssueTime.ToDate();
            }
            if (item.ItemStatus == 90)
            {
                dlblItemEndModeID.FieldIDValue = item.ItemEndModeID.ToString();
                lblModifyUser.Text = item.ModifyUser;
                lblModifyTime.Text = item.ModifyTime.ToDate();
                lblItemEndReMark.Text = item.ItemEndReMark;
            }
        }
    }
}