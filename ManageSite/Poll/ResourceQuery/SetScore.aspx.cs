using System;
using System.Collections.Generic;
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
public partial class Poll_ResourceQuery_SetScore : System.Web.UI.Page
{
    private static Poll_QueryAreaLogic QueryAreaLogic = new Poll_QueryAreaLogic();
    private static Poll_QueryAreaDetailLogic QueryAreaDetailLogic = new Poll_QueryAreaDetailLogic();
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
    private static Tr_ItemLogic Tr_ItemLogic = new Tr_ItemLogic();
    private static Tr_ItemCourseLogic Tr_ItemCourseLogic = new Tr_ItemCourseLogic();
    private static Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
    #region 页面条件参数存放
    public int QueryPublishObjectID
    {
        get
        {
            return int.Parse(Request.QueryString["id"]);
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Poll_QueryPublishObject entity = ResourceQueryLogic.GetById(this.QueryPublishObjectID);
            this.txtScore.Text = entity.Score.ToString();//设置综合分数

            Poll_Query query = QueryLogic.GetById(entity.QueryID);
            this.lblQueryName.Text = query.QueryName;
            this.lblBeginTime.DateTimeValue = query.BeginTime;
            this.lblEndTime.DateTimeValue = query.EndTime;
            this.lblDutyUser.Text = query.DutyUser;


            Poll_QueryArea queryArea = QueryAreaLogic.GetResourceQueryArea(query.QueryID, entity.ResourceTypeCode, entity.ResourceCode);
            this.lblItem.Text = Tr_ItemLogic.GetById(queryArea.AreaCode.ToGuid()).ItemName;

            //判断载入已选课程
            int totalRecords;
            IList<Poll_QueryAreaDetail> details = QueryAreaLogic.GetResourceQueryAreaDetail(queryArea.QueryAreaID, 1, int.MaxValue - 1, out totalRecords);
            if (details.Count > 0
                && details[0].DetailType.Equals(EnumQueryAreaDetailType.Course.ToString()))//设置到课程
            {
                //获取培训项目课程信息
                Tr_ItemCourse trItemCourse = Tr_ItemCourseLogic.GetById(details[0].DetailCode.ToGuid());
                //获取课程名称
                this.lblCourse.Text = new ETMS.Components.Basic.Implement.PublicFacade().GetCourseNameByID(trItemCourse.CourseID);//DetailType="Course"

                if (details.Count == 2)//设置到讲师
                {
                    ETMS.Components.Basic.API.Entity.Security.User userInfo = new ETMS.Components.Basic.Implement.BLL.Security.UserLogic().GetUserByID(details[1].DetailCode.ToInt());
                    this.lblTeacher.Text = userInfo.RealName ?? userInfo.LoginName;//DetailType="Teacher"
                }
            }

        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            Poll_QueryPublishObject entity = ResourceQueryLogic.GetById(this.QueryPublishObjectID);
            entity.Score = this.txtScore.Text.ToInt();//设置综合分数
            entity.Modifier = ETMS.AppContext.UserContext.Current.RealName;
            entity.ModifiyTime = DateTime.Now;
            ResourceQueryLogic.Save(entity);
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("综合分数保存成功!");

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }


    }
}