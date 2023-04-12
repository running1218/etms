using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Resources;
using ETMS.Utility;

public partial class TraningImplement_ProjectCourseResourceBatch_SetLearnCycle : System.Web.UI.Page
{
    public Guid[] SelectedValues {
        get {
            return ViewState["selectedValues"] as Guid[];
        }
        set {
            ViewState["selectedValues"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (Session["selectedValues"] != null)
            {
                SelectedValues = Session["selectedValues"] as Guid[];
                labCount.Text = SelectedValues.Length.ToString();
            }
        }
    }

     /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        //如果统一设置时间时 验证时间是否输入
        if (rb_BatchSetResStudyTime.Checked)
        {
            if (Dtt_LearnCycleBegin.Text.Trim() == "")
            {
                ETMS.Utility.JsUtility.AlertMessageBox("学习周期开始时间不能为空！");
                return;
            }
            else if (Dtt_LearnCycleEnd.Text.Trim() == "")
            {
                ETMS.Utility.JsUtility.AlertMessageBox("学习周期结束时间不能为空！");
                return;
            }
        }

        try
        {
            Tr_ItemCourseResLogic itemCourseRes = new Tr_ItemCourseResLogic();
            //修改项目课程资源学习周期与项目课程学习周期一致
            if (rb_BatchSetResStudyTimeToItemCourse.Checked)
            {
                itemCourseRes.BatchSetResStudyTimeToItemCourse(SelectedValues);
            }
            else
            {
                itemCourseRes.BatchSetResStudyTime(SelectedValues, Dtt_LearnCycleBegin.Text.ToDateTime(), Dtt_LearnCycleEnd.Text.ToDateTime());
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("资源学习周期保存成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}