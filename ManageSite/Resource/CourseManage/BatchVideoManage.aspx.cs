using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Resource_CourseManage_BatchVideoManage : ETMS.Controls.BasePage
{
    private static readonly Res_ContentLogic resContentLogic = new Res_ContentLogic();
    /// <summary>
    /// 资源对象集合
    /// </summary>
    public ResContentMore[] Sources
    {
        get
        {
            return (ResContentMore[])ViewState["Source"];
        }
        set
        {
            ViewState["Source"] = value;
        }
    }

    /// <summary>
    /// 课件ID
    /// </summary>
    public Guid CoursewareID
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CoursewareID = getSafeRequest(this, "CoursewareID").ToGuid();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<FileUploadInfo> uploaders = this.uploader1.FileUrl;
            if (uploaders.Count < 1)
            {
                JsUtility.AlertMessageBox("请上传文件, 或等待上传完成100%！");
                return;
            }

            //保存数据
            InitialEntity();
            resContentLogic.BatchVideoSave(Sources);

            ETMS.Utility.JsUtility.SuccessMessageBoxAndCloseWindow("保存成功！", "function(){var url=window.parent.location.href;if(url.indexOf('add')<0){url=url+'&oper=add';}window.parent.location.href=url;}");
        }
        catch (ETMS.AppContext.BusinessException bizEx)
        {
            ETMS.Utility.JsUtility.AlertMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

    private void InitialEntity()
    {
        List<FileUploadInfo> uploaders = this.uploader1.FileUrl;
        if (uploaders.Count > 0) {
            Sources = new ResContentMore[uploaders.Count];
            int count = resContentLogic.GetContentCount(CoursewareID);
            for (int i = 0; i < uploaders.Count; i++)
            {
                count++;
                ResContentMore Source = new ResContentMore()
                {
                    ContentID = Guid.NewGuid(),
                    CoursewareID = CoursewareID,
                    Type = 1,
                    Sort = count,
                    ModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };
                FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader1.FileUrl[i] : null;
                Source.DataInfo = fileDefine == null ? Source.DataInfo : fileDefine.BizUrl;
                Source.Name = fileDefine == null ? "" : fileDefine.FileOldName.Substring(0, fileDefine.FileOldName.LastIndexOf("."));
                Source.Status = 1;//radStatus.SelectedValue.ToInt();
                Source.TeacherName = "";//txtTeacherName.Text.Trim();
                Source.IsOpen = false;//radIsOpen.SelectedValue.ToInt() == 1 ? true : false;
                Sources[i] = Source;
            }
        }
    }
}