//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013/2/1 9:27:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System.Collections.Generic;
using System.Data;
using ETMS.Utility.Logging;
using ETMS.Components.QS.API.Entity;
using ETMS.Components.QS.Implement.DAL;
namespace ETMS.Components.Basic.Implement.BLL.Import
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class Import_SurveyAreaLogic
	{
		private static readonly Import_SurveyAreaDataAccess DAL = new Import_SurveyAreaDataAccess();
		
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Import_SurveyArea import_SurveyArea)
		{
			DAL.Add(import_SurveyArea);
            BizLogHelper.AddOperate(import_SurveyArea);
		}


		/// <summary>
		/// 保存
		/// </summary>
		public void Update(Import_SurveyArea import_SurveyArea)
		{
            //修改前信息
            Import_SurveyArea originalEntity = GetById(import_SurveyArea.DetailID);
			DAL.Save(import_SurveyArea);
            BizLogHelper.UpdateOperate(originalEntity,import_SurveyArea);
		}

    
    
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Import_SurveyArea GetById(int detailID)
		{
            Import_SurveyArea import_SurveyArea = DAL.GetById(detailID);
			if (import_SurveyArea == null)
			{
				throw new ETMS.AppContext.BusinessException(".Import_SurveyArea.NotFoundException",new object[]{});
			}
			
			return import_SurveyArea;
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
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
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
		public IList<Import_SurveyArea> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria,out int totalRecords)
		{				
			return DAL.GetEntityList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
		}
		
	}
	
	
}

