using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.QuestionDB;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;

public partial class QuestionDB_Controls_ExerciseInfo : System.Web.UI.UserControl
{
    #region 页面条件参数存放

    public BizExerciseType ExerciseType
    {
        get
        {
            if (ViewState["ExerciseType"] == null)
            {
                ViewState["ExerciseType"] = 1;
            }
            return (BizExerciseType)ViewState["ExerciseType"];
        }
        set
        {
            ViewState["ExerciseType"] = value;
        }
    }


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
        QuestionInfo l_questioninfo = new QuestionInfo();
        string strOperation = "";
        switch (Operation)
        {
            case 1:
                strOperation = "新增";
                break;
            case 2:
                strOperation = "编辑";
                break;
            default:
                break;
        }
        Literal5.Text = l_questioninfo.ExerciseName(ExerciseType) + "管理&gt;&gt;" + strOperation + l_questioninfo.ExerciseName(ExerciseType);
        Literal1.Text = l_questioninfo.ExerciseName(ExerciseType);

        Literal2.Text = Literal1.Text + "：";
                
        Literal4.Text = System.DateTime.Now.ToString();
        Literal14.Text = "崔岩";
        
        if (Operation == 2)
        {
            InitControl();
        }

        switch (ExerciseType)
        {
            case BizExerciseType.ExContest:
                Literal6.Text = this.ActionHref(string.Format("<a href='~/QuestionDB/TestPaper/TestpaperAddSimple.aspx?iExerciseType=1&Operation={0}' class='btn_4'>手工出题</a><a href='~/QuestionDB/TestPaper/TestpaperAddSenior.aspx?iExerciseType=1&Operation={0}' class='btn_4'>随机出题</a>", Operation.ToString()));
                break;
            case BizExerciseType.ExOfflineHomework:
                break;
            case BizExerciseType.ExOnlineHomework:
                Literal6.Text = this.ActionHref(string.Format("<a href='~/QuestionDB/TestPaper/TestpaperAddSimple.aspx?iExerciseType=3&Operation={0}' class='btn_4'>手工出题</a><a href='~/QuestionDB/TestPaper/TestpaperAddSenior.aspx?iExerciseType=3&Operation={0}' class='btn_4'>随机出题</a>", Operation.ToString()));
                break;
            case BizExerciseType.ExOnlinePractice:
                break;
            case BizExerciseType.ExOnlineTest:
                Literal6.Text = this.ActionHref(string.Format("<a href='~/QuestionDB/TestPaper/TestpaperAddSimple.aspx?iExerciseType=5&Operation={0}' class='btn_4'>手工出题</a><a href='~/QuestionDB/TestPaper/TestpaperAddSenior.aspx?iExerciseType=5&Operation={0}' class='btn_4'>随机出题</a>",  Operation.ToString()));
                break;
            case BizExerciseType.ExRandomPractice:
                break;
            default:
                break;
        }
    }

    private void InitControl()
    {
        QuestionInfo l_questioninfo = new QuestionInfo();
        ChooseCourseDropdown1.CourseName = "谈判艺术";
        TextBox1.Text = "谈判艺术" + l_questioninfo.ExerciseName(ExerciseType) + "1";
        TextBox2.Text =  l_questioninfo.ExerciseName(ExerciseType);
        
        
        TextBox4.Text = "60";
        
        Dic_TrueOrFalse1.SelectedValue = "1";
        Literal4.Text = "2012-2-15";
                
    }


}