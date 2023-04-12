using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Point.Implement.BLL;
using ETMS.Components.Point.API.Entity;

public partial class Score_ItemCourseScore :ETMS.Controls.BasePage 
{
    private static Point_Student_CourseRoleLogic pointStudentCourseRoleLogic = new Point_Student_CourseRoleLogic();
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
        string crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
        DataTable dt = pointStudentCourseRoleLogic.GetPagedList(pageIndex, pageSize, " MinNum,MaxNum", crieria, out totalRecordCount);
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(dt, pageIndex, pageSize);
        return pageDataSource.PageDataSource;
    }

    protected void CustomGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !CustomGridView1.IsEmpty)
        {
            
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlFootCourseAttrID = e.Row.FindControl("ddlFootCourseAttrID") as DropDownList;
            foreach (ListItem item in ddl_CourseAttrID.Items)
            {
                ddlFootCourseAttrID.Items.Add(item);
            }
           
        }
    }

    /// <summary>
    /// 行操作
    /// </summary>
    protected void CustomGridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        //保存编辑列
        if (e.CommandName == "save")
        {
            #region 
            //获取控件         
            TextBox txtFootMinNum = (TextBox)CustomGridView1.FooterRow.FindControl("txtFootMinNum");
            TextBox txtFootMaxNum = (TextBox)CustomGridView1.FooterRow.FindControl("txtFootMaxNum");
            TextBox txtFootScore = (TextBox)CustomGridView1.FooterRow.FindControl("txtFootScore");
            DropDownList ddlFootCourseAttrID = (DropDownList)CustomGridView1.FooterRow.FindControl("ddlFootCourseAttrID");

            string result = string.Empty;
            //验证
            if (string.IsNullOrEmpty(ddlFootCourseAttrID.SelectedValue) || ddlFootCourseAttrID.SelectedValue.ToInt() == -1 || ddlFootCourseAttrID.SelectedValue.ToInt() == 0)
            {
                result += "请选择课程属性！"+"  ";
            }
            if (string.IsNullOrEmpty(txtFootMinNum.Text.Trim()))
            {
               result+="起始数不能为空！"+"  ";
            }
            if (string.IsNullOrEmpty(txtFootMaxNum.Text.Trim()))
            {
                result += "截止数不能为空！" + "  ";
            }
            if (string.IsNullOrEmpty(txtFootScore.Text.Trim()))
            {
                result += "积分不能为空！" + "  ";
            }
            if (!string.IsNullOrEmpty(result))
            {
                ETMS.Utility.JsUtility.AlertMessageBox(result);
                return;
            }
            else
            {
                string strRegex = @"^[1-9]+$";
                System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(strRegex);
                string checkText = string.Empty;
                if (!rg.IsMatch(txtFootMinNum.Text.Trim()))
                {
                    checkText += "起始数格式错误!"+"  ";
                }
                if (!rg.IsMatch(txtFootMaxNum.Text.Trim()))
                {
                    checkText += "截止数格式错误!"+"  ";
                }
                if (!rg.IsMatch(txtFootScore.Text.Trim()))
                {
                    checkText += "积分格式错误!";
                }
                if (!string.IsNullOrEmpty(checkText))
                {
                    checkText += "必须为[1-9]的数";
                    ETMS.Utility.JsUtility.AlertMessageBox(checkText);
                    return;
                }
            }
            if (txtFootMinNum.Text.Trim().ToInt()==0)
            {
                result += "起始数超出了范围或为零！";
            }
            if (txtFootMaxNum.Text.Trim().ToInt() == 0)
            {
                result += "截止数超出了范围或为零！";
            }
            if (txtFootScore.Text.Trim().ToInt() == 0)
            {
                result += "积分超出了范围或为零！";
            }
            if (!string.IsNullOrEmpty(result))
            {
                ETMS.Utility.JsUtility.AlertMessageBox(result);
                return;
            }

            if (txtFootMinNum.Text.Trim().ToInt() > txtFootMaxNum.Text.Trim().ToInt())
            {
                ETMS.Utility.JsUtility.AlertMessageBox("起始数超过截止数！");
                return;
            }
            
            //保存
            try
            {
                Point_Student_CourseRole courseRole=new Point_Student_CourseRole();
                courseRole.CourseAttrID = ddlFootCourseAttrID.SelectedValue.ToInt();
                courseRole.StudentCoursePointTypeID = 0;//0:项目课程课时（默认）
                courseRole.MinNum = txtFootMinNum.Text.Trim().ToInt();               
                courseRole.MaxNum = txtFootMaxNum.Text.Trim().ToInt();
                courseRole.GivePoints = txtFootScore.Text.Trim().ToInt();
                courseRole.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
                courseRole.CreateTime = DateTime.Now;
                courseRole.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
                courseRole.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
                pointStudentCourseRoleLogic.Save(courseRole);
                ETMS.Utility.JsUtility.SuccessMessageBox("保存成功！");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
            //保存完成 
            this.CustomGridView1.ShowFooter = false;
            this.PageSet1.DataBind();
            #endregion
        }else
        //取消编辑列
        if (e.CommandName == "cancel")
        {
           this.CustomGridView1.ShowFooter = false;
            this.PageSet1.DataBind();
        }else
            if (e.CommandName == "del")
            {
                try
                {
                    pointStudentCourseRoleLogic.doRemove(e.CommandArgument.ToGuid());
                    ETMS.Utility.JsUtility.SuccessMessageBox("删除成功！");
                    this.PageSet1.DataBind();
                }
                catch (ETMS.AppContext.BusinessException bizEx)
                {
                    ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                    return;
                }
            }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        CustomGridView1.ShowFooter = true;
        this.PageSet1.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange();
    }
}