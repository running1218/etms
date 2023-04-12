using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ETMS.AppContext.Component;
using ETMS.Components.Basic.API;
using ETMS.Components.Basic.Implement;
namespace ETMS.WebApp.Manage
{
    /*
     * 
     * 
     * 
     * 
     * 
     *    组件装配配置清单位置：
     *    
     *    ETMS.Product/ExtendComponents/*.xml
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     **/
    ///// <summary>
    /////核心组件装配器
    ///// </summary>
    //public class BasicComponentsAssemble
    //{
    //    public static IList<ETMS.AppContext.Component.IComponent> DoAssemble()
    //    {
    //        List<IComponent> components = new List<IComponent>();
    //        //1、注册统一认证与授权组件
    //        components.Add(new DefaultPassportFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(IPassportFacade).Name,
    //            /*组件名称*/
    //            Name = "统一认证与授权",
    //            /*组件描述*/
    //            Description = "向Security模块提供对：统一认证与授权的基本操作支持！"
    //        });

    //        return components;
    //    }
    //}

    ///// <summary>
    ///// 扩展组件装配器
    ///// </summary>
    //public class ExtendComponentsAssemble
    //{
    //    public static IList<ETMS.AppContext.Component.IComponent> DoAssemble()
    //    {
    //        List<IComponent> components = new List<IComponent>();
    //        #region 课程资源模块

    //        //1、在线课件模块
    //        components.Add(new ETMS.Components.Courseware.Implement.CourseCoursewareFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(ICourseResourcesFacade).Name,
    //            /*组件名称*/
    //            Name = "在线课件",
    //            /*组件描述*/
    //            Description = "课件资源",
    //            /*应用入口*/
    //            ManageAppHome = "~/Resource/CoursewareManage/ResourceCoursewareList.aspx?CourseID={0}"
    //        });
    //        //2、在线作业模块
    //        components.Add(new ETMS.Components.ExOnlineJob.Implement.ExOnLineJobFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(ICourseResourcesFacade).Name,
    //            /*组件名称*/
    //            Name = "在线作业",
    //            /*组件描述*/
    //            Description = "在线作业资源",
    //            /*应用入口*/
    //            ManageAppHome = "~/QuestionDB/ExOnlineHomework/ResourceExerciseList.aspx?CourseID={0}"
    //        });
    //        //3、在线测试模块
    //        components.Add(new ETMS.Components.ExOnlineTest.Implement.ExOnlineTestFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(ICourseResourcesFacade).Name,
    //            /*组件名称*/
    //            Name = "在线测试",
    //            /*组件描述*/
    //            Description = "在线测试资源",
    //            /*应用入口*/
    //            ManageAppHome = "~/QuestionDB/ExOnlineTest/ResourceExerciseList.aspx?CourseID={0}"
    //        });

    //        #endregion

    //        #region 项目课程资源模块

    //        //1、项目课程资源-在线课件模块
    //        components.Add(new ETMS.Components.Courseware.Implement.CourseCoursewareFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(ITrainingItemCourseResourcesFacade).Name,
    //            /*组件名称*/
    //            Name = "在线课件",
    //            /*组件描述*/
    //            Description = "课件资源",
    //            /*应用入口*/
    //            ManageAppHome = "~/TraningImplement/ProjectCourseResource/CourseWareList.aspx?TrainingItemCourseID={0}",

    //            /*应用查看入口*/
    //            ListAppHome = "~/TraningImplement/ProjectCourseResourceQuery/CourseWareList.aspx?TrainingItemCourseID={0}"
    //        });
    //        //2、项目课程资源-在线作业模块
    //        components.Add(new ETMS.Components.ExOnlineJob.Implement.ExOnLineJobFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(ITrainingItemCourseResourcesFacade).Name,
    //            /*组件名称*/
    //            Name = "在线作业",
    //            /*组件描述*/
    //            Description = "在线作业资源",
    //            /*应用入口*/
    //            ManageAppHome = "~/TraningImplement/ProjectCourseResource/OnLineHomeWork.aspx?TrainingItemCourseID={0}",
                                
    //            /*应用查看入口*/
    //            ListAppHome = "~/TraningImplement/ProjectCourseResourceQuery/OnLineHomeWork.aspx?TrainingItemCourseID={0}"
    //        });
    //        //3、项目课程资源-在线测试模块
    //        components.Add(new ETMS.Components.ExOnlineTest.Implement.ExOnlineTestFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(ITrainingItemCourseResourcesFacade).Name,
    //            /*组件名称*/
    //            Name = "在线测试",
    //            /*组件描述*/
    //            Description = "在线测试资源",
    //            /*应用入口*/
    //            ManageAppHome = "~/TraningImplement/ProjectCourseResource/OnLineTest.aspx?TrainingItemCourseID={0}",

    //            /*应用查看入口*/
    //            ListAppHome = "~/TraningImplement/ProjectCourseResourceQuery/OnLineTest.aspx?TrainingItemCourseID={0}"
    //        });
    //        #endregion



    //        //费用接口
    //        components.Add(new ETMS.Components.Fee.Implement.FeeFacade()
    //        {
    //            /*组件ID*/
    //            ID = typeof(IFeeFacade).Name,
    //            /*组件名称*/
    //            Name = "费用接口",
    //            /*组件描述*/
    //            Description = "费用接口",
    //            /*应用入口*/
    //            ManageAppHome = ""
    //        });



    //        return components;
    //    }


    //}
}