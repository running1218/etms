using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

using ETMS.Components.Basic.API.Entity.Bulletin;
using ETMS.Components.Basic.Implement.BLL.Bulletin;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Announcement")]
    public class AnnouncementController : ApiController
    {
        [Route("{OrgID}/{PageSize}/{PageIndex}", Name ="获取公告列表信息")]
        public HttpResponseMessage Get(int OrgID, int PageSize, int PageIndex)
        {
            Inf_BulletinLogic Logic = new Inf_BulletinLogic();
            int totalRecords = 0;
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(string.Format(" and IsUse=1 and OrgID={0} and ArticleTypeID={1}", OrgID, BulletinTypeEnum.Builletin.ToEnumValue()));
            //日常公告 
            DataTable dtBulletin = Logic.GetPagedList(PageIndex, PageSize, " CreateTime desc ", strQuery.ToString(), out totalRecords);

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
            return ResponseJson.GetSuccessJson(listBulletin);
        }

        [Route("{ArticleID}", Name = "获取公告详情")]
        public HttpResponseMessage Get(int ArticleID)
        {
            Inf_Bulletin Entity = new Inf_BulletinLogic().GetById(ArticleID);
            Announcement announcement = new Announcement()
            {
                ArticleID=Entity.ArticleID,
                MainHead=Entity.MainHead,
                ArticleContent= Entity.ArticleContent,
                CreateTime=Entity.CreateTime.ToString("yyyy-MM-dd")

            };
            return ResponseJson.GetSuccessJson(announcement);
        }
    }
}