using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.Implement.BLL.Import;
using ETMS.Components.Basic.API.Entity.Import;
using ETMS.AppContext;
using ETMS.Utility;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Components.Basic.API.Entity.Course;
using System.Data;
using ETMS.Components.Exam.API.Entity.ImportQuestion;
using System.Text;

public partial class QuestionDB_ImportStudent : System.Web.UI.Page
{

    private static readonly Res_CourseLogic courseLogic = new Res_CourseLogic();

    /// <summary>
    /// 试题类型 1 单选题 2 多选题 3 判断题 4 填空题 5 简答题
    /// </summary>
    public QuestionType QuestionType;

    /// <summary>
    /// 课程ID
    /// </summary>
    public Guid CourseID
    {
        get
        {
            if (ViewState["CourseID"] == null)
            {
                ViewState["CourseID"] = Request.Params["CourseID"].ToGuid();
            }
            return (Guid)ViewState["CourseID"];
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }

    public Guid QuestionBankID
    {
        get
        {
            if (ViewState["QuestionBankID"] == null)
            {
                ViewState["QuestionBankID"] = Request.Params["QuestionBankID"].ToGuid();
            }
            return (Guid)ViewState["QuestionBankID"];
        }
        set
        {
            ViewState["QuestionBankID"] = value;
        }
    }

    Dictionary<QuestionType, string> ds = new Dictionary<QuestionType, string>();
    protected string Name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Enum.TryParse<QuestionType>(Request.Params["QuestionType"], out QuestionType);// (QuestionType);
        if (!Page.IsPostBack)
        {
            Res_Course RC = courseLogic.GetById(CourseID);
            lblItemName.Text = RC.CourseName;
            lblItemCode.Text = RC.CourseCode;
            lblRemark.Text = RC.CourseIntroduction;
        }
        ds.Add(QuestionType.SingleChoice, "单选题.xls");
        ds.Add(QuestionType.MultipleChoice, "多选题.xls");
        ds.Add(QuestionType.Judgement, "判断题.xls");
        ds.TryGetValue(QuestionType, out Name);
    }
    /// <summary>
    /// 导入信息
    /// </summary>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string msg = "";
        //FileUploadCard entity = this.fileUpload1.SaveUploadFiles();
        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;

        if (uploaders.Count == 0)
        {
            JsUtility.AlertMessageBox("请上传文件！");
            return;
        }

        //1、提取上传文件信息
        //UploadFileDefine fileDefine = entity.FileDetails[0];
        //文件物理路径
        string xlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("ImportCourseQuestion", fileDefine.BizUrl);

        //2、新增数据导入任务 
        Import_TaskLogic taskLogic = new Import_TaskLogic();
        Import_Task task = new Import_Task()
        {
            OrganizationID = UserContext.Current.OrganizationID,//任务所属机构
            ImportTypeID = (int)ImportType.CourseQuestion,//导入类型
            TaskName = string.Format("需求调查范围学员信息导入({0})", DateTime.Now.ToString("yyyyMMddHHmmss")),
            FilePath = fileDefine.BizUrl,
            FilleName = fileDefine.FileName,
            CreateTime = DateTime.Now,
            CreatorID = UserContext.Current.UserID,
        };
        taskLogic.Save(task);


        Import_QuestionLogic importQuestion = new Import_QuestionLogic(QuestionType, fileDefine.FileName, xlsFilePath, task, QuestionBankID);
        int errorNum = 0;
        ETMS.Components.Basic.Implement.BLL.Import.IQuestion iq = null;

        switch (QuestionType)
        {
            case QuestionType.SingleChoice:
                iq = new SingleQuestion(importQuestion, QuestionBankID);
                break;
            case QuestionType.MultipleChoice:
                iq = new MuiltpleQuestion(importQuestion, QuestionBankID);
                break;
            case QuestionType.Judgement:
                iq = new JudgeQuestion(importQuestion, QuestionBankID);
                break;
        }
        if (!iq.GetOrpateValuate(out msg))
        {
            JsUtility.FailedMessageBox("提示", msg);
            return;
        }
        List<QuestionBasic> listQS = iq.GetQuestionList(iq.GetQbList, out errorNum);
        if (errorNum > 0)
        {

            StringBuilder sb = new StringBuilder();
            foreach (QuestionBasic qb in listQS)
            {

                sb.AppendFormat("<tr><td rowspan='{3}'>{0}</td><td rowspan='{3}'>{1}</td><td rowspan='{3}'>{2}</td>", qb.QuestionID, qb.QuestionTitle, getQSDifficult(qb.Difficult), qb.Qreplenish.Count);
                sb.AppendFormat("<td>{0}</td><td>{1}</td><td>{2}</td>", qb.Qreplenish[0].AnswerContent, qb.Qreplenish[0].Option, qb.Qreplenish[0].OptionContent);
                sb.AppendFormat("<td rowspan='{2}'>{0}</td><td rowspan='{2}'>{1}</td></tr>", qb.State, qb.Msg, qb.Qreplenish.Count);
                for (int i = 1; i < qb.Qreplenish.Count; i++)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", getAwsuer(QuestionType, qb.Qreplenish[i].Answer), qb.Qreplenish[i].Option, qb.Qreplenish[i].OptionContent);
                }

            }
            task.Remark = sb.ToString();
            task.Status = 1;
            taskLogic.Save(task);
            this.lblRemark.Text = string.Format("共{2}条数据，<span style='color:Red'>'{1}'</span>条数据导入失败。<a href='{0}' target='_black'>查看详情</a>", this.ActionHref(string.Format("DownLoadFailed.aspx?Guid={0}", task.TaskID)), errorNum, listQS.Count);
            JsUtility.FailedMessageBox("提示", "导入试题失败！");
            return;
        }
        else
        {
            JsUtility.SuccessMessageBoxAndCloseWindow("导入试题成功！", "function(){window.parent.location.href=window.parent.location.href;}");
            return;
        }
    }
    /// <summary>
    /// 导出学员信息
    /// </summary>
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        ////copy template.xls到temp文件夹
        //string xlsFileName = string.Format("'{0}'试题导入.xlsx", lblItemName.Text);
        //string fullXlsFilePath = StaticResourceUtility.GetFullRootPathByFileType("Temp", xlsFileName);
        //string dirPath = System.IO.Path.GetDirectoryName(fullXlsFilePath);
        //if (!System.IO.Directory.Exists(dirPath))
        //{
        //    System.IO.Directory.CreateDirectory(dirPath);
        //}

        ////复制excel
        //System.IO.File.Copy(Server.MapPath("~/QuestionDB/Template/" + name), fullXlsFilePath, true);

        ////Excel导出
        //FileDownLoadUtility.ExportFile(fullXlsFilePath);
    }

    protected void CloseWindow(object sender, EventArgs e)
    {
        JsUtility.CloseWindow("function(){window.parent.location.href=window.parent.location.href;}");
        return;
    }


    private string getQSDifficult(short difficult)
    {
        string i = "";
        switch (difficult)
        {
            case 1:
                i = "易";
                break;
            case 3:
                i = "难";
                break;
            default:
                i = "中";
                break;
        }
        return i;
    }

    private string getAwsuer(QuestionType qt, bool awsuer)
    {

        string _awser = "";
        if (qt == QuestionType.Judgement)
        {
            switch (awsuer)
            {
                case true:
                    {
                        _awser = "是";
                        break;
                    }
                default:
                    _awser = "否";
                    break;
            }
        }
        else
        {
            switch (awsuer)
            {
                case true:
                    {
                        _awser = "√";
                        break;
                    }
                default:
                    _awser = "";
                    break;

            }
        }
        return _awser;
    }

}