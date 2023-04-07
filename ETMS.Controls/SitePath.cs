/*SitePath.Config��ʽ����
    <?xml version="1.0" encoding="utf-8" ?>
    <sitePath>
        <sitePathNode url="" title="��ҳ"  description="" >
            <sitePathNode url="~/Public/Test.aspx" title="����1"  description="" />
            <sitePathNode url="~/Public/Test.aspx?QueryString1=FixValue[SitePathQueryString]&amp;QueryString2=[/SitePathQueryString]" title="����2"  description="" >
                <sitePathNode url="~/Public/Welcome.aspx?QueryString2=MyValue" title="��ӭ"  description="" />
            </sitePathNode>
        </sitePathNode>
    </sitePath>
    ע�������һ����Ҫȡ��һ����QueryString������Ҫȡֵ��QueryString������[SitePathQueryString]��[/SitePathQueryString]�м䣬ǰ��Ϊ?��&������Ϊ=
    */
using System;
using System.Web;
using System.Web.UI;

using System.Text;
using System.Xml.XPath;
using System.Web.Caching;

using ETMS.Utility;

namespace ETMS.Controls
{
    /// <summary>
    /// ��ʾվ��·������ǰλ�ã�
    /// վ��·���ļ���~/SitePath.Config
    /// </summary>
    /// <remarks>
    /// ZhouYonghua, 2006-7-6
    /// </remarks>    
    public class SitePath : System.Web.UI.WebControls.WebControl
    {
        private const string CacheKey = "SitePath::XPathDocument";
        private const string SitePathFile = "~/Config/SitePath.Config";
        private string _Separator = " > ";
        private int _HeaderSpace = 0;
        /// <summary>
        /// ·����ָ���
        /// </summary>
        public string Separator
        {
            get
            {
                return _Separator;
            }
            set
            {
                _Separator = value;
            }
        }

        /// <summary>
        /// ǰ�ÿո���
        /// </summary>
        public int HeaderSpace
        {
            get
            {
                return _HeaderSpace;
            }
            set
            {
                _HeaderSpace = value;
            }
        }
        public string PreTitle
        {
            get;
            set;
        }
        private EventHandler m_RenderEvent;
        /// <summary>
        /// �ؼ������¼���Ĭ��null)
        /// ���ã��ṩ��Url�滻�Ļ���
        /// </summary>
        public EventHandler RenderEvent
        {
            get
            {
                return this.m_RenderEvent;
            }
            set
            {
                this.m_RenderEvent = value;
            }
        }
        private string m_SitePathXml;
        /// <summary>
        /// �ⲿ�ṩ��xml����Դ
        /// ��Ҫ���Portalÿ��վ���ṩ������SitePath.Config
        ///     ����·����~/Portal/SitePaths/SitePath_3.xml
        /// </summary>
        public string SitePathXml
        {
            get
            {
                return m_SitePathXml;
            }
            set
            {
                m_SitePathXml = value;
            }
        }
        public XPathDocument LoadXmlDocument(string sitePathXml)
        {
            string key = string.Format(CacheKey + "{0}", sitePathXml);
            XPathDocument document = (XPathDocument)CacheHelper.Get(key);
            if (document == null)
            {
                string sitePathFile = HttpContext.Current.Server.MapPath(sitePathXml);
                try
                {
                    document = new XPathDocument(sitePathFile);
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    throw new Exception(string.Format("ȱ��{0}�ļ���", sitePathFile), ex);
                }

                CacheDependency dependency = new CacheDependency(sitePathFile);
                CacheHelper.AddPermanent(key, document, dependency);
            }
            return document;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            //���ģʽ������ȡ��Ϣ
            if (DesignMode)
            {
                writer.Write("Root >> CurrentSitePath");
                return;
            }

            //CSS
            string sLinkCss = "";
            if (!String.IsNullOrEmpty(CssClass))
            {
                sLinkCss = " class=\"" + CssClass + "\"";
            }

            //վ��·���ļ�
            XPathDocument document;
            if (this.SitePathXml == null)//����ⲿû���ṩ������SitePath.Config���ã����ȡĬ�����á�
            {
                document = this.LoadXmlDocument(SitePathFile);                 
            }
            else//��ȡ�ⲿSitePath.Config����
            {
                document = this.LoadXmlDocument(this.SitePathXml);
            }

            try
            {
                XPathNavigator navigator = document.CreateNavigator();

                //��ǰ����·����Сд��
                string request = HttpContext.Current.Request.Url.PathAndQuery.ToLower();
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    request = request.Remove(0, HttpContext.Current.Request.ApplicationPath.Length);
                }
                request = "~" + request;//��"~"

                //�ҵ���ǰ����·����url��Ϊ�գ���ǰ·������url��url��[SitePathQueryString]ǰ�沿�֣�
                //starts-with��������ʹ��contains����//string patten = "//sitePathNode[starts-with('" + request + "', translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))]";
                //string patten = "//sitePathNode[@url != '' and contains('" + request + "', translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))]";//��֧����һ��ȡ��һ����QueryString            
                string patten = string.Format("//sitePathNode[@url != '' and (contains('" + request + "', translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')) or contains('{0}', substring-before(translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'), '[sitepathquerystring]')) and substring-before(@url, '[SitePathQueryString]') !='')]", request);
                XPathNodeIterator nodes = navigator.Select(patten);

                if (nodes.MoveNext())
                {
                    XPathNavigator nav = nodes.Current;
                    StringBuilder sbPath = new StringBuilder(nav.GetAttribute("title", ""));//��ǰ·��

                    while (nav.MoveToParent())//�ϼ�·��
                    {
                        if (nav.Name == "sitePath")//����������
                        {
                            break;
                        }

                        string url = nav.GetAttribute("url", "").Trim();

                        if (string.IsNullOrEmpty(url))//û��url����ʾtitle
                        {
                            sbPath.Insert(0, nav.GetAttribute("title", "") + _Separator);
                        }
                        else//��url����ʾtitle������
                        {
                            string root = HttpContext.Current.Request.ApplicationPath;
                            if (root == "/")
                            {
                                root = "";
                            }
                            url = root + url.Substring(1);//ȥ��"~";

                            //��url��[SitePathQueryString]��[/SitePathQueryString]���QueryString��ֵ
                            string[] seperator = new string[] { "[SitePathQueryString]", "[/SitePathQueryString]" };
                            string[] urls = url.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                            string linkUrl = "";
                            for (int i = 0; i < urls.Length; i++)
                            {
                                string s = urls[i];
                                linkUrl += s;
                                if (i > 0)
                                {
                                    string qs = s.Substring(1, s.Length - 2);//ȡ��QueryString����(ȥ��ǰ���?��&�Լ������=)
                                    linkUrl += System.Web.HttpUtility.UrlEncode(HttpContext.Current.Request.QueryString[qs]);//���ӵ�ǰRequest��QueryString��ֵ
                                }
                            }

                            //sbPath.Insert(0, "<a href= \"" + linkUrl + "\" " + sLinkCss + ">" + nav.GetAttribute("title", "") + "</a>" + _Separator);
                            sbPath.Insert(0, nav.GetAttribute("title", "") + _Separator);
                        }
                    }

                    //ǰ�ÿո�
                    for (int spaceCount = 0; spaceCount < HeaderSpace; spaceCount++)
                    {
                        sbPath.Insert(0, "&nbsp;");
                    }

                    #region �Ѻ�·���滻������Portalվ�㣬Ѧ����
                    if (this.RenderEvent != null)
                    {
                        this.RenderEvent(sbPath, EventArgs.Empty);
                    }
                    #endregion
                    //���
                    writer.Write(string.Format("{0}{1}", PreTitle, sbPath.ToString()));
                }
            }
            catch
            {
            }
        }
    }
}
