using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.ExOfflineHomework.API.Entity;
using ETMS.Components.ExOfflineHomework.Implement.BLL;

public partial class QuestionDB_ExOfflineHomework_ExerciseView :ETMS.Controls.BasePage
{

    public Res_e_OffLineJobLogic Logic = new Res_e_OffLineJobLogic();
    protected Guid JobID
    {
        get
        {
            if (ViewState["JobID"] == null)
            {
                string obj = Request.QueryString["id"].ToLower();
                ViewState["JobID"] = new Guid(UrlParamDecode(Request.QueryString["id"].ToLower()));
            }
            return (Guid)ViewState["JobID"];
        }
        set
        {
            ViewState["JobID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Res_e_OffLineJob entity = new Res_e_OffLineJob();
            entity = Logic.GetById(JobID);

            if (entity.JobName != null)
            {
                this.lblJobName.Text = entity.JobName;
            }

            if (entity.JobDescription != null)
            {
                this.lblJobDescription.Text = entity.JobDescription;
            }

            if (entity.JobFileName != null)
            {
                this.lblJobFileName.Text = entity.JobFileName;
            }

            switch (entity.IsUse)
            {
                case 1:
                    this.lblJobStatus.Text = "启用";
                    break;
                case 0:
                    this.lblJobStatus.Text = "停用";
                    break;
            }

            this.lblCreateTime.Text = entity.CreateTime.ToString();

            if (entity.TeacherID != null)
            {
                this.lblTeacherID.Text = entity.TeacherID;

            }
        }

    }
}