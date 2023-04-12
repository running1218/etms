using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;
using ETMS.Controls;

public partial class TraningImplement_TraningProjectManager_ChargeCourseConfig : System.Web.UI.Page
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

    public static int payMode = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        PageSet1.PageSize = 100;
        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));
            payMode = Request.QueryString["Mode"].ToInt();
            if (payMode == 2)
            {
                Dic_PayMode.Items[0].Enabled = false;
                Dic_PayMode.Items[0].Attributes["css"] = "link_colorGray";
                Dic_PayMode.Items[1].Selected = true;
            }
            bind();
            this.PageSet1.QueryChange();
        }

    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            Lbl_ItemCode.Text = item.ItemCode;
            Lbl_ItemName.Text = item.ItemName;
            Dic_PayMode.SelectedValue = item.PayFrom.ToString();
            if (item.IsIssue && payMode == 4)
            {
                Dic_PayMode.Enabled = false;
                Dic_PayMode.CssClass = "link_colorGray";
            }
        }
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize, "", out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要设置的课程！");
            return;
        }
        else
        {
            List<KeyValue> dic = new List<KeyValue>();
            for (int i = 0; i < this.CustomGridView1.Rows.Count; i++)
            {
                CheckBox checkBox = (CheckBox)this.CustomGridView1.Rows[i].FindControl("CheckBox1");
                if (checkBox.Checked)
                {
                    CustomTextBox ctxtCourseHours = (CustomTextBox)this.CustomGridView1.Rows[i].FindControl("ctbPayMoney");
                    KeyValue keyValue = new KeyValue();
                    keyValue.Key = CustomGridView1.DataKeys[i].Value.ToString();
                    keyValue.Value = ctxtCourseHours.Text.ToDecimal();
                    dic.Add(keyValue);
                }
            }
            var p = dic.Where(q => q.Value <= 0).Count();
            if (p > 0)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("您设置的课程收费标准不合法！");
                return;
            }
            try
            {
                foreach (KeyValue a in dic)
                {
                    itemCourseLogic.UpdateItemCourseBudgetFee(a.Key, a.Value.ToString());
                }
                Tr_ItemLogic itemLogic = new Tr_ItemLogic();
                itemLogic.UpdateItemMoney(TrainingItemID.ToString(), Dic_PayMode.SelectedValue.ToInt());
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("设置成功！");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }
}
public class KeyValue
{
    public string Key { get; set; }
    public decimal Value { get; set; }
    public int Value2 { get; set; }
}