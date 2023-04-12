using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;

public partial class Grade_GradePublish_GradePublish : System.Web.UI.Page
{
    private static Sty_StudentOffLineJob studentJobRecord = new Sty_StudentOffLineJob();
    public static PublicFacade publicFaced = new PublicFacade();
    private static Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    private static Tr_Item item = new Tr_Item();
    private static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    private static Sty_StudentCourseLogic studentcourseLogic = new Sty_StudentCourseLogic();

    /// <summary>
    /// 培训项目编号
    /// </summary>
    private Guid TrainingItemID
    {
        get { return Request.QueryString["TrainingItemID"].ToGuid(); }
    }

    /// <summary>
    /// 课程编号
    /// </summary>
    private Guid CourseID
    {
        get { return Request.QueryString["CourseID"].ToGuid(); }
    }

    /// <summary>
    /// 培训项目课程编号
    /// </summary>
    private Guid TrainingItemCourseID
    {
        get { return Request.QueryString["TrainingItemCourseID"].ToGuid(); }
    }

    private int PublishView
    {
        get 
        {
            return Request.ToparamValue<int>("view");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            Initial();
        }
        aBack.HRef = this.ActionHref(string.Format("GradePublishList.aspx?CourseID={0}&TrainingItemCourseID={1}&TrainingItemID={2}", CourseID, TrainingItemCourseID, TrainingItemID));
        //单机构版本隐藏“组织机构”列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            this.CustomGridView1.Columns[3].Visible = false;
        }
    }


    protected void Initial()
    {
       
        this.lblCourseName.Text = publicFaced.GetCourseNameByID(CourseID);       
        item = itemLogic.GetById(TrainingItemID);
        this.lblItemName.Text = item.ItemName;
        this.lblItemCode.Text=item.ItemCode;
        int total=0;
        DataTable dt = itemCourseLogic.GetGradeIssueList(1, 1, null, string.Format(" and Tr_ItemCourse.TrainingItemCourseID='{0}'", TrainingItemCourseID.ToString()), out total);
        this.lblCourseCode.Text = dt.Rows[0]["CourseCode"].ToString();
        this.lblCourseName.Text=dt.Rows[0]["CourseName"].ToString();
        //this.lblTrainingModel.FieldIDValue = dt.Rows[0]["TrainingModelID"].ToString();
        this.lblTeachModel.FieldIDValue = dt.Rows[0]["TeachModelID"].ToString();
        this.lblStudentNum.Text = studentcourseLogic.GetItemCourseStudentNum(TrainingItemCourseID).ToString();        //this.ddlDepartment.Text =publicFaced.GetDeptNameByID((Int32)dt.Rows[0]["DutyDeptID"]);
        //this.ddlDepartment.FieldIDValue = dt.Rows[0]["DutyDeptID"].ToString();
        if (PublishView != 0)
        {
            this.btnPublish.Visible = false;
        }
    }

   
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        DataTable dt = itemCourseLogic.GetItemCourseStudentScoreList(TrainingItemCourseID, pageIndex, pageSize, string.Empty, string.Empty, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
       
        return pageDataSource.PageDataSource;
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        try
        {
            switch ((sender as Button).CommandName)
            {
                case "publish":
                    itemCourseLogic.Tr_ItemCourse_GradeIssue(TrainingItemCourseID, 1, ETMS.AppContext.UserContext.Current.RealName);
                     //string tempUrl=string.Format("{0}/Grade/GradePublish/GradePublishList.aspx", WebUtility.AppPath);
                     //ETMS.Utility.JsUtility.MessageBoxAndRedirect("发布成功",tempUrl,this.Page);//.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("发布成功");
                     ETMS.Utility.JsUtility.SuccessMessageBox("提示", "发布成功！", "function(){window.location = '" + this.ActionHref("GradePublishList.aspx") + "'}");

                    break;
                case "cancel":
                    //ETMS.Utility.JsUtility.SuccessMessageBox("提示", "发布成功！", "function(){window.location = '" + this.ActionHref("GradePublishList.aspx") + "'}");

                    //itemCourseLogic.Tr_ItemCourse_GradeIssue(TrainingItemCourseID, 0, ETMS.AppContext.UserContext.Current.RealName);
                     //ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("取消发布成功");
                    break;
            }

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }

    }

}