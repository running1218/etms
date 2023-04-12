using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Evaluation.API.Entity;
using ETMS.Components.Evaluation.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Valuation_EvaluationApproveList : System.Web.UI.Page
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
    public string TrainingItemID
    {
        get
        {
            return ViewState["TrainingItemID"].ToString();
        }
        set
        {
            ViewState["TrainingItemID"] = value;
        }
    }

    /// <summary>
    /// 评价量表ID
    /// </summary>
    public Guid PlateID
    {
        get
        {
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
    private static readonly Evaluation_PlateResultLogic resultlogic = new Evaluation_PlateResultLogic();
    private static readonly Evaluation_ItemResultLogic itemResultlogic = new Evaluation_ItemResultLogic();
    private static readonly UserLogic userlogic = new UserLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectType = ETMS.Components.Basic.Implement.BLL.BizEvaluationObjectType.ItemCourse;
        ObjectID = Request.QueryString["TrainingItemCourseID"];
        TrainingItemID = Request.QueryString["TrainingItemID"];
        if (!IsPostBack)
        {
            InitPlate();
        }
        PageSet1.pageInit(CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
        }
        lbtnreturn.PostBackUrl = this.ActionHref(string.Format("ItemCourseEvaluationApprove.aspx?TrainingItemID={0}", TrainingItemID));
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
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
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string Crieria = string.Format(" and PlateId='{0}' and ObjectID = '{1}' and EvaluationContent like '%{2}%'", PlateID, ObjectID,txt_Content.Text.Trim());
        if (ddl_TrainingLevelID.SelectedValue != "-1")
            Crieria = Crieria + " and ApproveStatus=" + ddl_TrainingLevelID.SelectedValue;
        string SortExpression = " CreateTime desc ";

        DataTable dt = resultlogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);

        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 审批
    /// </summary>
    protected void btnApproveAll_Click(object sender, EventArgs e)
    {
        Guid[] selectedValues = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            ETMS.Utility.JsUtility.AlertMessageBox("请选择要审批的点评！");
            return;
        }
        else
        {
            Evaluation_PlateResultLogic plateResultLogic = new Evaluation_PlateResultLogic();
            Evaluation_PlateResult plateResult = new Evaluation_PlateResult();
            foreach (Guid resultSubID in selectedValues)
            {
                plateResult = plateResultLogic.GetById(resultSubID);
                plateResult.ApproveStatus = 1;
                plateResult.ApproveTime = DateTime.Now;
                plateResult.ApproveUserID = ETMS.AppContext.UserContext.Current.UserID;
                plateResultLogic.Update(plateResult);
            }
            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "点评审批成功！");
            this.PageSet1.DataBind();
        }
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

    protected string getUserImg(int userID)
    {
        if (userID > 0)
        {
            Site_Student student = new Site_StudentLogic().GetStudentById(userID);

            return StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.UserIcon, string.IsNullOrEmpty(student.PhotoUrl) ? "default.gif" : student.PhotoUrl); ;
        }
        return "";
    }

    protected void CustomGridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hf_ApproveStatus = (HiddenField)e.Row.FindControl("hf_ApproveStatus");
            hf_ApproveStatus = hf_ApproveStatus == null ? new HiddenField() : hf_ApproveStatus;

            LinkButton lbtn_Approve = (LinkButton)e.Row.FindControl("lbtn_Approve");
            lbtn_Approve = lbtn_Approve == null ? new LinkButton() : lbtn_Approve;

            LinkButton lbtn_CancelApprove = (LinkButton)e.Row.FindControl("lbtn_CancelApprove");
            lbtn_CancelApprove = lbtn_CancelApprove == null ? new LinkButton() : lbtn_CancelApprove;
            
            CheckBox CheckBox1 = (CheckBox)e.Row.FindControl("CheckBox1");
            CheckBox1 = CheckBox1 == null ? new CheckBox() : CheckBox1;

            switch (hf_ApproveStatus.Value)
            {
                case "0":
                    CheckBox1.Enabled = true;
                    lbtn_CancelApprove.Visible = false;
                    lbtn_Approve.Visible = true;
                    lbtn_Approve.Attributes["onClick"] = "javascript:showWindow('点评审批','" + this.ActionHref("EvaluationApprove.aspx?ResultSubID=" + lbtn_Approve.CommandArgument) + "',550,430);javascript:return false;";
                    break;
                case "1":
                case "2":
                    lbtn_CancelApprove.Visible = true;
                    lbtn_Approve.Visible = false;

                    break;
            }

        }
    }
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CancelApprove") {
            Evaluation_PlateResultLogic plateResultLogic = new Evaluation_PlateResultLogic();
            Evaluation_PlateResult plateResult = plateResultLogic.GetById(e.CommandArgument.ToGuid());
            plateResult.ApproveStatus =0;
            plateResult.ApproveTime = DateTime.Now;
            plateResult.ApproveUserID = ETMS.AppContext.UserContext.Current.UserID;
            plateResultLogic.Update(plateResult);

            ETMS.Utility.JsUtility.SuccessMessageBox("提示", "点评审批取消成功！");
            this.PageSet1.DataBind();
        }
    }

    protected string getScore(int userID)
    {
        string str = "";
        DataTable dt = itemResultlogic.GetResultUserScore(ObjectID, PlateID, userID);
        if (null != dt && dt.Rows.Count > 0)
        {
            switch (dt.Rows[0]["Score"].ToString())
            {
                case "1":
                    str = "差评";
                    break;
                case "2":
                    str = "中评";
                    break;
                case "3":
                    str = "好评";
                    break;
            }
        }
        return str;
    }
}