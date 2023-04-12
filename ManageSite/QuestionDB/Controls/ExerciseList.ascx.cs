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

public partial class QuestionDB_Controls_ExerciseList : System.Web.UI.UserControl
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

    public Boolean isReadOnly
    {
        get
        {
            if (ViewState["isReadOnly"] == null)
            {
                ViewState["isReadOnly"] = false;
            }
            return (Boolean)ViewState["isReadOnly"];
        }
        set
        {
            ViewState["isReadOnly"] = value;
        }
    }

    public Int32 iExerciseType = 1;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        QuestionInfo l_questioninfo = new QuestionInfo();
        Literal1.Text = l_questioninfo.ExerciseName(ExerciseType) + "管理";
        Literal2.Text = Literal1.Text;
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }

        if (isReadOnly)
        {
            dv_selectall.Visible = false;
        }
    }
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (isReadOnly) {
                e.Row.Cells[0].Width = 0;
                e.Row.Cells[0].Text = "";
            }
        }
    }

    private BizDataSourceEnum DataSourceEnum
    {
        get
        {
            switch (ExerciseType)
            {
                case BizExerciseType.ExContest:
                    iExerciseType = 1;
                    return BizDataSourceEnum.qu_ExContestList;
                    
                case BizExerciseType.ExOfflineHomework:
                    return BizDataSourceEnum.qu_ExOfflineHomeworkList;
                    
                case BizExerciseType.ExOnlineHomework:
                    iExerciseType = 2;
                    return BizDataSourceEnum.qu_ExOnlineHomeworkList;
                    
                case BizExerciseType.ExOnlinePractice:
                    iExerciseType = 3;
                    return BizDataSourceEnum.qu_ExOnlinePracticeList;
                case BizExerciseType.ExOnlineTest:
                    iExerciseType = 4;
                    return BizDataSourceEnum.qu_ExOnlineTestList;
                case BizExerciseType.ExRandomPractice:
                    iExerciseType = 5;
                    return BizDataSourceEnum.qu_ExRandomPracticeList;
            }
            return BizDataSourceEnum.qu_ExOnlineHomeworkList;
        }
    }
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        totalRecordCount = 0;
        DataTable dt = ReadExcel.GetData(DataSourceEnum);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        totalRecordCount = dt.Rows.Count;
        return pageDataSource.PageDataSource;
    }

    protected string getExerciseName()
    {
        QuestionInfo l_questioninfo = new QuestionInfo();
        return l_questioninfo.ExerciseName(ExerciseType);
    }
}