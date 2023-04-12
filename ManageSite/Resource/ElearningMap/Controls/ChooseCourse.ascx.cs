using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Components.Basic.Implement.BLL;
using ETMS.Controls;
using ETMS.WebApp;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.AppContext;
using ETMS.Components.Basic.Implement.BLL.ELearningMap;
using ETMS.Components.Basic.API.Entity.ELearningMap;

public partial class StudyMap_ChooseCourse : System.Web.UI.UserControl
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
                ViewState["SortExpression"] = " CourseCode ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }
    
    #endregion

    /// <summary>
    /// 关联的对象 Teacher ElearningMap
    /// </summary>
    public ObjectCourseRelation ObjectRefType
    {
        get
        {
            if (ViewState["ObjectRefType"] == null)
            {
                ViewState["ObjectRefType"] = 1;
            }
            return (ObjectCourseRelation)ViewState["ObjectRefType"];
        }
        set
        {
            ViewState["ObjectRefType"] = value;
        }
    }

    /// <summary>
    /// 外部关联的对象ID
    /// </summary>
    public String ObjectRefID
    {
        get
        {
            return ViewState["ObjectRefID"].ToString();
        }
        set
        {
            if (ViewState["ObjectRefID"] == null)
                ViewState["ObjectRefID"] = "";
            ViewState["ObjectRefID"] = value;
        }
    }

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
        Res_CourseLogic courseLogic = new Res_CourseLogic();

        DataTable dt = new DataTable();

        Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        Crieria = string.Format("{0} AND OrgID={1} AND CourseStatus=1", Crieria, UserContext.Current.OrganizationID);

        //根据不同的ObjectRefType, ObjectRefID值，返回不同业务下的未关联课程列表
        dt = courseLogic.GetCourseNotInListByObjectID(ObjectRefType, ObjectRefID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

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
        //upList.Update();
    }

    /// <summary>
    /// 批量建立关系
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        object[] selectedValues = CustomGridView.GetSelectedValues<object>(this.CustomGridView1);
        if (selectedValues.Length == 0)
        {
            JsUtility.AlertMessageBox("请选择要关联的课程！");
            return;
        }
        try
        {
            Res_StudyMapReferCourseLogic studyMapCourseLogic = new Res_StudyMapReferCourseLogic();
            List<Res_StudyMapReferCourse> source = new List<Res_StudyMapReferCourse>();
            foreach (GridViewRow row in CustomGridView1.Rows)
            {
                CheckBox ckb = row.FindControl("CheckBox1") as CheckBox;
                if (null != ckb && ckb.Checked)
                {
                    DropDownList ddlStudyMap = (DropDownList)row.FindControl("ddlStudyModel");
                    TextBox txtChargeMan = (TextBox)row.FindControl("txtActualMan");

                    source.Add(new Res_StudyMapReferCourse() { 
                        StudyMapReferCourseID = Guid.NewGuid(),
                        StudyMapID = ObjectRefID.ToGuid(),
                        CourseID = CustomGridView1.DataKeys[row.RowIndex].Value.ToGuid(),
                        StudyModelID = ddlStudyMap.SelectedValue.ToInt(),
                        ChargeMan = txtChargeMan.Text,
                        CreateTime = DateTime.Now,
                        CreateUser = UserContext.Current.RealName,
                        CreateUserID = UserContext.Current.UserID,
                        ModifyTime = DateTime.Now,
                        ModifyUser = UserContext.Current.RealName
                    });
                }
            }

            studyMapCourseLogic.Save(source);
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }

        btnSearch_Click(sender, e);
        JsUtility.SuccessMessageBoxAndRedirectToParent(string.Format("成功关联[{0}]条课程信息！", selectedValues.Length), getCancelUrl());
    }

    protected string getCancelUrl()
    {
        return this.ActionHref(string.Format("~/Resource/ElearningMap/MapCourseList.aspx?StudyMapID={0}", ObjectRefID));
    }
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            e.Row.Cells[6].CssClass = "visibleS";
        }
    }
}