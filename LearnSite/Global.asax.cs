using University.Mooc.AppContext;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using System;

namespace Studying
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //加载应用上下文
            ApplicationContextLoader.Load();
            //加载-系统字典数据(通过回调的方式获取字典数据源,优点：1、页面访问到字典下拉时加载才加载数据 2、字典数据缓存由回调方法维护）
            foreach (string item in Enum.GetNames(typeof(ETMS.Components.Basic.API.Entity.Dictionary.SysDicionaryTypeEnum)))
            {
                ETMS.Controls.LoadDictionaryDataHandler handler = new LoadDictionaryDataHandler(delegate (string dicType)
                {
                    return ETMS.Components.Basic.Implement.BLL.Dictionary.SysDict.GetCommonSysDictionary(dicType);
                });


                if (!DictionaryDropDownList.HandlerDictionarys.ContainsKey(item))
                {
                    DictionaryDropDownList.HandlerDictionarys.Add(item, handler);
                    DictionaryRadioButtonList.HandlerDictionarys.Add(item, handler);
                }

                ETMS.Controls.LoadDictionaryItemDataHandler itemHandler = new LoadDictionaryItemDataHandler(delegate (string dicType, string dicIDValue)
                {
                    return ETMS.Components.Basic.Implement.BLL.Dictionary.SysDict.GetDictionaryItemInfoByID(dicType, dicIDValue);
                });

                if (!DictionaryLabel.HandlerDictionarys.ContainsKey(item))
                {
                    DictionaryLabel.HandlerDictionarys.Add(item, itemHandler);
                }
            }

            //加载-业务字典数据(通过回调的方式获取字典数据源，优点：1、页面访问到字典下拉时加载才加载数据 2、字典数据缓存由回调方法维护）
            foreach (string item in Enum.GetNames(typeof(BizDicionaryType)))
            {
                ETMS.Controls.LoadDictionaryDataHandler handler = new LoadDictionaryDataHandler(delegate (string dicType)
                {
                    return Dictionarys.GetDictionaryDataTable((BizDicionaryType)Enum.Parse(typeof(BizDicionaryType), dicType, true));
                });


                if (!DictionaryDropDownList.HandlerDictionarys.ContainsKey(item))
                {
                    DictionaryDropDownList.HandlerDictionarys.Add(item, handler);
                    DictionaryRadioButtonList.HandlerDictionarys.Add(item, handler);
                }


                ETMS.Controls.LoadDictionaryItemDataHandler itemHandler = new LoadDictionaryItemDataHandler(delegate (string dicType, string dicIDValue)
                {
                    return Dictionarys.GetDictionaryItemInfoByID((BizDicionaryType)Enum.Parse(typeof(BizDicionaryType), dicType, true), dicIDValue);
                });

                if (!DictionaryLabel.HandlerDictionarys.ContainsKey(item))
                {
                    DictionaryLabel.HandlerDictionarys.Add(item, itemHandler);
                }

                //字典项鼠标提示信息ToolTip
                ETMS.Controls.LoadDictionaryToolTipHandler itemHandlerTip = new LoadDictionaryToolTipHandler(delegate (string dicType, string dicIDValue)
                {
                    return Dictionarys.GetDictionaryItemToolTipInfoByID((BizDicionaryType)Enum.Parse(typeof(BizDicionaryType), dicType, true), dicIDValue);
                });

                if (!DictionaryLabel.HandlerDictionarysTip.ContainsKey(item))
                {
                    DictionaryLabel.HandlerDictionarysTip.Add(item, itemHandlerTip);
                }
            }

            WriteVistorNum();
        }

        private void WriteVistorNum()
        {
            Application["total"] = new PassportVistorLogic().GetNum(1);
            Application["increment"] = 0;
            Application["online"] = 0;
        }

        void Application_End(object sender, EventArgs e)
        {
            new PassportVistorLogic().Save(1, Application["total"].ToInt());
        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码
            Session.Timeout = 1;
            Application.Lock();
            Application["total"] = System.Convert.ToInt32(Application["total"]) + 1;
            Application["online"] = System.Convert.ToInt32(Application["online"]) + 1;
            Application.UnLock();
        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
            // 或 SQLServer，则不会引发该事件。
            Application.Lock();
            Application["online"] = System.Convert.ToInt32(Application["online"]) - 1;
            Application.UnLock();
        }

        void Application_Error(object sender, EventArgs e)
        {
            Server.Transfer("~/ErrorForm.aspx", true);
        }
    }
}