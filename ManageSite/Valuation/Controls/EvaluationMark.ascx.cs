using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Evaluation.Implement.BLL;
using ETMS.Components.Evaluation.API.Entity;
using System.Data;
using ETMS.Utility;

public partial class Comment_Controls_EvaluationMark : System.Web.UI.UserControl
{
    #region 页面参数
    
    /// <summary>
    /// 评价对象ID
    /// </summary>
    public string ObjectID
    {
        get
        {
            if (ViewState["ObjectID"] == null)
                ViewState["ObjectID"] = "";
            return ViewState["ObjectID"].ToString();
        }
        set
        {
            ViewState["ObjectID"] = value;
        }
    }

    /// <summary>
    /// 评价量表ID
    /// </summary>
    public Guid PlateID
    {
        get
        {
            if (ViewState["PlateID"] == null)
                ViewState["PlateID"] = Guid.Empty;
            return (Guid)ViewState["PlateID"];
        }
        set
        {
            ViewState["PlateID"] = value;
        }
    }

    /// <summary>
    /// 评价类型
    /// </summary>
    public BizEvaluationObjectType ObjectType
    {
        get
        {
            return (BizEvaluationObjectType)ViewState["ObjectType"];
        }
        set
        {
            ViewState["ObjectType"] = value;
        }
    }

    #endregion

    private static readonly Evaluation_PlateLogic logic = new Evaluation_PlateLogic();
    private static readonly Evaluation_ItemResultLogic itemResultLogic = new Evaluation_ItemResultLogic();

    #region 数据
    
    
    protected float MarkGood
    {
        get
        {
            if (ViewState["MarkGood"] == null)
                ViewState["MarkGood"] = 0;
            return float.Parse(ViewState["MarkGood"].ToString());
        }
        set
        {
            ViewState["MarkGood"] = value;
        }
    }

    protected float MarkGeneral
    {
        get
        {
            if (ViewState["MarkGeneral"] == null)
                ViewState["MarkGeneral"] = 0;
            return float.Parse(ViewState["MarkGeneral"].ToString());
        }
        set
        {
            ViewState["MarkGeneral"] = value;
        }
    }

    protected float MarkBad
    {
        get
        {
            if (ViewState["MarkBad"] == null)
                ViewState["MarkBad"] = 0;
            return float.Parse(ViewState["MarkBad"].ToString());
        }
        set
        {
            ViewState["MarkBad"] = value;
        }
    }

    protected float wMarkGood
    {
        get
        {
            if (ViewState["wMarkGood"] == null)
                ViewState["wMarkGood"] = 0;
            return float.Parse(ViewState["wMarkGood"].ToString());
        }
        set
        {
            ViewState["wMarkGood"] = value;
        }
    }
    protected float wMarkGeneral
    {
        get
        {
            if (ViewState["wMarkGeneral"] == null)
                ViewState["wMarkGeneral"] = 0;
            return float.Parse(ViewState["wMarkGeneral"].ToString());
        }
        set
        {
            ViewState["wMarkGeneral"] = value;
        }
    }
    protected float wMarkBad
    {
        get
        {
            if (ViewState["wMarkBad"] == null)
                ViewState["wMarkBad"] = 0;
            return float.Parse(ViewState["wMarkBad"].ToString());
        }
        set
        {
            ViewState["wMarkBad"] = value;
        }
    }

    #endregion
    public bool IsApprove
    {
        get
        {
            if (ViewState["IsApprove"] == null)
                ViewState["IsApprove"] = false;
            return ViewState["IsApprove"].ToBoolean();
        }
        set { ViewState["IsApprove"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(ObjectID))
            {
                InitPlate();
                InitItems();
                initEvaluation();
            }
        }
    }

    /// <summary>
    /// 好评度
    /// </summary>
    private void initEvaluation()
    {
        DataTable dtItems = null;
        if (IsApprove)
        {
            dtItems = itemResultLogic.GetResultShowGoodApprove(ObjectID, PlateID);
        }
        else {
            dtItems = itemResultLogic.GetResultShowGood(ObjectID, PlateID);
        }
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
            if (MarkGood + MarkGeneral + MarkBad > 0)
            {
                ltlPercentMark.Text = string.Format("{0:N0}", ((MarkGood) / (MarkGood + MarkGeneral + MarkBad) * 100).ToString().ToDecimal());
            }
            wMarkGood = MarkGood / (MarkGood + MarkGeneral + MarkBad) * 100;
            wMarkGeneral = (MarkGeneral / (MarkGood + MarkGeneral + MarkBad)) * 100;
            wMarkBad = MarkBad / (MarkGood + MarkGeneral + MarkBad) *100;
        }
        
    }

    /// <summary>
    /// 评价量表
    /// </summary>
    private void InitPlate()
    {
        Evaluation_Plate plate = new Evaluation_Plate();
        switch (ObjectType)
        {
            case BizEvaluationObjectType.ItemCourse:
                plate = logic.GetByObjectTypeItemCourse();
                break;
            case BizEvaluationObjectType.Teacher:
                plate = logic.GetByObjectTypeTeacher();
                break;
            default:
                break;
        }
        PlateID = plate.PlateID;
    }

    /// <summary>
    /// 显示评价项结果
    /// </summary>
    private void InitItems()
    {   
        DataTable dtItems = itemResultLogic.GetResultShow(ObjectID, PlateID);

        DataTable dt = new DataTable();
        
        dt.Columns.Add("ItemName", typeof(string));
        dt.Columns.Add("strLink", typeof(string));

        string mstr = string.Empty;
        string cssOn = string.Empty;

        //好评度

        //除了综合评价外的5颗星的结果
        for (int i = 1; i < dtItems.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ItemName"] = dtItems.Rows[i]["ItemName"];

            mstr = "<div id='rateMe'>";
            for (int k = 1; k <= dtItems.Rows[i]["EvaluationLevel"].ToInt(); k++)
            {
                //亮显的星星
                cssOn = "";
                if (k <= dtItems.Rows[i]["star"].ToInt())
                {
                    cssOn = " class='on'";
                }
                mstr += string.Format("<a id='{0}_{1}' {2}></a>", ObjectID, k,cssOn);
            }
            mstr += "</div>";

            dr["strLink"] = mstr;

            dt.Rows.Add(dr);
        }

        repeaterItems.DataSource = dt;
        repeaterItems.DataBind();
    }

}