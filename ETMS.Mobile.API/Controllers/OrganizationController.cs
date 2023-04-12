using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ETMS.Mobile.API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("Api/Organization")]
    public class OrganizationController : ApiController
    {
        [Route("{Domain}", Name = "获取机构信息")]
        public HttpResponseMessage GetOrganizationInfo(string Domain)
        {
            var organization = new OrganizationLogic().GetNodeByDomain(Domain);
            var organizationID = organization == null ? System.Configuration.ConfigurationManager.AppSettings["BulletinOrganizationID"] : organization.OrganizationID.ToString();

            return ResponseJson.GetSuccessJson(new { OrganizationID = organizationID });
        }
    }
}
