using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using ETMS.Components.ExOnlineJob.Implement.BLL.ExOnlineJob;
using ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob;
using ETMS.Controls;

using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Exam.API.Interface.Test;
using ETMS.Components.Exam.API.Entity.Test;
using ETMS.Components.Exam.API;

public partial class QuestionDB_ExOnlineHomework_Controls_ExerciseList : System.Web.UI.UserControl
{
    #region 页面参数
    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            return (string)ViewState["Crieria"];
        }
        set
        {
            ViewState["Crieria"] = value;
        }
    }

    /// <summary>
    /// 排序条件
    /// </summary>
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpression"] == null)
            {
                ViewState["SortExpression"] = " CreateTime DESC ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();

        }
    }

    /// <summary>
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        Crieria += string.Format("{0} AND OrgID={1}", Crieria, UserContext.Current.OrganizationID);
        Ex_OnLineJobLogic onlinejobLogic = new Ex_OnLineJobLogic();

        DataTable dt = onlinejobLogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    /// <summary>
    /// 单个删除课程信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            try
            {
                Ex_OnLineJobLogic onlinejobLogic = new Ex_OnLineJobLogic();
                onlinejobLogic.Remove(new Guid(e.CommandArgument.ToString()));
                //this.PageSet1.QueryChange();
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    protected int getExerciseType()
    {
        
        return (int)ETMS.Components.Basic.Implement.BLL.BizExerciseType.ExOnlineHomework;
    }

    public string getSumScore(string testPaperID, int totalScore)
    {
        string cssFont = "#333333";
        if (string.IsNullOrEmpty(testPaperID))
        {
            return string.Format("<font color='{0}'>未出题</font>", "#333333");
        }
        else
        {
            int totalSize = 0;
            IPaperQuestionLogic iPaperQuestionLogic = ServiceRepository.PaperQuestionService;
            IList<PaperQuestionView> paperQuestionViewList = iPaperQuestionLogic.FindQuestionView(testPaperID.ToGuid(), ETMS.Components.Exam.API.Entity.ItemBank.QuestionType.Null, 1000, 1, out totalSize);

            decimal SumScore = 0;

            foreach (PaperQuestionView eBasePriview in paperQuestionViewList)
            {
                SumScore += eBasePriview.QuestionScore;
            }

            if ((int)SumScore == totalScore)
            {
                cssFont = "#C03219";
            }
            else
            {
                cssFont = "#333333";
            }

            return string.Format("<font color='{0}'>{1}</font>", cssFont, (int)SumScore);
        }

    }
}