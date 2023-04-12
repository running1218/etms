using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_CatalogManage_Controls_CatalogInfo : System.Web.UI.UserControl
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
        Literal1.Text = "课程—第一课";
        if (Operation == 2)
        {
            InitControl();
        }
    }

    private void InitControl()
    {
      
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("试题分类信息保存成功，点击“确定”后，重新刷新当前页数据！");
    }
}