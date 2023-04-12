using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Point.API.Entity;
using ETMS.Components.Point.Implement.BLL;
using System.Data;

public partial class Point_CourseRoleSetting : ETMS.Controls.BasePage
{
    private static Point_Student_CourseRoleLogic pointStudentCourseRoleLogic = new Point_Student_CourseRoleLogic();
    
    /// <summary>
    /// 课程属性编码
    /// </summary>
    public int CourseAttrID
    {
        get { return Request.QueryString["CourseAttrID"].ToInt(); }
    }
    /// <summary>
    /// 临时表
    /// </summary>
    public DataTable CourseRoleTable
    {
        set { ViewState["CourseRoleTable"] = value; }
        get 
        {
            if (ViewState["CourseRoleTable"] == null)
            {
                ViewState["CourseRoleTable"] = new DataTable();
            }
            return ViewState["CourseRoleTable"] as DataTable; 
        }
    }
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binding();
        }
        aBack.HRef = this.ActionHref(string.Format("CourseRoleList.aspx?CourseAttrID={0}", CourseAttrID));
        btnUpdate.Attributes.Add("onclick", "javascript:return pointsaveFunoption('" + pointOptionList.ClientID + "')");
        btnReturn.PostBackUrl = this.ActionHref(string.Format("{0}/Point/CourseRoleList.aspx?CourseAttrID={1}", WebUtility.AppPath, CourseAttrID));
    }
    protected void binding()
    {
        this.lblCourseAttrID.FieldIDValue = CourseAttrID.ToString();
        //数据库信息保存到列表
        CourseRoleTable = pointStudentCourseRoleLogic.GetStudentCoursePointRoleListByCourseAttrID(ETMS.AppContext.UserContext.Current.OrganizationID, CourseAttrID);   
    }
    
    /// <summary>
    ///  课时临界点（含临界点）
    /// </summary>
    /// <returns></returns>
    protected int MaxNum(int index)
    {       
        return CourseRoleTable.Rows[index]["MaxNum"].ToInt();
    }

    /// <summary>
    ///  积分
    /// </summary>
    /// <returns></returns>
    protected int GivePoints(int index)
    {
        return CourseRoleTable.Rows[index]["GivePoints"].ToInt();
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //返回结果strResult:1Φ2Ω3Φ4Ω
        if (string.IsNullOrEmpty(pointOptionList.Value))
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("对不起没有数据！");
            return;
        }
        string[] pointTemp = pointOptionList.Value.Split('Ω');//ΩΦ
        SortedList sortedList = new SortedList();
        SortedList sortedListTemp = new SortedList();
        for (int i = 0; i < pointTemp.Length; i++)
        {
            string[] pointItem = pointTemp[i].Split('Φ');
            if (pointTemp.Length != 0)
            {
                if (pointItem[0].ToString() == "")
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox("课时临界点（含临界点）不能为空！");
                    return;
                }
                if (pointItem[1].ToString() == "")
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox("积分不能为空！");
                    return;
                }
                if (pointItem[0].ToInt() == 0)
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox(string.Format("课时临界点（含临界点）{0}超出范围！", pointItem[0].ToString()));
                    return;
                }
                //if (pointItem[1].ToInt() == 0)
                //{
                //    ETMS.WebApp.Manage.Extention.AlertMessageBox(string.Format("积分{0}超出范围！", pointItem[1].ToString()));
                //    return;
                //}
                int key = pointItem[0].ToInt();
                int value = pointItem[1].ToInt();
               
                if (sortedList[key] == null)
                {
                    sortedList.Add(key, value);
                }
                else
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox(string.Format("课时临界点（含临界点）{0}已经存在！",key.ToString()));
                    return;
                }
                if (sortedListTemp[value] == null)
                {
                    sortedListTemp.Add(value, key);
                }
                else
                {
                    ETMS.WebApp.Manage.Extention.AlertMessageBox(string.Format("积分{0}已经存在！",value.ToString()));
                    return;
                }
            }
        }
        //调用业务保存
        if (sortedList.Count > 0)
        {
            //保存
            try
            {
                int orgId = ETMS.AppContext.UserContext.Current.OrganizationID;
                int userID = ETMS.AppContext.UserContext.Current.UserID;
                string userName = ETMS.AppContext.UserContext.Current.RealName;
                pointStudentCourseRoleLogic.SaveStudentCoursePointRoleToCourseAttrID(orgId, CourseAttrID, sortedList, userID, userName);
                binding();
                ETMS.WebApp.Manage.Extention.SuccessMessageBox("提示", "保存成功！", "function(){window.location = '" + this.ActionHref(string.Format("CourseRoleList.aspx?CourseAttrID={0}",CourseAttrID)) + "'}");
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
        else
        {
            ETMS.WebApp.Manage.Extention.AlertMessageBox("对不起没有数据！");
        }
       
    }
}