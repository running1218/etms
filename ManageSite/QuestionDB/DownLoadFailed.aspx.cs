using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Components.Basic.API.Entity.Import;

public partial class QuestionDB_DownLoadFailed : System.Web.UI.Page
{
    public string StrContent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string taskid = Request.Params["guid"];
        Import_TaskLogic taskLogic = new Import_TaskLogic();
        Import_Task task = new Import_Task();
        task = taskLogic.GetById(int.Parse(taskid));
        StrContent = task.Remark;
    }
}