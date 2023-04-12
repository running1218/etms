using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for Notice
    /// </summary>
    public class Notice : IHttpHandler
    {

        private HttpContext currentContext = null;
        public void ProcessRequest(HttpContext context)
        {
            currentContext = context;
            string method = currentContext.Request["Method"];
            if (string.IsNullOrEmpty(method))
            {
                ReturnResponseContent(JsonHelper.GetParametersInValidJson());
            }
            switch (method.ToLower())
            {
                case "announcementlist":
                    ReturnResponseContent(GetAnnouncementList());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }
        //获取日常公告列表
        private string GetAnnouncementList()
        {
            int PageIndex = string.IsNullOrEmpty(currentContext.Request["PageIndex"].ToString()) ? 1 : currentContext.Request["PageIndex"].ToString().ToInt();
            int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"].ToString()) ? 2 : currentContext.Request["PageSize"].ToString().ToInt();
            Inf_BulletinLogic Logic = new Inf_BulletinLogic();
            int totalRecords = 0;
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(string.Format(" and IsUse=1 and OrgID={0} and ArticleTypeID={1}", BaseUtility.SiteOrganizationID, BulletinTypeEnum.Builletin.ToEnumValue()));
            //日常公告 
            DataTable dtBulletin = Logic.GetPagedList(PageIndex, PageSize, " CreateTime desc ", strQuery.ToString(), out totalRecords);

            int PageTotal = totalRecords % PageSize > 0 ? totalRecords / PageSize + 1 : totalRecords / PageSize;//总页数
            List<Announcement> listBulletin = new List<Announcement>();
            if (dtBulletin != null)
            {
                foreach (DataRow dr in dtBulletin.Rows)
                {
                    listBulletin.Add(new Announcement()
                    {
                        ArticleID = dr["ArticleID"].ToString().ToInt(),
                        ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.BulletinImage, string.IsNullOrEmpty(dr["ImageUrl"].ToString()) ? "default.png" : dr["ImageUrl"].ToString()),
                        MainHead = dr["MainHead"].ToString(),
                        CreateTime = dr["CreateTime"].ToDateTime().ToString("yyyy-MM-dd"),
                        ArticleContent = StringUtility.StripHTML(dr["ArticleContent"].ToString())
                    });
                }
            }
            var json = JsonHelper.SerializeObject(listBulletin);
            json = "{\"data\":" + json + ",\"CurrentPage\":" + PageIndex + ",\"PageTotal\":" + PageTotal + ",\"total\":" + totalRecords + "}";
            return json;
        }
        private void ReturnResponseContent(string content)
        {
            currentContext.Response.Clear();
            currentContext.Response.ContentType = "text/json";
            currentContext.Response.Write(content);
            currentContext.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}