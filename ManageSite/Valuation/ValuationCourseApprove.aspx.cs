using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Evaluation.API.Entity;
using ETMS.Components.Evaluation.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Valuation_ValuationCourseApprove : System.Web.UI.Page
{
    #region 页面参数
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }
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

    protected int UserCount = 0;
    private static readonly Evaluation_ItemResultLogic itemResultlogic = new Evaluation_ItemResultLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);
        if (!Page.IsPostBack)
        {
            bind();
            this.PageSet1.QueryChange();
        }
    }
    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        //对本组织机构创建的未归档的启用培训项目
        string Crieria = string.Format(" AND OrgID={0} AND ItemStatus in (10,20,40) AND IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID);
        int total = 0;
        ddl_Item.DataSource = itemLogic.GetPagedList(1, int.MaxValue - 1, " CreateTime DESC", Crieria, out total);
        ddl_Item.DataTextField = "ItemName";
        ddl_Item.DataValueField = "TrainingItemID";
        ddl_Item.DataBind();
        TrainingItemID = ddl_Item.SelectedValue.ToGuid();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic itemCourseLogic = new Tr_ItemCourseLogic();
        DataTable dt = itemCourseLogic.GetItemCourseListByTrainingItemID(TrainingItemID, pageIndex, pageSize, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        TrainingItemID = ddl_Item.SelectedValue.ToGuid();
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
                lbtn_View.PostBackUrl = this.ActionHref(string.Format("~/Valuation/TrainingItemCourseEvaluationApprove.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
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

        float MarkGood = 0;
        float MarkGeneral = 0;
        float MarkBad = 0;

        float wMarkGood = 0;
        float wMarkGeneral = 0;
        float wMarkBad = 0;

        float wArg = 0;

        plate = logic.GetByObjectTypeItemCourse();

        Guid PlateID = plate.PlateID;

        DataTable dtItems = itemResultLogic.GetResultShowGoodApprove(ObjectID, PlateID);
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
            //评价人次
            UserCount = (MarkGood + MarkGeneral + MarkBad).ToInt();

            //好评度算法
            if (UserCount > 0)
            {
                wArg = ((MarkGood) / (MarkGood + MarkGeneral + MarkBad) * 100);
            }
            wMarkGood = MarkGood / (MarkGood + MarkGeneral + MarkBad) * 100;
            wMarkGeneral = (MarkGeneral / (MarkGood + MarkGeneral + MarkBad)) * 100;
            wMarkBad = MarkBad / (MarkGood + MarkGeneral + MarkBad) * 100;
        }
        else
        {
            UserCount = 0;
        }
        return string.Format("{0:N0}%", wArg.ToString().ToDecimal());
    }

    /// <summary>
    /// 五星打分的平均分
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    protected string getScoreArgCourse(string ObjectID)
    {
        DataTable dt = itemResultlogic.ItemResultByUserApprove(ObjectID, Guid.Empty, -1);
        string scoreArg = string.Format("{0:N1}", dt.Rows[0]["ScoreArg"].ToString().ToDecimal());
        string returnValue = "";
        if (scoreArg == "0.0")
        {
            returnValue = scoreArg;
        }
        else
        {
            string url = this.ActionHref(string.Format("~/Valuation/ItemCourseEvaluationScoreApprove.aspx?ObjectID={0}", ObjectID));
            returnValue = string.Format("<a href='{0}'>{1}</a>", url, scoreArg);
        }
        return returnValue;

    }
}