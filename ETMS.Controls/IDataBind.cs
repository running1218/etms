using System;
using System.Collections.Generic;
using System.Text;

namespace ETMS.Controls
{
    /// <summary>
    /// ���ݰ󶨽ӿ�
    /// </summary>
    public interface IDataBind
    {
        /// <summary>
        /// ��ȡ���ɵ�html����
        /// </summary>
        /// <returns></returns>
        string GetHtml();
    }
    /// <summary>
    /// ���ݰί��
    /// </summary>
    /// <param name="item">������</param>
    /// <param name="rowIndex">�к�(��1��ʼ)</param>
    /// <returns></returns>
    public delegate string BindItem<T>(T item, Int32 itemIndex);

    public delegate string BindItem(object item, Int32 itemIndex);

    /// <summary>
    /// ���ݸ�ʽ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFormaterControl
    {
        /// <summary>
        ///  ��ʽ�����Ӵ�
        /// </summary>
        string UrlFormat { get;set;}

        /// <summary>
        ///  ���Ӵ򿪷�ʽ
        /// </summary>
        string UrlTarget { get;set;}

        /// <summary>
        /// �������
        /// </summary>
        System.Collections.Hashtable Parms { get;set;}

        BindItem ItemFormater { get;set;}
        /// <summary>
        /// ���ɸ�ʽ������
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        StringBuilder GetHtml(System.Collections.IEnumerable dataSource);
    }

    public abstract class AbstractFormaterControl : IFormaterControl
    {

        #region IFormaterControl ��Ա

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
    /// Box��������ӿ�
    /// </summary>
    public interface IBoxContainerControl
    {
        string BoxClassExtend { get;set;}
        string Title { get;set;}
        string MoreLinkText { get;set;}
        string MoreLinkUrl { get;set;}
        /// <summary>
        /// ��������λ�ã�Ĭ��:0 ������  1:����֮��
        /// </summary>
        int MorePosition { get;set;}
        string ContentClass { get;set;}
        IFormaterControl Formater { get;set;}
        void DataBind(System.Collections.IEnumerable dataSource);
    }

    /// <summary>
    /// Tag��������
    /// </summary>
    public interface ITabsContainerControl
    {
        IBoxContainerControl[] BoxContainers { get;set;}
        void DataBind(IDictionary<string, System.Collections.IEnumerable> dataSource);
    }
}
