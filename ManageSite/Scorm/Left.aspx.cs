using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ETMS.Components.Scrom.Implement.BLL;
using ETMS.Components.Scrom.API.Entity;
using ETMS.Controls;
using ETMS.Utility;
using ETMS.Components.Courseware.API.Entity;
using ETMS.Components.Courseware.Implement.BLL;

public partial class Scorm_Left : System.Web.UI.Page
{

    #region 页面参数
    public Guid CourseID
    {
        get
        {
            return (Guid)(ViewState["CourseID"]);
        }
        set
        {
            ViewState["CourseID"] = value;
        }
    }
    public Guid CourseWareID
    {
        get
        {
            return (Guid)(ViewState["CourseWareID"]);
        }
        set
        {
            ViewState["CourseWareID"] = value;
        }
    }
    /// <summary>
    /// 项目课程资源ID
    /// </summary>
    private Guid ItemCourseResID
    {
        get
        {
            return (Guid)(ViewState["ItemCourseResID"]);
        }
        set
        {
            ViewState["ItemCourseResID"] = value;
        }
    }

    #endregion

    public string strLogUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {

            if (!IsPostBack)
            {
                if (Request.QueryString["ItemCourseResID"] != null)
                {
                    ItemCourseResID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "ItemCourseResID")));
                }
                if (Request.QueryString["CourseWareID"] != null)
                {
                    CourseWareID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseWareID")));

                    bind();
                }
                if (Request.QueryString["CourseID"] != null)
                {
                    CourseID = new Guid(BasePage.UrlParamDecode(BasePage.getSafeRequest(this, "CourseID")));
                }
                
            }

    }

    //绑定信息
    private void bind()
    {
        //清空
        tvChapterList.Nodes.Clear();
        ItemResourceLogic itemResourceLogic = new ItemResourceLogic();

        DataTable tab = itemResourceLogic.GetLessonTree(CourseWareID,ItemCourseResID, ETMS.AppContext.UserContext.Current.UserID);


        //添加根级节点
        TreeNode root = new TreeNode();
        root.Value = "1";

        Res_Courseware eCourseware = new Res_Courseware();
        Res_CoursewareLogic coursewareLogic = new Res_CoursewareLogic();
        eCourseware = coursewareLogic.GetById(CourseWareID);

        root.Text = string.Format("<span class=\"span_overflow\">{0}</span>", eCourseware.CoursewareName);
        root.SelectAction = TreeNodeSelectAction.Expand;
        root.Expanded = true;


        //递归添加子级
        addChildNodes(tab, root, "is null");
        tvChapterList.Nodes.Add(root);

    }

    //递归添加子级
    private void addChildNodes(DataTable tab, TreeNode node, string ParentItemID)
    {
        string resourceLocation = "";
        //循环添加节点
        foreach (DataRow item in tab.Select("ParentItemID " + ParentItemID, "SequenceNo"))
        {
            TreeNode cNode = new TreeNode();
            //链接路径
            resourceLocation = Convert.IsDBNull(item["ResourceHref"]) ? "" : this.ActionHref(string.Format(@"~/ScormFiles/{0}/{1}", CourseWareID, item["ResourceHref"]));
            //获取上级 并连接返回(导航栏)
            string itemTitle = getNodeTitle(tab, Convert.ToString(item["ParentItemID"]), Convert.ToString(item["ItemTitle"]));

            //cNode.Text = string.Format("<span id='{0}' class=\"span_overflow\" title=\"{1}\" onclick=\"window.open('{2}','rightFrame')\">{3}</span>", Convert.ToString(item["ResourceID"]), itemTitle, resourceLocation, Convert.ToString(item["ItemTitle"]));

            cNode.Text = "<span id='" + Convert.ToString(item["ResourceID"]) + "' class=\"span_overflow\" title=\"" + itemTitle + "\" onclick='SendResourceId(\"" + Convert.ToString(item["ResourceID"]) + "\",\"" + resourceLocation + "\",\"" + itemTitle + "\",\"" + Convert.ToString(item["ItemID"]) + "\")'>" + Convert.ToString(item["ItemTitle"]) + "</span>";


            cNode.Value = Convert.ToString(item["ItemID"]);
            cNode.SelectAction = TreeNodeSelectAction.Expand;
            cNode.Expanded = false;
            //递归调用
            addChildNodes(tab, cNode, "='" + cNode.Value + "'");
            node.ChildNodes.Add(cNode);
        }
    }

    //获取上级 并连接返回(导航栏)
    private string getNodeTitle(DataTable tab, string ItemID, string ItemTitle)
    {
        foreach (DataRow row in tab.Select("ItemID='" + ItemID + "'"))
        {
            ItemTitle = Convert.ToString(row["ItemTitle"]) + " > " + ItemTitle;
            //如果上级不为空 继续递归添加
            if (Convert.ToString(row["ParentItemID"]) != "")
            {
                ItemTitle = getNodeTitle(tab, Convert.ToString(row["ParentItemID"]), ItemTitle);
            }
        }
        return ItemTitle.ReplaceQuotAndApos();
    }
}
