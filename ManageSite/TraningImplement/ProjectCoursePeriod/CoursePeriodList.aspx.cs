using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.API.Entity.Common;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.Implement.BLL.ClassRoom;
using ETMS.Components.Basic.API.Entity.ClassRoom;

public partial class TraningImplement_ProjectCoursePeriod_CoursePeriodList : System.Web.UI.Page
{
    #region 页面参数

    /// <summary>
    /// 项目课程ID 
    /// </summary>
    public Guid TrainingItemCourseID
    {
        get
        {
            if (ViewState["TrainingItemCourseID"] == null)
                ViewState["TrainingItemCourseID"] = Guid.Empty;
            return ViewState["TrainingItemCourseID"].ToGuid();
        }
        set
        {
            ViewState["TrainingItemCourseID"] = value;
        }
    }

    /// <summary>
    /// 查询条件 
    /// </summary>
    protected string Crieria
    {
        get
        {
            if (ViewState["Crieria"] == null)
                ViewState["Crieria"] = "";
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
                ViewState["SortExpression"] = "";
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
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.CustomGridView1, PageDataSource);

        if (!Page.IsPostBack && !string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemCourseID")))
        {
            TrainingItemCourseID = BasePage.getSafeRequest(this.Page, "TrainingItemCourseID").ToGuid();
            bind();

            this.PageSet1.QueryChange();
        }
        btnAdd.Attributes["onclick"] = string.Format("javascript:showWindow('新增','{0}',650,500);javascript:return false", this.ActionHref(string.Format("CoursePeriodAdd.aspx?TrainingItemCourseID={0}", TrainingItemCourseID)));
        lbtnReturn.PostBackUrl = this.ActionHref("CourseList.aspx");
    }

    /// <summary>
    /// 邦定
    /// </summary>
    private void bind()
    {
        //获得项目课程信息
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        Tr_ItemCourse ItemCourse = ItemCourseLogic.GetById(TrainingItemCourseID);
        if (ItemCourse != null)
        {
            #region 项目代码与名称
            Tr_ItemLogic itemLogic = new Tr_ItemLogic();
            Tr_Item item = itemLogic.GetById(ItemCourse.TrainingItemID);
            if (item != null)
            {
                lblItemName.Text = item.ItemName;
            }
            #endregion

            #region 课程相关信息
            Res_CourseLogic CourseLogic = new Res_CourseLogic();
            Res_Course Course = CourseLogic.GetById(ItemCourse.CourseID);
            if (Course != null)
            {
                lblCourseName.Text = Course.CourseName;
            }
            #endregion

            dlblTeachModel.FieldIDValue = ItemCourse.TeachModelID.ToString();
            dlblTrainingModel.FieldIDValue = ItemCourse.TrainingModelID.ToString();
            dlblCourseAttr.FieldIDValue = ItemCourse.CourseAttrID.ToString();
        }
        lblSelectCourse.Text = new Sty_StudentCourseLogic().GetItemCourseStudentNum(TrainingItemCourseID).ToString();
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
        DataTable dt = hoursLogic.GetItemCourseHoursListByTrainingItemCourseID(TrainingItemCourseID, pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 行绑定
    /// </summary>
    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid itemCourseHoursID = CustomGridView1.DataKeys[e.Row.RowIndex].Value.ToGuid();
            #region 获取控件
            //编辑
            LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
            lbtnEdit = lbtnEdit == null ? new LinkButton() : lbtnEdit;
            //删除
            CustomLinkButton clbtnDel = (CustomLinkButton)e.Row.FindControl("clbtnDel");
            clbtnDel = clbtnDel == null ? new CustomLinkButton() : clbtnDel;
            //查看
            LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
            lbtnView = lbtnView == null ? new LinkButton() : lbtnView;
            //设置学员
            LinkButton lbtnSetStudent = (LinkButton)e.Row.FindControl("lbtnSetStudent");
            lbtnSetStudent = lbtnSetStudent == null ? new LinkButton() : lbtnSetStudent;
            //报名表
            LinkButton lbtnSignInTable = (LinkButton)e.Row.FindControl("lbtnSignInTable");
            lbtnSignInTable = lbtnSignInTable == null ? new LinkButton() : lbtnSignInTable;

            //课程课时状态
            DictionaryLabel dlblCourseHoursStatus = (DictionaryLabel)e.Row.FindControl("dlblCourseHoursStatus");
            dlblCourseHoursStatus = dlblCourseHoursStatus == null ? new DictionaryLabel() : dlblCourseHoursStatus;

            //学员数
            Label lblStudentTotal = (Label)e.Row.FindControl("lblStudentTotal");
            lblStudentTotal = lblStudentTotal == null ? new Label() : lblStudentTotal;

            LinkButton lbtnSetResult = (LinkButton)e.Row.FindControl("lbtnSetResult");
            lbtnSetResult = lbtnSetResult == null ? new LinkButton() : lbtnSetResult;

            HiddenField hfPayStatus = (HiddenField)e.Row.FindControl("hfPayStatus");
            hfPayStatus = hfPayStatus == null ? new HiddenField() : hfPayStatus;
            #endregion
            //查看
            lbtnView.Attributes["onclick"] = string.Format("javascript:showWindow('查看课时信息','{0}',650,500);javascript:return false;"
                    , this.ActionHref(string.Format("CoursePeriodView.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}", TrainingItemCourseID, itemCourseHoursID)));
            //课时学员数
            lblStudentTotal.Text = new Tr_ItemCourseHoursStudentLogic().GetItemCourseHoursStudentNumByItemCourseHoursID(itemCourseHoursID).ToString();
            #region 如果课时状态为已执行与未执行时 不可编辑删除设置学员
            switch (dlblCourseHoursStatus.FieldIDValue.Trim())
            {
                case "0"://未设置
                    lbtnEdit.Attributes["onclick"] = string.Format("javascript:showWindow('编辑课时信息','{0}',650,500);javascript:return false;"
                 , this.ActionHref(string.Format("CoursePeriodEdit.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}", TrainingItemCourseID, itemCourseHoursID)));
                    lbtnSetStudent.PostBackUrl = this.ActionHref(string.Format("SetsStudentList.aspx?TrainingItemCourseID={0}&ItemCourseHoursID={1}", TrainingItemCourseID, itemCourseHoursID));
                    break;
                case "1"://已执行
                case "2"://未执行
                    lbtnEdit.Enabled = false;
                    lbtnEdit.CssClass = "link_colorGray";
                    clbtnDel.Enabled = false;
                    clbtnDel.EnableConfirm = false;
                    clbtnDel.CssClass = "link_colorGray";
                    lbtnSetStudent.Enabled = false;
                    lbtnSetStudent.CssClass = "link_colorGray";
                    break;
            }
            //如果已支付不可修改结果信息
            switch (hfPayStatus.Value.Trim())
            {
                case "0":
                    lbtnSetResult.Enabled = true;
                    lbtnSetResult.Attributes["onclick"] = string.Format("javascript:showWindow('设置执行结果','{0}',650,500);javascript:return false;"
                                        , this.ActionHref(string.Format("../ProjectCoursePeriodResult/CoursePeriodResultEdit.aspx?ItemCourseHoursID={0}", itemCourseHoursID)));
                    break;
                default:
                    lbtnSetResult.Enabled = false;
                    lbtnSetResult.CssClass = "link_colorGray";
                    break;
            }
            #endregion
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //删除
        if (e.CommandName == "delCourseHours")
        {
            try
            {
                Tr_ItemCourseHoursLogic hoursLogic = new Tr_ItemCourseHoursLogic();
                hoursLogic.Remove(e.CommandArgument.ToGuid());
                ETMS.Utility.JsUtility.SuccessMessageBox("课时信息删除成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
        //签到表
        if (e.CommandName == "SignInTable")
        {
            ExportFile(e.CommandArgument.ToGuid());
        }
    }

    #region 导出数据相关操作
    /// <summary>
    /// 导出数据
    /// </summary>
    /// <param name="itemCourseHoursID">项目课程课时ID</param>
    private void ExportFile(Guid itemCourseHoursID)
    {
        //列表邦定数据
        int totalRecordCount = 0;
        this.CustomGridViewExport.DataSource = PageDataSourceExport(itemCourseHoursID, 1, int.MaxValue - 1, out totalRecordCount);
        this.CustomGridViewExport.DataBind();

        #region 表头数据
        string content = @"<table width='100%' border='1'>
                              <tr>
                                 <td colspan='8' align='center' style='font-size:14; font-weight:bold'>培训签到表</td>
                              </tr>
                              <tr>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训项目</td>
                                <td colspan='6'>{0}</td>
                              </tr>
                              <tr>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训课程</td>
                                <td width='30' colspan='2'>{1}</td>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训讲师</td>
                                <td width='30' colspan='2'>{2}</td>
                              </tr>
                              <tr>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训时间</td>
                                <td width='30' colspan='2'>{3}</td>
                                <td width='20%' colspan='2' style='font-size:12;font-weight:bold'>培训地点</td>
                                <td width='30' colspan='2'>{4}</td>
                              </tr>
                        </table>";
        //填充数据
        int total = 0;
        DataTable dt = new Tr_ItemCourseHoursLogic().GetItemCourseHoursALLInfoList(1, 1, "", string.Format(" AND a.ItemCourseHoursID='{0}'", itemCourseHoursID), out total);
        if (dt.Rows.Count > 0)
        {
            content = string.Format(content
                , dt.Rows[0]["ItemName"].ToString()
                , dt.Rows[0]["CourseName"].ToString()
                , dt.Rows[0]["TeacherName"].ToString()
                , dt.Rows[0]["TrainingDate"].ToDate() + "（" + dt.Rows[0]["TrainingBeginTime"].ToDateTime().ToString("HH:mm") + " - " + dt.Rows[0]["TrainingEndTime"].ToDateTime().ToString("HH:mm") + "）"
                , dt.Rows[0]["ClassRoomName"].ToString() + (string.IsNullOrEmpty(dt.Rows[0]["Address"].ToString().Trim()) ? "": "（" + dt.Rows[0]["Address"].ToString() + "）")
                );
        }
        #endregion

        #region 读取列表数据
        //System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(System.Globalization.CultureInfo.CurrentCulture);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        CustomGridViewExport.RenderControl(oHtmlTextWriter);//将服务器控件的内容输出  
        content += oStringWriter.ToString();
        if (CustomGridViewExport is GridView)
            content = content.Replace("border=\"0\"", "border=\"1\"");
        #endregion
        //输出
        ETMS.Utility.FileDownLoadUtility.ExportToExcel("培训签到表.xls", content);
    }


    private System.Collections.IList PageDataSourceExport(Guid ItemCourseHoursID, int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseHoursStudentLogic hoursLogic = new Tr_ItemCourseHoursStudentLogic();
        DataTable dt = hoursLogic.GetItemCourseHoursStudentByItemCourseHoursID(ItemCourseHoursID, pageIndex, pageSize, " u.OrganizationID,u.DepartmentID,u.RealName", "", out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// GridView导出Excel特殊设置
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!control.GetType().Equals(CustomGridViewExport.GetType()))
        {
            base.VerifyRenderingInServerForm(control);
        }
    }

    /// <summary>
    /// 培训地点
    /// </summary>
    protected string GetAddress(object ClassRoomName, object Address)
    {
        Address = string.IsNullOrEmpty(Address.ToString()) ? "" : "（" + Address.ToString() + "）";

        return ClassRoomName.ToString() + Address;
    }

    /// <summary>
    /// 行邦定
    /// </summary>
    protected void CustomGridViewExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //单机构版本隐藏机构列
        if (ETMS.Product.ProductDefine.IsSingleOrganizationVersion)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
    #endregion
}