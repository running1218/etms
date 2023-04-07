using System;
using System.Data;
using ETMS.Utility.Logging;

using ETMS.Components.Courseware.Implement.DAL;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Resources;

namespace ETMS.Components.Courseware.Implement.BLL
{
    public class Res_ItemCourse_CoursewareLogic
    {
        Res_ItemCourse_CoursewareDataAccess DAL = new Res_ItemCourse_CoursewareDataAccess();


        #region 业务数据维护方法，比如：添加、修改、删除




        /// <summary>
        /// 增加
        /// </summary>
        public void AddCoursewareToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
        {
            tr_ItemCourseRes.CourseResTypeID = 1;//类型是在线课件
            DAL.AddCoursewareToItemCourse(tr_ItemCourseRes);
            BizLogHelper.AddOperate(tr_ItemCourseRes);
        }


        /// <summary>
        /// 批量添加课程资源(在线课件)到指定项目课程的资源中
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="courseResIDArray">要添加的在线课件ID数组（GUID）</param>
        public void BatchAdd(Guid trainingItemCourseID, Guid[] courseResIDArray, DateTime ResBeginTime, DateTime ResEndTime)
        {
            foreach (Guid courseResID in courseResIDArray)
            {
                Tr_ItemCourseRes entity = new Tr_ItemCourseRes();
                entity.ItemCourseResID = System.Guid.NewGuid();
                entity.TrainingItemCourseID = trainingItemCourseID;
                entity.CourseResTypeID = 1;//类型是在线课件
                entity.CourseResID = courseResID.ToString();
                entity.IsUse = 1;//默认为“启用”
                entity.ResBeginTime=ResBeginTime;
                entity.ResEndTime=ResEndTime;
                entity.CreateTime = System.DateTime.Now;
                entity.CreateUser = AppContext.UserContext.Current.RealName;
                entity.CreateUserID = AppContext.UserContext.Current.UserID;
                try
                {
                    //如果添加不成功，继续添加下一条
                    DAL.AddCoursewareToItemCourse(entity);
                }
                catch
                {
                }
            }

        }


        /// <summary>
        /// 从培训项目课程资源中，删除某个资源
        /// </summary>
        /// <param name="itemCourseResID">要删除的培训项目课程资源ID</param>
        public void RemoveCoursewareFromItemCourse(Guid itemCourseResID)
        {
            //先判断该资源是否被使用，如果被使用，则不能删除
            //Tr_ItemCourseResLogic logic = new Tr_ItemCourseResLogic();
            //Tr_ItemCourseRes entity = logic.GetById(itemCourseResID);
            //if (DAL.CheckResourceIsUsed(new Guid(entity.CourseResID)))
            //{
            //    throw new ETMS.AppContext.BusinessException("该培训项目课程资源已经被学习或使用，不能删除！");
            //}

            DAL.RemoveCoursewareFromItemCourse(itemCourseResID);
        }


        /// <summary>
        /// 从培训项目课程资源中，批量删除其资源
        /// </summary>
        /// <param name="itemCourseResIDArray">要删除的培训项目课程资源ID数组</param>
        public void BatchRemoveCoursewareFromItemCourse(Guid[] itemCourseResIDArray)
        {
            int noDelNum = 0;
            foreach (Guid itemCourseResID in itemCourseResIDArray)
            {
                try
                {
                    RemoveCoursewareFromItemCourse(itemCourseResID);
                }
                catch
                {
                    noDelNum++;
                }
            }
            if (noDelNum > 0)
            {
                string errorMsg = "删除完毕：当前要删除的记录数为“{0}”个，有“{1}”个删除不成功，原因可能是这些培训项目课程资源已经被学习或使用！";
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsg, itemCourseResIDArray.Length, noDelNum));
            }
        }



        /// <summary>
        /// 保存
        /// </summary>
        public void SaveCoursewareToItemCourse(Tr_ItemCourseRes tr_ItemCourseRes)
        {
            DAL.SaveCoursewareToItemCourse(tr_ItemCourseRes);
        }


        #endregion

        #region 数据查询方法
        
        
        /// <summary>
        /// 根据培训项目课程ID获取课件总数
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public Int32 GetItemCourseCoursewareTotal(Guid trainingItemCourseID)
        {
            return DAL.GetItemCourseCoursewareTotal(trainingItemCourseID);
        }


        public DataTable GetTrainingItemResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            return DAL.GetTrainingItemResourcesList(trainingItemCourseID, pageIndex, pageSize, criteria, out  totalRecords);

        }



        /// <summary>
        /// 获取某个培训项目课程未使用的课程资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, int pageIndex, int pageSize, string criteria, out int totalRecords)
        {
            return DAL.GetTrainingItemNoSelectResourcesList(trainingItemCourseID, pageIndex, pageSize, criteria, out  totalRecords);

        }


        /// <summary>
        /// 获取某个培训项目课程未使用的课程资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemNoSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            return DAL.GetTrainingItemNoSelectResourcesList(trainingItemCourseID, out totalRecords);
        }


        /// 获取某个培训项目课程已使用的在线课程资源列表
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetTrainingItemSelectResourcesList(Guid trainingItemCourseID, out int totalRecords)
        {
            return DAL.GetTrainingItemSelectResourcesList(trainingItemCourseID, out totalRecords);
        }

        /// <summary>
        /// 修改项目课程资源排序号
        /// </summary>
        public void ItemCourseResUpdateOrderNum(Guid ItemCourseResID, int OrderNum)
        {
            DAL.ItemCourseResUpdateOrderNum(ItemCourseResID, OrderNum);
        }


        /// <summary>
        /// 获取某个培训项目课程的某个在线课程资源信息
        /// </summary>
        /// <param name="trainingItemCourseID">培训项目课程ID</param>
        /// <param name="ItemCourseResID">培训项目课程资源ID</param>
        /// <returns></returns>
        public DataTable GetTrainingItemGetOneResources(Guid trainingItemCourseID, Guid ItemCourseResID)
        {
            return DAL.GetTrainingItemGetOneResources( trainingItemCourseID,  ItemCourseResID);
        }

        #endregion


    }
}
