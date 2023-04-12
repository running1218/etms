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

public partial class Scorm_WebService_ErrorManage : System.Web.UI.Page
{
    private string UserName
    {
        get
        {
            return Request["UserName"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        string ReturnValue = "";

        if (Request["t"] != null && Request["id"] != null)
        {
            Guid ResourceID = new Guid(Request["id"].ToString());
            switch (Convert.ToString(Request["t"]))
            {
                //GetLastError
                case "5":
                    ReturnValue = LMSGetLastError(ResourceID).ToString();
                    break;
                //GetErrorString
                case "6":
                    ReturnValue = LMSGetErrorString(Convert.ToString(Request["parm"]));
                    break;
                //GetDiagnostic
                case "7":
                    ReturnValue = LMSGetDiagnostic(Convert.ToString(Request["parm"]), ResourceID);
                    break;
            }
        }
        
  
        Response.Clear();
        Response.Write(ReturnValue.ToString().ToLower());
        Response.Flush();
        Response.Close();
    }

    #region 错误处理管理函数
    /// <summary>
    /// Sco调用api方法，如果失败，调用这个函数返回错误状态代码，
    /// 每次api函数的调用，都要更新错误代码。
    /// 获取最后一次错误的代码
    /// </summary>
    public string LMSGetLastError(Guid ResourceID)
    {
        string ErrorCode;
        try
        {
            ItemResourceLogic l_ItemResource = new ItemResourceLogic();
            //从Session中获取最后一次出现的错误代码
            CmiCoreInfo cmiCoreInfo = CmiCoreInfo.GetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString(),ResourceID.ToString(), string.Empty);

            ErrorCode = cmiCoreInfo.LastError;
        }
        catch {
            ErrorCode = "0";
        }
        return ErrorCode;

    }

    /// <summary>
    /// 根据错误代码获取错误信息
    /// </summary>
    public string LMSGetErrorString(string ECode)
    {
        string ErrString = "";

        try
        {
            switch (ECode)
            {
                case "0":
                    ErrString = "No error";
                    break;
                case "101":
                    ErrString = "General exception";
                    break;
                case "201":
                    ErrString = "Invalid argument error";
                    break;
                case "202":
                    ErrString = "Element cannot have children";
                    break;
                case "203":
                    ErrString = "Element not an array - cannot have count";
                    break;
                case "301":
                    ErrString = "Not initialized";
                    break;
                case "401":
                    ErrString = "Not implemented error";
                    break;
                case "402":
                    ErrString = "Invlid set value,element is a keyword";
                    break;
                case "403":
                    ErrString = "Element is ready only";
                    break;
                case "404":
                    ErrString = "Element is write only";
                    break;
                case "405":
                    ErrString = "Incorrect Data Type";
                    break;

            }
        }
        catch(Exception ex)
        {
            ECode = "0";
            ErrString = "";
        }
        return ErrString;
    }

    /// <summary>
    /// 参数有两种可能：1.错误代码，返结果同LMSGetErrorString、
    /// 2.空字符串，返回最后一个错误描述，相当于LMSGetErrorString(int ECode)
    /// </summary>
    /// <param name="ECode"></param>
    /// <returns></returns>
    public string LMSGetDiagnostic(string ECode, Guid ResourceID)
    {
        string returnValue = "";
        if (string.IsNullOrEmpty(ECode) || ECode=="null")
        {

            ItemResourceLogic l_ItemResource = new ItemResourceLogic();
            //从Session中获取最后一次出现的错误代码
            CmiCoreInfo cmiCoreInfo = CmiCoreInfo.GetCurrentCoreInfo(ETMS.AppContext.UserContext.Current.UserID.ToString(), ResourceID.ToString(), string.Empty);

            returnValue=cmiCoreInfo.ErrorInfo;
            //returnValue = LMSGetErrorString(LMSGetLastError(ResourceID).ToString());
        }
        else
        {
            returnValue = LMSGetErrorString(ECode);
        }
        return returnValue;
    }
    #endregion
}
