using System.Data;
using ETMS.Utility.Data;

namespace ETMS.Components.Point.Implement.DAL
{
    /// <summary>
    /// 学员培训项目课程积分规则数据访问扩展类
    /// 黄中福：2012－05－08
    /// </summary>
    public partial class Point_Student_CourseRoleDataAccess
    {

        public void DeleteStudentCoursePointRoleFromCourseAttrID(int orgID,int courseAttrID)
        {
            //先删除该课程属性下已经设置的所有积分规则
            string sqlDeleteModal = "delete from Point_Student_CourseRole where OrgID='{0}' AND CourseAttrID='{1}'";
            string sql = string.Format(sqlDeleteModal, orgID,courseAttrID);
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.Text, sql);

        }


    }




}
