using System;
using System.Collections.Generic;

using ETMS.AppContext.Component;
namespace ETMS.Product
{
    /// <summary>
    /// 基本组件装配器
    /// </summary>
    public class BasicComponentsAssemble
    {
        /// <summary>
        /// 装配
        /// </summary>
        /// <returns></returns>
        public static IList<ETMS.AppContext.Component.IComponent> DoAssemble()
        {
            List<IComponent> components = new List<IComponent>();
            //根据策略装配组件
            foreach (BasicComponentType componentType in Enum.GetValues(typeof(BasicComponentType)))
            { 
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
        private static void LoadComponentFromIocContainer(BasicComponentType componentType, IList<IComponent> components)
        {
            string objectXml = string.Format("assembly://ETMS.Product/ETMS.Product.BasicComponents/{0}.xml", componentType.ToString());
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
    }
}
