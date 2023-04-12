using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Utility;
using System.Collections.Generic;
using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.Teacher;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;
public partial class Poll_ResourceQuery_SetArea_R1 : System.Web.UI.Page
{
    private static Poll_QueryAreaLogic QueryAreaLogic = new Poll_QueryAreaLogic();
    private static Poll_QueryAreaDetailLogic QueryAreaDetailLogic = new Poll_QueryAreaDetailLogic();
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
    private static Tr_ItemLogic Tr_ItemLogic = new Tr_ItemLogic();
    private static Tr_ItemCourseLogic Tr_ItemCourseLogic = new Tr_ItemCourseLogic();
    private static Tr_ItemCourseTeacherLogic ItemCourseTeacherLogic = new Tr_ItemCourseTeacherLogic();
    #region 页面条件参数存放

    public string ResourceType
    {
        get
        {
            return "R1";
        }
    }
    public string ResourceCode
    {
        get
        {
            return "00000000-0000-0000-0000-000000000001";
        }
    }
    public int QueryID
    {
        get
        {
            return int.Parse(Request.QueryString["QueryID"]);
        }
    }

    public Poll_QueryArea CurrentQueryArea
    {
        get
        {
            return (Poll_QueryArea)ViewState["CurrentQueryArea"];
        }
        set
        {
            ViewState["CurrentQueryArea"] = value;
        }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.CurrentQueryArea = QueryAreaLogic.GetResourceQueryArea(QueryID, this.ResourceType, this.ResourceCode);

            int totalRecords;
            string filter = string.Format(" AND OrgID={0} AND IsIssue=1 AND IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID);
            string orderBy = " CreateTime DESC ";
            //载入项目列表
            System.Data.DataTable dt = Tr_ItemLogic.GetPagedList(1, int.MaxValue - 1, orderBy, filter, out totalRecords);
            this.ddlProject.DataSource = dt;
            this.ddlProject.DataTextField = "ItemName";
            this.ddlProject.DataValueField = "TrainingItemID";
            this.ddlProject.DataBind();
            this.ddlProject.Items.Insert(0, new ListItem("请选择培训项目", ""));

            if (this.ddlProject.Items.Count > 0)
            {
                if (this.CurrentQueryArea != null)
                {
                    this.ddlProject.SelectedValue = this.CurrentQueryArea.AreaCode;//选中培训项目ID 
                    this.ddlProject_SelectedIndexChanged(sender, e);//触发载入课程列表
                    //判断载入已选课程
                    IList<Poll_QueryAreaDetail> details = QueryAreaLogic.GetResourceQueryAreaDetail(this.CurrentQueryArea.QueryAreaID, 1, int.MaxValue - 1, out totalRecords);
                    if (details.Count > 0)//设置到课程
                    {
                        this.ddlCourse.SelectedValue = details[0].DetailCode;//DetailType="Course"
                    }
                    this.ddlCourse_SelectedIndexChanged(sender, e);//触发载入讲师列表

                    if (details.Count == 2)//设置到讲师
                    {
                        this.ddlTeacher.SelectedValue = details[1].DetailCode;//DetailType="Teacher"
                    }
                }
                else
                {
                    this.ddlProject_SelectedIndexChanged(sender, e);//触发载入课程列表
                    this.ddlCourse.SelectedIndex = 0;
                }
            }
        }
        hidAwserUserCount.Value = new Poll_UserResourceQueryResultLogic().GetAnswerQueryUserCount(QueryID, ResourceType, ResourceCode).ToString();
        //Html.DropDownList("Types", Model.Types, new {@disabled ="disabled"})
    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlCourse.DataSource = Tr_ItemCourseLogic.GetItemCourseListByTrainingItemID(this.ddlProject.SelectedValue.ToGuid());
        this.ddlCourse.DataTextField = "CourseName";
        this.ddlCourse.DataValueField = "TrainingItemCourseID";
        this.ddlCourse.DataBind();
        this.ddlCourse.Items.Insert(0, new ListItem("请选择课程", "-1"));
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlTeacher.Items.Clear();
        if (this.ddlCourse.SelectedValue == "-1")
        {
            this.ddlTeacher.Items.Add(new ListItem("请先选择课程", "-1"));
        }
        else
        {
            int totalRecordCount;
            System.Data.DataTable dt = ItemCourseTeacherLogic.GetTeacherListByItemCourseID(this.ddlCourse.SelectedValue.ToGuid(), out totalRecordCount);
            this.ddlTeacher.DataSource = dt;
            this.ddlTeacher.DataTextField = "RealName";
            this.ddlTeacher.DataValueField = "TeacherID";
            this.ddlTeacher.DataBind();
            this.ddlTeacher.Items.Insert(0, new ListItem("请选择讲师", "-1"));
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.CurrentQueryArea == null)
            {
                this.CurrentQueryArea = new Poll_QueryArea()
                {
                    QueryPublishID = ResourceQueryLogic.GetQueryPublishObjectForResource(this.QueryID, this.ResourceType, this.ResourceCode).QueryPublishID,
                    AreaType = EnumQueryAreaType.TrainItem.ToString(),
                    AreaCode = this.ddlProject.SelectedValue,
                    CreateTime = DateTime.Now,
                    Creator = ETMS.AppContext.UserContext.Current.RealName,
                };
            }
            else
            {
                this.CurrentQueryArea.AreaCode = this.ddlProject.SelectedValue;//更新项目
                //清除课程、讲师范围
                int totalRecords;
                IList<Poll_QueryAreaDetail> details = QueryAreaLogic.GetResourceQueryAreaDetail(this.CurrentQueryArea.QueryAreaID, 1, 999, out totalRecords);
                foreach (Poll_QueryAreaDetail detailItem in details)
                {
                    QueryAreaDetailLogic.doRemoveItem(detailItem.QueryAreaDetailID);//删除
                }
            }

            //保存操作
            QueryAreaLogic.Save(this.CurrentQueryArea);

            //设置到课程
            if (this.ddlCourse.SelectedValue != "-1")
            {
                QueryAreaDetailLogic.Add(new Poll_QueryAreaDetail()
                {
                    QueryAreaID = this.CurrentQueryArea.QueryAreaID,
                    DetailType = EnumQueryAreaDetailType.Course.ToString(),//课程范围
                    DetailCode = this.ddlCourse.SelectedValue,
                    CreateTime = DateTime.Now,
                    Creator = ETMS.AppContext.UserContext.Current.RealName,
                });
            }
            //设置到讲师
            if (this.ddlTeacher.SelectedValue != "-1")
            {
                QueryAreaDetailLogic.Add(new Poll_QueryAreaDetail()
                {
                    QueryAreaID = this.CurrentQueryArea.QueryAreaID,
                    DetailType = EnumQueryAreaDetailType.Teacher.ToString(),//讲师范围
                    DetailCode = this.ddlTeacher.SelectedValue,
                    CreateTime = DateTime.Now,
                    Creator = ETMS.AppContext.UserContext.Current.RealName,
                });
            }
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("提示","问卷范围设置成功！");
            //ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "问卷范围设置成功！", ");

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }


    }

}