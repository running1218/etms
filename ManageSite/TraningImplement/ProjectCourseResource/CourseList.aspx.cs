using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL;
using System.Data;
using ETMS.WebApp;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Courseware.Implement;
using ETMS.WebApp.Manage;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Teacher;

public partial class TraningImplement_ProjectCourseResource_CourseList : BasePage
{
    #region 页面参数
    
    /// <summary>
    /// 项目ID 此ID用于子页面返回时用户选择的项目ID
    /// </summary>
    public string TrainingItemID
    {
        get
        {
            if (ViewState["TrainingItemID"] == null)
                ViewState["TrainingItemID"] = "";
            return ViewState["TrainingItemID"].ToString();
        }
        set { ViewState["TrainingItemID"] = value; }
    }
    /// <summary>
    /// 排序URL
    /// </summary>
    protected string SortUrl
    {
        get
        {
            return this.ActionHref(string.Format("../TraningProjectManager/SetsCourseSort.aspx?TrainingItemID={0}", ddl_Item.SelectedValue));
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        PageSet1.pageInit(this.rptCourseList, PageDataSource);
        PageSet1.PageSize = 5;
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(BasePage.getSafeRequest(this.Page, "TrainingItemID")))
            {
                TrainingItemID = BasePage.getSafeRequest(this.Page, "TrainingItemID");
            }
            bind();
            this.PageSet1.QueryChange();
        }
        btnSearch.Attributes.Add("onclick", string.Format("return CheckSelectData('{0}')", ddl_Item.ClientID));
        btnAdd.PostBackUrl = this.ActionHref(string.Format("../TraningProjectManager/SetsCourseAdd.aspx?TrainingItemID={0}", ddl_Item.SelectedValue));
    }
        
    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        //对本组织机构创建的 待审核、审核通过 并且启用的培训项目
        string Crieria = string.Format(" AND OrgID={0} AND ItemStatus in (10,20) AND IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID);
        int total = 0;
        ddl_Item.DataSource = itemLogic.GetPagedList(1, int.MaxValue - 1, " CreateTime DESC", Crieria, out total);
        ddl_Item.DataTextField = "ItemName";
        ddl_Item.DataValueField = "TrainingItemID";
        ddl_Item.DataBind();
        ddl_Item.SelectedValue = TrainingItemID;
    }

    /// <summary>
    /// 查询
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.PageSet1.QueryChange(); 
    }

    private System.Collections.IList PageDataSource(int pageIndex, int pageSize, out int totalRecordCount)
    {
        Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
        DataTable tab = ItemCourseLogic.GetItemCourseListByTrainingItemID(ddl_Item.SelectedValue.ToGuid(), pageIndex, pageSize, out totalRecordCount);

        if (tab.Rows.Count == 0)
        {
            ltlNull.Visible = true;
            ltlNull.Text = "没有任何记录！";
        }
        else
        {
            ltlNull.Visible = false;
        }
        PageDataSourceProvider pageDataSource = new PageDataSourceProvider(tab, pageIndex, pageSize);

        return pageDataSource.PageDataSource;
    }

    /// <summary>
    /// 
    /// </summary>
    protected void rptCourseList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image imgLogo = (Image)e.Item.FindControl("imgLogo");
            if (null != imgLogo)
            {
                imgLogo.ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(imgLogo.ImageUrl) ? "default.jpg" : imgLogo.ImageUrl);
            }
            HiddenField hfTrainingItemCourseID = (HiddenField)e.Item.FindControl("hfTrainingItemCourseID");
            HiddenField hfCourseID = (HiddenField)e.Item.FindControl("hfCourseID");

            if (hfTrainingItemCourseID != null && hfCourseID != null)
            {
                Guid TrainingItemCourseID = hfTrainingItemCourseID.Value.ToGuid();
                Guid CourseID = hfCourseID.Value.ToGuid();

                DataList dltResource = (DataList)e.Item.FindControl("dltJobList");
                CourseResourceInfo info = null;
                if (null != dltResource)
                {
                    info = new CourseResourceInfo(TrainingItemCourseID, CourseID);
                    dltResource.DataSource = info.CourseResourceList;
                    dltResource.DataBind();
                }
                LinkButton Lbtn_Edit = (LinkButton)e.Item.FindControl("Lbtn_Edit");
                LinkButton lbtnAnalysis = (LinkButton)e.Item.FindControl("lbnAnalysis");
                Lbtn_Edit.Attributes["onClick"] = "javascript:showWindow('编辑项目课程信息','" + this.ActionHref("../TraningProjectManager/SetsCourseEdit.aspx?TrainingItemCourseID=" + TrainingItemCourseID) + "',650,500);javascript:return false;";
                lbtnAnalysis.Attributes["onClick"] = "javascript:showWindow('课程实施进度','" + this.ActionHref("CourseAnalysis.aspx?TrainingItemCourseID=" + TrainingItemCourseID + "&CourseID=" + hfCourseID.Value) + "',800,160);javascript:return false;";
                //讲师管理
                LinkButton lbtn_SetTeacher = (LinkButton)e.Item.FindControl("lbtn_SetTeacher");
                lbtn_SetTeacher.PostBackUrl = this.ActionHref(string.Format("../CourseTeacherManager/SetTeacher.aspx?TrainingItemCourseID={0}", TrainingItemCourseID));
                lbtn_SetTeacher.Text = string.Format("讲师({0})",new Tr_ItemCourseTeacherLogic().GetTeacherTotal(TrainingItemCourseID).ToString());
            }
        }
    }
    protected void rptCourseList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Dels")
        {
            try
            {
                Guid[] selectedValues = new Guid[1];
                selectedValues[0] = e.CommandArgument.ToGuid();
                Tr_ItemCourseLogic ItemCourseLogic = new Tr_ItemCourseLogic();
                ItemCourseLogic.BatchRemoveItemCourseAndCourseware(selectedValues);
                ETMS.Utility.JsUtility.SuccessMessageBox("信息删除成功！");
                this.PageSet1.DataBind();
            }
            catch (ETMS.AppContext.BusinessException bizEx)
            {
                ETMS.Utility.JsUtility.FailedMessageBox(ETMS.Utility.ExceptionUtility.GetTranslateExceptionMessage(bizEx));
                return;
            }
        }
    }

    public class CourseResourceInfo
    {
        public Guid TrainingItemCourseID { get; set; }
        public Guid CourseID {get;set;}

        public CourseResourceInfo(Guid trainingItemCourseID, Guid courseID)
        {
            TrainingItemCourseID = trainingItemCourseID;
            CourseID = courseID;
        }

        public List<ItemCourseResourceDetail> CourseResourceList
        {
            get
            {
                List<ItemCourseResourceDetail> list = new List<ItemCourseResourceDetail>();

                foreach (ITrainingItemCourseResourcesFacade ItemCourseResourceFacade in ETMS.AppContext.ApplicationContext.Current.ComponentRepository.GetBizComponentsByGroupID<ITrainingItemCourseResourcesFacade>())
                {
                    list.Add(new ItemCourseResourceDetail()
                    {
                        ResourceName = string.Format("{0}:", ItemCourseResourceFacade.Name),
                        ResourceNum = ItemCourseResourceFacade.GetResourcesTotal(CourseID),
                        ItemResourceNum = ItemCourseResourceFacade.GetTrainingItemResourcesTotal(TrainingItemCourseID),
                        FunctionUrl =ETMS.Utility.HrefUtility.ActionHref(string.Format(ItemCourseResourceFacade.ManageAppHome, TrainingItemCourseID))
                    });
                }
                return list;
            }
        }
    }

}