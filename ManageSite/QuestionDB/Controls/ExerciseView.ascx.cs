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
public partial class QuestionDB_Controls_ExerciseView : System.Web.UI.UserControl
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

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        QuestionInfo l_questioninfo = new QuestionInfo();
        Literal99.Text = l_questioninfo.ExerciseName(ExerciseType) + "基本信息";
        Literal2.Text = l_questioninfo.ExerciseName(ExerciseType);
        InitControl();
    }

    private void InitControl()
    {   
        QuestionInfo l_questioninfo = new QuestionInfo();
        Literal1.Text = "谈判艺术" + l_questioninfo.ExerciseName(ExerciseType) + "1";
        Literal3.Text = Literal2.Text;
               
        Literal5.Text = "60";
        
        
        Literal8.Text = "是";
        Literal9.Text = "谈判艺术";
        Literal12.Text = "2012-5-5";
        Literal13.Text = "崔岩";
    }

    
}