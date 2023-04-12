using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using System.Collections;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
using System.Data;
using ETMS.Components.Basic.Implement.BLL.Security;

public partial class LearningManagement_ClassManager_SetsClassCommittee : ETMS.Controls.BasePage
{
    private static Sty_ClassStudentLogic classStudentLogic = new Sty_ClassStudentLogic();
    public static Sty_ClassLogic classLogic = new Sty_ClassLogic();
    private static Sty_ClassMonitorLogic classMonitorLogic = new Sty_ClassMonitorLogic();
    private static Sty_ClassMonitor classMoinitor = new Sty_ClassMonitor();
    /// <summary>
    /// 班级ID
    /// </summary>
    private Guid ClassID
    {
        get { return Request.QueryString["ClassID"].ToGuid(); }
    }
    /// <summary>
    /// 学员编码
    /// </summary>
    private Guid ClassStudentID
    {
        get { return Request.QueryString["ClassStudentID"].ToGuid(); }
    }

    /// <summary>
    /// 学员ID
    /// </summary>
    private int UserID
    {
        get { return Request.QueryString["UserID"].ToInt(); }
    }

    private ArrayList ClassMonitorID
    {
        set { ViewState["ClassMonitorID"] = value; }
        get 
        {
            if (ViewState["ClassMonitorID"] == null)
            {
                ViewState["ClassMonitorID"] = new ArrayList();
            }
            return (ArrayList)ViewState["ClassMonitorID"];
        }
    }
   

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            InitialControlers();
        }
    }

    private void InitialControlers()
    {
        this.lblClassName.Text = classLogic.GetById(ClassID).ClassName;

        this.lblRealName.Text =new UserLogic().GetUserByID(UserID).RealName;

        this.chkLeadStyle.DataSource = SysDict.Dic_Sys_StudentType();
        this.chkLeadStyle.DataTextField = "StudentTypeName";
        this.chkLeadStyle.DataValueField = "StudentTypeID";
        this.chkLeadStyle.DataBind();
           
        //修改班委
        int total=0;
        string whereStr=string.Format(" and ClassStudentID='{0}'",ClassStudentID);
        DataTable monitorDt = classMonitorLogic.GetPagedList(1, int.MaxValue - 1, string.Empty, whereStr, out total);

        foreach(DataRow row in monitorDt.Rows)
        {
            foreach (ListItem item in chkLeadStyle.Items)
            {
                if (item.Value == row["StudentTypeID"].ToString())
                {
                    item.Selected = true;
                }
            }
            ClassMonitorID.Add(row["ClassMonitorID"].ToString());
        }
        
        //修改版主
        Sty_ClassStudent classStudent= classStudentLogic.GetById(ClassStudentID);
        if (classStudent.IsBamboo)
        {
            this.chkIsBanboo.Checked = true;
        }
    }

    private void InititalEntity()
    {
        classMoinitor.ClassStudentID = ClassStudentID;

        //先删除班委
        for (int j = 0; j < this.ClassMonitorID.Count; j++)
        {
            classMonitorLogic.Remove(ClassMonitorID[j].ToGuid());
        }
        //再保存班委
        for(int i=0;i<this.chkLeadStyle.Items.Count;i++)
        {
            if (chkLeadStyle.Items[i].Selected && chkLeadStyle.Items[i].Value!="0")
            {
                classMoinitor.ClassMonitorID = Guid.NewGuid();
                classMoinitor.StudentTypeID = chkLeadStyle.Items[i].Value.ToInt();
                classMonitorLogic.Add(classMoinitor);
            }
        }
        //设置版主
        if (this.chkIsBanboo.Checked)
        {
            Sty_ClassStudent classStudent = classStudentLogic.GetById(ClassStudentID);
            classStudent.IsBamboo = true;
            classStudentLogic.Save(classStudent);
        }
        else//取消版主
        {
            Sty_ClassStudent classStudent = classStudentLogic.GetById(ClassStudentID);
            classStudent.IsBamboo = false;
            classStudentLogic.Save(classStudent);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            InititalEntity();
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }
    }
}