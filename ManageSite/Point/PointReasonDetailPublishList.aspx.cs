using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Utility;
using ETMS.Controls;
using ETMS.Components.Point.Implement.BLL;
using ETMS.AppContext;

public partial class Point_ItemPointReasonDetailList : ETMS.Controls.BasePage
{
    private static Point_Student_PointReasonDetailLogic pointreasonDetailLogic = new Point_Student_PointReasonDetailLogic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialControls();
            //列表绑定
            binding();
        }
    }
    private void InitialControls()
    {
        //数据项目绑定
        this.ddl_ItemName.DataSource = pointreasonDetailLogic.GetWaitPublishPointItemList();
        this.ddl_ItemName.DataTextField = "ItemName";
        this.ddl_ItemName.DataValueField = "TrainingItemID";
        this.ddl_ItemName.DataBind();
        this.ddl_ItemName.Items.Insert(0, new ListItem("全部", string.Empty));
    }

    private void binding()
    {
        int total=0;
        Guid trainItemID;
        if(string.IsNullOrEmpty(this.ddl_ItemName.SelectedValue))
        {
            trainItemID=Guid.Empty;
        }else
        {
            trainItemID=this.ddl_ItemName.SelectedValue.ToGuid();
        }

        this.CustomGridView1.DataSource = pointreasonDetailLogic.GetWaitPublishPointItemList(trainItemID, 1, int.MaxValue - 1, out total);
        this.CustomGridView1.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        binding();
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        try
        {
            Guid[] trainitemIDs = CustomGridView.GetSelectedValues<Guid>(this.CustomGridView1);
            if (trainitemIDs.Length > 0)
            {
                pointreasonDetailLogic.StudentStudyProcessPublish(trainitemIDs);
                JsUtility.SuccessMessageBox("操作提示", "积分发布成功！", "function(){window.location=window.location;}");
            }
            else
            {
                JsUtility.AlertMessageBox("请选择要发布的项目！");
            }
        }
        catch (BusinessException bizEx)
        {
            ETMS.WebApp.Manage.Extention.ShowScriptManagerMessage(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
            return;
        }
    }

}