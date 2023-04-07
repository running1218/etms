using System;
using System.Web.UI.WebControls;
using System.ComponentModel;
using ETMS.Editor.Common;
using System.Web;

namespace ETMS.Editor.UEditor
{
    public partial class UEditor : Panel
    {
        #region "常规设置项目"

        /// <summary>
        /// 编辑器中的HTML文本
        /// </summary>
        [Description("编辑器中的HTML文本"),
        Category("常规设置项目"),
        DefaultValue(""),
        Browsable(true)]
        public String Text
        {
            get
            {
                Object o = HttpContext.Current.Items["inC" + this.ID];
                if (o != null)
                {
                    return o.ToString();
                }
                else if (!string.IsNullOrEmpty(Context.Request.Form[this.TextArea]))
                {
                    return Context.Request.Form[this.TextArea];
                }
                else
                {
                    return "";
                }

            }
            set
            {
                HttpContext.Current.Items["inC" + this.ID] = value;

                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["initialContent"] = value;
                }
            }
        }

        /// <summary>
        /// 编辑器回车标签。p或br
        /// </summary>
        [Description("编辑器回车标签。p或br")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public EnterTag EnterTag
        {
            get
            {
                Object o = ViewState["CompressSide"];
                return (o != null ? (EnterTag)o : EnterTag.p);
            }
            set
            {
                ViewState["CompressSide"] = value;
                if (!String.IsNullOrEmpty(Convert.ToString(value)))
                {
                    d["enterTag"] = Convert.ToString(value);
                }
            }
        }
        /// <summary>
        /// 源码的查看方式，codemirror 是代码高亮，textarea是文本框
        /// </summary>
        [Description("源码的查看方式，codemirror 是代码高亮，textarea是文本框")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public SourceEditor SourceEditor
        {
            get
            {
                Object o = ViewState["sourceEditor"];
                return (o != null ? (SourceEditor)o : SourceEditor.codemirror);
            }
            set
            {
                ViewState["sourceEditor"] = value;
                if (!String.IsNullOrEmpty(Convert.ToString(value)))
                {
                    d["sourceEditor"] = Convert.ToString(value);
                }
            }
        }
        /// <summary>
        /// 编辑器的内容控件的name
        /// </summary>
        [Description("编辑器的内容控件的name")]
        [Category("常规设置项目")]
        private String TextArea
        {
            get
            {
                Object o = ViewState["TextArea"];
                return (o != null ? (String)o : "Text" + this.ID);
            }
            set
            {
                ViewState["TextArea"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["textarea"] = value;
                }
            }
        }
        /// <summary>
        /// 初始化时，首行缩进距离
        /// </summary>
        [Description("初始化时，首行缩进距离")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public String IndentValue
        {
            get
            {
                Object o = ViewState["indentValue"];
                return (o != null ? (String)o : "2em");
            }
            set
            {
                ViewState["indentValue"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["indentValue"] = value;
                }
            }
        }
        /// <summary>
        /// 分页符
        /// </summary>
        [Description("分页符")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public String PageBreakTag
        {
            get
            {
                Object o = ViewState["pageBreakTag"];
                return (o != null ? (String)o : "_ueditor_page_break_tag_");
            }
            set
            {
                ViewState["pageBreakTag"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["pageBreakTag"] = value;
                }
            }
        }

        /// <summary>
        /// 百度应用的APIkey
        /// </summary>
        [Description("百度应用的APIkey")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public String WebAppKey
        {
            get
            {
                Object o = ViewState["webAppKey"];
                return (o != null ? (String)o : "");
            }
            set
            {
                ViewState["webAppKey"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["webAppKey"] = value;
                }
            }
        }
        /// <summary>
        /// 为editor添加一个全局路径
        /// </summary>
        [Description("为editor添加一个全局路径")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public String UEDITOR_HOME_URL
        {
            get
            {
                Object o = ViewState["UEDITOR_HOME_URL"];
                return (o != null ? (String)o : "URL");
            }
            set
            {
                ViewState["UEDITOR_HOME_URL"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["UEDITOR_HOME_URL"] = value;
                }
            }
        }

        /// <summary>
        /// tab时的单一字符
        /// </summary>
        [Description("tab时的单一字符")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public String TabNode
        {
            get
            {
                Object o = ViewState["tabNode"];
                return (o != null ? (String)o : "&nbsp;");
            }
            set
            {
                ViewState["tabNode"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["tabNode"] = value;
                }
            }
        }

        /// <summary>
        /// 清除格式删除的标签
        /// </summary>
        [Description("清除格式删除的标签")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public String RemoveFormatTags
        {
            get
            {
                Object o = ViewState["removeFormatTags"];
                return (o != null ? (String)o : "b,big,code,del,dfn,em,font,i,ins,kbd,q,samp,small,span,strike,strong,sub,sup,tt,u,var");
            }
            set
            {
                ViewState["removeFormatTags"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["removeFormatTags"] = value;
                }
            }
        }
        /// <summary>
        /// 清除格式删除的属性
        /// </summary>
        [Description("清除格式删除的属性")]
        [Category("常规设置项目")]
        [Browsable(true)]
        public String RemoveFormatAttributes
        {
            get
            {
                Object o = ViewState["removeFormatAttributes"];
                return (o != null ? (String)o : "class,style,lang,width,height,align,hspace,valign");
            }
            set
            {
                ViewState["removeFormatAttributes"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["removeFormatAttributes"] = value;
                }
            }
        }


        /// <summary>
        /// 是否自动清除编辑器初始内容，注意：如果focus属性设置为true,这个也为真，那么编辑器一上来就会触发导致初始化的内容看不到了
        /// </summary>
        [
        Description("是否自动清除编辑器初始内容，注意：如果focus属性设置为true,这个也为真，那么编辑器一上来就会触发导致初始化的内容看不到了"),
        Category("常规设置项目"),
        DefaultValue(true),
        Browsable(true)
        ]
        public Boolean AutoClearinitialContent
        {
            get
            {
                Object o = ViewState["autoClearinitialContent"];
                return (o != null ? (Boolean)o : true);
            }
            set
            {
                ViewState["autoClearinitialContent"] = value;
                Object o = ViewState["autoClearinitialContent"];
                if (o != null)
                {
                    d["autoClearinitialContent"] = value;
                }
            }
        }

        /// <summary>
        /// 资源目录相对路径
        /// </summary>
        [
        Description("资源目录相对路径"),
        Category("常规设置项目"),
        DefaultValue("/Tools/ueditor/"),
        Browsable(true)
        ]
        public String ScriptPath
        {
            get
            {
                Object o = ViewState["ScriptPath"];
                String path = Convert.ToString(o);
                if (!String.IsNullOrEmpty(path) && path.IndexOf('~') == 0)
                {
                    path = path.Remove(0, 1);
                }
                return (String.IsNullOrEmpty(path) ? "/Tools/ueditor/" : path);
            }
            set
            {
                ViewState["ScriptPath"] = value;
            }
        }

        /// <summary>
        /// 默认显示编辑器
        /// </summary>
        [
        Description("默认显示编辑器"),
        Category("常规设置项目"),
        DefaultValue(true),
        Browsable(true)
        ]
        public Boolean IsShow
        {
            get
            {
                Object o = ViewState["isShow"];
                return (o != null ? (Boolean)o : true);
            }
            set
            {
                ViewState["isShow"] = value;
                Object o = ViewState["isShow"];
                if (o != null)
                {
                    d["isShow"] = value;
                }
            }
        }

        /// <summary>
        /// 关闭字数统计
        /// </summary>
        [
        Description("关闭字数统计"),
        Category("常规设置项目"),
        DefaultValue(false),
        Browsable(true)
        ]
        public Boolean WordCount
        {
            get
            {
                Object o = ViewState["wordCount"];
                return (o != null ? (Boolean)o : false);
            }
            set
            {
                ViewState["wordCount"] = value;
                Object o = ViewState["wordCount"];
                if (o != null)
                {
                    d["wordCount"] = value;
                }
            }
        }




        /// <summary>
        /// 初始化时，是否让编辑器获得焦点true或false
        /// </summary>
        [
        Description("初始化时，是否让编辑器获得焦点true或false"),
        Category("常规设置项目"),
        DefaultValue(false),
        Browsable(true)
        ]
        public new Boolean Focus
        {
            get
            {
                Object o = ViewState["focus"];
                return (o != null ? (Boolean)o : false);
            }
            set
            {
                ViewState["focus"] = value;
                Object o = ViewState["focus"];
                if (o != null)
                {
                    d["focus"] = value;
                }
            }
        }


        /// <summary>
        /// 是否开启表情本地化，默认关闭。若要开启请确保emotion文件夹下包含官网提供的images表情文件夹
        /// </summary>
        [
        Description("是否开启表情本地化，默认关闭。若要开启请确保emotion文件夹下包含官网提供的images表情文件夹"),
        Category("常规设置项目"),
        DefaultValue(false),
        Browsable(true)
        ]
        public Boolean EmotionLocalization
        {
            get
            {
                Object o = ViewState["emotionLocalization"];
                return (o != null ? (Boolean)o : false);
            }
            set
            {
                ViewState["emotionLocalization"] = value;
                Object o = ViewState["emotionLocalization"];
                if (o != null)
                {
                    d["emotionLocalization"] = value;
                }
            }
        }

        /// <summary>
        /// 是否纯文本粘贴。false为不使用纯文本粘贴，true为使用纯文本粘贴
        /// </summary>
        [
        Description("是否纯文本粘贴。false为不使用纯文本粘贴，true为使用纯文本粘贴"),
        Category("常规设置项目"),
        DefaultValue(false),
        Browsable(true)
        ]
        public Boolean Pasteplain
        {
            get
            {
                Object o = ViewState["pasteplain"];
                return (o != null ? (Boolean)o : false);
            }
            set
            {
                ViewState["pasteplain"] = value;
                Object o = ViewState["pasteplain"];
                if (o != null)
                {
                    d["pasteplain"] = value;
                }
            }
        }


        /// <summary>
        /// 是否删除空的inlineElement节点
        /// </summary>
        [
        Description("是否删除空的inlineElement节点"),
        Category("常规设置项目"),
        DefaultValue(true),
        Browsable(true)
        ]
        public Boolean AutoClearEmptyNode
        {
            get
            {
                Object o = ViewState["autoClearEmptyNode"];
                return (o != null ? (Boolean)o : true);
            }
            set
            {
                ViewState["autoClearEmptyNode"] = value;
                Object o = ViewState["autoClearEmptyNode"];
                if (o != null)
                {
                    d["autoClearEmptyNode"] = value;
                }
            }
        }
        /// <summary>
        /// 允许的最大字符数
        /// </summary>
        [
        Description("允许的最大字符数"),
        Category("常规设置项目"),
        DefaultValue(10000),
        Browsable(true)
        ]
        public Int32 MaximumWords
        {
            get
            {
                Object o = ViewState["MaximumWords"];
                return (o != null ? (Int32)o : 10000);
            }
            set
            {
                ViewState["MaximumWords"] = value;
                Object o = ViewState["MaximumWords"];
                if (o != null)
                {
                    d["maximumWords"] = value;
                    WordCount = true;
                }
            }
        }
        /// <summary>
        /// 最多可以回退的次数
        /// </summary>
        [
        Description("最多可以回退的次数"),
        Category("常规设置项目"),
        DefaultValue(20),
        Browsable(true)
        ]
        public UInt32 MaxUndoCount
        {
            get
            {
                Object o = ViewState["MaxUndoCount"];
                return (o != null ? (UInt32)o : 20);
            }
            set
            {
                ViewState["MaxUndoCount"] = value;
                Object o = ViewState["MaxUndoCount"];
                if (o != null)
                {
                    d["MaxUndoCount"] = value;
                }
            }
        }
        /// <summary>
        /// tab的宽度
        /// </summary>
        [
        Description("tab的宽度"),
        Category("常规设置项目"),
        DefaultValue(4),
        Browsable(true)
        ]
        public UInt32 TabSize
        {
            get
            {
                Object o = ViewState["tabSize"];
                return (o != null ? (UInt32)o : 4);
            }
            set
            {
                ViewState["tabSize"] = value;
                Object o = ViewState["tabSize"];
                if (o != null)
                {
                    d["tabSize"] = value;
                }
            }
        }
        /// <summary>
        /// 当输入的字符数超过该值时，保存一次现场
        /// </summary>
        [
        Description("当输入的字符数超过该值时，保存一次现场"),
        Category("常规设置项目"),
        DefaultValue(1),
        Browsable(true)
        ]
        public UInt32 MaxInputCount
        {
            get
            {
                Object o = ViewState["maxInputCount"];
                return (o != null ? (UInt32)o : 1);
            }
            set
            {
                ViewState["maxInputCount"] = value;
                Object o = ViewState["maxInputCount"];
                if (o != null)
                {
                    d["maxInputCount"] = value;
                }
            }
        }
        #endregion

        #region "上传图片相关配置"

        /// <summary>
        /// 图片操作的浮层开关，默认打开
        /// </summary>
        [
        Description("图片操作的浮层开关，默认打开"),
        Category("上传图片相关配置"),
        DefaultValue(true),
        Browsable(true)
        ]
        public Boolean ImagePopup
        {
            get
            {
                Object o = ViewState["imagePopup"];
                return (o != null ? (Boolean)o : true);
            }
            set
            {
                ViewState["imagePopup"] = value;
                Object o = ViewState["imagePopup"];
                if (o != null)
                {
                    d["imagePopup"] = value;
                }
            }
        }


        #endregion

        #region "编辑器界面配置"
        /// <summary>
        /// 工具栏配置类型
        /// </summary>
        [
        Description("工具栏配置类型:Default,General,Basic"),
        Category("编辑器界面配置"),
        Browsable(true)
        ]
        public ToolbarModel ToolType
        {
            get
            {
                Object o = ViewState["Toolbars"];
                return (o != null ? (ToolbarModel)o : ToolbarModel.Default);
            }
            set
            {
                ViewState["Toolbars"] = value;
                switch (value)
                {
                    case ToolbarModel.General:
                        d["toolbars"] = @"[
                                            ['fullscreen', 'source', '|',
                                                'fontfamily', 'fontsize', 'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', '|',
                                                'rowspacingtop', 'rowspacingbottom', 'lineheight', '|',                                                 
                                                'indent', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                                                'link', 'unlink', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|',
                                                'insertimage', 'emotion', 'pagebreak', 'background', '|',
                                                'horizontal', 'spechars', 'snapscreen', '|',
                                                'inserttable', 'charts', '|',
                                                'preview', 'searchreplace'
                                            ]
                                        ]";
                        break;
                    case ToolbarModel.Basic:
                        d["toolbars"] = @"[
                                            ['fullscreen', 'source', '|', 
                                                'fontfamily', 'fontsize','bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'removeformat', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', '|',
                                                'rowspacingtop', 'rowspacingbottom', 'lineheight', 'indent', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 
                                                'link', 'unlink', 'insertimage', 'emotion', 'attachment', 'pagebreak', 'background', 'inserttable', '|',
                                                'horizontal', 'spechars', 'snapscreen', 'preview'
                                            ]
                                        ]";
                        break;
                    case ToolbarModel.Small:
                        d["toolbars"] = @"[
                                            ['source','fontfamily', 'fontsize','bold', 'italic', 'underline', 'fontborder', 'forecolor', 'backcolor', 'removeformat']                                            
                                        ]";
                        break;
                    case ToolbarModel.Exam:
                        d["toolbars"] = @"[
                                            ['fullscreen', 'source', '|', 
                                                'fontfamily', 'fontsize','bold', 'italic', 'underline', 'fontborder', 'superscript', 'subscript', 'removeformat', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', '|',
                                                'rowspacingtop', 'rowspacingbottom', 'lineheight', 'indent', '|', 
                                                'insertimage', 'attachment','background', 'inserttable','spechars','kityformula', 'preview'
                                            ]
                                        ]";
                        break;
                    case ToolbarModel.NotesAndQuestion:
                        d["toolbars"] = @"[
                                            ['fontfamily', 'fontsize','bold', 'italic', 'underline', 'forecolor', 'backcolor', 'emotion', 'attachment']                                            
                                        ]";
                        break;
                    case ToolbarModel.Kity:
                        d["toolbars"] = @"[
                                            [ 'fullscreen', 'source', '|','bold', 'italic', 'underline', '|', 'fontsize', '|', 'kityformula', 'preview']                                            
                                        ]";
                        break;
                    default:
                        d["toolbars"] = @"[
                                            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                                                'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', 'cleardoc', '|',
                                                'rowspacingtop', 'rowspacingbottom', 'lineheight', '|',
                                                'customstyle', 'paragraph', 'fontfamily', 'fontsize', '|',
                                                'directionalityltr', 'directionalityrtl', 'indent', '|',
                                                'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'touppercase', 'tolowercase', '|',
                                                'link', 'unlink', 'anchor', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|',
                                                'insertimage', 'emotion', 'scrawl', 'insertvideo', 'music', 'attachment', 'map', 'gmap', 'insertframe','insertcode', 'webapp', 'pagebreak', 'template', 'background', '|',
                                                'horizontal', 'date', 'time', 'spechars', 'snapscreen', 'wordimage','kityformula', '|',
                                                'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', 'charts', '|',
                                                'print', 'preview', 'searchreplace', 'help', 'drafts'
                                            ]
                                        ]";
                        break;
                }
            }
        }

        /// <summary>
        /// 工具栏配置名称
        /// </summary>
        [
        Description("工具栏配置名称"),
        Category("编辑器界面配置"),
        Browsable(true)
        ]
        private String Toolbars
        {
            get
            {
                Object o = ViewState["Toolbars"];
                return (o != null ? (String)o : @"[
            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', 'cleardoc', '|',
                'rowspacingtop', 'rowspacingbottom', 'lineheight', '|',
                'customstyle', 'paragraph', 'fontfamily', 'fontsize', '|',
                'directionalityltr', 'directionalityrtl', 'indent', '|',
                'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'touppercase', 'tolowercase', '|',
                'link', 'unlink', 'anchor', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|',
                'insertimage', 'emotion', 'scrawl', 'insertvideo', 'music', 'attachment', 'map', 'gmap', 'insertframe','insertcode', 'webapp', 'pagebreak', 'template', 'background', '|',
                'horizontal', 'date', 'time', 'spechars', 'snapscreen', 'wordimage', '|',
                'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', 'charts', '|',
                'print', 'preview', 'searchreplace', 'help', 'drafts']
        ]");
            }
            set
            {
                ViewState["Toolbars"] = value;
                if (value != null && !String.IsNullOrEmpty(value))
                {
                    d["toolbars"] = value;
                }
            }
        }
        /// <summary>
        /// 工具栏浮动开关
        /// </summary>
        [
        Description("工具栏浮动"),
        Category("编辑器界面配置"),
        DefaultValue(true),
        Browsable(true)
        ]
        public Boolean AutoFloatEnabled
        {
            get
            {
                Object o = ViewState["AutoFloatEnabled"];
                return (o != null ? (Boolean)o : true);
            }
            set
            {
                ViewState["AutoFloatEnabled"] = value;
                Object o = ViewState["AutoFloatEnabled"];
                if (o != null)
                {
                    d["autoFloatEnabled"] = value;
                }
            }
        }
        /// <summary>
        /// 是否上来就是全屏
        /// </summary>
        [
        Description("是否上来就是全屏"),
        Category("编辑器界面配置"),
        DefaultValue(false),
        Browsable(true)
        ]
        public Boolean Fullscreen
        {
            get
            {
                Object o = ViewState["fullscreen"];
                return (o != null ? (Boolean)o : false);
            }
            set
            {
                ViewState["fullscreen"] = value;
                Object o = ViewState["fullscreen"];
                if (o != null)
                {
                    d["fullscreen"] = value;
                }
            }
        }
        /// <summary>
        /// 是否只读
        /// </summary>
        [
        Description("是否只读"),
        Category("编辑器界面配置"),
        DefaultValue(false),
        Browsable(true)
        ]
        public Boolean IsReadonly
        {
            get
            {
                Object o = ViewState["readonly"];
                return (o != null ? (Boolean)o : false);
            }
            set
            {
                ViewState["readonly"] = value;
                Object o = ViewState["readonly"];
                if (o != null)
                {
                    d["readonly"] = value;
                }
            }
        }
        /// <summary>
        /// 编辑框下部的path部分
        /// </summary>
        [
        Description("编辑框下部的path部分"),
        Category("编辑器界面配置"),
        DefaultValue(true),
        Browsable(true)
        ]
        public Boolean ElementPathEnabled
        {
            get
            {
                Object o = ViewState["ElementPathEnabled"];
                return (o != null ? (Boolean)o : true);
            }
            set
            {
                ViewState["ElementPathEnabled"] = value;
                Object o = ViewState["ElementPathEnabled"];
                if (o != null)
                {
                    d["elementPathEnabled"] = value;
                }
            }
        }

        /// <summary>
        /// 编辑器最小高度
        /// </summary>
        [
        Browsable(true),
        Description("编辑器最小高度"),
        Category("编辑器界面配置"),
        DefaultValue("320")
        ]
        public override Unit Height
        {
            get
            {
                Object o = ViewState["Height"];
                return (o != null ? (Unit)o : new Unit(320));
            }
            set
            {
                ViewState["Height"] = value;
            }
        }
        /// <summary>
        /// 编辑器宽度
        /// </summary>
        [
        Description("编辑器宽度"),
        Category("编辑器界面配置"),
        DefaultValue("1000"),
        Browsable(true)
        ]
        public override Unit Width
        {
            get
            {
                Object o = ViewState["Width"];
                return (o != null ? (Unit)o : new Unit(1000));
            }
            set
            {
                ViewState["Width"] = value;
            }
        }





        /// <summary>
        /// 控件编辑器z-index的基数
        /// </summary>
        [
        Description("控件编辑器z-index的基数"),
        Category("编辑器界面配置"),
        DefaultValue("900"),
        Browsable(true)
        ]
        public String ZIndex
        {
            get
            {
                Object o = ViewState["zIndex"];
                return (o != null ? Convert.ToString(o) : "900");
            }
            set
            {
                ViewState["zIndex"] = value;
                if (value != null)
                {
                    d["zIndex"] = value;
                }
            }
        }

        /// <summary>
        /// 让编辑器的编辑框部分可以随着编辑内容的增加而自动长高
        /// </summary>
        [
        Description("让编辑器的编辑框部分可以随着编辑内容的增加而自动长高"),
        Category("编辑器界面配置"),
        DefaultValue(true),
        Browsable(true)
        ]
        public Boolean AutoHeightEnabled
        {
            get
            {
                Object o = ViewState["AutoHeightEnabled"];
                return (o != null ? (Boolean)o : true);
            }
            set
            {
                ViewState["AutoHeightEnabled"] = value;
                Object o = ViewState["AutoHeightEnabled"];
                if (o != null)
                {
                    d["autoHeightEnabled"] = value;
                }
            }
        }
        #endregion
    }

    public enum ToolbarModel
    {
        General,
        Basic,
        Small,
        Exam,
        NotesAndQuestion,
        Kity,
        Default
    }
}
