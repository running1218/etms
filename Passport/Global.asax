﻿<%@ Import Namespace="ETMS.Controls" %>
<%@ Import Namespace="ETMS.Components.Basic.Implement.BLL" %>
<%@ Import Namespace="ETMS.AppContext" %>
<%@ Import Namespace="ETMS.WebApp.Passport" %>
<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //加载应用上下文
        ApplicationContextLoader.Load(new LoadComponentsHandler[] {
            /*核心组件装配器*/
            BasicComponentsAssemble.DoAssemble
        }); 
    }


    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
         
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
