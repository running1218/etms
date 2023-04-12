using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.QS.Implement.BLL;
using ETMS.Components.QS.API.Entity;

public partial class QS_UploadReport : System.Web.UI.Page
{

    #region 页面条件参数存放
    public string QueryID
    {
        get
        {
            return Request.QueryString["id"];
        }
    }

    QS_QueryLogic QueryBiz = new QS_QueryLogic();
    private QS_Query queryEntity = new QS_Query();
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

                queryEntity = QueryBiz.GetById(new Guid(QueryID));
                queryEntity.FileName = uploadInfo.FileDetails[0].FileName;
                queryEntity.FilePath = uploadInfo.FileDetails[0].BizUrl;
                queryEntity.ModifyUser = ETMS.AppContext.UserContext.Current.RealName;
                queryEntity.ModifyTime = DateTime.Now;
                QueryBiz.Save(queryEntity,ETMS.AppContext.OperationAction.Edit);
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