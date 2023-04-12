using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Components.StudyClass.Implement.BLL.StudyClass;
using ETMS.Components.StudyClass.API.Entity.StudyClass;

public partial class LearningManagement_GroupManager_SetLeader : ETMS.Controls.BasePage
{
    private static Sty_ClassSubgroupStudentLogic classSubgroupstudentLogic = new Sty_ClassSubgroupStudentLogic();
    private Sty_ClassSubgroupStudent classSubgroupStudent=new Sty_ClassSubgroupStudent();
    protected Guid SubgroupStudentID
    {
        get { return Request.QueryString["SubgroupStudentID"].ToGuid(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }
    }

    protected void Initial()
    {
        //修改队长
        classSubgroupStudent = classSubgroupstudentLogic.GetById(SubgroupStudentID);
        if (classSubgroupStudent.IsLeader)
        {
            this.chkIsLeader.Checked = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            classSubgroupStudent = classSubgroupstudentLogic.GetById(SubgroupStudentID);
            if (chkIsLeader.Checked)
            {
                classSubgroupStudent.IsLeader = true;
            }
            else
            {
                classSubgroupStudent.IsLeader = false;
            }
            classSubgroupstudentLogic.Save(classSubgroupStudent);
            
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