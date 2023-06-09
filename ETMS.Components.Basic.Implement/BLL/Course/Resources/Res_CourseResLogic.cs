
using System;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course.Resources;

using ETMS.Components.Basic.API.Entity;

namespace ETMS.Components.Basic.Implement.BLL.Course.Resources
{
    /// <summary>
    /// 课程资源表业务逻辑
    /// </summary>
    public partial class Res_CourseResLogic
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResID"></param>
        /// <param name="resType"></param>
        public void SaveCourseRes(string resID,string resName, EnumResourcesType resType, Guid courseID)
        {
            Res_CourseRes courseRes = new Res_CourseRes();

            courseRes.CourseID = courseID;
            courseRes.ResID = resID;
            courseRes.ResName = resName;
            courseRes.CourseResTypeID = (int)resType;
            courseRes.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            courseRes.CreateUser = ETMS.AppContext.UserContext.Current.RealName;
            courseRes.CreateTime = System.DateTime.Now;
            courseRes.IsUse = 1;//默认为启用状态

            Save(courseRes);
        }

 		/// <summary>
		/// 保存操作
		/// </summary>
		public void Save(Res_CourseRes res_CourseRes)
		{
            try
            {
			    if(res_CourseRes.CourseResID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    res_CourseRes.CourseResID = res_CourseRes.CourseResID.NewID();
                    Add(res_CourseRes);
                }
                else
                {
                    Update(res_CourseRes);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("Index_U_Res_CourseResCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Course.Resources.Res_CourseRes.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Res_CourseResName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Course.Resources.Res_CourseRes.NameExists");
                }
                else if (ex.Message.IndexOf("Index_UT_Res_CourseRes", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Course.Resources.Res_CourseRes.NameExists");
                }
                 //如果仍未处理，则抛出
                throw ex;
            } 
		}

        /// <summary>
        /// 修改资源时更新关系状态
        /// </summary>
        /// <param name="resourceID"></param>
        /// <param name="status"></param>
        /// <param name="resType"></param>
        public void UpdateStatus(string resourceID, int status, EnumResourcesType resType)
        {
            Res_CourseRes courseRes = getCourseResByResID(resourceID, resType);
            courseRes.IsUse = status;
            Save(courseRes);
        }

        /// <summary>
		/// 删除
		/// </summary>
		protected void doRemove(Guid courseResID)
		{
            try
            {
			     DAL.Remove(courseResID);
                 //记录删除日志（根据ID删除）
                 BizLogHelper.Operate(courseResID,"删除");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("Course.Resources.Res_CourseRes.DataUsed");
                } 
                //如果仍未处理，则抛出
                throw ex;
            }  
		}

        /// <summary>
        /// 根据课程资源ID和资源类型获取课程信息
        /// </summary>
        /// <param name="resID"></param>
        /// <param name="resType"></param>
        /// <returns></returns>
        public Res_CourseRes getCourseResByResID(string resID, EnumResourcesType resType)
        {
            Res_CourseRes res_CourseRes = DAL.getCourseResByResID(resID, (int)resType);
            if (res_CourseRes == null)
            {
                throw new ETMS.AppContext.BusinessException("Course.Resources.Res_CourseRes.NotFoundException", new object[] { resID });
            }

            return res_CourseRes;
        }

        /// <summary>
        /// 根据课程资源ID和资源类型获取课程ID
        /// </summary>
        /// <param name="ResID"></param>
        /// <param name="resType"></param>
        /// <returns></returns>
        public Guid getCourseIDByResID(string resID, EnumResourcesType resType)
        {
            Res_CourseRes courseRes = getCourseResByResID(resID, resType);

            return courseRes.CourseID;
        }
        /// <summary>
        /// 根据课程ID和资源类型获取课程资源ID
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="resType"></param>
        /// <returns></returns>
        public string getResourcesIDByCourseID(Guid courseID, EnumResourcesType resType)
        {
            Res_CourseRes courseRes = DAL.getCourseResByCourseID(courseID, (int)resType);

            return courseRes.ResID;
        }

    }		
}

