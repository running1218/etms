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

public partial class Point_CourseRoleList : ETMS.Controls.BasePage
{   
    private static Point_Student_CourseRoleLogic pointStudentCourseRoleLogic = new Point_Student_CourseRoleLogic();
    /// <summary>
    /// 课程属性编码
    /// </summary>     
    public int CourseAttrID
    {        
        get
        {
            return Request.ToparamValue<int>("CourseAttrID");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //设置完后传过来的课程属性
            if (CourseAttrID != -1)
            {
                ddl_CourseAttrID.SelectedValue = CourseAttrID.ToString();
            }
            ddl_CourseAttrID.SelectedValue ="1";//下拉框默认必修课
            binding();           
        }
    }
    private void binding()
    {
        int total = 0;
        //string crieria = BasePage.getQueryConditionFromQueryControlList(tableQueryControlList);
       
        string crieria=string.Empty;
        if (!string.IsNullOrEmpty(ddl_CourseAttrID.SelectedValue) && ddl_CourseAttrID.SelectedValue.ToInt() != -1 )
        {
            crieria= string.Format(" and CourseAttrID={0}", ddl_CourseAttrID.SelectedValue.ToInt());
        }
        crieria += string.Format(" and OrgID={0}", ETMS.AppContext.UserContext.Current.OrganizationID);
        DataTable dt = pointStudentCourseRoleLogic.GetPagedList(1, int.MaxValue - 1, " CourseAttrID,MinNum,MaxNum", crieria, out total);
        this.CustomGridView1.DataSource = dt.DefaultView;
        this.CustomGridView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        binding();
        upList.Update();
    }

    public string GetUrl()
    { 
        return this.ActionHref(string.Format("CourseRoleSetting.aspx?CourseAttrID={0}", ddl_CourseAttrID.SelectedValue));
    }
 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.ddl_CourseAttrID.SelectedValue) || this.ddl_CourseAttrID.SelectedValue.ToInt() == -1)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage("请选择课程属性！");
            return;
        }        
        Response.Redirect(this.ActionHref(string.Format("CourseRoleSetting.aspx?CourseAttrID={0}", this.ddl_CourseAttrID.SelectedValue.ToInt())));
    }
}