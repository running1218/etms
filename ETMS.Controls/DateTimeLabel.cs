using System;
using System.Web.UI.WebControls;
namespace ETMS.Controls
{
    /// <summary>
    /// 时间显示标签
    /// </summary>
    public class DateTimeLabel : Label
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime DateTimeValue
        {
            get
            {
                if (ViewState["DateTimeValue"] == null)
                {
                    return DateTime.MinValue;
                }
                return (DateTime)ViewState["DateTimeValue"];
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))
                {
                    base.Text = value.ToString((DateTimeFormat ?? "yyyy-MM-dd"));
                }
            }
        }
    }
}
