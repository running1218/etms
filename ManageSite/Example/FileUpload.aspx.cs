using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility.Service.FileUpload;
public partial class Example_FileUpload : ETMS.Controls.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        FileUploadCard entity = this.fileUpload1.SaveUploadFiles();
        if (entity.Files.Count > 0)
        {
            //提取上传文件信息
            UploadFileDefine fileDefine = entity.FileDetails[0];
            string fileInfo = string.Format("需要业务保存路径={0}<br/>附件原始文件名={1}<br/>附件大小={2}<br/>附件类型={3}<br/>附件下载路径={4}",
                fileDefine.BizUrl, fileDefine.FileName, fileDefine.FileSize, fileDefine.FileType, fileDefine.FullUrl);
            Response.Write(fileInfo);

            //前端如何绑定已有业务的完整URL路径
            string bizUrl = fileDefine.BizUrl;//请从数据库读取，此次为demo
            Response.Write("显示完整=");
            //拼接完整路径
            string fullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("UserIcon", bizUrl);
            Image1.ImageUrl = fullUrl;//设置图片路径
            Response.Write(fullUrl);
        }
    }
}