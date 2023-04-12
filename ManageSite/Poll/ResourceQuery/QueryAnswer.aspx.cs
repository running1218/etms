using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using System.Text;
using System.Data;

public partial class Poll_ResourceQuery_QueryAnswer : System.Web.UI.Page
{
    private static Poll_QueryLogic Logic = new Poll_QueryLogic();
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
    private static Poll_QueryAreaLogic queryAreaLogic = new Poll_QueryAreaLogic();
    private static Poll_QueryAreaDetailLogic queryAreaDetailLogic = new Poll_QueryAreaDetailLogic();
    private static Tr_ItemLogic itemLogic = new Tr_ItemLogic();
    private static Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
    private static Res_CourseLogic courseLogic = new Res_CourseLogic();
    private static UserLogic userLogic = new UserLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StringBuilder resourceInfo = new StringBuilder();

            if (Request.QueryString["ResourceType"].ToString() == "R1")
            {
                int total = 0;
                string filter = string.Format(" and QueryID={0} and ResourceCode='{1}' and ResourceTypeCode='{2}'", int.Parse(Request.QueryString["QueryID"]), "00000000-0000-0000-0000-000000000001", "R1");
                IList<Poll_QueryPublishObject> pulishlist = ResourceQueryLogic.GetEntityList(1, int.MaxValue - 1, "", filter, out total);
                if (total != 0)
                {
                    total = 0;
                    filter = string.Format(" AND QueryPublishID={0}", pulishlist[0].QueryPublishID);
                    IList<Poll_QueryArea> list = queryAreaLogic.GetEntityList(1, 1, "", filter, out total);

                    resourceInfo.Append("<span class='infoComments beforeTit'>");
                    resourceInfo.AppendFormat(" 调查项目：{0}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", itemLogic.GetById(list[0].AreaCode.ToGuid()).ItemName);
                    total = 0;
                    DataTable dt = queryAreaDetailLogic.GetPagedList(1, int.MaxValue - 1, "", string.Format(" and QueryAreaID={0}", list[0].QueryAreaID), out total);
                    DataRow[] rows = dt.Select(" DetailType='Course'");
                    if (rows.Length > 0)
                    {
                        resourceInfo.Append(" 课程：");
                    }
                    foreach (DataRow item in rows)
                    {
                        Tr_ItemCourse itemCousre = itemCourseLogic.GetById(item["DetailCode"].ToGuid());
                        Res_Course course = courseLogic.GetById(itemCousre.CourseID);
                        resourceInfo.AppendFormat(" {0}  ", course.CourseName);
                    }
                    rows = dt.Select(" DetailType='Teacher'");
                    if (rows.Length > 0)
                    {
                        resourceInfo.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;讲师：");
                    }
                    foreach (DataRow item in rows)
                    {
                        resourceInfo.AppendFormat(" {0} ", userLogic.GetUserByID(item["DetailCode"].ToInt()).RealName);
                    }
                    resourceInfo.Append("</span>");
                }
            }


            if (Request.QueryString["isShowBack"] != null)
            { this.isShow.Visible = false; }
            //获取时间html片段
            ltContent.Text = PollManager.GetResponseViewPreView(int.Parse(Request.Params["queryID"]), int.Parse(Request.Params["batchID"])).ToString();
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("提示：", ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx), "function(){window.close();}");
        }
    }
    /// <summary>
    /// 查询未提交试卷的用户信息 add 2013-1-9 hjy 
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="queryID"></param>
    /// <param name="resourceTypeCode"></param>
    /// <param name="totalRecords"></param>
    /// <returns></returns>

    //}

}