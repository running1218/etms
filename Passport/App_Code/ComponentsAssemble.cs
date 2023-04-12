using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ETMS.AppContext.Component;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.Implement;
namespace ETMS.WebApp.Passport
{
    /// <summary>
    ///核心组件装配器
    /// </summary>
    public class BasicComponentsAssemble
    {
        public static IList<ETMS.AppContext.Component.IComponent> DoAssemble()
        {
            List<IComponent> components = new List<IComponent>();
            //1、注册统一认证与授权组件
            components.Add(new DefaultPassportFacade()
            {
                /*组件ID*/
                ID = typeof(IPassportFacade).Name,
                /*组件名称*/
                Name = "统一认证与授权",
                /*组件描述*/
                Description = "向Security模块提供对：统一认证与授权的基本操作支持！"
            });

            return components;
        }
    }

    /// <summary>
    /// 扩展组件装配器
    /// </summary>
    public class ExtendComponentsAssemble
    {
        public static IList<ETMS.AppContext.Component.IComponent> DoAssemble()
        {
            List<IComponent> components = new List<IComponent>();

            ////1、在线作业、闯过竞赛
            //components.Add(new DefaultPassportFacade()
            //{
            //    /*组件ID*/
            //    ID = typeof(IPassportFacade).ToString(),
            //    /*组件名称*/
            //    Name = "统一认证与授权",
            //    /*组件描述*/
            //    Description = "向Security模块提供对：统一认证与授权的基本操作支持！"
            //});


            return components;
        }
    }
}