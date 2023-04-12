using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Information_AfficheManager_Controls_AfficheInfo : System.Web.UI.UserControl
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

    /// <summary>
    /// 类型 1 公司级 2 项目级
    /// </summary>
    public Int32 InfoType
    {
        get
        {
            if (ViewState["InfoType"] == null)
            {
                ViewState["InfoType"] = 1;
            }
            return (Int32)ViewState["InfoType"];
        }
        set
        {
            ViewState["InfoType"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (InfoType == 1)
        {
            trCompany.Visible = true;
            trProject.Visible = false;
        }
        else
        {
            trCompany.Visible = false;
            trProject.Visible = true;
        }
        if (Operation == 2)
        {
            InitControl();
        }
    }

    private void InitControl()
    {
        //TextBox1.Text = "全国异地高考即将破冰 三大焦点问题待解(组图)";
        //TextBox2.Text = "全国政协委员、教育部副部长杜玉波表示，解决异地高考的问题，“既想到要解决随迁子女的考试问题，又不能影响北京、上海当地考生的权益”。";
        //fckEditor.Value = "3月3日，教育部部长袁贵仁在列席全国政协十一届五次会议开幕式接受媒体采访时表示，关于异地高考的问题，教育部正鼓励各地尽快推进，现在到最后的冲刺阶段了，用不了10个月就会出台相关政策。<br />他同时表示：“异地高考是有条件的，除了考虑城市承载能力外，还对家长和孩子有一定条件要求，家长要在当地有工作。涉及很多情况，比较复杂，有些城市可能难度稍微大一些，教育部也为此正在积极协调，鼓励各地尽快推进。” <br />此前，山东省在全国率先突破高考户籍限制，出台政策，明确从2014年起将允许非户籍考生在山东参加高考。袁贵仁表示，教育部酝酿中的异地高考改革方案与山东出台的政策“思路上一致”。 <br />全国政协委员、教育部副部长杜玉波进一步表示，各地要在年底前出台有关允许异地高考的时间表。 <br />两会刚刚开始，此番表态无疑引起巨大反响。两会期间，异地高考成为一个热点，引起各方讨论。 <br />全国异地高考即将破冰，已无疑问，但异地高考的门槛如何设置、如何兼顾本地户籍考生的正当权益、实施的时间等三大焦点问题仍需要明晰的答案。 <br />异地高考政策久拖不决。其实，这不是袁贵仁部长第一次表态。2011年全国两会上，袁贵仁就明确表示，对于流动人口子女在就读地参加中、高考问题，“很快会有一个方案”，“目前正在和上海、北京研究”。 <br />有了这一信号，在北京长期工作居住，但因为孩子无法在京高考而焦虑的家长看到了希望。 <br />2011年3月24日，20名在北京的学生家长向教育部学生司工作人员递交了“学籍与户籍分开，以居住地和学籍确定高考地的建议方案”。这些家长都是外地来京工作多年，孩子当中大部分在北京完成了小学、初中教育，马上面临高考。 <br />2011年10月，他们又向社会公开了民间版的《随迁子女输入地高考方案》，并递交给教育部。这份方案提出，不再把户籍作为高考报名的限制条件，高考报名资格依据学籍和父母经常居住地的标准认定。具体措施是随父母在经常居住地上学，至高中毕业3年以上连续学籍的，高中毕业即可在经常居住地参加高考和录取。对于北京、上海等情况较为特殊的区域，方案中也有明确设计：随父母在经常居住地上学，至高中毕业有连续4年以上学籍的，高中毕业即可在经常居住地参加高考和录取。 <br />之后，这些非京籍家长每个月到教育部信访办提交公开信，希望能够得到明确答复。同时，北京、上海、广东等地的家长志愿者自发组织，在地铁口、广场等公共场所进行社会宣传，目前，已争取到接近10万人次的公众签名。民间版《随迁子女输入地高考方案》就是他们在调研、征求意见，组织专家学者、家长、媒体记者召开研讨会的基础上拟成的。 <br />不过，这些努力没有带来任何进展。其中，有些孩子已经进入高三，有的与父母分离回到几乎没有生活过的户口所在地，以社会报名的方式参加当地高考。有的只好选择出国。<br />一位网名为“网上游”的母亲，孩子在北京一所知名中学读书，成绩在班里排在前几名，是北京市级三好学生。由于没有北京市户口，经过痛苦的考虑，这位母亲无奈地决定让孩子去读一所民办学校，准备将来出国读书。因为小孩从一年级就在北京读书，老家的教材与北京不同，孩子回去参加考试也不可能有好成绩。 <br />“教育部曾表示要在2011年年底前出台相关政策，但是一直没有出台，对我们的信访也没有明确的答复。”一位家长志愿者用“忍无可忍”来形容已经到了承受极限的心情。";
        //txtBeginTime.Text = "2012-1-5";
        //txtEndTime.Text = "2012-10-5";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("公告保存成功，点击“确定”后，重新刷新当前页数据！");
    }

}