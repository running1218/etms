using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Utility;

using ETMS.Controls;
using ETMS.Components.Poll.API.Entity;
using ETMS.Components.Poll.Implement.BLL;
public partial class Poll_ResourceQuery_UploadReport : System.Web.UI.Page
{
    private static Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
    #region 页面条件参数存放
    public int QueryPublishObjectID
    {
        get
        {
            return int.Parse(Request.QueryString["id"]);
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {

            ETMS.Utility.Service.FileUpload.FileUploadCard uploadInfo = this.fileUpload1.SaveUploadFiles();
            //如果上传报告，则更新
            if (uploadInfo.FileDetails.Count != 0)
            {
                Poll_QueryPublishObject entity = ResourceQueryLogic.GetById(this.QueryPublishObjectID);
                entity.FileName = uploadInfo.FileDetails[0].FileName;
                entity.FilePath = uploadInfo.FileDetails[0].BizUrl;
                entity.Modifier = ETMS.AppContext.UserContext.Current.RealName;
                entity.ModifiyTime = DateTime.Now;
                ResourceQueryLogic.Save(entity);
                ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("分析报告保存成功!");
            }
            else
            {
                ETMS.Utility.JsUtility.AlertMessageBox("请先上传分析报告！");
            }

        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }


    }
}