using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Controls;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_QuestionManage_QuestionHandler : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //根据题型转到特定页面
        string queryID = Request.QueryString["QueryID"];
        string columnID = Request.QueryString["columnID"];
        string questionID = Request.QueryString["questionID"];
        string questionType = Request.QueryString["QuestionType"];
        string action = Request.QueryString["action"];

        string redirectUrl = "";
        if (questionType == "1" || questionType == "2")//单选，//多选
        {
            if (action == "add")
            {
                redirectUrl = "QuSelectionAdd.aspx";
            }
            else if (action == "edit")
            {
                redirectUrl = "QuSelectionEdit.aspx";
            }
            else//view
            {
                redirectUrl = "QuSelectionView.aspx";
            }
        }

        if (questionType == "3")//矩阵
        {

        }
        if (questionType == "4")//简答
        {
            if (action == "add")
            {
                redirectUrl = "QuFillBlanksAdd.aspx";
            }
            else if (action == "edit")
            {
                redirectUrl = "QuFillBlanksEdit.aspx";
            }
            else//view
            {
                redirectUrl = "QuFillBlanksView.aspx";
            }
        }
        if (action == "add")
        {
            redirectUrl += string.Format("?QueryID={0}&QuestionType={1}&ColumnID={2}", queryID, questionType, columnID);
        }
        else
        {
            redirectUrl += string.Format("?QuestionID={0}", questionID);
        }
        Response.Redirect(this.ActionHref(redirectUrl));
    }
}