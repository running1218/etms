using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_QuFillBlanks_Controls_QuestionInfo : System.Web.UI.UserControl
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
        ChooseCourseDropdown1.CourseName = "谈判艺术";
        Dic_QuestionLevel1.SelectedValue = "1";
        FCKeditor1.Text = "谈判的要素是<input maxlength=\"30\" size=\"30\" value=\"谈判主题和参与者的个性\" />。";

        FCKeditor2.Text = "明晰解题思路，总结解题技巧，提高解题速度，提升应试能力。";

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("填空题保存成功，点击“确定”后，重新刷新当前页数据！");
    }
}