
using System.Data;
using ETMS.Components.Basic.Implement.DAL.Course;
namespace ETMS.Components.Basic.Implement.BLL.Course
{
    public class Dic_CourseTypeLogic
    {
       private static readonly Dic_CourseTypeDataAccess DAL = new Dic_CourseTypeDataAccess();

       /// <summary>
       /// 获取全部课程类型
       /// </summary>
       /// <returns>返回：DataTable集合</returns>
       public DataTable GetCourseTypeList()
       {
           return DAL.GetCourseTypeList();
       }
    }
}
