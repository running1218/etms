using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Point.API.Entity;
using ETMS.Components.Point.Implement.BLL;

public partial class Point_PointReasonTypeInfo : ETMS.Controls.BasePage
{
    private static Dic_PointReasonTypeLogic pointReasonTypeLogic = new Dic_PointReasonTypeLogic();
    public int PointReasonTypeID
    {
        get 
        {
            if (Request.QueryString["PointReasonTypeID"] == null)
            {
                return -1;
            }
            return Request.QueryString["PointReasonTypeID"].ToInt(); 
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }
    }
    private void Initial()
    {
        if (PointReasonTypeID != -1)//修改
        {
            Dic_PointReasonType pointReasonType = pointReasonTypeLogic.GetById(PointReasonTypeID);
            this.txtPointReasonTypeName.Text = pointReasonType.PointReasonTypeName;            
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (PointReasonTypeID == -1)//新增
            {
                Dic_PointReasonType pointReasonType = new Dic_PointReasonType();
                pointReasonType.OrgID = ETMS.AppContext.UserContext.Current.OrganizationID;
                pointReasonType.PointReasonTypeName = this.txtPointReasonTypeName.Text.Trim();
                pointReasonType.IsUse = 1;
                pointReasonTypeLogic.Save(pointReasonType);
            }
            else //修改
            {
                Dic_PointReasonType pointReasonType = pointReasonTypeLogic.GetById(PointReasonTypeID);
                pointReasonType.PointReasonTypeName= this.txtPointReasonTypeName.Text;
                pointReasonTypeLogic.Save(pointReasonType);
            }
            JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerSearchEvent("保存成功");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }
}