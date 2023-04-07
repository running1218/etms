using System;
using System.Web.UI;

namespace ETMS.Utility
{
    public static class JsUtility
    {
        #region �������ڹ���

        #endregion

        /// <summary>
        /// �������ڣ����ڵĿ�ȡ��߶Ȳ���ϵͳĬ�ϣ����Ժ�����Ӧ��
        /// </summary>
        /// <param name="url"></param>
        public static void ShowWindow(string url)
        {
            ShowWindow(url, 600, 600);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="url">����URL</param>
        /// <param name="width">ָ�����ڿ��</param>
        /// <param name="height">ָ�����ڸ߶�</param>
        public static void ShowWindow(string url, int width, int height)
        {

        }

        /// <summary>
        /// �رմ���
        /// </summary>
        public static void CloseWindow()
        {
            CloseWindow("");
        }

        /// <summary>
        /// �رմ��ڣ��ر�ͬʱ֧�ֻص�����
        /// </summary>
        /// <param name="handler">�ص���js function</param>
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
        /// ��ʾ��Ĭ�ϱ��⣺������ʾ��
        /// </summary>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void AlertMessageBox(string message)
        {
            AlertMessageBox("������ʾ", message);
        }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void AlertMessageBox(string title, string message)
        {
            AlertMessageBox(title, message, null);
        }

        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
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
        /// �ɹ���ʾ��Ĭ�ϱ��⣺������ʾ��
        /// </summary>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBox(string message)
        {
            SuccessMessageBox("������ʾ", message);
        }
        /// <summary>
        /// �ɹ���ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBox(string title, string message)
        {
            SuccessMessageBox(title, message, null);
        }
        /// <summary>
        /// �ɹ���ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
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
        /// ��ʾ�����ɹ���Ϣ��������ǰ��������ˢ���¼�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBoxAndTriggerRefreshEvent(string message)
        {
            SuccessMessageBoxAndTriggerRefreshEvent("������ʾ", message);
        }
        /// <summary>
        /// ��ʾ�����ɹ���Ϣ��������ǰ��������ˢ���¼�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBoxAndTriggerRefreshEvent(string title, string message)
        {
            SuccessMessageBox(title, message, "triggerRefreshEvent");
        }

        /// <summary>
        /// ��ʾ�����ɹ���Ϣ��������ǰ��������ˢ���¼�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBoxAndTriggerSearchEvent(string message)
        {
            SuccessMessageBoxAndTriggerSearchEvent("������ʾ", message);
        }
        /// <summary>
        /// ��ʾ�����ɹ���Ϣ��������ǰ�������ݲ�ѯ�¼�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBoxAndTriggerSearchEvent(string title, string message)
        {
            SuccessMessageBox(title, message, "triggerSearchEvent");
        }

        public static void SuccessMessageBoxAndTriggerSearchEvent(string title, string message, bool isParentForm)
        {
            if (isParentForm)
                SuccessMessageBox(title, message, "triggerParentSearchEvent()");
        }

        #region ��ʾ�����ɹ���Ϣ���ر�ģ̬���ڡ������������¼�
        /// <summary>
        /// ��ʾ�����ɹ���Ϣ���ر�ģ̬���ڣ���������������ˢ���¼���Ĭ�ϱ��⣺������ʾ��
        /// </summary>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(string message)
        {
            SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("������ʾ", message);
        }

        /// <summary>
        /// ��ʾ�����ɹ���Ϣ���ر�ģ̬���ڣ���������������ˢ���¼�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, "window.parent.triggerRefreshEvent");
        }

        /// <summary>
        /// ��ʾ�����ɹ���Ϣ���ر�ģ̬���ڣ��������������ݲ�ѯ�¼���Ĭ�ϱ��⣺������ʾ��
        /// </summary>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>       
        public static void SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent(string message)
        {
            SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("������ʾ", message);
        }

        /// <summary>
        /// ��ʾ�����ɹ���Ϣ���ر�ģ̬���ڣ��������������ݲ�ѯ�¼�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent(string title, string message)
        {
            SuccessMessageBoxAndCloseWindow(title, message, "window.parent.triggerSearchEvent");
        }

        /// <summary>
        /// ��ȡ�ؼ��ͻ���ȷ�Ͽ�ű�����
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">����</param>
        /// <returns>�ű�����</returns>
        public static string GetControlClientConfirmScriptCode(string message)
        {
            return GetControlClientConfirmScriptCode("����ȷ��", message);
        }
        /// <summary>
        /// ��ȡ�ؼ��ͻ���ȷ�Ͽ�ű�����
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">����</param>
        /// <returns>�ű�����</returns>
        public static string GetControlClientConfirmScriptCode(string title, string message)
        {
            return string.Format("popConfirmMsgForControl(this,'{0}','{1}');if(this.isCalled==undefined) return false;", message, title);
        }
        /// <summary>
        /// �ɹ���ʾ���-->�ر�ģ̬����-->�ص��ض���js function ��Ĭ�ϱ��⣺������ʾ��
        /// �磺ģ̬�����С����桱������ɺ���ʾ�����ɹ�-->�ر�ģ̬����-->����ˢ�¸������б�
        /// </summary>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
        public static void SuccessMessageBoxAndCloseWindow(string message)
        {
            SuccessMessageBoxAndCloseWindow("������ʾ", message, "");
        }

        /// <summary>
        /// �ɹ���ʾ���-->�ر�ģ̬����-->�ص��ض���js function��Ĭ�ϱ��⣺������ʾ��
        /// �磺ģ̬�����С����桱������ɺ���ʾ�����ɹ�-->�ر�ģ̬����-->����ˢ�¸������б�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
        public static void SuccessMessageBoxAndCloseWindow(string message, string handler)
        {
            SuccessMessageBoxAndCloseWindow("������ʾ", message, handler);
        }

        /// <summary>
        /// �ɹ���ʾ���-->�ر�ģ̬����-->�ص��ض���js function
        /// �磺ģ̬�����С����桱������ɺ���ʾ�����ɹ�-->�ر�ģ̬����-->����ˢ�¸������б�
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
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
        /// ʧ����ʾ��Ĭ�ϱ��⣺������ʾ��
        /// </summary>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void FailedMessageBox(string message)
        {
            FailedMessageBox("������ʾ", message);
        }

        /// <summary>
        /// ʧ����ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void FailedMessageBox(string title, string message)
        {
            FailedMessageBox(title, message, null);
        }

        public static void KeyRepeatMessageBox(string message)
        {
            KeyRepeatMessageBox("������ʾ", message, null);
        }

        /// <summary>
        /// ����ҵ���ж��߼��ظ�
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public static void KeyRepeatMessageBox(string title, string message)
        {
            KeyRepeatMessageBox(title, message, null);
        }
        /// <summary>
        /// ʧ����ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
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
        /// ʧ����ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
        public static void FailedMessageBoxAndRedirectToParent(string message)
        {
            string handler = "window.history.go(-1)";
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popFailedMsg('{0}','{1}',function()$closeWindow({2});^); \r\n", message, "��ʾ��Ϣ", handler) +
                "</Script> \r\n";
            sJsContent = sJsContent.Replace("$", "{").Replace("^", "}");
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// ʧ����ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
        public static void SuccessMessageBoxAndRedirectToParent(string message, string parentUrl)
        {
            string handler = string.Format("window.location.href='{0}'", parentUrl);
            System.Random r = new Random();
            string sKey = r.Next(9999999).ToString();
            message = message.Replace("'", "");
            string sJsContent;
            sJsContent =
                "<Script Language='JavaScript'> \r\n" +
                string.Format("   popSuccessMsg('{0}','{1}',function()$closeWindow({2});^); \r\n", message, "��ʾ��Ϣ", handler) +
                "</Script> \r\n";
            sJsContent = sJsContent.Replace("$", "{").Replace("^", "}");
            System.Web.UI.Page page = (System.Web.HttpContext.Current.Handler) as System.Web.UI.Page;
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), sKey, sJsContent, false);
        }

        /// <summary>
        /// ʧ����ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
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
        /// ȷ����ʾ��Ĭ�ϱ��⣺������ʾ��
        /// </summary>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void ConfirmMessageBox(string message)
        {
            ConfirmMessageBox("������ʾ", message);
        }

        /// <summary>
        /// ȷ����ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        public static void ConfirmMessageBox(string title, string message)
        {
            ConfirmMessageBox(title, message, null);
        }

        /// <summary>
        /// ȷ����ʾ��
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">��Ϣ���ݣ�֧��html��ʽ</param>
        /// <param name="handler">���"ȷ��"ʱ�ص��¼�</param>
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
        /// ��������
        /// </summary>
        /// <param name="sUrl">��ַ</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <param name="page">ԭҳ��</param>
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
        /// �򿪴���
        /// </summary>
        /// <param name="sUrl">��ַ</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <param name="page">ԭҳ��</param>
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
        /// ��ʾ��Ϣ����
        /// </summary>
        /// <param name="sMessageText">��Ϣ</param>
        /// <param name="page">ҳ��</param>
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
        /// ��ʾ��Ϣ���رմ���
        /// </summary>
        /// <param name="sMessageText">��Ϣ</param>
        /// <param name="page">��Ҫ�رյ�ҳ��</param>
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
        /// ��ʾ��Ϣ����ת
        /// </summary>
        /// <param name="sMessageText">��Ϣ</param>
        /// <param name="sDescURL">��ת��ַ</param>
        /// <param name="page">ҳ��</param>
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
        /// ��֧��IE5.5����
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
        /// ˢ�¶�����ҳ
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
        /// ˢ��ҳ��
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
        /// ��ȡ�ؼ��ͻ���ȷ�Ͽ�ű�����
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="message">����</param>
        /// <returns>�ű�����</returns>
        public static string GetCheckedValueAndConfirmScriptCode(string checkMsg, string message)
        {
            return string.Format("return IsSelectRecord('{2}'); popConfirmMsgForControl(this,'{0}','{1}');if(this.isCalled==undefined) return false;", message, "��ʾ��Ϣ", checkMsg);
        }
    }
}
