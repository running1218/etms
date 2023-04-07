//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-3-7 10:28:02.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem.StudentCourse;
using ETMS.Components.Basic.Implement.DAL.TrainingItem.StudentCourse;
namespace ETMS.Components.Basic.Implement.BLL.TrainingItem.StudentCourse
{
    /// <summary>
    /// 学员选课表业务逻辑
    /// </summary>
    public partial class Sty_StudentCourseLogic
    {
        private static readonly Sty_StudentCourseDataAccess DAL = new Sty_StudentCourseDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Sty_StudentCourse sty_StudentCourse)
        {
            DAL.Add(sty_StudentCourse);
            BizLogHelper.AddOperate(sty_StudentCourse);
        }


        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Sty_StudentCourse sty_StudentCourse)
        {
            //修改前信息
            Sty_StudentCourse originalEntity = GetById(sty_StudentCourse.StudentCourse);
            DAL.Save(sty_StudentCourse);
            BizLogHelper.UpdateOperate(originalEntity, sty_StudentCourse);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid studentCourse)
        {
            doRemove(studentCourse);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Guid[] studentCourses)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Guid id in studentCourses)
            {
                Remove(id);
            }
#if !DEBUG
				ts.Complete();
			}
#endif
        }


        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Sty_StudentCourse GetById(Guid studentCourse)
        {
            Sty_StudentCourse sty_StudentCourse = DAL.GetById(studentCourse);
            if (sty_StudentCourse == null)
            {
                throw new ETMS.AppContext.BusinessException(".TrainingItem.StudentCourse.Sty_StudentCourse.NotFoundException", new object[] { studentCourse });
            }

            return sty_StudentCourse;
        }

        /// <summary>
        /// 查询数据列表分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 查询实体分页数据
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public IList<Sty_StudentCourse> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

    }


}

