using ETMS.Components.Basic.API.Entity.Operation;
using ETMS.Components.Basic.Implement.BLL.Operation;
using ETMS.Utility;
using ETMS.Utility.Service.FileUpload;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ETMS.Mobile.API.Controllers
{
    /// <summary>
    /// Banner图
    /// </summary>
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Banner")]
    public class BannerController : ApiController
    {
        /// <summary>
        /// 获取Banner图信息
        /// </summary>
        /// <returns>Banner图信息</returns>
        [Route("{OrgID}",Name = "获取Banner图信息")]
        public HttpResponseMessage Get(string OrgID)
        {
            DataTable dtBanner=new BannerSpreadLogic().GetPageList("", "1", OrgID.ToInt());
            List<Banner> listBanner = new List<Banner>();
            foreach (DataRow dr in dtBanner.Rows)
            {
                listBanner.Add(new Banner()
                {
                    Name = dr["SpreadName"].ToString(),
                    Link=dr["SpreadMobileLink"].ToString(),
                    ImageUrl = StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.BannerImage, dr["MobileImagePath"].ToString())
                    
                });
            }
            return ResponseJson.GetSuccessJson(listBanner);
        }

    }
}
