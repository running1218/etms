using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS.WebApp.Manage.Resource.ElearningMap.Controls
{
    public partial class ElearningMapTypeInfo : System.Web.UI.UserControl
    {
        #region 页面条件参数存放

        /// <summary>
        /// 操作类型 1 Add 2 Edit
        /// </summary>
        public Int32 Operation
        {
            get
            {
                if (ViewState["Operation"] == null)
                {
                    ViewState["Operation"] = 1;
                }
                return (Int32)ViewState["Operation"];
            }
            set
            {
                ViewState["Operation"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Operation == 2)
            {
                InitControl();
            }
        }

        private void InitControl()
        {
            TextBox1.Text = "M10001";//学习地图类型编码
            TextBox2.Text = "开发—测试学习地图";//学习地图类型名称
            CheckBox1.Checked = true;
            CheckBox2.Checked = true;
            
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("学习地图类型信息保存成功，点击“确定”后，重新刷新当前页数据！");
        }
    }
}