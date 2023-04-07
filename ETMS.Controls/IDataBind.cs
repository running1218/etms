using System;
using System.Collections.Generic;
using System.Text;

namespace ETMS.Controls
{
    /// <summary>
    /// 数据绑定接口
    /// </summary>
    public interface IDataBind
    {
        /// <summary>
        /// 获取生成的html代码
        /// </summary>
        /// <returns></returns>
        string GetHtml();
    }
    /// <summary>
    /// 数据邦定委托
    /// </summary>
    /// <param name="item">数据行</param>
    /// <param name="rowIndex">行号(从1开始)</param>
    /// <returns></returns>
    public delegate string BindItem<T>(T item, Int32 itemIndex);

    public delegate string BindItem(object item, Int32 itemIndex);

    /// <summary>
    /// 内容格式化工具
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFormaterControl
    {
        /// <summary>
        ///  格式化链接串
        /// </summary>
        string UrlFormat { get;set;}

        /// <summary>
        ///  链接打开方式
        /// </summary>
        string UrlTarget { get;set;}

        /// <summary>
        /// 更多参数
        /// </summary>
        System.Collections.Hashtable Parms { get;set;}

        BindItem ItemFormater { get;set;}
        /// <summary>
        /// 生成格式化数据
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        StringBuilder GetHtml(System.Collections.IEnumerable dataSource);
    }

    public abstract class AbstractFormaterControl : IFormaterControl
    {

        #region IFormaterControl 成员

        public string UrlFormat
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public string UrlTarget
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public System.Collections.Hashtable Parms
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public BindItem ItemFormater
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public abstract StringBuilder GetHtml(System.Collections.IEnumerable dataSource);

        #endregion
    }
    /// <summary>
    /// Box容器定义接口
    /// </summary>
    public interface IBoxContainerControl
    {
        string BoxClassExtend { get;set;}
        string Title { get;set;}
        string MoreLinkText { get;set;}
        string MoreLinkUrl { get;set;}
        /// <summary>
        /// 更多链接位置：默认:0 标题栏  1:内容之后
        /// </summary>
        int MorePosition { get;set;}
        string ContentClass { get;set;}
        IFormaterControl Formater { get;set;}
        void DataBind(System.Collections.IEnumerable dataSource);
    }

    /// <summary>
    /// Tag容器定义
    /// </summary>
    public interface ITabsContainerControl
    {
        IBoxContainerControl[] BoxContainers { get;set;}
        void DataBind(IDictionary<string, System.Collections.IEnumerable> dataSource);
    }
}
