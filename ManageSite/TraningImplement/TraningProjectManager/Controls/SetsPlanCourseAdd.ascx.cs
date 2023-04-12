using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingPlan.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;

public partial class TraningImplement_TraningProjectManager_Controls_SetsPlanCourseAdd : System.Web.UI.UserControl
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = Guid.Empty;

            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
    /// <summary>
    /// 计划ID
    /// </summary>
    public Guid PlanID
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

    /// <summary>
    /// 项目下的课程IDs
    /// </summary>
    protected string CourseIDs
    {
        get
        {
            if (ViewState["CourseIDs"] == null)
            {
                ViewState["CourseIDs"] = "";
            }
            return (string)ViewState["CourseIDs"];
        }
        set
        {
            ViewState["CourseIDs"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }
    }

    /// <summary>
    /// 邦定项目信息 项目周期等于课程周期
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        Tr_Item item = itemLogic.GetById(TrainingItemID);
        if (item != null)
        {
            ttbCourseBeginTime.Text = item.ItemBeginTime.ToDate();
            ttbCourseEndTime.Text = item.ItemEndTime.ToDate();
        }

        //查出项目下的课程ID
        int total = 0;
        DataTable dt = new Tr_ItemCourseLogic().GetItemCourseListByTrainingItemID(TrainingItemID, 1, int.MaxValue - 1, out total);
        CourseIDs = ",";
        foreach (DataRow row in dt.Rows)
        {
            CourseIDs += row["CourseID"].ToString() + ",";
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt = new Tr_PlanCourseLogic().GetPlanCourseALLInfoListByPlanID(PlanID, pageIndex, pageSize, "", "", out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 添加
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要添加的课程！");
            return;
        }
        //把列表中的课程ID与课时保存到集合中
        Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
        for (int i = 0; i < this.CustomGridView1.Rows.Count; i++)
        {
            HiddenField hfCourseID = (HiddenField)this.CustomGridView1.Rows[i].FindControl("hfCourseID");
            CustomTextBox ctxtCourseHours = (CustomTextBox)this.CustomGridView1.Rows[i].FindControl("ctxtCourseHours");
            if (hfCourseID != null && ctxtCourseHours != null)
            {
                dic.Add(hfCourseID.Value.ToGuid(), ctxtCourseHours.Text.ToDecimal());
            }
        }
        
        #region 验证课程学时是否合法
        foreach (Guid courseID in selectedValues)
        {
            string[] str = dic[courseID].ToString().Split('.');
            if (str[0].Length > 6)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("您设置的课程学时“" + dic[courseID].ToString() + "”已超过最大课程学时999999.99，请重新设置。");
                return;
            }
            else if (dic[courseID] <= (Decimal)0.0)
            {
                ETMS.Utility.JsUtility.AlertMessageBox("您设置的课程学时不能为空同时必须大于0，请重新设置。");
                return;
            }
        }
        #endregion

        //选中的课程放入集合中
        List<Tr_ItemCourse> list = new List<Tr_ItemCourse>();
        foreach (Guid courseID in selectedValues)
        {
            Tr_ItemCourse itemCourse = new Tr_ItemCourse();
            itemCourse.TrainingItemCourseID = Guid.NewGuid();
            itemCourse.TrainingItemID = TrainingItemID;
            itemCourse.CourseID = courseID;
            itemCourse.CourseHours = dic[courseID];
            itemCourse.CourseStatus = 1;
            itemCourse.TeachModelID = ddlTeachModelID.SelectedValue.ToInt();
            itemCourse.CourseAttrID = ddlCourseAttrID.SelectedValue.ToInt();
            itemCourse.CourseBeginTime = ttbCourseBeginTime.Text.ToDateTime();
            itemCourse.CourseEndTime = (ttbCourseEndTime.Text + " 23:59:59").ToDateTime();
            itemCourse.PassLine = Convert.ToDecimal(60);//及格线默认60
            itemCourse.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            itemCourse.CreateUser = ETMS.AppContext.UserContext.Current.UserName;
            itemCourse.CreateTime = System.DateTime.Now;
            itemCourse.ModifyUser = ETMS.AppContext.UserContext.Current.UserName;
            itemCourse.ModifyTime = System.DateTime.Now;

            list.Add(itemCourse);
        }

        try
        {
            Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
            ItemCourseLogic.AddItemCourseAndCourseware(list);
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "课程添加成功！", "function(){window.location = '" + this.ActionHref("../../ProjectCourseResource/CourseList.aspx?TrainingItemID=" + TrainingItemID.ToString()) + "'}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            this.PageSet1.DataBind();
            return;
        }
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string CourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");
            CheckBox1 = CheckBox1 == null ? new CheckBox() : CheckBox1;

            CustomTextBox ctxtCourseHours = (CustomTextBox)e.Row.FindControl("ctxtCourseHours");
            ctxtCourseHours = ctxtCourseHours == null ? new CustomTextBox() : ctxtCourseHours;
            //已选为项目课程 选择框设为不可用
            if (CourseIDs.IndexOf(string.Format(",{0},", CourseID)) > -1)
            {
                CheckBox1.Enabled = false;
                CheckBox1.CssClass = "link_colorGray";
                CheckBox1.ToolTip = "已选为项目课程";
                ctxtCourseHours.Enabled = false;
            }
        }
    }
}
