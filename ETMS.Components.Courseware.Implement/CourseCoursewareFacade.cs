namespace ETMS.Components.Courseware.Implement
{

    public class CourseCoursewareFacade //: DefaultComponent, ICourseResourcesFacade, ITrainingItemCourseResourcesFacade
    {

        //private static Res_CoursewareLogic Logic = new Res_CoursewareLogic();
        //private static Res_ItemCourse_CoursewareLogic res_ItemCourse_CoursewareLogic = new Res_ItemCourse_CoursewareLogic();

        //#region ICourseResourcesFacade 成员

        ///// <summary>
        ///// 根据课程编号获取课件的可用总数（就是状态为“启用”）
        ///// </summary>
        ///// <param name="courseID"></param>
        ///// <returns></returns>
        //public Int32 GetResourcesTotal(Guid courseID)
        //{
        //    return Logic.GetCourseWareTotal(courseID);
        //}

        
        ///// <summary>
        ///// 返回某课程的资源总数
        ///// </summary>
        ///// <param name="courseID"></param>
        ///// <returns></returns>
        //public int GetALLResourcesTotal(Guid courseID)
        //{
        //    return Logic.GetALLCourseWareTotal(courseID);
        //}

        //public Basic.API.Entity.EnumResourcesType GetResourcesType()
        //{
        //    return Basic.API.Entity.EnumResourcesType.Courseware;
        //}



        ///// <summary>
        /////获取某个课程的课件列表
        ///// </summary>
        ///// <param name="courseID"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="criteria"></param>
        ///// <param name="totalRecords"></param>
        ///// <returns></returns>
        //public DataTable GetResourcesList(Guid courseID, int pageIndex, int pageSize,  string criteria, out int totalRecords)
        //{
        //    throw new NotImplementedException();
        //}

        //public DataTable GetResourcesList(Guid courseID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion





        //#region ITrainingItemCourseResourcesFacade 成员

        //public int GetTrainingItemResourcesTotal(Guid trainingItemCourseID)
        //{
        //    return res_ItemCourse_CoursewareLogic.GetItemCourseCoursewareTotal(trainingItemCourseID);
        //}

        //public DataTable GetTrainingItemResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        //{
        //    return res_ItemCourse_CoursewareLogic.GetTrainingItemResourcesList(trainingItemCourseID, pageIndex, pageSize, criteria, out  totalRecords);
        //}

        //public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        //{
        //    return res_ItemCourse_CoursewareLogic.GetTrainingItemNoSelectResourcesList(trainingItemCourseID, pageIndex, pageSize, criteria, out  totalRecords);
        //}


        //#endregion



    }
}

