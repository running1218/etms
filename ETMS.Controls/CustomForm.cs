#define NET_2_0

#region Imports

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
#endregion

namespace ETMS.Controls
{
    /// <summary>
    /// This control allows for suppressing output of the 'action' attribute.
    /// </summary>
    /// <remarks>
    /// the 'action' attribute rendered by the default <see cref="HtmlForm"/> control causes troubles 
    /// in case of URL-rewriting. See e.g. <a href="http://www.thescripts.com/forum/thread408777.html">'thescripts.com' forum</a>
    /// and also <a href="http://opensource.atlassian.com/projects/spring/browse/SPRNET-560">JIRA SPRNET-560</a> for more info.
    /// </remarks>
    /// <author>Erich Eichinger</author>
    public class CustomForm : HtmlForm
    {
        private bool suppressAction = false;
        private string action = null;

        /// <summary>
        /// Sets or Gets a value indicating if the 'action' attribute shall be rendered. Defaults to 'false'
        /// </summary>
        /// <remarks>
        /// The following possibilites are available:
        /// <list>
        /// <item>If <see cref="SuppressAction"/> is 'true', rendering of the 'action' attribute is suppressed.</item>
        /// <item>If <see cref="SuppressAction"/> is 'false' and <see cref="Action"/> is not set,
        ///       'action' attribute will.be rendered to <see cref="HttpRequest.RawUrl"/>
        /// </item>
        /// <item>If <see cref="SuppressAction"/> is 'false' and <see cref="Action"/> is set,
        ///       'action' attribute will.be rendered to <see cref="Action"/>
        /// </item>
        /// </list>
        /// </remarks>
        public bool SuppressAction
        {
            get { return this.suppressAction; }
            set { this.suppressAction = value; }
        }

        /// <summary>
        /// Sets or Gets an explicit url to be rendered
        /// </summary>
        /// <remarks>
        /// The url specified here is only rendered, if <see cref="SuppressAction"/> is true.
        /// </remarks>
#if !NET_2_0
        public string Action
#else
        public new string Action
#endif
        {
            get { return this.action; }
            set { this.action = value; }
        }

        /// <summary>
        /// Renders attributes but performs 'action' suppressing logic.
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            base.RenderAttributes(new ActionSupressingHtmlTextWriter(writer));
            if (!this.suppressAction)
            {
                string url = (!String.IsNullOrEmpty(this.action)) ? this.action : Context.Request.RawUrl;
                writer.WriteAttribute("action", url, true);
            }
        }

        #region Nested type: ActionSupressingHtmlTextWriter

        /// <summary>
        /// This wrapper suppresses output of 'action' attributes.
        /// </summary>
        private class ActionSupressingHtmlTextWriter : HtmlTextWriter
        {
            public ActionSupressingHtmlTextWriter(HtmlTextWriter wrappedWriter)
                : base(wrappedWriter.InnerWriter)
            {
            }

            public override void WriteAttribute(string name, string value, bool fEncode)
            {
                if (string.Compare(name, "action", true) != 0)
                {
                    base.WriteAttribute(name, value, fEncode);
                }
            }
        }

        #endregion
    }
}