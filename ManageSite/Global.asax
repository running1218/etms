<%@ Import Namespace="ETMS.Controls" %>
<%@ Import Namespace="ETMS.Components.Basic.Implement.BLL" %>
<%@ Import Namespace="ETMS.AppContext" %>
<%@ Import Namespace="ETMS.WebApp.Manage" %>
<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        #region 加载应用上下文
        ApplicationContextLoader.Load(new LoadComponentsHandler[] {
            /*核心组件装配器*/
            //BasicComponentsAssemble.DoAssemble,
            ETMS.Product.BasicComponentsAssemble.DoAssemble,
            /*扩展组件装配器*/            
            //ExtendComponentsAssemble.DoAssemble,
            ETMS.Product.ExtendComponentsAssemble.DoAssemble,
        });
        #endregion

        #region 字典控件装配

        //加载-系统字典数据(通过回调的方式获取字典数据源,优点：1、页面访问到字典下拉时加载才加载数据 2、字典数据缓存由回调方法维护）
        foreach (string item in Enum.GetNames(typeof(ETMS.Components.Basic.API.Entity.Dictionary.SysDicionaryTypeEnum)))
        {
            ETMS.Controls.LoadDictionaryDataHandler handler = new LoadDictionaryDataHandler(delegate(string dicType)
            {
                return ETMS.Components.Basic.Implement.BLL.Dictionary.SysDict.GetCommonSysDictionary(dicType);
            });


            if (!DictionaryDropDownList.HandlerDictionarys.ContainsKey(item))
            {
                DictionaryDropDownList.HandlerDictionarys.Add(item, handler);
                DictionaryRadioButtonList.HandlerDictionarys.Add(item, handler);
            }

            ETMS.Controls.LoadDictionaryItemDataHandler itemHandler = new LoadDictionaryItemDataHandler(delegate(string dicType, string dicIDValue)
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
            ETMS.Controls.LoadDictionaryDataHandler handler = new LoadDictionaryDataHandler(delegate(string dicType)
            {
                return Dictionarys.GetDictionaryDataTable((BizDicionaryType)Enum.Parse(typeof(BizDicionaryType), dicType, true));
            });


            if (!DictionaryDropDownList.HandlerDictionarys.ContainsKey(item))
            {
                DictionaryDropDownList.HandlerDictionarys.Add(item, handler);
                DictionaryRadioButtonList.HandlerDictionarys.Add(item, handler);
            }


            ETMS.Controls.LoadDictionaryItemDataHandler itemHandler = new LoadDictionaryItemDataHandler(delegate(string dicType, string dicIDValue)
            {
                return Dictionarys.GetDictionaryItemInfoByID((BizDicionaryType)Enum.Parse(typeof(BizDicionaryType), dicType, true), dicIDValue);
            });

            if (!DictionaryLabel.HandlerDictionarys.ContainsKey(item))
            {
                DictionaryLabel.HandlerDictionarys.Add(item, itemHandler);
            }


            //字典项鼠标提示信息ToolTip
            ETMS.Controls.LoadDictionaryToolTipHandler itemHandlerTip = new LoadDictionaryToolTipHandler(delegate(string dicType, string dicIDValue)
            {
                return Dictionarys.GetDictionaryItemToolTipInfoByID((BizDicionaryType)Enum.Parse(typeof(BizDicionaryType), dicType, true), dicIDValue);
            });

            if (!DictionaryLabel.HandlerDictionarysTip.ContainsKey(item))
            {
                DictionaryLabel.HandlerDictionarysTip.Add(item, itemHandlerTip);
            }
            
            

        }
        #endregion

        //服务宿主启动
        if ("true".Equals(ETMS.AppContext.ApplicationContext.Current.AppSettings["JobService.Enable"], StringComparison.InvariantCultureIgnoreCase))
        {
            HttpRequest request = HttpContext.Current.Request;
            ETMS.Components.Basic.Implement.BLL.JobService.ServiceHost.DefaultUrl = string.Format("http://{0}{1}/ErrorForm.aspx", request.Url.Authority, ETMS.Utility.WebUtility.AppPath);
            ETMS.Components.Basic.Implement.BLL.JobService.ServiceHost.DefaultHost.Start();
        }
    }


    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

        //服务宿主停止
        {
            ETMS.Components.Basic.Implement.BLL.JobService.ServiceHost.DefaultHost.Stop();
        }
    }

    void Application_Error(object sender, EventArgs e)
    {
        Server.Transfer("~/ErrorForm.aspx", true);
    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码


    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为

        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。


    }
       
</script>
