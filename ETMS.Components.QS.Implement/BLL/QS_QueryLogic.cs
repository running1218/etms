//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-23 11:37:38.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;

using ETMS.AppContext;
using ETMS.Utility.Logging;
using ETMS.Components.QS.API.Entity;
namespace ETMS.Components.QS.Implement.BLL
{
    /// <summary>
    /// 问卷调查表业务逻辑
    /// </summary>
    public partial class QS_QueryLogic
    {


        /// <summary>
        /// 重新设置某个问卷调查的模板状态
        /// </summary>
        /// <param name="queryID">要设置的问卷调查ID</param>
        /// <param name="isTemplate">true设置为模板,false取消设置</param>
        /// <param name="modifyUser">修改人</param>
        public void ReSetTemplate(Guid queryID, bool isTemplate, string modifyUser)
        {
            QS_Query entity = DAL.GetById(queryID);
            entity.IsTemplate = isTemplate;
            entity.ModifyUser = modifyUser;
            entity.ModifyTime = DateTime.Now;
            Save(entity, OperationAction.Edit);
        }


        /// <summary>
        /// 设置某个问卷调查的为模板
        /// </summary>
        /// <param name="queryID">要设置的问卷调查ID</param>
        /// <param name="modifyUser">设置人</param>
        public void SetTemplate(Guid queryID, string modifyUser)
        {
            QS_Query entity = DAL.GetById(queryID);
            if (entity.IsTemplate == true)
            {
                throw new ETMS.AppContext.BusinessException("设置模板不成功:该问卷调查已经是模板,或者该问卷调查不存在!");
            }
            else
            {
                ReSetTemplate(queryID, true, modifyUser);
            }
        }


        /// <summary>
        /// 取消某个问卷调查模板
        /// </summary>
        /// <param name="queryID">要取消的问卷调查ID</param>
        /// <param name="modifyUser">取消人</param>
        public void CancelTemplate(Guid queryID, string modifyUser)
        {
            QS_Query entity = DAL.GetById(queryID);
            if (entity.IsTemplate == false)
            {
                throw new ETMS.AppContext.BusinessException("取消模板不成功:该问卷调查不是模板,或者该问卷调查不存在!");
            }
            else
            {
                ReSetTemplate(queryID, false, modifyUser);
            }
        }


    

        /// <summary>
        /// 复制整个问卷调查
        /// </summary>
        /// <param name="queryID">要复制的问卷调查ID</param>
        /// <param name="createUser">操作人</param>
        /// <param name="createUserID">操作人ID</param>
        /// <returns>新创建的调查问卷ID</returns>
        public Guid CopyTemplate(Guid queryID, string createUser, int createUserID)
        {
            return DAL.CopyTemplate(queryID, createUser, createUserID);
        }


        /// <summary>
        /// 发布某个“问卷调查”
        /// </summary>
        /// <param name="queryID">“问卷调查”ID</param>
        /// <param name="isIssue">是否发布（0：不发布，1：发布）</param>
        /// <param name="issueUser">发布人</param>
        public void QS_Query_Issue(Guid queryID, int isIssue, string issueUser)
        {
            try
            {
                DAL.QS_Query_Issue(queryID, isIssue, issueUser);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }


        /// <summary>
        /// 批量发布问卷调查
        /// </summary>
        /// <param name="queryIDArray">要发布的问卷调查ID数组</param>
        public void BatchIssue(Guid[] queryIDArray, int isIssue, string issueUser)
        {
            //如果只发布一条
            if (queryIDArray.Length == 1)
            {
                QS_Query_Issue(queryIDArray[0], isIssue, issueUser);
                return;
            }
            int noSuccessNum = 0;
            foreach (Guid queryID in queryIDArray)
            {
                try
                {
                    QS_Query_Issue(queryID, isIssue, issueUser);
                }
                catch
                {
                    noSuccessNum++;
                }
            }
            if (noSuccessNum > 0)
            {
                string errorMsg = "发布完毕：当前要发布的记录数为“{0}”个，有“{1}”个发布不成功！！";
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsg, queryIDArray.Length, noSuccessNum));
            }

        }



        /// <summary>
        /// 问卷调查保存
        /// </summary>
        /// <param name="entity">问卷调查实体</param>
        /// <param name="action">操作方法：添加或者修改</param>
        public void Save(QS_Query entity, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                {
                    Add(entity);
                }
                else if (action == OperationAction.Edit)
                {
                    Update(entity);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 批量删除问卷调查
        /// </summary>
        /// <param name="queryIDArray">要删除的问卷调查ID数组</param>
        public void BatchDelete(Guid[] queryIDArray)
        {
            //如果只删除一条
            if (queryIDArray.Length == 1)
            {
                doRemove(queryIDArray[0]);
                return;
            }
            int noSuccessNum = 0;
            foreach (Guid queryID in queryIDArray)
            {
                try
                {
                    doRemove(queryID);
                }
                catch
                {
                    noSuccessNum++;
                }
            }
            if (noSuccessNum > 0)
            {
                string errorMsg = "删除完毕：当前要删除的记录数为“{0}”个，有“{1}”个删除不成功！！";
                throw new ETMS.AppContext.BusinessException(string.Format(errorMsg, queryIDArray.Length, noSuccessNum));
            }

        }


        /// <summary>
        /// 删除
        /// </summary>
        protected void doRemove(Guid queryID)
        {
            try
            {
                QS_Query entity = DAL.GetById(queryID);
                if (entity.IsPublish)
                {
                    throw new ETMS.AppContext.BusinessException("该问卷调查已经发布，不能删除！");
                }
                else
                {
                    DAL.Remove(queryID);
                    //记录删除日志（根据ID删除）
                    BizLogHelper.Operate(queryID, "删除");
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message.ToUpper();
                //枚举数据约束异常并转换为业务异常，数据已经使用
                if (errorMsg.IndexOf("FK_QS_QUERY_REF_201_QS_QUERY", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该问卷调查下已有“题目”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_QS_QUERY_REF_200_QS_QUERY", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该问卷调查已设置有“调查范围”，不能删除！");
                }
                else if (errorMsg.IndexOf("FK_QS_QUERY_REF_1973_QS_QUERY", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该问卷调查已有“调查结果”，不能删除！");
                }
                //如果仍未处理，则抛出
                throw ex;
            }
        }


        /// <summary>
        /// 根据标识问卷的XML文档
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <returns>xml文档</returns>
        public string CreateXMLByQueryID(Guid queryID)
        {
            return DAL.CreateXMLByQueryID(queryID);
        }

        /// <summary>
        /// 根据标识问卷的AnswerXML文档
        /// </summary>
        public String CreateAnswerXMLByQueryID(Guid queryID)
        {
            return DAL.CreateAnswerXMLByQueryID(queryID);
        }



    }


}

