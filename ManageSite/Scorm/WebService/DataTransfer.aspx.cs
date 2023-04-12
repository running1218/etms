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
public partial class Scorm_WebService_DataTransfer : System.Web.UI.Page
{
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

        if (Request["t"] != null && Request["id"] != null)
        {
            Guid ResourceID = new Guid(Request["id"].ToString());
            string parm = Convert.ToString(Request["parm"]).ToLower();
            switch (Convert.ToString(Request["t"]))
            {
                //GetValue
                case "2":
                    ReturnValue = LMSGetValue(parm, ResourceID);
                    break;
                //SetValue
                case "3":
                    ReturnValue = LMSSetValue(parm, Convert.ToString(Request["value"]), ResourceID);
                    break;
                //Commit
                case "4":
                    ReturnValue = LMSCommit(parm, ResourceID);
                    break;
            }
        }
        
      
        Response.Clear();
        Response.Write(ReturnValue.ToString().ToLower());
        Response.Flush();
        Response.Close();
    }

    /// <summary>
    /// 获取数据模型的值
    /// </summary>
    public string LMSGetValue(string parm, Guid ResourceID)
    {
        string ReturnValue = "";
        string[] arrParm = parm.Split('.');
        if (arrParm.Length > 2)
        {
            switch (arrParm[1])
            {
                case "core":
                    ReturnValue = LMSGetValueByCore(parm, ResourceID);
                    break;
                case "objectives":
                    ReturnValue = LMSGetValueByObjectives(parm, ResourceID);
                    break;
                case "interactions":
                    ReturnValue = LMSGetValueByInteractions(parm, ResourceID);
                    break;
                default:
                    //记录访问错误代码401，
                    RecordErrInfo(ResourceID, "401", parm);
                    ReturnValue = "";
                    break;
            }
        }
        return ReturnValue;
    }

    #region 根据传入的关键字获取模型对应的值
    /// <summary>
    /// 获取数据模型中包含Core的值
    /// </summary>
    private string LMSGetValueByCore(string key, Guid ResourceID)
    {
        CoreLogic coreLogic = new CoreLogic();
        string ReturnValue = "";
        try
        {
            switch (key)
            {
                //存储的用户状态信息
                case "cmi.core.lesson_status":
                    ReturnValue = coreLogic.GetCoreLessonStatus(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID);
                    RecordErrInfo(ResourceID, "0", "");
                    break;
                //退出标记
                case "cmi.core.exit":
                    //只写不读，记录错误号404
                    RecordErrInfo(ResourceID, "404", key);
                    ReturnValue = "exit";
                    break;
                //学习模式
                case "cmi.core.lesson_mode":
                    ReturnValue = coreLogic.GetCoreLessonMode();
                    RecordErrInfo(ResourceID, "0", "");
                    break;
                //用户姓名
                case "cmi.core.student_name":
                    ReturnValue = ETMS.AppContext.UserContext.Current.RealName;
                    RecordErrInfo(ResourceID, "0", "");
                    break;
                //用户唯一标识
                case "cmi.core.student_id":
                    ReturnValue = ETMS.AppContext.UserContext.Current.UserID.ToString();
                    RecordErrInfo(ResourceID, "0", "");
                    break;
                //SCO成绩0-100
                case "cmi.core.score.raw":
                    ReturnValue = coreLogic.GetCoreScoreRaw(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID);
                    RecordErrInfo(ResourceID, "0", "");
                    break;
                //上次位置
                case "cmi.core.lesson_location":
                    ReturnValue = coreLogic.GetCoreLessonLocation(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID);
                    RecordErrInfo(ResourceID, "0", "");
                    break;
                default:
                    RecordErrInfo(ResourceID, "401", key);
                    break;
            }
        }
        catch
        {
            RecordErrInfo(ResourceID, "401", key);
        }

        return ReturnValue;
    }

    /// <summary>
    /// 获取数据模型中包含objectives的值
    /// </summary>
    private string LMSGetValueByObjectives(string key, Guid ResourceID)
    {
        string ReturnValue = "";
        ObjectivesLogic objectivesLogic = new ObjectivesLogic();

        try
        {
            if (key == "cmi.objectives._count")
            {
                ReturnValue = objectivesLogic.GetObjectivesCount(ResourceID).ToString();
                RecordErrInfo(ResourceID, "0", "");
            }
            else 
            {
                bool isRecordErr = true;
                string[] arrkey = key.Split('.');
                if (arrkey.Length >= 4 && arrkey[0].ToString() == "cmi" && arrkey[1].ToString() == "objectives")
                {
                    int n = IsNumber(arrkey[2].ToString());
                    if (n >= 0)//为数字
                    {
                        //知识点状态
                        if (arrkey.Length == 4 && arrkey[3].ToString() == "status")
                        {
                            isRecordErr = false;
                            ReturnValue = objectivesLogic.GetObjectivesStatus(ResourceID, ETMS.AppContext.UserContext.Current.UserID, n);
                            RecordErrInfo(ResourceID, "0", "");
                        }
                        //知识点得分0-100
                        else if (arrkey.Length == 5 && arrkey[3].ToString() == "score" && arrkey[4].ToString() == "raw")
                        {
                            isRecordErr = false;
                            ReturnValue = objectivesLogic.GetObjectivesScoreRaw(ResourceID, ItemCourseResID,ETMS.AppContext.UserContext.Current.UserID, n);
                            RecordErrInfo(ResourceID, "0", "");
                        }
                    }
                }

                if (isRecordErr)
                {
                    //记录错误401
                    RecordErrInfo(ResourceID, "401", key);
                }
            }
        }
        catch
        {
            RecordErrInfo(ResourceID, "401", key);
        }
        return ReturnValue;
    }

    /// <summary>
    /// 获取数据模型中包含Interactions的值
    /// </summary>
    private string LMSGetValueByInteractions(string key, Guid ResourceID)
    {
        string ReturnValue = "";
        InteractionsLogic interactionsLogic = new InteractionsLogic();


        try
        {
            //交互数量
            if (key == "cmi.interactions._count")
            {
                //获取数量，数量不存在则返回0
                ReturnValue = interactionsLogic.GetInteractionCount(ResourceID).ToString();
                RecordErrInfo(ResourceID, "0", "");
            }
            else
            {
                bool isRecordErr = true;
                string[] arrkey = key.Split('.');
                if (arrkey.Length == 4 && arrkey[0].ToString() == "cmi" && arrkey[1].ToString() == "interactions")
                {
                    int n = IsNumber(arrkey[2].ToString());
                    if (n >= 0)//为数字
                    {
                        if (arrkey[3].ToString() == "student_response")
                        {
                            isRecordErr = false;
                            //获取答案
                            ReturnValue = interactionsLogic.GetInteractionResponse(ResourceID, ItemCourseResID,n, ETMS.AppContext.UserContext.Current.UserID).ToString();
                            RecordErrInfo(ResourceID, "0", "");
                        }
                        //交互唯一标识   成绩结果
                        if (arrkey[3].ToString() == "id")
                        {
                            isRecordErr = false;
                            ReturnValue = interactionsLogic.GetInteractionByIndex(ResourceID, n);
                            RecordErrInfo(ResourceID, "0", "");
                        }
                        //
                        if (arrkey[3].ToString() == "result")
                        {
                            isRecordErr = false;
                            //记录错误号404  只写属性
                            RecordErrInfo(ResourceID, "404", key);
                        }
                    }
                }

                if (isRecordErr)
                {
                    //记录错误401
                    RecordErrInfo(ResourceID, "401", key);
                }
            }
        }
        catch
        {
            RecordErrInfo(ResourceID, "401", key);
        }
        return ReturnValue;
    }
    #endregion


    /// <summary>
    /// 设置数据模型的值
    /// </summary>
    public bool LMSSetValue(string key, string value, Guid ResourceID)
    {
        bool returnValue = false;
        string[] arrKey = key.Split('.');
        if (arrKey.Length > 2)
        {
            switch (arrKey[1])
            {
                case "core":
                    returnValue = LMSSetValueByCore(key, value, ResourceID);
                    break;
                case "objectives":
                    returnValue = LMSSetValueByObjectives(key, value, ResourceID);
                    break;
                case "interactions":
                    returnValue = LMSSetValueByInteractions(key, value, ResourceID);
                    break;
                default:
                    RecordErrInfo(ResourceID, "401", key);
                    break;
            }
        }
        else
        {
            RecordErrInfo(ResourceID, "401", key);
        }
        return returnValue;
    }


    #region 根据传入的键值设置数据模型的值
    /// <summary>
    /// 设置数据模型中包含Core的值
    /// </summary>
    private bool LMSSetValueByCore(string key, string value, Guid ResourceID)
    {
        CoreLogic coreLogic = new CoreLogic();
        bool ReturnValue = false;
        switch (key)
        {
            //用户唯一标识
            case "cmi.core.student_id":
                //Read Only 不支持设置值 记录错误代码403
                RecordErrInfo(ResourceID, "403", key);
                ReturnValue = false;
                break;
            //用户姓名
            case "cmi.core.student_name":
                //Read Only 不支持设置值 记录错误代码403
                RecordErrInfo(ResourceID, "403", key);
                ReturnValue = false;
                break;
            //存储的用户状态信息（passed,completed,failed,incomplete,browsed,not attempted）
            case "cmi.core.lesson_status":
                //验证数据状态是否存在上面的，否则记录405
                if (CheckData(value))
                {
                    coreLogic.SetCoreLessonStatus(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID, value);
                    RecordErrInfo(ResourceID, "0", "");
                    ReturnValue = true;
                }
                else
                {
                    RecordErrInfo(ResourceID, "405", key);
                }
                break;
            //退出标记（time-out,suspend,logout,""）
            case "cmi.core.exit":
                //验证数据状态是否存在上面的，否则记录405
                string CheckValue = "[" + value + "]";
                string Values = "[time-out],[suspend],[logout],[]";
                if (Values.IndexOf(CheckValue) < 0)
                {
                    RecordErrInfo(ResourceID, "405", key);
                }
                else
                {
                    coreLogic.SetCoreExit(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID, value);
                    RecordErrInfo(ResourceID, "0", "");
                    ReturnValue = true;
                }
                break;
            //学习模式
            case "cmi.core.lesson_mode":
                //Read Only 不支持设置值 记录错误代码403
                RecordErrInfo(ResourceID, "403", key);
                ReturnValue = false;
                break;
            //SCO成绩0-100
            case "cmi.core.score.raw":
                //验证数据类型 405  
                if (IsFloat(value) != "err")
                {
                    coreLogic.SetCoreScoreRaw(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID, value);
                    RecordErrInfo(ResourceID, "0", "");
                    ReturnValue = true;
                }
                else
                {
                    RecordErrInfo(ResourceID, "405", key);
                }
                
                break;
            //上次位置
            case "cmi.core.lesson_location":
                //TODO:验证数据类型
                if (true)
                {
                    coreLogic.SetCoreLessonLocation(ResourceID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID, value);
                    RecordErrInfo(ResourceID, "0", "");
                    ReturnValue = true;
                }
                else
                {
                    RecordErrInfo(ResourceID, "405", key);
                }
                break;
            default:
                RecordErrInfo(ResourceID, "401", key);
                break;
        }

        return ReturnValue;
    }

    /// <summary>
    /// 设置数据模型中包含objectives的值
    /// </summary>
    private bool LMSSetValueByObjectives(string key, string value, Guid ResourceID)
    {
        bool ReturnValue = false;
        ObjectivesLogic objectivesLogic = new ObjectivesLogic();

        try
        {
            if (key == "cmi.objectives._count")
            {
                //Read Only
                RecordErrInfo(ResourceID, "402", key);
            }
            else
            {
                bool isRecordErr = true;
                string[] arrkey = key.Split('.');
                
                if (arrkey.Length >= 4 && arrkey[0].ToString() == "cmi" && arrkey[1].ToString() == "objectives")
                {
                    int n = IsNumber(arrkey[2].ToString());
                    if (n >= 0)//为数字
                    {
                        //知识点唯一标识    
                        if (arrkey[3].ToString() == "id")
                        {
                            isRecordErr = false;
                            //TODO：不正确的数据类型  记录错误代码;205
                            if (true)
                            {
                                if (objectivesLogic.GetObjectivesCount(ResourceID) <= n)
                                {
                                    objectivesLogic.AddObjective(ResourceID, value);
                                }
                                //interactionsLogic.SetInteractionID(ResourceID, n, ETMS.AppContext.UserContext.Current.UserID, value);
                                ReturnValue = true;
                                RecordErrInfo(ResourceID, "0", "");
                            }
                            else
                            {
                                RecordErrInfo(ResourceID, "205", key);
                            }
                        }
                        //知识点状态
                        if (arrkey.Length == 4 && arrkey[3].ToString() == "status")
                        {
                            //验证Value值 （passed,completed,failed,incomplete,browsed,not attempted）
                            isRecordErr = false;
                            if (CheckData(value))
                            {
                                objectivesLogic.SetObjectivesStatus(ResourceID, ETMS.AppContext.UserContext.Current.UserID, n, value);
                                RecordErrInfo(ResourceID, "0", "");
                                ReturnValue = true;
                            }
                            else
                            {
                                RecordErrInfo(ResourceID, "405", key);
                            }
                        }
                        //知识点得分0-100
                        else if (arrkey.Length == 5 && arrkey[3].ToString() == "score" && arrkey[4].ToString() == "raw")
                        {
                            isRecordErr = false;
                            if (IsFloat(value) != "err")
                            {
                                objectivesLogic.SetObjectivesScoreRaw(ResourceID, ETMS.AppContext.UserContext.Current.UserID, n, value);
                                RecordErrInfo(ResourceID, "0", "");
                                ReturnValue = true;
                            }
                            else
                            {
                                RecordErrInfo(ResourceID, "405", key);
                            }
                        }
                    }
                }

                if (isRecordErr)
                {
                    //记录错误401  未实现的错误
                    RecordErrInfo(ResourceID, "401", key);
                }
            }
        }
        catch
        {
            RecordErrInfo(ResourceID, "401", key);
        }
        return ReturnValue;
    }

    /// <summary>
    /// 设置数据模型中包含Interactions的值
    /// </summary>
    private bool LMSSetValueByInteractions(string key, string value, Guid ResourceID)
    {
        bool ReturnValue = false;
        InteractionsLogic interactionsLogic = new InteractionsLogic();

        try
        {
            //交互数量
            if (key == "cmi.interactions._count")
            {
                //Read only 设置值无效，记录错误代码：402
                RecordErrInfo(ResourceID, "402", key);
            }
            else
            {
                bool isRecordErr = true;
                string[] arrkey = key.Split('.');
                if (arrkey.Length == 4 && arrkey[0].ToString() == "cmi" && arrkey[1].ToString() == "interactions")
                {
                    int n = IsNumber(arrkey[2].ToString());
                    if (n >= 0)//为数字
                    {
                        //交互唯一标识    
                        if (arrkey[3].ToString() == "id")
                        {
                            isRecordErr = false;
                            //TODO：不正确的数据类型  记录错误代码;205
                            if (true)
                            {
                                if (interactionsLogic.GetInteractionCount(ResourceID) <= n)
                                {
                                    interactionsLogic.AddInteraction(ResourceID, value);
                                }
                                //interactionsLogic.SetInteractionID(ResourceID, n, ETMS.AppContext.UserContext.Current.UserID, value);
                                ReturnValue = true;
                                RecordErrInfo(ResourceID, "0", "");
                            }
                            else
                            {
                                RecordErrInfo(ResourceID, "205", key);
                            }
                        }
                        //学生答案
                        else if (arrkey[3].ToString() == "student_response")
                        {
                            isRecordErr = false;
                            //TODO：不正确的数据类型  记录错误代码;205
                            if (true)
                            {
                                interactionsLogic.SetInteractionResponse(ResourceID,ItemCourseResID, n, ETMS.AppContext.UserContext.Current.UserID, value);
                                ReturnValue = true;
                                RecordErrInfo(ResourceID, "0", "");
                            }
                            else
                            {
                                RecordErrInfo(ResourceID, "205", key);
                            }
                        }
                        //成绩结果
                        else if(arrkey[3].ToString() == "result")
                        {
                            isRecordErr = false;
                            //TODO：不正确的数据类型  记录错误代码;205
                            if (true)
                            {
                                interactionsLogic.SetInteractionResult(ResourceID,ItemCourseResID, n, ETMS.AppContext.UserContext.Current.UserID, value);
                                ReturnValue = true;
                                RecordErrInfo(ResourceID, "0", "");
                            }
                            else
                            {
                                RecordErrInfo(ResourceID, "205", key);
                            }
                        }
                    }
                }

                if (isRecordErr)
                {
                    //记录错误401
                    RecordErrInfo(ResourceID, "401", key);
                }
            }
        }
        catch
        {
            RecordErrInfo(ResourceID, "401", key);
        }
        return ReturnValue;
    }
    #endregion


    /// <summary>
    /// 如果在api接口中缓存由LMSSetValue接口提交到lms的数据，
    /// 而没有提交到lms，那么，LMSCommit方法实现了将缓存数据提交到lms。
    /// 将缓存数据提交到LMS
    /// </summary>
    public bool LMSCommit(string parm, Guid ResourceID)
    {
        RecordErrInfo(ResourceID, "0", "");
        return true;
    }

    //记录错误信息
    private void RecordErrInfo(Guid ResourceID, string ErrCode, string modekey)
    {
        ItemResourceLogic l_ItemResource = new ItemResourceLogic();
        
        CmiCoreInfo cmiCoreInfo = CmiCoreInfo.GetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString(), ResourceID.ToString(), ItemCourseResID.ToString());
        if (cmiCoreInfo != null)
        {
            cmiCoreInfo.ErrorInfo = "模型" + modekey + "错误信息：" + l_ItemResource.LMSGetErrorString(ErrCode);
            cmiCoreInfo.LastError = ErrCode;
            CmiCoreInfo.SetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString(),ResourceID.ToString(),ItemCourseResID,  cmiCoreInfo);
        }
    }

    //判断是否为数字
    private int IsNumber(string strParm)
    {
        try
        {
            return int.Parse(strParm);
        }
        catch
        {
            return -1;
        }
    }

    //判断是否为分数
    private string IsFloat(string strParm)
    {
        try
        {
            if (strParm == "")
            {
                return strParm;
            }
            else
            {
                return float.Parse(strParm).ToString();
            }
        }
        catch
        {
            return "不是分数形式的值!";
        }
    }

    
    private bool CheckData(string value)
    { 
        string CheckValue = "[" + value + "]";
        string Values = "[passed],[completed],[failed],[incomplete],[browsed],[not attempted]";
        if (Values.IndexOf(CheckValue) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
