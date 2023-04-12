using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Evaluation.API.Entity;
using ETMS.Components.Evaluation.Implement.BLL;
using System.Data;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Components.Basic.API.Entity.Teacher;

public partial class Valuation_ValuationTeacher : System.Web.UI.Page
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
    #endregion

   
    private static readonly Evaluation_ItemResultLogic itemResultlogic = new Evaluation_ItemResultLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
    }


    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Site_TeacherLogic logic = new Site_TeacherLogic();
        string teacherCode = txt_TeacherCode.Text.Trim();
        string teacherName = txt_Site_User999RealName.Text.Trim();
        List<Site_Teacher> teacherList = logic.GetTeachersByOrganization(teacherCode,teacherName, pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(teacherList, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }


    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string TrainingItemCourseID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            LinkButton lbtn_View = (LinkButton)e.Row.FindControl("lbtn_View");
            if (lbtn_View != null)
            {
                lbtn_View.PostBackUrl = this.ActionHref(string.Format("~/Valuation/TeacherEvaluation.aspx?UserID={0}", TrainingItemCourseID));
            }
        }
    }

    /// <summary>
    /// 好评度
    /// </summary>
    protected string initEvaluation(string ObjectID)
    {
        Evaluation_Plate plate = new Evaluation_Plate();
        Evaluation_PlateLogic logic = new Evaluation_PlateLogic();
        Evaluation_ItemResultLogic itemResultLogic = new Evaluation_ItemResultLogic();
        int UserCount = 0;
        float MarkGood = 0;
        float MarkGeneral = 0;
        float MarkBad = 0;

        float wMarkGood = 0;
        float wMarkGeneral = 0;
        float wMarkBad = 0;

        float wArg = 0;

        plate = logic.GetByObjectTypeTeacher();

        Guid PlateID = plate.PlateID;

        DataTable dtItems = itemResultLogic.GetResultShowGood(ObjectID, PlateID);
        if (dtItems.Rows.Count > 0)
        {
            foreach (DataRow dr in dtItems.Rows)
            {
                if (dr["Score"].ToInt() == 3)
                {
                    MarkGood = dr["UserCount"].ToInt();
                }
                if (dr["Score"].ToInt() == 2)
                {
                    MarkGeneral = dr["UserCount"].ToInt();
                }
                if (dr["Score"].ToInt() == 1)
                {
                    MarkBad = dr["UserCount"].ToInt();
                }
            }

            //好评度算法

            UserCount = (MarkGood + MarkGeneral + MarkBad).ToInt();

            if (UserCount > 0)
            {
                wArg = ((MarkGood) / (MarkGood + MarkGeneral + MarkBad) * 100);
            }
            wMarkGood = MarkGood / (MarkGood + MarkGeneral + MarkBad) * 100;
            wMarkGeneral = (MarkGeneral / (MarkGood + MarkGeneral + MarkBad)) * 100;
            wMarkBad = MarkBad / (MarkGood + MarkGeneral + MarkBad) * 100;


        }

        return string.Format("{0:N0}%",wArg.ToString().ToDecimal()); 

    }

    protected string getUserCount(string ObjectID)
    {
        Evaluation_Plate plate = new Evaluation_Plate();
        Evaluation_PlateLogic logic = new Evaluation_PlateLogic();
        Evaluation_ItemResultLogic itemResultLogic = new Evaluation_ItemResultLogic();
        int UserCount = 0;
        float MarkGood = 0;
        float MarkGeneral = 0;
        float MarkBad = 0;

        float wMarkGood = 0;
        float wMarkGeneral = 0;
        float wMarkBad = 0;

        float wArg = 0;

        plate = logic.GetByObjectTypeTeacher();

        Guid PlateID = plate.PlateID;

        DataTable dtItems = itemResultLogic.GetResultShowGood(ObjectID, PlateID);
        if (dtItems.Rows.Count > 0)
        {
            foreach (DataRow dr in dtItems.Rows)
            {
                if (dr["Score"].ToInt() == 3)
                {
                    MarkGood = dr["UserCount"].ToInt();
                }
                if (dr["Score"].ToInt() == 2)
                {
                    MarkGeneral = dr["UserCount"].ToInt();
                }
                if (dr["Score"].ToInt() == 1)
                {
                    MarkBad = dr["UserCount"].ToInt();
                }
            }

            //好评度算法

            UserCount = (MarkGood + MarkGeneral + MarkBad).ToInt();
        }
        return UserCount.ToString();
    }

    /// <summary>
    /// 五星打分的平均分
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    protected string getScoreArgTeacher(string ObjectID)
    {
        DataTable dt = itemResultlogic.ItemResultByUser(ObjectID, Guid.Empty, -1);
        string scoreArg = string.Format("{0:N1}", dt.Rows[0]["ScoreArg"].ToString().ToDecimal());
        string returnValue = "";
        if (scoreArg == "0.0")
        {
            returnValue = scoreArg;
        }
        else
        {
            string url = this.ActionHref(string.Format("~/Valuation/TeacherEvaluationScore.aspx?ObjectID={0}", ObjectID));
            returnValue = string.Format("<a href='{0}'>{1}</a>",url,scoreArg);
        }
        return returnValue;
       
    }
}