using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ETMS.Utility.Service.FileUpload;
public partial class Example_DocConvert : System.Web.UI.Page
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

            //文档转换
           

        }
    }
}