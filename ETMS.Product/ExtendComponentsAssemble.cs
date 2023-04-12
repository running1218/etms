using System;
using System.Collections.Generic;

using Common.Logging;
using ETMS.AppContext.Component;
namespace ETMS.Product
{
    /// <summary>
    /// 扩展组件装配器
    /// </summary>
    public class ExtendComponentsAssemble
    {
        private static ILog Logger = LogManager.GetLogger("ApplicationSystemError");
        /// <summary>
        /// 装配
        /// </summary>
        /// <returns></returns>
        public static IList<ETMS.AppContext.Component.IComponent> DoAssemble()
        {
            List<IComponent> components = new List<IComponent>();
            //根据策略装配组件
            foreach (ExtendComponentType componentType in Enum.GetValues(typeof(ExtendComponentType)))
            {
                if (!ProductComponentStrategy.IsSupport(componentType))//如果策略不支持组件，则跳过
                {
                    continue;
                }

                //IOC方式装载
                LoadComponentFromIocContainer(componentType, components);
            }
            return components;
        }

        /// <summary>
        /// 组件类型加载
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="components"></param>
        private static void LoadComponentFromIocContainer(ExtendComponentType componentType, IList<IComponent> components)
        {
            //如果组件类型按业务细化分多种，但组件配置文件仅一份！如Poll_R1,Poll_R2,但配置文件为Poll.xml
            string componentXmlName = componentType.ToString();
            string[] strs = componentXmlName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length > 1)
            {
                componentXmlName = strs[0];
            }
            string objectXml = string.Format("assembly://ETMS.Product/ETMS.Product.ExtendComponents/{0}.xml", componentXmlName);
            if (new Autumn.Core.IO.AssemblyResource(objectXml).Exists)//如果组件配置文件存在，则装配
            {
                try
                {
                    //加载上下文
                    Autumn.Context.IApplicationContext appContext = new Autumn.Context.Support.XmlApplicationContext(objectXml);
                    //将应用上下文中的注册对象（实现接口IComponent）添加到组件库
                    foreach (string objectID in appContext.GetObjectDefinitionNames())
                    {
                        object obj = appContext.GetObject(objectID);
                        if (obj is IComponent)
                        {
                            components.Add(obj as IComponent);
                        } 
                    }
                }
                catch (Exception ex)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(string.Format("加载应用上下文配置{0}出错，原因：{1}", objectXml, ex.Message), ex);
                    }
                }

            }
        }
    }
}
