using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETMS.Controls;
using ETMS.Utility.Service.FileUpload;
using System.Web.UI;

namespace ETMS.WebApp.Manage
{
    /// <summary>
    ///Extention 的摘要说明
    /// </summary>
    public static class Extention
    {
        public static UploadFileDefine UploadFileEntity(this FileUpload control)
        {
            UploadFileDefine entity = new UploadFileDefine();
            FileUploadCard fileUploadCard = control.SaveUploadFiles();
            if (fileUploadCard.Files.Count > 0)
            {
                entity = fileUploadCard.FileDetails[0];
            }

            return entity;
        }

        public static void ShowScriptManagerMessage(this Page page, string message)
        {
            string sJsContent = "<Script Language='JavaScript'> \r\n" +
                                    string.Format("   popFailedMsg('{0}','{1}'); \r\n", message, "提示信息") +
                                    "</Script> \r\n";

            ScriptManager.RegisterStartupScript(page, typeof(string), "提示信息", sJsContent, false);
        }

        /// <summary>
        /// 失败提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void ShowScriptManagerMessage(string message)
        {
            string sJsContent = "<Script Language='JavaScript'> \r\n" +
                                    string.Format("   popFailedMsg('{0}','{1}'); \r\n", message, "提示信息") +
                                    "</Script> \r\n";

            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterStartupScript(page, typeof(string), "提示信息", sJsContent, false);
        }

        /// <summary>
        /// 成功提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBox(string message)
        {
            SuccessMessageBox("操作提示：", message);
        }
        /// <summary>
        /// 成功提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBox(string title, string message)
        {
            SuccessMessageBox(title, message, null);
        }

        /// <summary>
        /// 成功提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void SuccessMessageBox(string title, string message, string handler)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popSuccessMsg('{0}','{1}'{2}); \r\n", message, title, (string.IsNullOrEmpty(handler) ? "" : "," + handler)) +
                "</Script> \r\n";
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            //ScriptManager.RegisterClientScriptBlock(page.GetType(), sKey, sJsContent);
            ScriptManager.RegisterStartupScript(page, typeof(string), title, sJsContent, false);
        }

        /// <summary>
        /// 提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void AlertMessageBox(string message)
        {
            AlertMessageBox("操作提示：", message);
        }
        /// <summary>
        /// 提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void AlertMessageBox(string title, string message)
        {
            AlertMessageBox(title, message, null);
        }

        /// <summary>
        /// 消息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void AlertMessageBox(string title, string message, string handler)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popAlertMsg('{0}','{1}'{2}); \r\n", message, title, (string.IsNullOrEmpty(handler) ? "" : "," + handler)) +
                "</Script> \r\n";
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            //ScriptManager.RegisterClientScriptBlock(page.GetType(), sKey, sJsContent);
            ScriptManager.RegisterStartupScript(page, typeof(string), title, sJsContent, false);
        }

        /// <summary>
        /// 成功提示框后-->关闭模态窗口-->回调特定的js function
        /// 如：模态窗口中“保存”操作完成后，提示操作成功-->关闭模态窗口-->触发刷新父窗口列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void SuccessMessageBoxAndCloseWindow(string title, string message, string handler)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popSuccessMsg('{0}','{1}',function()$closeWindow({2});^); \r\n", message, title, handler) +
                "</Script> \r\n";
            sJsContent = sJsContent.Replace("$", "{").Replace("^", "}");
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            //ScriptManager.RegisterClientScriptBlock(page.GetType(), sKey, sJsContent);
            ScriptManager.RegisterStartupScript(page, typeof(string), title, sJsContent, false);
        }

        /// <summary>
        /// 成功并关闭当前窗口，刷新父页面
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessMessageBoxAndCloseWindow(string message)
        {
            SuccessMessageBoxAndCloseWindow("消息提示", message, "window.parent.triggerRefreshEvent");
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void FailedMessageBox(string title, string message, string handler)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "").Replace("\r", "").Replace("\n", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popFailedMsg('{0}','{1}'{2}); \r\n", message, title, (string.IsNullOrEmpty(handler) ? "" : "," + handler)) +
                "</Script> \r\n";
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            //ScriptManager.RegisterClientScriptBlock(page.GetType(), sKey, sJsContent);
            ScriptManager.RegisterStartupScript(page, typeof(string), title, sJsContent, false);
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void FailedMessageBox( string message)
        {
            FailedMessageBox("提示消息", message, null);
        }
    }
}