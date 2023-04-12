using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.AppContext;

public partial class Security_Organization_Setting : System.Web.UI.Page
{
    private readonly static OrganizationLogic logic = new OrganizationLogic();
    public int OrgID { get { return Request.ToparamValue<int>("id"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialData();
        }
    }

    public void InitialData()
    {
        Organization result = logic.QueryByID(OrgID);
        if (null != result)
        {
            txtTitle.Text = result.Title;
            ueSettingInfo.Text = result.FooterInfo;

            OrganizationLimits limits = GetLimits(result.MenuLimit);
            if (limits != null)
            {
                ck1.Checked = limits.CourseMenu;
                ck2.Checked = limits.CompareJob;
                ck3.Checked = limits.TeacherInfo;
                ck4.Checked = limits.Notice;
                ck5.Checked = limits.ViewNum;
                ck6.Checked = limits.PhoneLink;
            }
        }
    }

    private OrganizationLimits GetLimits(string json)
    {
        return JsonHelper.DeserializeObject<OrganizationLimits>(json);
    }

    private string SetLimits()
    {
        return JsonHelper.SerializeObject(new OrganizationLimits() {
            CourseMenu = ck1.Checked,
            CompareJob = ck2.Checked,
            TeacherInfo = ck3.Checked,
            Notice = ck4.Checked,
            ViewNum = ck5.Checked,
            PhoneLink = ck6.Checked
        });
    }

    private void Save()
    {
        try
        {
            logic.Setting(OrgID, txtTitle.Text.Trim(), SetLimits(), ueSettingInfo.Text);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("设置成功！");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
    }
}