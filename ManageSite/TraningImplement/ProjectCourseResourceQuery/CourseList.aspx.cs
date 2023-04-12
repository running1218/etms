using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Controls;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.Implement.BLL.Bulletin;

public partial class TraningImplement_ProjectCourseResourceQuery_CourseList : System.Web.UI.Page
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
    }

    /// <summary>
    /// 邦定项目信息
    /// </summary>
    private void bind()
    {
        Tr_ItemLogic itemLogic = new Tr_ItemLogic();
        //对本组织机构创建的 待审核、审核通过 并且启用的培训项目
        string Crieria = string.Format(" AND OrgID={0} AND ItemStatus not in (40) AND IsUse=1", ETMS.AppContext.UserContext.Current.OrganizationID);
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
        this.PageSet1.QueryChange(); //upList.Update();
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
            }
        }
    }

    public class CourseResourceInfo
    {
        public Guid TrainingItemCourseID { get; set; }
        public Guid CourseID { get; set; }

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
                        FunctionUrl = ETMS.Utility.HrefUtility.ActionHref(string.Format(ItemCourseResourceFacade.ListAppHome, TrainingItemCourseID))
                    });
                }
                ////离线作业 与 导学资料  稍后加到上面
                //list.Add(new ItemCourseResourceDetail()
                //{
                //    ResourceName = string.Format("{0}:", "离线作业"),
                //    ItemResourceNum =new Tr_ItemCourseLogic().GetOffLineJobCountByTrainingItemCourseID(TrainingItemCourseID),
                //    FunctionUrl =ETMS.Utility.HrefUtility.ActionHref(string.Format("OffLineHomeWork.aspx?TrainingItemCourseID={0}", TrainingItemCourseID))
                //});
                //list.Add(new ItemCourseResourceDetail()
                //{
                //    ResourceName = string.Format("{0}:", "导学资料"),
                //    ItemResourceNum = new Inf_BulletinLogic().GetMontorDataNumbyItemCourse(TrainingItemCourseID),
                //    FunctionUrl = ETMS.Utility.HrefUtility.ActionHref(string.Format("ArticleList.aspx?TrainingItemCourseID={0}", TrainingItemCourseID))
                //});
                return list;
            }
        }
    }
}