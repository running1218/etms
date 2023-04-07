using System;

namespace ETMS.Utility
{
    using System.Collections;
    using System.IO;
    using Autumn.Context;
    using Autumn.Context.Support;
    using Common.Logging;
    /// <summary>
    /// 消息转换器（提供消息多语言翻译支持）
    /// </summary>
    public class MessageTranslatorUtility
    {
        private static IMessageSource Instance_MessageSource;

        /// <summary>
        /// 消息源代理（允许通过IOC容器进行赋值）
        /// </summary>
        public static IMessageSource MessageTranslatorProxy
        {
            get
            {
                if (Instance_MessageSource != null)
                {
                    return Instance_MessageSource;
                }
                ILog logger = LogManager.GetLogger(typeof(MessageTranslatorUtility));
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("默认方式：注册多语言翻译器开始");
                }
                //实例化（通过扫描所有API后缀的dll，并注册)
                ResourceSetMessageSource messageSourceInstance = new ResourceSetMessageSource();
                messageSourceInstance.UseCodeAsDefaultMessage = true;//如果没有配置消息，则消息码之间返回
                messageSourceInstance.ResourceManagers = new ArrayList();
                string dllPath = "";
                if (System.Web.HttpContext.Current != null)
                {
                    dllPath = System.Web.HttpContext.Current.Server.MapPath("~/bin");
                }
                else
                {
                    dllPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                foreach (string apiDll in Directory.GetFiles(dllPath, "*API.dll", SearchOption.TopDirectoryOnly))
                {
                    string fileName = Path.GetFileNameWithoutExtension(apiDll);
                    //资源类型定义："ETMS.Components.Basic.API.BizErrorDefine，ETMS.Components.Basic.API"
                    string resourceItem = string.Format("{0}.BizErrorDefine,{0}", fileName);
                    messageSourceInstance.ResourceManagers.Add(resourceItem);
                    if (logger.IsDebugEnabled)
                    {
                        logger.Debug("默认方式：注册多语言翻译器,资源包：" + resourceItem);
                    }
                }
                //初始化过程
                messageSourceInstance.AfterPropertiesSet();

                Instance_MessageSource = messageSourceInstance;
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("默认方式：注册多语言翻译器完成");
                }
                return Instance_MessageSource;
            }
            set
            {
                ILog logger = LogManager.GetLogger(typeof(MessageTranslatorUtility));
                if (logger.IsDebugEnabled)
                {
                    logger.Debug("用户自定义方式：注册多语言翻译器," + value.ToString());
                }
                Instance_MessageSource = value;
            }
        }

        /// <summary>
        /// 不带消息格式化参数的翻译
        /// </summary>
        /// <param name="name">消息编码</param>
        /// <returns>翻译后消息</returns>
        public static string GetMessage(string name)
        {
            return MessageTranslatorProxy.GetMessage(name);
        }
        /// <summary>
        /// 带有消息格式化参数的翻译
        /// </summary>
        /// <param name="name">消息编码</param>
        /// <param name="arguments">消息格式化参数</param>
        /// <returns>翻译后消息</returns>
        public static string GetMessage(string name, params object[] arguments)
        {
            //if (arguments == null)
            //    return name;
            //else
            return MessageTranslatorProxy.GetMessage(name, arguments);
        }




    }
}
