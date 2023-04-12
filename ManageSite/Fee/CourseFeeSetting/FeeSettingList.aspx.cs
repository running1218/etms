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
using ETMS.Components.Fee.Implement.BLL;
using ETMS.Components.Fee.API.Entity;
using ETMS.Utility;


public partial class Fee_CourseFeeSetting_FeeSettingList : System.Web.UI.Page
{
    private static readonly Fee_CourseFeeSettingLogic CourseFeeSettingLogic = new Fee_CourseFeeSettingLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitData();

        }
    }

    private void InitData()
    {   
        int totalRecords = 0;
        string SortExpression = " TeacherLevelID asc , TrainingTimeDescID asc";
        string Criteria = string.Format(" And OrgID={0}",ETMS.AppContext.UserContext.Current.OrganizationID);

        DataTable dt = CourseFeeSettingLogic.GetPagedList(1, 100, SortExpression, Criteria, out totalRecords);

        if (totalRecords == 0)
        {
            CourseFeeSettingLogic.AddByOrgID();
            dt = CourseFeeSettingLogic.GetPagedList(1, 100, SortExpression, Criteria, out totalRecords);
        }

        CustomGridView1.DataSource = dt;
        CustomGridView1.DataBind();
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        InitData();
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < CustomGridView1.Rows.Count;i++ )
        {
            TextBox txtCourseFee = ((TextBox)CustomGridView1.Rows[i].FindControl("txtCourseFee"));
            TextBox txtRemark = ((TextBox)CustomGridView1.Rows[i].FindControl("txtRemark"));
            Fee_CourseFeeSetting courseFeeSetting = CourseFeeSettingLogic.GetById(CustomGridView1.DataKeys[i].Value.ToGuid());

            courseFeeSetting.CourseFee = txtCourseFee.Text.ToDecimal();
            courseFeeSetting.Remark = txtRemark.Text;
            courseFeeSetting.ModifyTime = DateTime.Now;
            courseFeeSetting.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;

            CourseFeeSettingLogic.Save(courseFeeSetting);

        }
        ETMS.Utility.JsUtility.SuccessMessageBox("课酬标准保存成功！");
        InitData();
    }
}