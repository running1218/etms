using System;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Controls;
using ETMS.AppContext;

public partial class TraningImplement_TraningProjectManager_ScoreStandardSetting : BasePage
{
    /// <summary>
    /// 项目ID
    /// </summary>
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            bind();
        }
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
            lbl_ItemCode.Text = item.ItemCode;
            lbl_ItemName.Text = item.ItemName;

            Tr_Appraise entity = new Tr_AppraiseLogic().Get(TrainingItemID);
            if (entity != null)
            {
                if (entity.IsCheckCourse)
                {
                    ckbCourse.Checked = true;
                    txtCourse.Text = entity.CourseRate.ToString();
                }

                if (entity.IsCheckStudying)
                {
                    ckbTesting.Checked = true;
                    txtTesting.Text = entity.StudyingRate.ToString();
                }

                if (entity.IsCheckActual)
                {
                    ckbActual.Checked = true;
                    txtActual.Text = entity.ActualRate.ToString();
                }
            }
        }
    }

     /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        Tr_Appraise entity = new Tr_Appraise();

        if (!Validate(out message, entity))
        {
            ETMS.Utility.JsUtility.AlertMessageBox(message);
            return;
        }

        entity.TrainingItemID = this.TrainingItemID;
        entity.CreateTime = DateTime.Now;
        entity.CreateUser = UserContext.Current.UserName;
        entity.CreateUserID = UserContext.Current.UserID;
        entity.ModifyTime = DateTime.Now;
        entity.ModifyUser = UserContext.Current.UserName;
        
        try
        {
            Tr_AppraiseLogic logic = new Tr_AppraiseLogic();
            logic.Save(entity);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("提示", "项目课程考核标准设置成功！");
        }
        catch(BusinessException biz)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(biz));
        }
    }

    private bool Validate(out string message, Tr_Appraise entity)
    {
        int rate = 0;
        if (ckbCourse.Checked)
        {
            if (txtCourse.Text.Trim() == string.Empty)
            {
                message = "请输入课程学习比率";
                return false;
            }
            else
            {
                entity.IsCheckCourse = true;
                entity.CourseRate = txtCourse.Text.ToInt();
                rate += txtCourse.Text.ToInt();
            }
        }

        if (ckbTesting.Checked)
        {
            if (txtTesting.Text.Trim() == string.Empty)
            {
                message = "请输入在线测试比率";
                return false;
            }
            else
            {
                entity.IsCheckStudying = true;
                entity.StudyingRate = txtTesting.Text.ToInt();
                rate += txtTesting.Text.ToInt();
            }
        }

        if (ckbActual.Checked)
        {
            if (txtActual.Text.Trim() == string.Empty)
            {
                message = "请输入在线作业比率";
                return false;
            }
            else
            {
                entity.IsCheckActual = true;
                entity.ActualRate = txtActual.Text.ToInt();
                rate += txtActual.Text.ToInt();
            }
        }

        if (rate != 100)
        {
            message = "勾选项输入的比率之和必须为100";
            return false;
        }

        message = string.Empty;
        return true;
    }
}