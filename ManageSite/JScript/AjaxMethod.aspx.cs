using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ETMS.Components.Basic.Implement.BLL.Course;

public partial class JScript_AjaxMethod : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string returnVal = "fail";
        try
        {
            string type = Request.QueryString["type"];
            switch (type)
            {
                //添加课程时验证课程编码是否存在
                case "checkCourseCode":
                    {
                        string courseCode = Request.QueryString["courseCode"];
                        string courseID = Request.QueryString["CourseID"];
                        Res_CourseLogic courselogic = new Res_CourseLogic();
                        //if (courselogic.GetByCode(courseID,courseCode))
                        //{
                        //    returnVal = "课程编码已存在！";
                        //}
                        //else
                        //{
                        //    returnVal = "课程编码可以使用！";
                        //}
                        break;
                    }
            }
        }
        catch
        {

        }
        finally
        {
            Response.Clear();
            Response.Write(returnVal);
            Response.End();
        }
    }
}