using System.Collections.Generic;
using Common.Logging;
using University.Mooc.AppContext.Component;
namespace University.Mooc.AppContext
{
    /// <summary>
    /// 应用上下文加载器
    /// </summary>
    public abstract class ApplicationContextLoader
    {
        public static void Load()
        {
            //设置应用上下文实例
            ApplicationContext appContext = new ApplicationContext();
            ApplicationContext.Current = appContext;

            //配置应用上下文实例
            appContext.Logger = LogManager.GetLogger("ApplicationContext");
            appContext.ComponentRepository = new DefaultComponentRepository(appContext);
        }

        public static void Load(LoadComponentsHandler[] handlers)
        {
            //设置应用上下文实例
            ApplicationContext appContext = new ApplicationContext();
            ApplicationContext.Current = appContext;

            //配置应用上下文实例
            appContext.Logger = LogManager.GetLogger("ApplicationContext");
            appContext.ComponentRepository = new DefaultComponentRepository(appContext);

            //如果有回调接口，则获取组件
            if (handlers != null)
            {
                foreach (LoadComponentsHandler handler in handlers)
                {
                    if (handler == null)
                        continue;

                    IList<IComponent> components = handler();
                    if (components != null && components.Count > 0)
                    {
                        foreach (IComponent component in components)
                        {
                            appContext.ComponentRepository.RegisterComponent(component);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 载入组件回调接口
        /// </summary>
        /// <returns></returns>
        public delegate IList<IComponent> LoadComponentsHandler();
    }
   
}
