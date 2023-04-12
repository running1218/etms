using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours.Student;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Hours;

public partial class TraningImplement_AskApplyAudit_AskApplyOption : ETMS.Controls.BasePage
{
    private static Tr_ItemCourseHoursStudentLogic itemCourseHoursStudentLogic = new Tr_ItemCourseHoursStudentLogic();
    private static Tr_ItemCourseHoursLogic itemCourseHoursLogic = new Tr_ItemCourseHoursLogic();
    /// <summary>
    /// 20:审核通过 40:审核不通过
    /// </summary>
    public int AuditStatus
    {
        get { return Request.QueryString["AuditStatus"].ToInt(); }
    }
    /// <summary>
    /// 选中的数量
    /// </summary>
    public int SelectNum
    {
        get { return Request.QueryString["SelectNum"].ToInt(); }
    }
    /// <summary>
    /// 培训项目课程课时学员编码
    /// </summary>
    public string ItemCourseHoursStudentID
    {
        get { return Request.QueryString["itemCourseHoursStudentIDStr"].ToString(); }
    }
    /// <summary>
    /// 培训项目课程课时学员编码
    /// </summary>
    private ArrayList ItemCourseHoursStudentIDs = new ArrayList();
    
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SelectNum == 0)
            {
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("请选择学员");
            }
        }
    }
    private void GetIDs()
    {
        ItemCourseHoursStudentIDs.Clear();
        string[] temp = ItemCourseHoursStudentID.Split('_');
        for (int i = 0; i < temp.Length; i++)
        {
            if (!string.IsNullOrEmpty(temp[i]))
            {
                ItemCourseHoursStudentIDs.Add(temp[i]);
            }
        }

    }

    private Guid[] GetSelectedGuid()
    {
        string[] temp = ItemCourseHoursStudentID.Split('_');
        Hashtable hashSelectGuid = new Hashtable();
        for (int i = 0; i < temp.Length; i++)
        {
            if (!string.IsNullOrEmpty(temp[i]))
            {
                Guid key = temp[i].ToGuid();
                if (hashSelectGuid[key] == null)
                    hashSelectGuid.Add(key, key);
            }
        }
        Guid[] selectGuidArray = new Guid[hashSelectGuid.Count];
        int count = 0;
        System.Collections.IDictionaryEnumerator myEnumerator = hashSelectGuid.GetEnumerator();
        while (myEnumerator.MoveNext())
        {
            selectGuidArray[count] = myEnumerator.Key.ToGuid();
            count++;
        }
        return selectGuidArray;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SelectNum == 0)
        {
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("请选择学员");
            return;
        }
        try
        {
            itemCourseHoursStudentLogic.ItemCourseHoursStudent_LeaveAuditBatch(GetSelectedGuid(), AuditStatus, ETMS.AppContext.UserContext.Current.RealName, this.txtOption.Text);

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("保存成功！");

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
        catch (Exception ex)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
        }
    }

}