using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_QuFillBlanks_QuestionView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Literal2.Text = "谈判艺术";
        Literal3.Text = "难";

        Literal1.Text = "正确：恭喜你，答对了！<br />答错了，下次努力哦！";
        Literal5.Text = "明晰解题思路，总结解题技巧，提高解题速度，提升应试能力。";
    }
}