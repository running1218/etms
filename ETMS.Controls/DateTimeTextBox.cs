using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// 时间输入框
    /// 基于：WdatePicker97来封装，要求页面加载“WdatePicker.js”
    /// 依赖的前端资源"~/Tools/DateControl
    /// </summary>
    public class DateTimeTextBox : TextBox
    {

        private string m_DateTimeFormat = "%Y-%M-%D";
        /// <summary>
        /// 默认日期格式：“%Y-%M-%D”        
        /// 完整日期时间格式：%Y-%M-%D %h:%m:%s
        /// 时间格式：%h:%m:%s
        /// </summary>
        public string DateTimeFormat
        {
            get
            {
                return m_DateTimeFormat;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.m_DateTimeFormat = value;
            }
        }

        /// <summary>
        /// 结束时间控件ID
        /// </summary>
        public string EndTimeControlID
        {
            get
            {
                object ret = this.ViewState["EndTimeControlID"];
                if (ret != null)
                {
                    return (string)ret;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["EndTimeControlID"] = value;
            }
        }

        /// <summary>
        /// 开始时间控件ID
        /// </summary>
        public string BeginTimeControlID
        {
            get
            {
                object ret = this.ViewState["BeginTimeControlID"];
                if (ret != null)
                {
                    return (string)ret;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["BeginTimeControlID"] = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Attributes.Add("readonly", "");
            this.Attributes.Add("onfocus", "new WdatePicker(this,'" + this.DateTimeFormat + "',true)");

            this.Attributes.Add("class", string.Format("{0} Wdate", this.CssClass));
            //如果设置了结束时间控件，则表示当前做为开始时间控件
            if (!string.IsNullOrEmpty(EndTimeControlID))
            {
                Control con = FindControl(this.EndTimeControlID);
                if (con != null)//同一控件树层次下找到ID为this.ContainerID的控件
                {
                    this.Attributes.Add("MAXDATE", "#F{$$('" + con.ClientID + "').value}");
                    this.Attributes.Add("onPicked", "$$('" + con.ClientID + "').onfocus()");
                }
            }
            //如果设置了开始时间控件，则表示当前做为结束时间控件
            else if (!string.IsNullOrEmpty(BeginTimeControlID))
            {
                Control con = FindControl(this.BeginTimeControlID);
                if (con != null)//同一控件树层次下找到ID为this.ContainerID的控件
                {
                    this.Attributes.Add("MINDATE", "#F{$$('" + con.ClientID + "').value}");
                }
            }
        }
        private string m_CssClass;
        public override string CssClass
        {
            get
            {
                return m_CssClass;
            }
            set
            {
                m_CssClass = value;
            }
        }

        public DateTime DateTimeValue
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    return DateTime.Parse(this.Text);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))//如果是0000-1-1时间时，则不显示
                {
                    string dateTimeFormat = "";
                    if (DateTimeFormat.IndexOf(" ") != -1)
                    {
                        dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                    }
                    else if (DateTimeFormat.StartsWith("%Y"))
                    {
                        dateTimeFormat = "yyyy-MM-dd";
                    }
                    else if (DateTimeFormat.IndexOf("%s") != -1)
                    {
                        dateTimeFormat = "HH:mm:ss";
                    }
                    else
                    {
                        dateTimeFormat = "HH:mm";
                    }
                    base.Text = value.ToString(dateTimeFormat);
                }
            }
        }
    }
}
