using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Evaluation.API.Entity;
using System.Data;
using ETMS.Components.Evaluation.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;

public partial class Valuation_Controls_EvaluationScore : System.Web.UI.UserControl
{
    #region 页面参数

    /// <summary>
    /// 评价对象ID
    /// </summary>
    public string ObjectID
    {
        get
        {
            return ViewState["ObjectID"].ToString();
        }
        set
        {
            ViewState["ObjectID"] = value;
        }
    }

    #endregion

    private static readonly UserLogic userlogic = new UserLogic();
    private static readonly Evaluation_ItemResultLogic itemResultlogic = new Evaluation_ItemResultLogic();

    public bool IsApprove{
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
        PageSet1.pageInit(CustomGridView1, PageDataSource);

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
        string Crieria = "";
        string SortExpression = "";
        DataTable dt = null;
        if (IsApprove)
        {
            Crieria = string.Format(" And ei.ObjectID='{0}' And ei.ItemID in (select ItemID from Evaluation_Item where EvaluationLevel=5) ", ObjectID);
            SortExpression = " ei.CreateTime desc ";
            dt = itemResultlogic.GetPagedListApprove(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        }
        else
        {
            Crieria = string.Format(" And ObjectID='{0}' And ItemID in (select ItemID from Evaluation_Item where EvaluationLevel=5) ", ObjectID);
            SortExpression = " CreateTime desc ";
            dt = itemResultlogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);
        }
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);

        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// UserName
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    protected string getUserName(int userID)
    {
        if (userID > 0)
        {
            IUser iUser = userlogic.GetUserByID(userID);

            return iUser.RealName;
        }
        return "";
    }

    
}