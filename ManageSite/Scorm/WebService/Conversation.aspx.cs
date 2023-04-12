using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ETMS.Components.Scrom.Implement.BLL;
using ETMS.Components.Scrom.API.Entity;

using ETMS.Utility;

public partial class Scorm_WebService_Conversation : System.Web.UI.Page
{

    /// <summary>
    /// 用户姓名
    /// </summary>
    private string UserName
    {
        get
        {
            return Request["UserName"].ToString();
        }
    }

    /// <summary>
    /// 项目课程资源ID
    /// </summary>
    private Guid ItemCourseResID
    {
        get
        {
            return Request["ItemCourseResID"].ToGuid();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        object ReturnValue = null;
     
        if (Request["t"] != null)
        {
            if (Request["id"] != null)
            {
                Guid RequestID = new Guid(Request["id"].ToString());

                if (Convert.ToString(Request["t"]) == "0")
                {
                    //初始化
                    ReturnValue = LMSInitialize(Request["parm"].ToString(), RequestID);
                }
                else if (Convert.ToString(Request["t"]) == "1")
                {
                    //完成
                    ReturnValue = LMSFinish(Request["parm"].ToString(), RequestID);
                }
            }
        }
        
        Response.Clear();
        Response.Write(ReturnValue.ToString().ToLower());
        Response.Flush();
        Response.Close();
    }

    /// <summary>
    /// 在SCO与LMS交互前初始化课件环境
    /// </summary>
    /// <param name="parm">调用时传入空字符串""</param>
    /// <param name="ResourceID"></param>
    /// <returns></returns>
    public bool LMSInitialize(string parm, Guid ResourceID)
    {
        //初始化数据：
        CmiCoreInfo cmiCoreInfo = new CmiCoreInfo();

        ItemResourceLogic l_ItemResource = new ItemResourceLogic();
        CoreLogic l_Core = new CoreLogic();

        DataTable dt = l_ItemResource.GetInfoByRescourceID(ResourceID);
        if (dt.Rows.Count > 0)
        {   
            cmiCoreInfo.CourseID =dt.Rows[0]["CourseID"].ToGuid();
            cmiCoreInfo.CoursewareID = dt.Rows[0]["CoursewareID"].ToGuid();
            cmiCoreInfo.OrgID = dt.Rows[0]["OrgID"].ToString();
            cmiCoreInfo.ItemID = dt.Rows[0]["ItemID"].ToString();
            cmiCoreInfo.ResourceID = dt.Rows[0]["ResourceID"].ToString();

            cmiCoreInfo.LessonMode = l_Core.GetCoreLessonMode();
            string CoreLessonStatus = l_Core.GetCoreLessonStatus(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID);
            if (CoreLessonStatus == "")
            {
                l_Core.InsertCoreLessonStatus(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID, "not attempted");
                CoreLessonStatus = "not attempted";
            }
            cmiCoreInfo.LessonStatus = CoreLessonStatus;

            cmiCoreInfo.StudentID = ETMS.AppContext.UserContext.Current.UserID;
            cmiCoreInfo.StudentName = ETMS.AppContext.UserContext.Current.UserName;

            cmiCoreInfo.ErrorInfo = "";
            cmiCoreInfo.LastError = "0";
            cmiCoreInfo.StartTime = DateTime.Now;
        }

        CmiCoreInfo.SetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString(), ResourceID.ToString(), ItemCourseResID, cmiCoreInfo);

        return true;
    }

    /// <summary>
    /// 在SCO与LMS技术交互前销毁课件环境
    /// </summary>
    /// <param name="parm">调用时传入空字符串""</param>
    /// <param name="ResourceID"></param>
    /// <returns></returns>
    public bool LMSFinish(string parm, Guid ResourceID)
    {   
        ItemResourceLogic l_ItemResource = new ItemResourceLogic();
        CoreLogic l_Core = new CoreLogic();
        l_Core.SetCoreSessionTime(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID, Convert.ToInt32(DateTime.Now.Subtract(CmiCoreInfo.GetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString() , ResourceID.ToString(), ItemCourseResID.ToString()).StartTime).TotalSeconds));
        CmiCoreInfo cmiCoreInfo = CmiCoreInfo.GetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString(),ResourceID.ToString(), ItemCourseResID.ToString());

        cmiCoreInfo.CourseID = Guid.Empty;
        cmiCoreInfo.CoursewareID = Guid.Empty;
        cmiCoreInfo.OrgID = null;
        cmiCoreInfo.ItemID = null;
        cmiCoreInfo.ResourceID = null;
        cmiCoreInfo.LessonMode = null;
        cmiCoreInfo.LessonStatus = null;
        cmiCoreInfo.StudentID = 0;
        cmiCoreInfo.StudentName = null;
        cmiCoreInfo.ErrorInfo = "";
        cmiCoreInfo.LastError = "0";
        cmiCoreInfo.StartTime = DateTime.MinValue;

        CmiCoreInfo.SetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString(), ResourceID.ToString(),ItemCourseResID, cmiCoreInfo);
        return true;
    }
}
