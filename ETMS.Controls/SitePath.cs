/*SitePath.Config格式如下
    <?xml version="1.0" encoding="utf-8" ?>
    <sitePath>
        <sitePathNode url="" title="首页"  description="" >
            <sitePathNode url="~/Public/Test.aspx" title="测试1"  description="" />
            <sitePathNode url="~/Public/Test.aspx?QueryString1=FixValue[SitePathQueryString]&amp;QueryString2=[/SitePathQueryString]" title="测试2"  description="" >
                <sitePathNode url="~/Public/Welcome.aspx?QueryString2=MyValue" title="欢迎"  description="" />
            </sitePathNode>
        </sitePathNode>
    </sitePath>
    注：如果上一级需要取下一级的QueryString，将需要取值的QueryString包括在[SitePathQueryString]和[/SitePathQueryString]中间，前面为?或&，后面为=
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
    /// 显示站点路径（当前位置）
    /// 站点路径文件：~/SitePath.Config
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
        /// 路径间分隔符
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
        /// 前置空格数
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
        /// 控件绘制事件（默认null)
        /// 作用：提供了Url替换的机制
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
        /// 外部提供的xml数据源
        /// 主要解决Portal每个站点提供独立的SitePath.Config
        ///     配置路径：~/Portal/SitePaths/SitePath_3.xml
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
                    throw new Exception(string.Format("缺少{0}文件！", sitePathFile), ex);
                }

                CacheDependency dependency = new CacheDependency(sitePathFile);
                CacheHelper.AddPermanent(key, document, dependency);
            }
            return document;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            //设计模式：不提取信息
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

            //站点路径文件
            XPathDocument document;
            if (this.SitePathXml == null)//如果外部没有提供对立的SitePath.Config配置，则读取默认配置。
            {
                document = this.LoadXmlDocument(SitePathFile);                 
            }
            else//读取外部SitePath.Config配置
            {
                document = this.LoadXmlDocument(this.SitePathXml);
            }

            try
            {
                XPathNavigator navigator = document.CreateNavigator();

                //当前请求路径（小写）
                string request = HttpContext.Current.Request.Url.PathAndQuery.ToLower();
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    request = request.Remove(0, HttpContext.Current.Request.ApplicationPath.Length);
                }
                request = "~" + request;//加"~"

                //找到当前请求路径（url不为空，当前路径包括url或url中[SitePathQueryString]前面部分）
                //starts-with函数错误，使用contains函数//string patten = "//sitePathNode[starts-with('" + request + "', translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))]";
                //string patten = "//sitePathNode[@url != '' and contains('" + request + "', translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))]";//不支持上一级取下一级的QueryString            
                string patten = string.Format("//sitePathNode[@url != '' and (contains('" + request + "', translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')) or contains('{0}', substring-before(translate(@url,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'), '[sitepathquerystring]')) and substring-before(@url, '[SitePathQueryString]') !='')]", request);
                XPathNodeIterator nodes = navigator.Select(patten);

                if (nodes.MoveNext())
                {
                    XPathNavigator nav = nodes.Current;
                    StringBuilder sbPath = new StringBuilder(nav.GetAttribute("title", ""));//当前路径

                    while (nav.MoveToParent())//上级路径
                    {
                        if (nav.Name == "sitePath")//顶级，跳出
                        {
                            break;
                        }

                        string url = nav.GetAttribute("url", "").Trim();

                        if (string.IsNullOrEmpty(url))//没有url，显示title
                        {
                            sbPath.Insert(0, nav.GetAttribute("title", "") + _Separator);
                        }
                        else//有url，显示title的链接
                        {
                            string root = HttpContext.Current.Request.ApplicationPath;
                            if (root == "/")
                            {
                                root = "";
                            }
                            url = root + url.Substring(1);//去掉"~";

                            //给url中[SitePathQueryString]和[/SitePathQueryString]间的QueryString赋值
                            string[] seperator = new string[] { "[SitePathQueryString]", "[/SitePathQueryString]" };
                            string[] urls = url.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                            string linkUrl = "";
                            for (int i = 0; i < urls.Length; i++)
                            {
                                string s = urls[i];
                                linkUrl += s;
                                if (i > 0)
                                {
                                    string qs = s.Substring(1, s.Length - 2);//取出QueryString名称(去掉前面的?或&以及后面的=)
                                    linkUrl += System.Web.HttpUtility.UrlEncode(HttpContext.Current.Request.QueryString[qs]);//增加当前Request中QueryString的值
                                }
                            }

                            //sbPath.Insert(0, "<a href= \"" + linkUrl + "\" " + sLinkCss + ">" + nav.GetAttribute("title", "") + "</a>" + _Separator);
                            sbPath.Insert(0, nav.GetAttribute("title", "") + _Separator);
                        }
                    }

                    //前置空格
                    for (int spaceCount = 0; spaceCount < HeaderSpace; spaceCount++)
                    {
                        sbPath.Insert(0, "&nbsp;");
                    }

                    #region 友好路径替换，用于Portal站点，薛永波
                    if (this.RenderEvent != null)
                    {
                        this.RenderEvent(sbPath, EventArgs.Empty);
                    }
                    #endregion
                    //输出
                    writer.Write(string.Format("{0}{1}", PreTitle, sbPath.ToString()));
                }
            }
            catch
            {
            }
        }
    }
}
