using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{

    public class SubmitTextBox : CustomTextBox
    {
        [TypeConverter(typeof(SubmitableControlConvertor)), DefaultValue(""), Category("Behavior")]
        public string SubmitControl
        {
            get
            {
                object ret = this.ViewState["SubmitControl"];
                if (ret != null)
                {
                    return (string)ret;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["SubmitControl"] = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("ClickHandleFunction"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClickHandleFunction", @"
               <script language='javascript'>
    function doClick(objId)
    {
        if(document.all)
        {
            document.getElementById(objId).click();
        }
        else
        {
            var evt = document.createEvent('MouseEvents');
            evt.initEvent('click', true, true);
            document.getElementById(objId).dispatchEvent(evt);
        } 
    }
    </script>
                ");
            }
            base.OnLoad(e);
        }
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (this.SubmitControl.Length > 0)
            {
                Control con = FindControl(SubmitControl);
                if (con != null)
                {
                    string script = "javascript:if(event.keyCode == 13){doClick('" + con.ClientID + "');event.returnValue=false;}";
                    writer.AddAttribute("onkeydown", script);
                }
            }

        }
    }
    public class SubmitableControlConvertor : StringConverter
    {
        private object[] GetControls(IContainer container)
        {
            ComponentCollection components = container.Components;
            ArrayList ret = new ArrayList();
            foreach (IComponent control in components)
            {
                if (!(control is Button || control is LinkButton || control is ImageButton))
                {
                    continue;
                }
                Control button = (Control)control;
                if ((button.ID != null) && (button.ID.Length != 0))
                {
                    ret.Add(string.Copy(button.ID));
                }
            }
            ret.Sort(Comparer.Default);
            return ret.ToArray();
        }


        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if ((context != null) && (context.Container != null))
            {
                object[] controls = this.GetControls(context.Container);
                if (controls != null)
                {
                    return new TypeConverter.StandardValuesCollection(controls);
                }
            }
            return null;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    } 
}
