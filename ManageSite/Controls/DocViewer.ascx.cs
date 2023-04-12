using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_DocViewer : System.Web.UI.UserControl
{
    /// <summary>
    /// 类型：string  【必填】 说明：要打开的文档的URL,(文档类型.SWF)
    /// </summary>
    public string DocURL
    {
        get
        {
            if (ViewState["DocURL"] == null)
            {
                throw new Exception("SWF文档阅读部件，传入参数“DocURL”未设置！");
            }
            return (string)ViewState["DocURL"];
        }
        set
        {
            ViewState["DocURL"] = value;
        }
    }


    /// <summary>
    ///  类型：int     【选填】 说明：显示宽度，默认1000
    /// </summary>
    public int Width
    {
        get
        {
            if (ViewState["Width"] == null)
            {
                return 1000;
            }
            return (int)ViewState["Width"];
        }
        set
        {
            ViewState["Width"] = value;
        }
    }

    /// <summary>
    ///  类型：int     【选填】 说明：显示高度，默认1000
    /// </summary>
    public int Height
    {
        get
        {
            if (ViewState["Height"] == null)
            {
                return 768;
            }
            return (int)ViewState["Height"];
        }
        set
        {
            ViewState["Height"] = value;
        }
    }


    /// <summary>
    ///  类型：boolean     【选填】 说明：是否显示水平滚动条，默认否
    /// </summary>
    public bool ShowHorizontalScrollBar
    {
        get
        {
            if (ViewState["ShowHorizontalScrollBar"] == null)
            {
                return false;
            }
            return (bool)ViewState["ShowHorizontalScrollBar"];
        }
        set
        {
            ViewState["ShowHorizontalScrollBar"] = value;
        }
    }

    /// <summary>
    ///   类型: boolean     【选填】 说明：是否显示垂直滚动条，默认否
    /// </summary>
    public bool ShowVerticalScrollBar
    {
        get
        {
            if (ViewState["ShowVerticalScrollBar"] == null)
            {
                return false;
            }
            return (bool)ViewState["ShowVerticalScrollBar"];
        }
        set
        {
            ViewState["ShowVerticalScrollBar"] = value;
        }
    }

    protected string ParamStr
    {
        get
        {
            return string.Format("SwfFile={0}&Scale={1}&PrintEnabled={2}&Language={3}&ShowHorizontalScrollBar={4}&ShowVerticalScrollBar={5}", new object[] { this.DocURL, 1, false, 0, ShowHorizontalScrollBar, ShowVerticalScrollBar });
        }
    }

    protected string DocViewerUrl
    {
        get
        {
            return ETMS.Utility.StaticResourceUtility.GetFlashFullPath("docviewer/player.swf");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}