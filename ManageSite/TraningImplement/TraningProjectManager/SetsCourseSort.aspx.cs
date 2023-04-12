using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;

public partial class TraningImplement_TraningProjectManager_SetsCourseSort : BasePage
{
    private Guid TrainingItemID {
        get {
            if (ViewState["TrainingItemID"] == null) {
                ViewState["TrainingItemID"] = Guid.Empty;
            }
            return ViewState["TrainingItemID"].ToGuid();
        }
        set {
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

    private void bind() {
        int totalRecords = 0;
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(TrainingItemID, 1, int.MaxValue - 1,"OrderNum", out totalRecords);
        foreach (DataRow row in dt.Rows) {
            lbCourseSort.Items.Add(new ListItem() { Value = row["TrainingItemCourseID"].ToString(), Text = string.Format("[{0}] {1}", row["CourseCode"], row["CourseName"]) });
        }
    }

    
    /// <summary>
    /// 保存
    /// </summary>
    protected void Btn_Save_Click(object sender, EventArgs e) {
       
    }
}