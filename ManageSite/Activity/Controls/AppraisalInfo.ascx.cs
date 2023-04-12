using ETMS.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Dictionary;
using System.Data;
using ETMS.Activity.Entity;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Activity.Implement.BLL;

public partial class Activity_Controls_AppraisalInfo : System.Web.UI.UserControl
{
    private static readonly AppraisalLogic logic = new AppraisalLogic();
    public OperationAction Action
    {
        get;
        set;
    }

    public Appraisal Entity
    {
        get; set;
    }

    public Guid AppraisalID { get { return Request.QueryString["AppraisalID"].ToGuid(); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProvince();
            BindRegions();
            BindGroup();

            if (Action == OperationAction.Edit)
                InitControlValue();
        }
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Action == OperationAction.Add)
            {
                InitialData();
                logic.Insert(Entity);
            }
            else
            {
                InitialUpdateData();
                logic.Update(Entity);
            }
            JsUtility.SuccessMessageBoxAndCloseWindowAndTriggerRefreshEvent("评比活动保存成功！");
        }
        catch (BusinessException bizEx)
        {
            JsUtility.AlertMessageBox(ExceptionUtility.GetTranslateExceptionMessage(bizEx));
        }
    }

    protected void BindProvince()
    {
        DataTable data = new ActivityDirectoryLogic().GetAreaList(1);
        ddlProvince.DataSource = data;
        ddlProvince.DataValueField = "AreaCode";
        ddlProvince.DataTextField = "AreaName";
        ddlProvince.DataBind();

        ddlProvince.Items.Insert(0, new ListItem("省份/直辖市", ""));
    }

    protected void BindRegions()
    {
        DataTable regions = new ActivityDirectoryLogic().GetRegionList();
        cblRegions.DataSource = regions;
        cblRegions.DataValueField = "ColumnCodeValue";
        cblRegions.DataTextField = "ColumnNameValue";
        cblRegions.DataBind();
    }

    protected void BindGroup()
    {
        DataTable grops = new ActivityDirectoryLogic().GetGroupList();
        cblGroup.DataSource = grops;
        cblGroup.DataValueField = "ColumnCodeValue";
        cblGroup.DataTextField = "ColumnNameValue";
        cblGroup.DataBind();        
    }

    private void BindCity(string parentCode)
    {
        DataTable source = new ActivityDirectoryLogic().GetAreaListByParent(parentCode);
        ddlCity.DataSource = source;
        ddlCity.DataValueField = "AreaCode";
        ddlCity.DataTextField = "AreaName";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, new ListItem("地区 / 市", ""));
    }

    private void InitControlValue()
    {
        Entity = logic.GetAppraisalByID(AppraisalID);
        txtAppraisalName.Text = Entity.AppraisalTitle;
        ddlType.SelectedValue = Entity.TypeID.ToString();
        ddlShape.SelectedValue = Entity.ShapeID.ToString();

        if (Entity.TypeID == 2)
        {
            trArea.Style.Add("display", "table-row");
            ddlProvince.SelectedValue = Entity.Province;
            BindCity(Entity.Province);
            ddlCity.SelectedValue = Entity.City;
            txtAddress.Text = Entity.Address;
        }

        imgActivity.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.MediaLogo, string.IsNullOrEmpty(Entity.ImageUrl) ? "default.jpg" : Entity.ImageUrl);
        txtStartTime.Text = Entity.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
        txtEndTime.Text = Entity.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
        txtActivityNum.Text = Entity.LimitNum.ToString();
        txtAbstract.Text = Entity.Abstract;
        ueDetail.Text = Entity.Details;
        ueRule.Text = Entity.ReviewRule;
        SetCheckItems(Entity.Region, cblRegions);
        SetCheckItems(Entity.Group, cblGroup);
    }

    private void InitialData()
    {
        Entity = new Appraisal();
        Entity.AppraisalID = Guid.NewGuid();
        Entity.AppraisalTitle = txtAppraisalName.Text.Trim();
        Entity.TypeID = ddlType.SelectedValue.ToInt();
        Entity.ShapeID = ddlShape.SelectedValue.ToInt();

        if (Entity.TypeID == 2)
        {
            Entity.Province = ddlProvince.SelectedValue;
            Entity.City = ddlCity.SelectedValue;
            Entity.Address = txtAddress.Text.Trim();
        }

        Entity.BeginTime = txtStartTime.Text.ToDateTime();
        Entity.EndTime = txtEndTime.Text.ToDateTime();

        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;
        Entity.ImageUrl = fileDefine == null ? Entity.ImageUrl : fileDefine.BizUrl;
        Entity.Region = GetCheckValues(cblRegions);
        Entity.Group = GetCheckValues(cblGroup);
        Entity.LimitNum = txtActivityNum.Text.ToInt();
        Entity.Abstract = txtAbstract.Text.Trim();
        Entity.Details = ueDetail.Text;
        Entity.Status = 0;
        Entity.ReviewRule = ueRule.Text;
        Entity.OrganizationID = UserContext.Current.OrganizationID;
        Entity.IsTop = false;
        Entity.CreateTime = Entity.ModifyTime = DateTime.Now;
        Entity.Creator = Entity.Modifior = UserContext.Current.UserID;
    }

    private void InitialUpdateData()
    {
        Entity = logic.GetAppraisalByID(AppraisalID);
        Entity.AppraisalTitle = txtAppraisalName.Text.Trim();
        Entity.TypeID = ddlType.SelectedValue.ToInt();
        Entity.ShapeID = ddlShape.SelectedValue.ToInt();

        if (Entity.TypeID == 2)
        {         
            Entity.Province = ddlProvince.SelectedValue;            
            Entity.City = ddlCity.SelectedValue;
            Entity.Address = txtAddress.Text.Trim();
        }
        else
        {
            Entity.Province = string.Empty;
            Entity.City = string.Empty;
            Entity.Address = string.Empty;
        }

        Entity.BeginTime = txtStartTime.Text.ToDateTime();
        Entity.EndTime = txtEndTime.Text.ToDateTime();

        List<FileUploadInfo> uploaders = this.uploader.FileUrl;
        FileUploadInfo fileDefine = uploaders.Count > 0 ? this.uploader.FileUrl[0] : null;
        Entity.ImageUrl = fileDefine == null ? Entity.ImageUrl : fileDefine.BizUrl;
        Entity.Region = GetCheckValues(cblRegions);
        Entity.Group = GetCheckValues(cblGroup);
        Entity.LimitNum = txtActivityNum.Text.ToInt();
        Entity.Abstract = txtAbstract.Text.Trim();
        Entity.Details = ueDetail.Text;
        Entity.ReviewRule = ueRule.Text;
        Entity.ModifyTime = DateTime.Now;
        Entity.Modifior = UserContext.Current.UserID;
    }

    private string GetCheckValues(CheckBoxList control)
    {
        List<string> list = new List<string>();

        for (int i=0; i < control.Items.Count; i ++)
        {
            if (control.Items[i].Selected)
            {
                list.Add(control.Items[i].Value);
            }
        }

        return JsonHelper.SerializeObject(list);
    }

    private void SetCheckItems(string text, CheckBoxList control)
    {
        List<string> list = JsonHelper.DeserializeObject<List<string>>(text);

        foreach (ListItem item in control.Items)
        {
            foreach (var value in list)
            {
                if (item.Value == value)
                {
                    item.Selected = true;
                }
            }
        }
    }
}