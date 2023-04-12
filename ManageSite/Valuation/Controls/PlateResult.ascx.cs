using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Components.Evaluation.API.Entity;
using ETMS.Components.Evaluation.Implement.BLL;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System.Data;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class Comment_Controls_PlateResult : System.Web.UI.UserControl
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
            InitPlate();
        }
        PageSet1.pageInit(CustomGridView1, PageDataSource);

        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
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
    /// 查询结果
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalRecordCount"></param>
    /// <returns></returns>
    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        string Crieria = string.Format(" and PlateId='{0}' and ObjectID = '{1}'",PlateID,ObjectID);
        if (IsApprove) {
            Crieria = string.Format(" {0} and ApproveStatus=1", Crieria);
        }
        string SortExpression = " CreateTime desc ";

        DataTable dt = resultlogic.GetPagedList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

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

    protected string getUserImg(int userID)
    {
        if (userID > 0)
        {
        Site_Student student = new Site_StudentLogic().GetStudentById(userID);
        
            return StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.UserIcon, string.IsNullOrEmpty(student.PhotoUrl) ? "default.gif" : student.PhotoUrl); ;
        }
        return "";
    }    
}