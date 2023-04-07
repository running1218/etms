using System;
using ETMS.Utility.Logging;

using ETMS.AppContext;

using ETMS.Components.Courseware.API.Entity;

using ETMS.Components.Courseware.API;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;

using ETMS.Components.Courseware.Implement.DAL;
using ETMS.Components.Basic.API.Entity;
using System.Data;
using System.Transactions;

namespace ETMS.Components.Courseware.Implement.BLL
{
    /// <summary>
    /// 课件表扩展类
    /// 黄中福
    /// </summary>
    public partial class Res_CoursewareLogic
    {
        Res_CourseResDataAccess dalCourseRes = new Res_CourseResDataAccess();

        /// <summary>
        /// 添加一个在线课件，并与指定的课程关联
        /// </summary>
        /// <param name="res_Courseware">在线课件实体</param>
        /// <param name="courseID">课程ID</param>
        /// <returns></returns>
        public void AddCourseCourseware(Res_Courseware res_Courseware, Guid courseID)
        {
            try
            {
                DAL.Add(res_Courseware);

                Res_CourseResLogic courseResLogic = new Res_CourseResLogic();
                courseResLogic.SaveCourseRes(res_Courseware.CoursewareID.ToString(), res_Courseware.CoursewareName, EnumResourcesType.Courseware, courseID);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message;
                if (errorMsg.IndexOf("Index_U_CoursewareName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该课件名称已经存在，请修改！");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }



        /// <summary>
        /// 从课程中删除一个在线课件
        /// </summary>
        /// <param name="res_Courseware">在线课件实体</param>
        /// <param name="courseID">课程ID</param>
        public void RemoveCourseCourseware(Guid courseCoursewareID)
        {
            try
            {
                

                using (TransactionScope ts = new TransactionScope())
                {
                    //取课件ID
                    string coursewareID = dalCourseRes.GetById(courseCoursewareID).ResID;
                    //判断其是否能删除
                    if (DAL.CheckResourceIsUsed(new Guid( coursewareID)))
                    {
                        throw new ETMS.AppContext.BusinessException("该学习资源已经被培训项目的课程引用，不能删除！");
                    }
                    //先删除关系
                    dalCourseRes.Remove(courseCoursewareID);
                    //再删除在线课件实体（以后要是多对多就不用删除）
                    DAL.Remove(new Guid(coursewareID));

                    ts.Complete();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(string.Format(BizErrorDefine.CourseWareIsUsing, courseCoursewareID));
                }
                throw ex;
            }
        }


        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(Res_Courseware res_Courseware, Guid courseID, Guid coursewareID, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    res_Courseware.CoursewareID = coursewareID;
                    Add(res_Courseware);
                    new Res_CourseResLogic().Add(new Basic.API.Entity.Course.Resources.Res_CourseRes()
                    {
                        CourseResID = Guid.NewGuid(),
                        CourseID = courseID,
                        CourseResTypeID = 1,
                        ResID = res_Courseware.CoursewareID.ToString(),
                        CreateTime = DateTime.Now,
                        CreateUser = res_Courseware.CreateUser,
                        CreateUserID = res_Courseware.CreateUserID,
                        IsUse = res_Courseware.CoursewareStatus,//默认是否启用为课件的状态，在这应该为1(huangzhf)
                        ResName = res_Courseware.CoursewareName
                    });

                    BizLogHelper.AddOperate(res_Courseware);
                }
                else
                {
                    Res_Courseware originalEntity = GetById(res_Courseware.CoursewareID);
                    Update(res_Courseware);
                    new Res_CourseResLogic().UpdateStatus(res_Courseware.CoursewareID.ToString(), res_Courseware.CoursewareStatus, EnumResourcesType.Courseware);
                    BizLogHelper.UpdateOperate(originalEntity, res_Courseware);
                }                
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message;
                if (errorMsg.IndexOf("Index_U_CoursewareName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该课件名称已经存在，请修改！");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid coursewareID)
        {
            try
            {
                DAL.Remove(coursewareID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(coursewareID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.CourseWareIsUsing);
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }  

         /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable Res_CoursewareGetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.Res_CoursewareGetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 根据课程编号获取课件的可用总数（就是状态为“启用”）
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 GetCourseWareTotal(Guid courseID)
        {
            return DAL.GetCourseWareTotal(courseID);
        }


        /// <summary>
        /// 根据课程编号获取课件的总数
        /// </summary>
        /// <param name="courseID">课程编号</param>
        /// <returns></returns>
        public Int32 GetALLCourseWareTotal(Guid courseID)
        {
            return DAL.GetALLCourseWareTotal(courseID);
        }

        /// <summary>
        /// 更新资源状态
        /// </summary>
        /// <param name="coursewareID"></param>
        /// <param name="resourceStatus"></param>
        /// <param name="resourcePath"></param>
        public void UpdateResourceStatus(Guid coursewareID, int resourceStatus, string resourcePath)
        {
            DAL.UpdateResourceStatus(coursewareID, resourceStatus, resourcePath);
        }

        /// <summary>
        /// 更新资源状态
        /// </summary>
        /// <param name="coursewareID"></param>
        /// <param name="resourceStatus"></param>
        public void UpdateResourceStatus(Guid coursewareID, int resourceStatus)
        {
            DAL.UpdateResourceStatus(coursewareID, resourceStatus);
        }
    }
}
