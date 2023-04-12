using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.AppContext;
using System.Data;

using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

using ETMS.Components.Fee.Implement.BLL;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using ETMS.Components.Basic.Implement;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours;
using ETMS.Components.Basic.Implement.BLL;

public partial class Fee_TeacherPay_Controls_PaymentList : System.Web.UI.UserControl
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
                ViewState["SortExpression"] = " ";
            }
            return (string)ViewState["SortExpression"];
        }
        set
        {
            ViewState["SortExpression"] = value;
        }
    }

    /// <summary>
    /// 支付状态
    /// </summary>
    public int PayStatus
    {
        get
        {
            if (ViewState["PayStatus"] == null)
            {
                ViewState["PayStatus"] = 0;
            }
            return (int)ViewState["PayStatus"];
        }
        set
        {
            ViewState["PayStatus"] = value;
        }
    }

    #endregion
    public static PublicFacade publicFacade = new PublicFacade();
    protected void Page_Load(object sender, EventArgs e)
    {      
        
        PageSet1.pageInit(this.rptResult, PageDataSource);
        PageSet1.PageSize = 4;
        if (!Page.IsPostBack)
        {
            this.PageSet1.QueryChange();
            if (PayStatus == 0)
            {   
                lbtnCancel.Visible = false;
                LinkButton1.Visible = true;
            }
            else
            {
                LinkButton1.Visible = false;
                lbtnCancel.Visible = true;
            }
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
        ////讲师课酬支付管理：按培训项目、讲师姓名、课程名称排序。
        SortExpression = " a.TrainingBeginTime asc, f.TrainingItemID asc ,d.RealName asc,g.CourseName asc ";
        Crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        
        //PayStatus 支付状态 课时安排状态必须是“已执行” TeacherSourceID = 1 必须是内部讲师
        Crieria += string.Format("{0} And a.PayStatus={1} And a.CourseHoursStatusID=1 And c.TeacherSourceID = 1  and d.OrganizationID={2} ", Crieria, PayStatus,ETMS.AppContext.UserContext.Current.OrganizationID);

        Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

        DataTable dt = itemCourseHoursLogic.GetItemCourseHoursALLInfoList(pageIndex, pageSize, SortExpression, Crieria, out totalRecordCount);

        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        if (totalRecordCount == 0)
        {
            ltlNull.Text = "没有任何记录！";
        }
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
    /// 批量支付
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 0;

            foreach (RepeaterItem item in this.rptResult.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("CheckBox1");
                TextBox realCourseHours = (TextBox)item.FindControl("txtRealCourseHours");
                TextBox realCourseFee = (TextBox)item.FindControl("txtRealCourseFee");
                HiddenField trainingItemCourseID = (HiddenField)item.FindControl("hfdTrainingItemCourseID");

                if (check.Checked)
                {
                    Tr_ItemCourseHours itemCourseHours = new Tr_ItemCourseHours();
                    Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

                    itemCourseHours = itemCourseHoursLogic.GetById(trainingItemCourseID.Value.ToGuid());
                    itemCourseHours.RealCourseHours = realCourseHours.Text.Replace(",","").ToDecimal();
                    itemCourseHours.RealCourseFee = realCourseFee.Text.Replace(",", "").ToDecimal();
                    itemCourseHours.PayStatus = 1;
                    itemCourseHours.ModifyTime = DateTime.Now;
                    itemCourseHours.ModifyUser = new PublicFacade().GetTeacherInfo(ETMS.AppContext.UserContext.Current.UserID).UserInfo.RealName;
                    itemCourseHoursLogic.Update(itemCourseHours);
                    i++;
                }
            }

            if (i > 0)
            {
                this.PageSet1.QueryChange();
                JsUtility.SuccessMessageBox("提示", string.Format("成功支付[{0}]项！", i), "function(){window.location=window.location}");            
            }
            else
            {
                JsUtility.AlertMessageBox("请勾选您要支付的讲师。");
            }
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            JsUtility.FailedMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (RepeaterItem item in this.rptResult.Items)
        {
            CheckBox check = (CheckBox)item.FindControl("CheckBox1");
            
            HiddenField trainingItemCourseID = (HiddenField)item.FindControl("hfdTrainingItemCourseID");

            if (check.Checked)
            {
                Tr_ItemCourseHours itemCourseHours = new Tr_ItemCourseHours();
                Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();

                itemCourseHours = itemCourseHoursLogic.GetById(trainingItemCourseID.Value.ToGuid());
                
                itemCourseHours.PayStatus = 0;
                itemCourseHours.RealCourseHours =0;
                itemCourseHours.RealCourseFee = 0;
                itemCourseHoursLogic.Update(itemCourseHours);
                i++;
            }
        }
        if (i > 0)
        {
            this.PageSet1.QueryChange();
            JsUtility.SuccessMessageBox("提示", string.Format("成功取消支付[{0}]项！", i), "function(){window.location=window.location}");
        }
        else {
            JsUtility.AlertMessageBox("请勾选您要取消支付的讲师。");
        }
    }

    public string getClientID()
    {
        return this.lbtnSave.ClientID;
    }

   
}