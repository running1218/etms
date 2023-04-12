using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Student;

public partial class Security_TeacherQuery_TeacherItemView : BasePage
{
    #region 页面参数
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
    /// 讲师ID
    /// </summary>
    public int TeacherID
    {
        get
        {
            if (ViewState["TeacherID"] == null)
                ViewState["TeacherID"] = 0;

            return (int)ViewState["TeacherID"];
        }
        set
        {
            ViewState["TeacherID"] = value;
        }
    }

    /// <summary>
    /// 课程开始时间 Begin
    /// </summary>
    public DateTime CourseBeginTimeBegin
    {
        get
        {
            if (ViewState["CourseBeginTimeBegin"] == null)
                ViewState["CourseBeginTimeBegin"] = "";
            return ViewState["CourseBeginTimeBegin"].ToDateTime();
        }
        set
        {
            ViewState["CourseBeginTimeBegin"] = value;
        }
    }

    /// <summary>
    /// 课程开始时间 End
    /// </summary>
    public DateTime CourseBeginTimeEnd
    {
        get
        {
            if (ViewState["CourseBeginTimeEnd"] == null)
                ViewState["CourseBeginTimeEnd"] = "";
            return ViewState["CourseBeginTimeEnd"].ToDateTime();
        }
        set
        {
            ViewState["CourseBeginTimeEnd"] = value;
        }
    }
    
    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
                ViewState["Crieria"] = "";
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件  按课程名称、组织机构、姓名排序
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " e.CourseName,u.OrganizationID,u.RealName";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
        {
            TrainingItemID = new Guid(BasePage.getSafeRequest(this.Page, "TrainingItemID"));           
            bind();
            this.PageSet1.QueryChange();
        }

        #region 返回用参数
        if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TeacherID")))
        {
            TeacherID = BasePage.getSafeRequest(this.Page, "TeacherID").ToInt();
        }

        CourseBeginTimeBegin = BasePage.getSafeRequest(this.Page, "CourseBeginTimeBegin").ToDateTime();
        CourseBeginTimeEnd = BasePage.getSafeRequest(this.Page, "CourseBeginTimeEnd").ToDateTime();

        //返回
        aBack.HRef = this.ActionHref(string.Format("TeacherItemList.aspx?TeacherID={0}&CourseBeginTimeBegin={1}&CourseBeginTimeEnd={2}"
                , TeacherID
                , CourseBeginTimeBegin
                , CourseBeginTimeEnd));
        #endregion

        TraningProjectView1.TrainingItemID = TrainingItemID;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
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
            lbl_ItemDate.Text = string.Format("{0} 至 {1}", item.ItemBeginTime.ToDate(), item.ItemEndTime.ToDate());
        }
        //项目课程
        int total = 0;
        ddl_d999TrainingItemCourseID.DataSource = new Tr_ItemCourseLogic().GetItemCourseListByTrainingItemID(TrainingItemID, 1, int.MaxValue - 1, out total);
        ddl_d999TrainingItemCourseID.DataTextField = "CourseName";
        ddl_d999TrainingItemCourseID.DataValueField = "TrainingItemCourseID";
        ddl_d999TrainingItemCourseID.DataBind();
        ddl_d999TrainingItemCourseID.Items.Insert(0, new ListItem("全部", ""));
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = string.Format(" {0}", BasePage.getQueryConditionFromQueryControlList(tableQueryControlList));

        DataTable dt = new Sty_StudentSignupLogic().GetStudentCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[5].Visible = false;
        }
    }
}