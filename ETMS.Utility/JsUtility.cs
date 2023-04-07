using System;
using System.Web.UI;

namespace ETMS.Utility
{
    public static class JsUtility
    {
        #region 弹出窗口管理

        #endregion

        /// <summary>
        /// 弹出窗口，窗口的宽度、高度采用系统默认（或以后自适应）
        /// </summary>
        /// <param name="url"></param>
        public static void ShowWindow(string url)
        {
            ShowWindow(url, 600, 600);
        }

        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="url">内容URL</param>
        /// <param name="width">指定窗口宽度</param>
        /// <param name="height">指定窗口高度</param>
        public static void ShowWindow(string url, int width, int height)
        {

        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public static void CloseWindow()
        {
            CloseWindow("");
        }

        /// <summary>
        /// 关闭窗口，关闭同时支持回调操作
        /// </summary>
        /// <param name="handler">回调的js function</param>
        public static void CloseWindow(string handler)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                "   closeWindow(" + handler + "); \r\n" +
                "</Script> \r\n";
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);

        }

        /// <summary>
        /// 提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void AlertMessageBox(string message)
        {
            AlertMessageBox("操作提示", message);
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
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 成功提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBox(string message)
        {
            SuccessMessageBox("操作提示", message);
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
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 提示操作成功信息，触发当前窗口数据刷新事件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBoxAndTriggerRefreshEvent(string message)
        {
            SuccessMessageBoxAndTriggerRefreshEvent("操作提示", message);
        }
        /// <summary>
        /// 提示操作成功信息，触发当前窗口数据刷新事件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBoxAndTriggerRefreshEvent(string title, string message)
        {
            SuccessMessageBox(title, message, "triggerRefreshEvent");
        }

        /// <summary>
        /// 提示操作成功信息，触发当前窗口数据刷新事件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBoxAndTriggerSearchEvent(string message)
        {
            SuccessMessageBoxAndTriggerSearchEvent("操作提示", message);
        }
        /// <summary>
        /// 提示操作成功信息，触发当前窗口数据查询事件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBoxAndTriggerSearchEvent(string title, string message)
        {
            SuccessMessageBox(title, message, "triggerSearchEvent");
        }

        public static void SuccessMessageBoxAndTriggerSearchEvent(string title, string message, bool isParentForm)
        {
            if (isParentForm)
                SuccessMessageBox(title, message, "triggerParentSearchEvent()");
        }

        #region 提示操作成功信息、关闭模态窗口、触发父窗口事件
        /// <summary>
        /// 提示操作成功信息，关闭模态窗口，触发父窗口数据刷新事件（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(string message)
        {
            SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("操作提示", message);
        }

        /// <summary>
        /// 提示操作成功信息，关闭模态窗口，触发父窗口数据刷新事件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, "window.parent.triggerRefreshEvent");
        }

        /// <summary>
        /// 提示操作成功信息，关闭模态窗口，触发父窗口数据查询事件（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>       
        public static void SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent(string message)
        {
            SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("操作提示", message);
        }

        /// <summary>
        /// 提示操作成功信息，关闭模态窗口，触发父窗口数据查询事件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, "window.parent.triggerSearchEvent");
        }

        /// <summary>
        /// 获取控件客户端确认框脚本代码
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        /// <returns>脚本代码</returns>
        public static string GetControlClientConfirmScriptCode(string message)
        {
            return GetControlClientConfirmScriptCode("操作确认", message);
        }
        /// <summary>
        /// 获取控件客户端确认框脚本代码
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        /// <returns>脚本代码</returns>
        public static string GetControlClientConfirmScriptCode(string title, string message)
        {
            return string.Format("popConfirmMsgForControl(this,'{0}','{1}');if(this.isCalled==undefined) return false;", message, title);
        }
        /// <summary>
        /// 成功提示框后-->关闭模态窗口-->回调特定的js function （默认标题：操作提示）
        /// 如：模态窗口中“保存”操作完成后，提示操作成功-->关闭模态窗口-->触发刷新父窗口列表
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void SuccessMessageBoxAndCloseWindow(string message)
        {
            SuccessMessageBoxAndCloseWindow("操作提示", message, "");
        }

        /// <summary>
        /// 成功提示框后-->关闭模态窗口-->回调特定的js function（默认标题：操作提示）
        /// 如：模态窗口中“保存”操作完成后，提示操作成功-->关闭模态窗口-->触发刷新父窗口列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void SuccessMessageBoxAndCloseWindow(string message, string handler)
        {
            SuccessMessageBoxAndCloseWindow("操作提示", message, handler);
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
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        #endregion
        /// <summary>
        /// 失败提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void FailedMessageBox(string message)
        {
            FailedMessageBox("操作提示", message);
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void FailedMessageBox(string title, string message)
        {
            FailedMessageBox(title, message, null);
        }

        public static void KeyRepeatMessageBox(string message)
        {
            KeyRepeatMessageBox("操作提示", message, null);
        }

        /// <summary>
        /// 定义业务判断逻辑重复
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public static void KeyRepeatMessageBox(string title, string message)
        {
            KeyRepeatMessageBox(title, message, null);
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
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void FailedMessageBoxAndRedirectToParent(string message)
        {
            string handler = "window.history.go(-1)";
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popFailedMsg('{0}','{1}',function()$closeWindow({2});^); \r\n", message, "提示信息", handler) +
                "</Script> \r\n";
            sJsContent = sJsContent.Replace("$", "{").Replace("^", "}");
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void SuccessMessageBoxAndRedirectToParent(string message, string parentUrl)
        {
            string handler = string.Format("window.location.href='{0}'", parentUrl);
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popSuccessMsg('{0}','{1}',function()$closeWindow({2});^); \r\n", message, "提示信息", handler) +
                "</Script> \r\n";
            sJsContent = sJsContent.Replace("$", "{").Replace("^", "}");
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void KeyRepeatMessageBox(string title, string message, string handler)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popBusinessRegulerMsg('{0}','{1}'{2}); \r\n", message, title, (string.IsNullOrEmpty(handler) ? "" : "," + handler)) +
                "</Script> \r\n";
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 确认提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void ConfirmMessageBox(string message)
        {
            ConfirmMessageBox("操作提示", message);
        }

        /// <summary>
        /// 确认提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void ConfirmMessageBox(string title, string message)
        {
            ConfirmMessageBox(title, message, null);
        }

        /// <summary>
        /// 确认提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void ConfirmMessageBox(string title, string message, string handler)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popConfirmMsg('{0}','{1}'{2}); \r\n", message, title, (string.IsNullOrEmpty(handler) ? "" : "," + handler)) +
                "</Script> \r\n";
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="page">原页面</param>
        public static void PopupWindow(string url, int width, int height, System.Web.UI.Page page)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent =
                    "<SCRIPT LANGUAGE=\"javascript\">\r\n" +
                        "<!--\r\n" +
                        " window.open (\"" + url + "\", \"newwindow\", \"height=" + height.ToString() + ", width=" + width.ToString() + ", top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no\");\r\n" +
                        "-->\r\n" +
                    "</SCRIPT>\r\n";

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="page">原页面</param>
        public static void OpenWindow(string url, int width, int height, System.Web.UI.Page page)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent =
                    "<SCRIPT LANGUAGE=\"javascript\">\r\n" +
                        "<!--\r\n" +
                        " window.open (\"" + url + "\", \"newwindow\", \"height=" + height.ToString() + ", width=" + width.ToString() + ", top=0, left=0, toolbar=yes, menubar=yes, scrollbars=yes, resizable=yes,location=yes, status=yes\");\r\n" +
                        "-->\r\n" +
                    "</SCRIPT>\r\n";

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }


        /// <summary>
        /// 提示消息窗口
        /// </summary>
        /// <param name="sMessageText">消息</param>
        /// <param name="page">页面</param>
        /// <remarks>ZhouYonghua, 20070708</remarks>
        public static void MessageBox(string sMessageText, System.Web.UI.Page page)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                "   alert(\"" + sMessageText + "\"); \r\n" +
                "</Script> \r\n";

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 提示消息并关闭窗口
        /// </summary>
        /// <param name="sMessageText">消息</param>
        /// <param name="page">需要关闭的页面</param>
        /// <remarks>ZhouYonghua, 20070708</remarks>
        public static void MessageBoxAndClose(string sMessageText, System.Web.UI.Page page)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                "   alert(\"" + sMessageText + "\"); \r\n" +
                "  window.opener=null;window.open('','_self');window.close(); \r\n " +
                "</Script> \r\n";

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 提示消息并跳转
        /// </summary>
        /// <param name="sMessageText">消息</param>
        /// <param name="sDescURL">跳转地址</param>
        /// <param name="page">页面</param>
        /// <remarks>ZhouYonghua, 20070710</remarks>
        public static void MessageBoxAndRedirect(string sMessageText, string sDescURL, System.Web.UI.Page page)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent = "<Script Language='JavaScript'> \r\n" +
                "   alert('" + sMessageText + "'); \r\n" +
                "   location=\"" + sDescURL + "\"; \r\n " +
                "</Script> \r\n";

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 仅支持IE5.5以上
        /// </summary>
        /// <param name="sMessageText"></param>
        /// <param name="page"></param>
        public static void MessageBoxAndCloseAndReloadParent(string sMessageText, System.Web.UI.Page page)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent = "<Script Language='JavaScript'> \r\n";
            sJsContent += "   alert(\"" + sMessageText + "\"); \r\n";
            sJsContent += "	  window.opener.location.reload();\r\n";
            sJsContent += "  window.opener=null;window.open('','_self');window.close(); \r\n ";
            sJsContent += "</Script> \r\n";

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }
        /// <summary>
        /// 刷新顶层框架页
        /// </summary>
        /// <param name="sMessageText"></param>
        /// <param name="page"></param>
        public static void MessageBoxAndReloadTop(string sMessageText, string sDescURL, System.Web.UI.Page page)
        {
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();

            string sJsContent;
            sJsContent = "<Script Language='JavaScript'> \r\n";
            sJsContent += "   alert(\"" + sMessageText + "\"); \r\n";
            sJsContent += "	  window.top.location=\"" + sDescURL + "\";\r\n";
            //sJsContent += "   window.close(); \r\n ";
            sJsContent += "</Script> \r\n";

            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="BtnClientID"></param>
        /// <param name="page"></param>
        public static void RefreshPage(string BtnClientID, System.Web.UI.Page page)
        {
            string sKey = "RefreshPage";
            string sScript = @"<script language='javascript' type='text/javascript'>
                                    function refreshpage()
                                    {0}
                                        document.getElementById('{1}').click();
                                    {2}
                                </script>";
            if (!page.ClientScript.IsClientScriptBlockRegistered(sKey))
            {
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, string.Format(sScript, "{", BtnClientID, "}"), false);
            }
        }

        /// <summary>
        /// 获取控件客户端确认框脚本代码
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        /// <returns>脚本代码</returns>
        public static string GetCheckedValueAndConfirmScriptCode(string checkMsg, string message)
        {
            return string.Format("return IsSelectRecord('{2}'); popConfirmMsgForControl(this,'{0}','{1}');if(this.isCalled==undefined) return false;", message, "提示信息", checkMsg);
        }
    }
}
