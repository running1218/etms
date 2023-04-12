using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LearningManagement_CourseApply_CourseApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitControl();
    }

    private void InitControl()
    {
        Literal1.Text = "学员姓名"; //学员姓名
        Literal2.Text = "所属部门";
        Literal3.Text = "岗　　位";
        Literal4.Text = "职　　级";
        Literal5.Text = "项目名称";
        Literal6.Text = "申请课程";
        Literal7.Text = "";
        Literal8.Text = "";
        Literal9.Text = "";
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("审核通过，点击“确定”后，重新刷新当前页数据！");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("审核不通过，点击“确定”后，重新刷新当前页数据！");
    }
}