using System;
using System.Collections.Generic;
using Autumn.Objects.Factory;
using Common.Logging;

namespace ETMS.Utility.Service.FileUpload
{
    /// <summary>
    /// 默认文件上传策略服务实现
    /// </summary>
    public class DefaultFileUploadStrategyService : IFileUploadStrategyService, IInitializingObject
    {
        private static ILog Logger = LogManager.GetLogger(typeof(DefaultFileUploadStrategyService));

        /// <summary>
        /// 配置项
        /// </summary>
        public IDictionary<string, FileUploadConfig> Strategys { get; set; }

        private string m_Root;
        /// <summary>
        /// 上传文件对应的物理文件根
        ///   支持：1、绝对路径方式{ d:\temp | \\202.205.170.166\temp}
        ///        2、以“~”开头的相对路径方式 {~/Temp}
        /// </summary>
        public string Root
        {
            get
            {
                return m_Root;
            }
            set
            {
                if (value.IndexOf(@"~") != -1)
                {
                    value = value.Replace("~", AppDomain.CurrentDomain.BaseDirectory).Replace("/", @"\");
                }
                m_Root = value;
            }
        }

        /// <summary>
        /// 上传文件对应的Url根
        /// </summary>
        public string UrlRoot { get; set; }

        public FileUploadConfig GetStrategy(string functionType)
        {
            if (!Strategys.ContainsKey(functionType))
            {
                Exception ex = new Exception(string.Format("未找到名为“{0}”的上传配置项！", functionType));
                if (Logger.IsErrorEnabled)
                {
                    Logger.Error(ex.Message);
                }
                throw ex;
            }
            return Strategys[functionType];

        }

        public void AfterPropertiesSet()
        {
            if (Strategys == null || Strategys.Count == 0)
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("当前应用未设置任何上传配置项，请确认！");
                }
            }
            else
            {
                foreach (string key in Strategys.Keys)
                {
                    FileUploadConfig item = Strategys[key];

                    //【物理路径】检测当前配置项是否是全路径？（d:\ 或 \\ 或 file:///d:/ 或 ~)
                    if (
                        item.Root.IndexOf(@":\") == -1
                        && !item.Root.StartsWith(@"\\")
                        && item.Root.IndexOf("file:///") == -1
                        && !item.Root.StartsWith("~")
                        )
                    {
                        if (string.IsNullOrEmpty(Root))
                        {
                            string message = "【物理路径】当前上传配置项未设置全路径，并且当前应用未设置默认的根路径！请设置！";
                            if (Logger.IsErrorEnabled)
                            {
                                Logger.Error(message);
                            }
                            throw new Exception(message);
                        }
                        string newRoot = (Root + @"\" + item.Root);//剔除路径中设置错误引起的"\\"或"/"
                        if (Logger.IsDebugEnabled)
                        {
                            Logger.Debug(string.Format("【物理路径】当前上传配置项未设置全路径：{0}，自动转换为全路径：{1}", item.Root, newRoot));
                        }
                        item.Root = newRoot;
                    }
                    else if (item.Root.StartsWith("~"))
                    {
                        item.Root = item.Root
                            .Replace("/", "\\")
                            .Replace("~", System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
                    }

                    //【Url路径】检测当前配置项是否是全路径？（http://)
                    if (item.UrlRoot.IndexOf(@"://") == -1)
                    {
                        if (string.IsNullOrEmpty(UrlRoot))
                        {
                            string message = "【Url路径】当前上传配置项未设置全路径，但当前应用未设置默认的上传根路径！请设置！";
                            if (Logger.IsErrorEnabled)
                            {
                                Logger.Error(message);
                            }
                            throw new Exception(message);
                        }
                        string newUrlRoot = (UrlRoot + "/" + item.UrlRoot);//剔除路径中设置错误引起的"//"
                        if (Logger.IsDebugEnabled)
                        {
                            Logger.Debug(string.Format("【Url路径】当前上传配置项未设置全路径：{0}，自动转换为全路径：{1}", item.UrlRoot, newUrlRoot));
                        }
                        item.UrlRoot = newUrlRoot;
                    }
                }
            }
        }
    }
}
