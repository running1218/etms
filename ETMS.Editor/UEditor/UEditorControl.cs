using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Editor.Common;

namespace ETMS.Editor.UEditor
{
    /// <summary>
    /// UEditor编辑器控件
    /// </summary>
    [Description("UEditorControl.UEditor")]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:UEditor runat=server></{0}:UEditor>")]
    public partial class UEditor : Panel
    {
        /// <summary>
        /// 编辑器配置文件地址
        /// </summary>
        private readonly String editor_config = "{0}ueditor.config.js";
        /// <summary>
        /// 编辑器渲染类库地址
        /// </summary>
        private readonly String editor_all = "{0}ueditor.all.min.js";
        /// <summary>
        /// 编辑器语言包地址
        /// </summary>
        private readonly String editor_lang = "{0}lang/zh-cn/zh-cn.js";

        #region 数学公式插件

        private readonly String addKityFormulaDialog = "{0}kityformula-plugin/addKityFormulaDialog.js";
        private readonly String getKfContent = "{0}kityformula-plugin/getKfContent.js";
        private readonly String defaultFilterFix = "{0}kityformula-plugin/defaultFilterFix.js";

        #endregion

        /// <summary>
        /// 用于保存配置信息字典
        /// </summary>
        private Dictionary<String, Object> d = new Dictionary<String, Object>();
        /// <summary>
        /// 编辑器注册脚本
        /// </summary>
        private readonly String editor_init = @"
        <script type=""text/javascript""> 
                var option{3} = {{{2}}};
                var {1} = new baidu.editor.ui.Editor(option{3});
                {1}.render('{0}');
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



            //注册配置脚本
            String f_editor_config = String.Format(editor_config, fullPath);


            if (!csm.IsClientScriptIncludeRegistered("editor_config"))
                csm.RegisterClientScriptInclude(this.GetType(), "editor_config", f_editor_config);

            //注册功能脚本
            String f_editor_all = String.Format(editor_all, fullPath);


            if (!csm.IsClientScriptIncludeRegistered("editor_all"))
                csm.RegisterClientScriptInclude(this.GetType(), "editor_all", f_editor_all);

            //注册语言脚本
            String f_editor_lang = String.Format(editor_lang, fullPath);

            if (!csm.IsClientScriptIncludeRegistered("editor_lang"))
                csm.RegisterClientScriptInclude(this.GetType(), "editor_lang", f_editor_lang);


            if (ToolType == ToolbarModel.Kity)
            {
                #region 数学公式插件

                String f_addKityFormulaDialog = String.Format(addKityFormulaDialog, fullPath);
                if (!csm.IsClientScriptIncludeRegistered("addKityFormulaDialog"))
                    csm.RegisterClientScriptInclude(this.GetType(), "addKityFormulaDialog", f_addKityFormulaDialog);

                String f_getKfContent = String.Format(getKfContent, fullPath);
                if (!csm.IsClientScriptIncludeRegistered("getKfContent"))
                    csm.RegisterClientScriptInclude(this.GetType(), "getKfContent", f_getKfContent);

                String f_defaultFilterFix = String.Format(defaultFilterFix, fullPath);
                if (!csm.IsClientScriptIncludeRegistered("defaultFilterFix"))
                    csm.RegisterClientScriptInclude(this.GetType(), "defaultFilterFix", f_defaultFilterFix);

                #endregion
            }
            //初始化组件脚本
            String f_editor_init = String.Format(editor_init, "editor" + this.ID, "ue" + this.ID, EditorPublic.GetConfigString(d), this.ID);
            csm.RegisterStartupScript(this.GetType(), "editor_init" + this.ID, f_editor_init, false);
        }
    }
}
