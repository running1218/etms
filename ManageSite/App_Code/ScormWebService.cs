using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// ScormWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ScormWebService : System.Web.Services.WebService
{

    public ScormWebService()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    #region 会话状态函数
    [WebMethod(Description = "在SCO与LMS交互前初始化课件环境")]
    public bool LMSInitialize(string parm)
    {
        //初始化数据：
        //CMI_CoreID：活动编号
        //ResourceID：资源ID
        //StatusCode：状态代码
        return true;
    }

    [WebMethod(Description = "在SCO与LMS技术交互前销毁课件环境")]
    public bool LMSFinish(string parm)
    {
        return true;
    }
    #endregion

    #region 数据传输函数

    [WebMethod(Description = "获取数据模型的值")]
    public string LMSGetValue(string parm)
    {
        string ReturnValue = "";
        string[] arrParm = parm.Split('.');
        if (arrParm.Length > 2)
        {
            switch (arrParm[1])
            {
                case "core":
                    ReturnValue = LMSGetValueByCore(parm);
                    break;
                case "objectives":
                    ReturnValue = LMSGetValueByObjectives(parm);
                    break;
                case "interactions":
                    ReturnValue = LMSGetValueByInteractions(parm);
                    break;
                default:
                    //TODO：记录访问错误，
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
    private string LMSGetValueByCore(string key)
    {
        string ReturnValue = "";
        switch (key)
        {
            //存储的用户状态信息（passed,completed,failed,incomplete,browsed,not attempted）
            case "cmi.core.lesson_status":
                //TODO：获取并返回对应的值
                ReturnValue = "passed";
                break;
            //退出标记
            case "cmi.core.exit":
                //只写不读，记录错误号404
                ReturnValue = "";
                break;
            //预览模式还是普通模式（browse，normal，review）
            case "cmi.core.lesson_mode":
                //TODO：获取并返回对应的值
                ReturnValue = "browse";
                break;
            //用户姓名
            case "cmi.core.student_name":
                //TODO：获取并返回对应的值
                ReturnValue = "Student, Mikl.A.Jr";
                break;
            //用户唯一标识
            case "cmi.core.student_id":
                //TODO：获取并返回对应的值
                ReturnValue = "JS-2003";
                break;
            //SCO成绩0-100
            case "cmi.core.score.raw":
                //TODO：获取并返回对应的值
                ReturnValue = "89.7";
                break;
            //上次位置
            case "cmi.core.lesson_location":
                //TODO：获取并返回对应的值
                ReturnValue = "Http://www.openedu.cn/#lession2";
                break;
            default:
                //TODO：记录访问错误，
                ReturnValue = "";
                break;
        }

        return ReturnValue;
    }

    /// <summary>
    /// 获取数据模型中包含objectives的值
    /// </summary>
    private string LMSGetValueByObjectives(string key)
    {
        string ReturnValue = "";
        switch (key)
        {
            //知识点数量
            case "cmi.objectives._count":
                //TODO：获取并返回对应的值
                ReturnValue = "4";
                break;
            //知识点得分0-100
            case "cmi.objectives.n.score.raw":
                //TODO：获取并返回对应的值
                ReturnValue = "96.7";
                break;
            //知识点状态（passed,completed,failed,incomplete,browsed,not attempted）
            case "cmi.objectives.n.status":
                //TODO：获取并返回对应的值 其中n为序号
                ReturnValue = "passed";
                break;
            default:
                //TODO：记录访问错误，
                ReturnValue = "";
                break;
        }

        return ReturnValue;
    }

    /// <summary>
    /// 获取数据模型中包含Interactions的值
    /// </summary>
    private string LMSGetValueByInteractions(string key)
    {
        string ReturnValue = "";
        switch (key)
        {
            //交互数量
            case "cmi.interactions._count":
                //TODO：获取并返回对应的值
                ReturnValue = "5";
                break;
            //交互唯一标识
            case "cmi.interactions.n.id":
                ReturnValue = "";//同时记录错误号404  只写属性
                break;
            //学生答案
            case "cmi.interactions.n.student_response":
                ReturnValue = "";//同时记录错误号404  只写属性
                break;
            //成绩结果
            case "cmi.interactions.n.result":
                ReturnValue = "";//同时记录错误号404  只写属性
                break;
            default:
                //TODO：记录访问错误，
                ReturnValue = "";
                break;
        }

        return ReturnValue;
    }
    #endregion

    [WebMethod(Description = "设置数据模型的值")]
    public bool LMSSetValue(string key, string value)
    {
        bool returnValue = false;
        string[] arrKey = key.Split('.');
        if (arrKey.Length > 2)
        {
            switch (arrKey[1])
            {
                case "core":
                    returnValue = true;
                    break;
                case "objectives":
                    returnValue = true;
                    break;
                case "interactions":
                    returnValue = true;
                    break;
                default:
                    //TODO：记录访问错误，
                    returnValue = false;
                    break;
            }
        }
        else
        {
            //TODO：记录访问错误，
            returnValue = false;
        }
        return returnValue;
    }


    #region 根据传入的键值设置数据模型的值
    /// <summary>
    /// 设置数据模型中包含Core的值
    /// </summary>
    private bool LMSSetValueByCore(string key, string value)
    {
        bool ReturnValue = false;
        switch (key)
        {
            //用户唯一标识
            case "cmi.core.student_id":
                //Read Only 不支持设置值 记录错误代码403
                ReturnValue = false;
                break;
            //用户姓名
            case "cmi.core.student_name":
                //Read Only 不支持设置值 记录错误代码403
                ReturnValue = false;
                break;
            //存储的用户状态信息（passed,completed,failed,incomplete,browsed,not attempted）
            case "cmi.core.lesson_status":
                //验证数据状态是否存在上面的，否则记录405
                ReturnValue = true;
                break;
            //退出标记（time-out,suspend,logout,""）
            case "cmi.core.exit":
                //验证数据状态是否存在上面的，否则记录405
                ReturnValue = true;
                break;
            //预览模式还是普通模式
            case "cmi.core.lesson_mode":
                //Read Only 不支持设置值 记录错误代码403
                ReturnValue = false;
                break;
            //SCO成绩0-100
            case "cmi.core.score.raw":
                //验证数据类型 405
                ReturnValue = true;
                break;
            //上次位置
            case "cmi.core.lesson_location":
                //是否需验证数据类型
                ReturnValue = true;
                break;
            default:
                //TODO：记录访问错误，
                ReturnValue = true;
                break;
        }

        return ReturnValue;
    }

    /// <summary>
    /// 设置数据模型中包含objectives的值
    /// </summary>
    private bool LMSSetValueByObjectives(string key, string value)
    {
        bool ReturnValue = false;
        switch (key)
        {
            //知识点数量
            case "cmi.objectives._count":
                //记录错误代码402
                ReturnValue = false;
                break;
            //知识点得分0-100
            case "cmi.objectives.n.score.raw":
                //数据类型Decimal ,int,""
                ReturnValue = true;
                break;
            //知识点状态（passed,completed,failed,incomplete,browsed,not attempted）
            case "cmi.objectives.n.status":
                //TODO：获取并返回对应的值
                ReturnValue = true;
                break;
            default:
                //TODO：记录访问错误，
                ReturnValue = false;
                break;
        }

        return ReturnValue;
    }

    /// <summary>
    /// 设置数据模型中包含Interactions的值
    /// </summary>
    private bool LMSSetValueByInteractions(string key, string value)
    {
        bool ReturnValue = false;
        switch (key)
        {
            //交互数量
            case "cmi.interactions._count":
                //设置值无效，记录错误代码：402
                ReturnValue = false;
                break;
            //交互唯一标识
            case "cmi.interactions.n.id":
                //不正确的数据类型  记录错误代码;205
                ReturnValue = true;
                break;
            //学生答案
            case "cmi.interactions.n.student_response":
                //不正确的数据类型  记录错误代码;205
                ReturnValue = true;
                break;
            //成绩结果(corrent,worng,)
            case "cmi.interactions.n.result":
                //不正确的数据类型  记录错误代码;205
                ReturnValue = true;
                break;
            default:
                //TODO：记录访问错误，
                ReturnValue = false;
                break;
        }

        return ReturnValue;
    }
    #endregion




    //如果在api接口中缓存由LMSSetValue接口提交到lms的数据，
    //而没有提交到lms，那么，LMSCommit方法实现了将缓存数据提交到lms。
    [WebMethod(Description = "将缓存数据提交到LMS")]
    public bool LMSCommit(string parm)
    {
        return true;
    }
    #endregion

    #region 错误处理管理函数
    //Sco调用api方法，如果失败，调用这个函数返回错误状态代码，
    //每次api函数的调用，都要更新错误代码。
    [WebMethod(Description = "获取最后一次错误的代码")]
    public int LMSGetLastError()
    {
        return 0;
    }

    //获取错误代码的错误描述信息
    [WebMethod(Description = "根据错误代码获取错误信息")]
    public string LMSGetErrorString(string ECode)
    {
        return ECode;
    }

    [WebMethod(Description = "参数有两种可能：1.错误代码，返结果同LMSGetErrorString、2.空字符串，返回最后一个错误描述，相当于LMSGetErrorString(int ECode)")]
    public string LMSGetDiagnostic(string ECode)
    {
        return ECode;
    }
    #endregion
}



