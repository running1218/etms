using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuestionDB_QuQuestionAndAnswer_QuestionView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Literal2.Text = "谈判艺术";
        Literal3.Text = "难";

        //FCKeditor3.Value = "根据谈判双方的实力对比和具体情况确定谈判技巧，是周恩来谈判艺术的重要体现。当己方处于优势时，他会毫不犹豫地首先报价，在气势上压垮对方，利用对方的弱点来让他就范；当处于劣势时，则严守底线，把精力集中在试探对方真实意图上，通过讨价还价和据理力争，尽可能多地争取利益。不到万不得已，不亮底牌。<br />基本有以下几点：<br />1硬于所当硬，让于所当让<br />2占优先报价，出劣后摊牌<br />3调整方案，打破僵局<br />4以链条式让步求稳，以一次性让步显真诚<br />5事实胜于雄辩<br />6先治气，后治心<br />7要互相承认，而不要互相敌视";

        Literal5.Text = " 明晰解题思路，总结解题技巧，提高解题速度，提升应试能力。";   
    }
}