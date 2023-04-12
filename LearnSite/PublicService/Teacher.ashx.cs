using ETMS.Components.Basic.Implement.BLL.Teacher;
using ETMS.Utility;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ETMS.Studying.PublicService
{
    /// <summary>
    /// Summary description for Teacher
    /// </summary>
    public class Teacher : IHttpHandler
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
                case "famousteacherlist":
                    ReturnResponseContent(GetFamousTeacherList());
                    break;
                default:
                    ReturnResponseContent(JsonHelper.GetParametersInValidJson());
                    break;
            }
        }
        //获取名师风采列表
        private string GetFamousTeacherList()
        {
            int PageIndex = string.IsNullOrEmpty(currentContext.Request["PageIndex"].ToString()) ? 1 : currentContext.Request["PageIndex"].ToString().ToInt();
            int PageSize = string.IsNullOrEmpty(currentContext.Request["PageSize"].ToString()) ? 20 : currentContext.Request["PageSize"].ToString().ToInt();
            string SortExpression = " IsTop DESC,Sort ASC ";
            int totalRecords = 0;
            DataTable dtTeacher = new Rec_TeacherLogic().GetFamousTeacherPagedList(PageIndex, PageSize, SortExpression, string.Format(" AND st.IsUse=1 AND c.Status=1 And c.OrganizationID={0}", BaseUtility.SiteOrganizationID), out totalRecords);
            int PageTotal = totalRecords % PageSize > 0 ? totalRecords / PageSize + 1 : totalRecords / PageSize;//总页数
            List<FamousTeacher> listTeacher= new List<FamousTeacher>();
            if (dtTeacher != null)
            {
                foreach (DataRow dr in dtTeacher.Rows)
                {
                    listTeacher.Add(new FamousTeacher()
                    {
                        TeacherID = dr["TeacherID"].ToString().ToInt(),
                        RealName = dr["RealName"].ToString(),
                        TeacherLevelName = dr["TeacherLevelName"].ToString(),
                        PhotoUrl = StaticResourceUtility.GetFullPathByFileType("UserIcon", string.IsNullOrEmpty(dr["PhotoUrl"].ToString()) ? "default.gif" : dr["PhotoUrl"].ToString()),
                        TeacherBrief = StringUtility.StripHTML(dr["TeacherBrief"].ToString())
                    });
                }
            }
            var json = JsonHelper.SerializeObject(listTeacher);
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
    public class FamousTeacher
    {
        public int TeacherID { get; set; }
        public string RealName { get; set; }
        public string TeacherLevelName { get; set; }
        public string TeacherBrief { get; set; }
        public string PhotoUrl { get; set; }
    }
}