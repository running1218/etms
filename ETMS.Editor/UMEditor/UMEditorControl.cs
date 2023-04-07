using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ETMS.Editor.Common;

namespace ETMS.Editor.UMEditor
{
    /// <summary>
    /// UEditor编辑器控件
    /// </summary>
    [Description("UMEditorControl.UMEditor")]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:UMEditor runat=server></{0}:UMEditor>")]
    public partial class UMEditor : Panel
    {
        /// <summary>
        /// jquery文件
        /// </summary>
        private readonly String editor_j = "{0}third-party/jquery.min.js";
        /// <summary>
        /// 编辑器配置文件地址
        /// </summary>
        private readonly String editor_config = "{0}umeditor.config.js";
        /// <summary>
        /// 编辑器渲染类库地址
        /// </summary>
        private readonly String editor_all = "{0}umeditor.min.js";
        /// <summary>
        /// 编辑器语言包地址
        /// </summary>
        private readonly String editor_lang = "{0}lang/zh-cn/zh-cn.js";
        /// <summary>
        /// 编辑器样式地址
        /// </summary>
        private readonly String editor_css = @"{0}themes/default/css/umeditor.css";

        /// <summary>
        /// 用于保存配置信息字典
        /// </summary>
        private Dictionary<String, Object> d = new Dictionary<String, Object>();
        /// <summary>
        /// 编辑器注册脚本
        /// </summary>
        private readonly String editor_init = @"
        <script type=""text/javascript""> 
                var option{2} = {{{1}}};
                UM.getEditor('{0}',option{2});                              
        </script>
        ";

        /// <summary>
        /// 页面渲染
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(
                "<script id=\"{0}\" type=\"text/plain\" style=\"width: {1}; height: {2}\"></script>", "editor" + this.ID, this.Width, this.Height);

        }
        /// <summary>
        /// 摘要:如果 System.Web.UI.WebControls.TextBox.AutoPostBack 是 true，则在客户端上呈现之前注册用于生成回发事件的客户端脚本。
        /// 参数:e:
        /// 包含事件数据的 System.EventArgs。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.TextArea = this.TextArea;
            this.Text = this.Text;

            this.Attributes.CssStyle.Add(HtmlTextWriterStyle.Width, Width.ToString());
            this.Attributes.CssStyle.Add(HtmlTextWriterStyle.Height, Height.ToString());


            ClientScriptManager csm = Page.ClientScript;
            String fullPath = ResolveUrl(@"~" + this.ScriptPath);


            String f_editor_css = String.Format(editor_css, fullPath);

            HtmlGenericControl _CssFile = new HtmlGenericControl("link");
            _CssFile.ID = "CssFile";
            _CssFile.Attributes["rel"] = "stylesheet";
            _CssFile.Attributes["type"] = "text/css";
            _CssFile.Attributes["href"] = f_editor_css;
            if (this.FindControl(_CssFile.ID) == null)
            {
                this.Page.Header.Controls.Add(_CssFile);
            }

            //jquery文件
            String f_editor_j = String.Format(editor_j, fullPath);

            if (!csm.IsClientScriptIncludeRegistered("meditor_j"))
                csm.RegisterClientScriptInclude(this.GetType(), "meditor_j", f_editor_j);

            //注册配置脚本
            String f_editor_config = String.Format(editor_config, fullPath);


            if (!csm.IsClientScriptIncludeRegistered("meditor_config"))
                csm.RegisterClientScriptInclude(this.GetType(), "meditor_config", f_editor_config);

            //注册功能脚本
            String f_editor_all = String.Format(editor_all, fullPath);


            if (!csm.IsClientScriptIncludeRegistered("meditor_all"))
                csm.RegisterClientScriptInclude(this.GetType(), "meditor_all", f_editor_all);

            //注册语言脚本
            String f_editor_lang = String.Format(editor_lang, fullPath);

            if (!csm.IsClientScriptIncludeRegistered("meditor_lang"))
                csm.RegisterClientScriptInclude(this.GetType(), "meditor_lang", f_editor_lang);

            //初始化组件脚本
            String f_editor_init = String.Format(editor_init, "editor" + this.ID, EditorPublic.GetConfigString(d), this.ID);
            csm.RegisterStartupScript(this.GetType(), "editor_init" + this.ID, f_editor_init, false);
        }
    }
}
