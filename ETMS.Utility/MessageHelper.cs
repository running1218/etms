using System;
using System.Web.UI;


namespace ETMS.Utility
{
    public static class MessageHelper
    {
        public static void ShowScriptManagerMessage(this Page page, string message)
        {
            string script = string.Format("<Script Language='JavaScript'>popFailedBox('{0}','{1}', null);</Script>", message, "提示信息");
            ScriptManager.RegisterStartupScript(page, typeof(string), "提示信息", script, false);
        }

        /// <summary>
        /// 失败提示框（默认标题：操作提示）
        /// </summary>
        /// <param name="message">消息内容，支持html格式</param>
        public static void ShowScriptManagerMessage(string message)
        {
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            string script = string.Format("<Script Language='JavaScript'>popFailedBox('{0}','{1}', null);</Script>", message, "提示信息");
            ScriptManager.RegisterStartupScript(page, typeof(string), "提示信息", script, false);
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
            SuccessMessageBox(title, message, "undefined");
        }

        /// <summary>
        /// 成功提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void SuccessMessageBox(string title, string message, string handler)
        {
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            message = message.Replace("'", "");
            string script = string.Format("<Script lang='javascript'>popSuccessBox('{0}','{1}', {2});</Script>", message, title, handler);
            ScriptManager.RegisterStartupScript(page, typeof(string), title, script, false);
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
            AlertMessageBox(title, message, "undefined");
        }

        /// <summary>
        /// 消息提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void AlertMessageBox(string title, string message, string handler)
        {
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            message = message.Replace("'", "");
            string script = string.Format("<Script Language='JavaScript'>popAlertBox('{0}','{1}', {2});</Script>", message, title, handler);
            ScriptManager.RegisterStartupScript(page, typeof(string), title, script, false);
        }

        /// <summary>
        /// 成功提示框后-->关闭模态窗口-->回调特定的js function
        /// 如：模态窗口中“保存”操作完成后，提示操作成功-->关闭模态窗口-->触发刷新父窗口列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void SuccessMessageBoxAndCloseWindow(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, string.Empty);
        }

        public static void SuccessMessageBoxAndCloseWindow(string title, string message, string handler)
        {
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            message = message.Replace("'", "");
            string script = string.Format("<Script Language='JavaScript'>popSuccessBoxAndCloseParent('{0}','{1}',{2})</Script>", message, title, handler);
            ScriptManager.RegisterStartupScript(page, typeof(string), DateTime.Now.ToString(), script, false);
        }

        /// <summary>
        /// 成功并关闭当前窗口，刷新父页面
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, "window.parent.triggerRefreshEvent");
        }

        /// <summary>
        /// 关闭，刷新父页面
        /// </summary>
        public static void CloseWindowAndTriggerRefreshEvent()
        {
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            string script = string.Format("<Script Language='JavaScript'>closeLayer({0});</Script>", "function(){window.parent.location = window.parent.location}");
            ScriptManager.RegisterStartupScript(page, typeof(string), DateTime.Now.ToString(), script, false);
        }

        /// <summary>
        /// 成功并关闭当前窗口，刷新父页面
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(string message)
        {
            SuccessMessageBoxAndCloseWindow("提示信息", message, "function(){window.parent.location = window.parent.location}");
        }

        /// <summary>
        /// 成功并关闭当前窗口，刷新父页面 使用此方法 {window.top.location = window.top.location}
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessMessageBoxAndCloseWindowAndTopLocation(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, "function(){window.top.location = window.top.location}");
        }
        /// <summary>
        /// 成功并关闭当前窗口，刷新父页面 使用此方法 {window.location = window.location}
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessMessageBoxAndCloseWindowAndParentLocation(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, "function(){window.parent.location = window.parent.location}");
        }

        /// <summary>
        /// 成功并关闭当前窗口，刷新父页面 使用此方法 {window.top.location = window.top.location}
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessMessageBoxAndCloseWindowAndTopLocation(string message)
        {
            SuccessMessageBoxAndCloseWindow("提示信息", message, "function(){window.top.location = window.top.location}");
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        /// <param name="handler">点击"确定"时回调事件</param>
        public static void FailedMessageBox(string title, string message, string handler)
        {
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            message = message.Replace("'", "").Replace("\r", "").Replace("\n", "");
            string script = string.Format("<Script Language='JavaScript'>popFailedBox('{0}','{1}', {2});</Script>", message, title, handler ?? "undefined");
            ScriptManager.RegisterStartupScript(page, typeof(string), title, script, false);
        }

        /// <summary>
        /// 失败提示框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容，支持html格式</param>
        public static void FailedMessageBox(string message)
        {
            FailedMessageBox("提示消息", message, "undefined");
        }

    }
}
