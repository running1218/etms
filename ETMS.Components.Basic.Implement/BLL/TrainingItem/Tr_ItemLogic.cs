using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.AppContext;
using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.TrainingItem;
using ETMS.Utility;
using ETMS.Components.Basic.API;

namespace ETMS.Components.Basic.Implement.BLL.TrainingItem
{
    /// <summary>
    /// 培训项目扩展类
    /// 黄中福
    /// </summary>
    public partial class Tr_ItemLogic
    {

        #region 业务操作方法，如：添加、修改、删除、审核等


        /// <summary>
        /// 培训项目保存
        /// </summary>
        /// <param name="entity">培训项目实体</param>
        /// <param name="action">操作方法：添加或者修改</param>
        public void Save(Tr_Item entity, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                    Add(entity);
                else if (action == OperationAction.Edit)
                {
                    Save(entity);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.ToLower().IndexOf("Index_U_ItemCode".ToLower(), StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.TraningImplement_Item_ItemCodeExists);
                }
                else if (ex.Message.ToLower().IndexOf("Index_U_ItemName".ToLower(), StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.TraningImplement_Item_ItemNameExists);
                }

                throw ex;
            }
        }



        /// <summary>
        /// 删除某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        public void doRemove(Guid trainingItemID)
        {
            try
            {
                DAL.Remove(trainingItemID);
                //记录删除日志（根据ID删除）
                BizLogHelper.Operate(trainingItemID, "删除");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message.ToUpper();
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (errorMsg.IndexOf("FK_STY_STUD_REFERENCE_TR_ITEM", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目下已经有“学员报名”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_TR_ITEMC_REFERENCE_TR_ITEM", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目下已经设置有“课程”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_FEE_FEEC_REFERENCE_TR_ITEM", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目下已经有“费用流水”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_STY_CLAS_REFERENCE_TR_ITEM", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该项目下已经设置有“班级”，不能删除！");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }



        /// <summary>
        /// 审核某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="itemStatus">审核结果（20：审核通过，40：审核不通过）</param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditOpinion">审核意见</param>
        public void Tr_Item_Audit(Guid trainingItemID, int itemStatus, string auditUser, string auditOpinion)
        {
            try
            {
                DAL.Tr_Item_Audit(trainingItemID, itemStatus, auditUser, auditOpinion);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }

        }



        /// <summary>
        /// 取消审核某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="auditUser">审核人</param>
        /// <param name="auditOpinion">审核意见</param>
        public void Tr_Item_CancelAudit(Guid trainingItemID, string auditUser, string auditOpinion)
        {
            try
            {
                DAL.Tr_Item_CancelAudit(trainingItemID, auditUser, auditOpinion);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }

        }


        /// <summary>
        /// 发布某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="isIssue">是否发布（0：不发布，1：发布）</param>
        /// <param name="issueUser">发布人</param>
        public void Tr_Item_Issue(Guid trainingItemID, int isIssue, string issueUser)
        {
            try
            {
                DAL.Tr_Item_Issue(trainingItemID, isIssue, issueUser);
                GenerateOrder(trainingItemID, null);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// 产生订单
        /// </summary>
        /// <param name="trainingItemID">项目ID</param>
        /// <param name="studentTab">项目学员表，如果为空 则为项目下所有学员产生订单</param>
        public void GenerateOrder(Guid trainingItemID, DataTable studentTab)
        {
            //OrderLogic orderLogic = new OrderLogic();
            //Tr_Item item = GetById(trainingItemID);
            ////支付方式为个人的情况下产生订单
            //if (item.PayFrom == 2 && item.IsIssue)
            //{
            //    //获取项目课程与学员信息
            //    int stuTotalRecords = 0;
            //    if (studentTab == null)
            //    {
            //        studentTab = new Sty_StudentSignupLogic().GetStudentListByTrainingItemID(trainingItemID, 1, int.MaxValue - 1, "", out stuTotalRecords);
            //    }

            //    OrderInfo orderInfo = null;
            //    ProductInfo productInfo = null;

            //    switch (item.SignupModeID)
            //    {
            //        //项目自主报名
            //        case 1:
            //            break;
            //        //课程自主报名
            //        case 2:
            //            break;
            //        //项目集中填报
            //        case 3:
            //            foreach (DataRow stuRow in studentTab.Rows)
            //            {
            //                //订单信息
            //                orderInfo = new OrderInfo();
            //                orderInfo.OrderDescription = string.Format("{0}-订单", item.ItemName);
            //                orderInfo.OrderStatus = 1;
            //                orderInfo.BuyNumber = 1;
            //                orderInfo.TotalPrice = item.BudgetFee;
            //                orderInfo.Discount = 0;
            //                orderInfo.DiscountTotalPrice = (decimal)0;
            //                orderInfo.PaymentStatus = 0;
            //                orderInfo.UserID = stuRow["UserID"].ToInt();
            //                orderInfo.PayerName = "";
            //                orderInfo.PayerPhone = "";
            //                orderInfo.CreateTime = DateTime.Now;
            //                orderInfo.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            //                orderInfo.CreateUser = ETMS.AppContext.UserContext.Current.UserName;

            //                //订单明细
            //                List<ProductInfo> productInfoList = new List<ProductInfo>();
            //                productInfo = new ProductInfo();
            //                productInfo.OrderDetailID = Guid.NewGuid();
            //                productInfo.TrainingItemID = item.TrainingItemID;
            //                productInfo.TrainingItemName = item.ItemName;
            //                productInfo.TrainingItemCourseID = Guid.Empty;
            //                productInfo.TrainingItemcourseName = "";
            //                productInfo.ProductPrice = item.BudgetFee;
            //                productInfo.ProductShowUrl = "";
            //                productInfoList.Add(productInfo);
            //                orderInfo.ProductInfo = productInfoList;
            //                orderLogic.GenerateOrder(orderInfo);
            //            }
            //            break;
            //        //课程集中填报
            //        case 4:
            //            foreach (DataRow stuRow in studentTab.Rows)
            //            {
            //                //订单信息
            //                orderInfo = new OrderInfo();
            //                orderInfo.OrderDescription = string.Format("{0}-订单", item.ItemName);
            //                orderInfo.OrderStatus = 1;
            //                orderInfo.BuyNumber = 0;
            //                orderInfo.TotalPrice = 0;
            //                orderInfo.Discount = 0;
            //                orderInfo.DiscountTotalPrice = 0;
            //                orderInfo.PaymentStatus = 0;
            //                orderInfo.UserID = stuRow["UserID"].ToInt();
            //                orderInfo.PayerName = "";
            //                orderInfo.PayerPhone = "";
            //                orderInfo.CreateTime = DateTime.Now;
            //                orderInfo.CreateUserID = ETMS.AppContext.UserContext.Current.UserID;
            //                orderInfo.CreateUser = ETMS.AppContext.UserContext.Current.UserName;

            //                //订单明细 获取当前项目下学员选课信息
            //                List<ProductInfo> productInfoList = new List<ProductInfo>();
                           
            //                DataTable  studentCourseTab = new Sty_StudentCourseLogic().GetStudentCourseByTrainingItemIDAndUserID(trainingItemID, stuRow["UserID"].ToInt());
                           
            //                foreach (DataRow stuCouresRow in studentCourseTab.Rows)
            //                {
            //                    productInfo = new ProductInfo();
            //                    productInfo.OrderDetailID = Guid.NewGuid();
            //                    productInfo.TrainingItemID = item.TrainingItemID;
            //                    productInfo.TrainingItemName = item.ItemName;
            //                    productInfo.TrainingItemCourseID = stuCouresRow["TrainingItemCourseID"].ToGuid();
            //                    productInfo.TrainingItemcourseName = stuCouresRow["CourseName"].ToString();
            //                    productInfo.ProductPrice = stuCouresRow["BudgetFee"].ToString().ToDecimal();
            //                    //累计选课费用
            //                    orderInfo.TotalPrice += productInfo.ProductPrice;
            //                    orderInfo.BuyNumber++;

            //                    productInfo.ProductShowUrl = "";
            //                    productInfoList.Add(productInfo);
            //                }

            //                orderInfo.ProductInfo = productInfoList;
            //                orderLogic.GenerateOrder(orderInfo);
            //            }
            //            break;
            //    }
            //}
        }

        /// <summary>
        /// 归档某个培训项目
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <param name="itemEndModeID">项目归档方式（1:正常结束,2:异常结束,3:审核通过结束,4:审核不通过结束）</param>
        /// <param name="itemEndReMark">归档备注</param>
        /// <param name="modifyUser">归档人</param>
        public void Tr_Item_Achive(Guid trainingItemID, int itemEndModeID, string itemEndReMark, string modifyUser)
        {
            try
            {
                DAL.Tr_Item_Achive(trainingItemID, itemEndModeID, itemEndReMark, modifyUser);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }


        #endregion

        #region 项目状态验证方法，如：是否能编辑、是否能添加课程和报名等

        /// <summary>
        /// 验证某个培训项目是否能维护，比如：修改、删除、审核等，满足如下条件
        /// 1.项目状态是“待审核”
        /// 2.项目还没有发布
        /// </summary>
        /// <param name="trainingItemID">培训项目ID</param>
        /// <returns></returns>
        public bool CheckTrainingItemIsCanEdit(Guid trainingItemID)
        {
            bool isCanEdit = false;
            int totalRecords = -1;
            string whereSQL = " AND TrainingItemID='" + trainingItemID.ToString() + "'";
            whereSQL += " AND ItemStatus = '10' AND IsIssue = '0'";
            DataTable dt = GetPagedList(1, 1, "", whereSQL, out totalRecords);
            if (dt.Rows.Count == 1)
                isCanEdit = true;
            return isCanEdit;
        }



        #endregion

        #region 数据查询方法，如：查询项目列表等


        /// <summary>
        /// 获取某个组织机构下的所有项目列表,默认按项目的创建时间倒序排序
        /// </summary>
        /// <param name="orgID">所属机构ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>DataTable</returns>
        public DataTable GetOrgTrainingItemAllListByOrgID(int orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetOrgTrainingItemListByCondition(orgID, "", pageIndex, pageSize, out totalRecords);
        }




        /// <summary>
        /// 获取某个组织机构下所有未归档的培训项目列表
        /// 按项目的创建时间倒序排序
        /// </summary>
        /// <param name="orgID">组织结构ID</param>
        /// <returns></returns>
        public DataTable getTrainingItemNoAchiveListByOrgID(int orgID)
        {
            int totalRecords = 0;
            string whereSQL = string.Format("AND ItemStatus!='90' AND OrgID='{0}'", orgID);
            string orderBy = " CreateTime DESC ";
            return GetPagedList(1, int.MaxValue - 1000, orderBy, whereSQL, out totalRecords);
        }


        /// <summary>
        /// 获取某个组织机构下的满足指定条件的项目列表,默认按项目的创建时间倒序排序
        /// </summary>
        /// <param name="orgID">所属机构ID</param>
        /// <param name="conditionSQL">以AND开头的查询条件</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>DataTable</returns>
        public DataTable GetOrgTrainingItemListByCondition(int orgID, string conditionSQL, int pageIndex, int pageSize, out int totalRecords)
        {
            conditionSQL += string.Format(" AND OrgID='{0}'", orgID);
            string orderBy = " CreateTime DESC ";
            return GetPagedList(pageIndex, pageSize, orderBy, conditionSQL, out totalRecords);
        }



        /// <summary>
        /// 获取某个组织机构下的指定时间下还能维护的项目列表,默认按项目的创建时间倒序排序
        /// 即满足如下条件：
        /// 1.项目状态为"待审核"
        /// 2.项目还没有发布
        /// 3.当前系统时间在项目的开始时间和结束时间之间（按日期）
        /// </summary>
        /// <param name="orgID">所属机构ID</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>DataTable</returns>
        public DataTable getTrainingItemListByOrgID(int orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            string whereSQL = MakeTrainingItemConditionSQLByOrgID(orgID, "", "0", "10");
            string orderBy = " [Tr_Item].CreateTime DESC ";
            return GetPagedList(pageIndex, pageSize, orderBy, whereSQL, out totalRecords);
        }


        /// <summary>
        /// 生成某个组织结构下指定状态的查询条件：
        /// 按当前系统时间：在项目的开始时间和结束时间之间
        /// </summary>
        /// <param name="orgID">组织机构ID,如果等于负数1（－1）则该条件不起左右</param>
        /// <param name="isUse">是否启用，如果是空串，则该条件不起作用</param>
        /// <param name="isIssue">是否发布，如果是空串，则该条件不起作用</param>
        /// <param name="itemStatus">项目状态，如果是空串，则该条件不起作用</param>
        /// <returns>以AND 开头的查询条件,每个字段都带表名[Tr_Item].为前缀</returns>
        public static string MakeTrainingItemConditionSQLByOrgID(int orgID, string isUse, string isIssue, string itemStatus)
        {
            string fieldSQLModal = " AND ([Tr_Item].[{0}] {1} '{2}') ";
            string whereSQL = "";
            string beginTime = System.DateTime.Now.ToString("yyyy-MM-dd");
            whereSQL += string.Format(fieldSQLModal, "ItemBeginTime", ">=", beginTime);
            string endTime = System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            whereSQL += string.Format(fieldSQLModal, "ItemEndTime", "<", endTime);
            if (orgID >= 0)
            {
                whereSQL += string.Format(fieldSQLModal, "OrgID", "=", orgID);
            }
            if (isUse != "")
            {
                whereSQL += string.Format(fieldSQLModal, "IsUse", "=", isUse);
            }
            if (isIssue != "")
            {
                whereSQL += string.Format(fieldSQLModal, "IsIssue", "=", isIssue);
            }
            if (isIssue != "")
            {
                whereSQL += string.Format(fieldSQLModal, "ItemStatus", "=", itemStatus);
            }

            return whereSQL;
        }

        /// <summary>
        /// 获取学员可以报名的项目列表(学员所在组织机构及其上级机构的项目）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Tr_Item> GetMySingnTrainingItemList(int pageIndex, int pageSize, out int totalRecords)
        {
            return GetMySingnTrainingItemList().PageList<Tr_Item>(pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// 获取学员可以报名的项目列表(学员所在组织机构及其上级机构的项目）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Tr_Item> GetMySingnTrainingItemList()
        {
            List<Tr_Item> source = new List<Tr_Item>();

            if (null == (source = (List<Tr_Item>)CacheHelper.Get(string.Format("{0}-{1}", UserContext.Current.OrganizationID, UserContext.Current.UserID))))
            {
                source = DAL.GetMySingnTrainingItemList(UserContext.Current.OrganizationID).ToList<Tr_Item>();
            }

            return source;
        }

        #endregion

        #region 提取项目学员，到期提醒项目开始学习（发送邮件,站内信)

        /// <summary>
        /// 获取项目到期提醒学员（发送邮件、站内信...)
        /// </summary>
        /// <param name="days">距离项目开始日期提前几天触发</param>
        /// <returns></returns>
        public DataTable GetNoticeItemStudent(double days)
        {
            return DAL.GetNoticeItemStudent(DateTime.Now.AddDays(days));
        }

        /// <summary>
        /// 根据项目ID，获取项目到期提醒学员（发送邮件、站内信...)
        /// </summary>
        /// <param name="days">项目ID</param>
        /// <returns></returns>
        public DataTable GetNoticeItemStudent(Guid trainingItemID)
        {
            return DAL.GetNoticeItemStudent(trainingItemID);
        }

        #endregion

        /// <summary>
        /// 培训项目复制
        /// </summary>
        public void ItemCopy(Tr_Item tr_Item)
        {
            try
            {
                DAL.ItemCopy(tr_Item);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.ToLower().IndexOf("Index_U_ItemCode".ToLower(), StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.TraningImplement_Item_ItemCodeExists);
                }
                else if (ex.Message.ToLower().IndexOf("Index_U_ItemName".ToLower(), StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.TraningImplement_Item_ItemNameExists);
                }

                throw ex;
            }
        }


        /// <summary>
        /// 修改项目费用
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="BudgetFee"></param>
        public void UpdateItemMoney(string itemID, string budgetFee, int payFrom)
        {
            DAL.UpdateItemMoney(itemID, budgetFee, payFrom);
        }

        /// <summary>
        /// 修改项目费用
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="BudgetFee"></param>
        public void UpdateItemMoney(string itemID, int payFrom)
        {
            DAL.UpdateItemMoney(itemID, payFrom);
        }

        /// <summary>
        /// 根据机构ID获取所有未付款项目列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orgID"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetNotPayItemList(int pageIndex, int pageSize, int orgID, out int totalRecords)
        {
            return DAL.GetNotPayItemList(pageIndex, pageSize, orgID, out totalRecords);
        }
    }
}
