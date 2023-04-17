//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-6 14:53:50.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;
using System.Collections;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.Point.API.Entity;
using ETMS.Components.Point.API;
namespace ETMS.Components.Point.Implement.BLL
{
    /// <summary>
    /// 学员培训项目课程积分规则表业务逻辑
    /// </summary>
    public partial class Point_Student_CourseRoleLogic
    {
        /// <summary>
        /// 保存操作
        /// </summary>
        public void Save(Point_Student_CourseRole point_Student_CourseRole)
        {
            try
            {
                if (point_Student_CourseRole.StudentCoursePointRoleID.IsEmpty())
                {
                    //设置主键ID（仅类型为GUID有效，Int型则由数据库自增产生）
                    point_Student_CourseRole.StudentCoursePointRoleID = point_Student_CourseRole.StudentCoursePointRoleID.NewID(); ;
                    Add(point_Student_CourseRole);
                }
                else
                {
                    Update(point_Student_CourseRole);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常 Index_UT_Point_Student_CourseRole
                if (ex.Message.IndexOf("Index_UT_Point_Student_CourseRole", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.Index_UT_Point_Student_CourseRole);
                }
                else if (ex.Message.IndexOf("Index_U_Point_Student_CourseRoleName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Point_Student_CourseRole.NameExists");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void doRemove(Guid studentCoursePointRoleID)
        {
            try
            {
                DAL.Remove(studentCoursePointRoleID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(studentCoursePointRoleID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(".Point_Student_CourseRole.DataUsed");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }




        /// <summary>
        /// 根据课程属性ID获取某个组织机构下的“学员培训项目课程积分规则”
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="courseAttrID">课程属性ID</param>
        /// <returns></returns>
        public DataTable GetStudentCoursePointRoleListByCourseAttrID(int orgID, int courseAttrID)
        {
            string sortExpression = " MaxNum";
            string criteria = string.Format(" AND CourseAttrID='{0}'", courseAttrID);
            criteria += string.Format(" AND OrgID='{0}'", orgID);
            int totalRecords = 0;
            return DAL.GetPagedList(1, int.MaxValue - 1000, sortExpression, criteria, out totalRecords);
        }


       
        /// <summary>
        /// 保存某个组织机构某个课程属性的课时积分规则
        /// </summary>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="courseAttrID">课程属性ID</param>
        /// <param name="sortedListMaxNumPoint">要保存的积分规则，SortedList，用MaxNum为Key，Point为value</param>
        /// <param name="createUserID">创建人Id </param>
        /// <param name="createUser">创建人</param>
        public void SaveStudentCoursePointRoleToCourseAttrID(int orgID, int courseAttrID, SortedList sortedListMaxNumPoint,int createUserID,string createUser)
        {
            //先删除该课程属性下已经设置的所有积分规则
            DAL.DeleteStudentCoursePointRoleFromCourseAttrID(orgID,courseAttrID);
            //创建添加的数据实体
            Point_Student_CourseRole entity = new Point_Student_CourseRole();
            entity.StudentCoursePointTypeID = 0;//默认为“课时”规则
            entity.OrgID = orgID;
            entity.CreateUser = createUser;
            entity.CreateTime = System.DateTime.Now;
            entity.CreateUserID = createUserID;
            entity.CourseAttrID = courseAttrID;

            int minValue = 0;

            System.Collections.IDictionaryEnumerator myEnumerator = sortedListMaxNumPoint.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                entity.MinNum = minValue;
                entity.MaxNum = int.Parse(myEnumerator.Key.ToString());
                entity.GivePoints = int.Parse(myEnumerator.Value.ToString());
                //调用单表保存
                entity.StudentCoursePointRoleID = System.Guid.NewGuid();
                DAL.Add(entity);
                minValue = entity.MaxNum;//下一个最小值
            }
           

        }



    }
}

