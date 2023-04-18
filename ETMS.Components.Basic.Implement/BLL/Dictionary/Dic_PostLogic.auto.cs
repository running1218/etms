
using System;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Dictionary;
using ETMS.Components.Basic.Implement.DAL.Dictionary;
using System.Transactions;
namespace ETMS.Components.Basic.Implement.BLL.Dictionary
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public partial class Dic_PostLogic
    {
        private static readonly Dic_PostDataAccess DAL = new Dic_PostDataAccess();

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Dic_Post dic_Post)
        {
            try
            {
                DAL.Add(dic_Post);
                BizLogHelper.AddOperate(dic_Post);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("IX_Dic_Post_U_Org_Code"))
                {
                    throw new BusinessException(string.Format("岗位编码：【{0}】已经存在，不能重复！", dic_Post.PostCode));
                }
                else if (ex.Message.Contains("IX_Dic_Post_U_Org_Name"))
                {
                    throw new BusinessException(string.Format("岗位名称：【{0}】已经存在，不能重复！", dic_Post.PostName));
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 postID)
        {
            doRemove(postID);
        }

        /// <summary>
        /// 批量删除(主键ID数组）
        /// </summary>
        public void Remove(Int32[] postIDs)
        {
#if !DEBUG
			using (TransactionScope ts = new TransactionScope())
			{
#endif
            foreach (Int32 id in postIDs)
            {
                Remove(id);
            }
#if !DEBUG
				ts.Complete();
			}
#endif
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Dic_Post dic_Post)
        {
            //修改前信息
            Dic_Post originalEntity = GetById(dic_Post.PostID);
            if (originalEntity.Status == 1 && dic_Post.Status == 0)//更改状态
            {
                //检查岗位是否有学员使用，如果有，则禁止停用
                Security.Site_StudentLogic studentLogic = new Security.Site_StudentLogic();
                int totalRecords = 0;
                studentLogic.GetManagePagedListAdv(1, 0, "", string.Format(" AND PostID={0}", dic_Post.PostID), out totalRecords);
                if (totalRecords > 0)
                {
                    throw new ETMS.AppContext.BusinessException("Dictionary.Dic_Post.DisableFaild");
                }
            }
            DAL.Save(dic_Post);
            BizLogHelper.UpdateOperate(originalEntity, dic_Post);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Dic_Post GetById(Int32? postID)
        {
            Dic_Post dic_Post = DAL.GetById(postID);            
            return dic_Post;
        }

        /// <summary>
        /// 查询分页数据
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
    }

}

