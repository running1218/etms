using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;
using Autumn.Data.ORM.IBatis;
using ETMS.Components.Exam.API.Entity;
using ETMS.Components.Exam.API.Entity.ItemBank;
using ETMS.Utility;
namespace ETMS.Components.Exam.Implement.DAL.ItemBank
{
    public class QuestionSearchIBatisDao : ReadWriteDataMapperDaoSupport, IQuestionSearchDao
    {
        /// 运营商查询试题列表
        /// </summary>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        public IList<QuestionSearchResult> GetQuestionList(QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            StringBuilder sb = new StringBuilder(128);
            sb.Append("a.[IsDelete]=0 ");
            if (qs != null)
            {
                if (!String.IsNullOrEmpty(qs.QuestionTitle))
                    sb.AppendFormat("and a.QuestionTitle like '%{0}%' ", qs.QuestionTitle.ToSafeSQLValue());
                if (!String.IsNullOrEmpty(qs.KnowledgePoints))
                    sb.AppendFormat("and a.KnowledgePoints like '%{0}%' ", qs.KnowledgePoints.ToSafeSQLValue());
                if (qs.Type > 0)
                    sb.AppendFormat("and a.QuestionType={0} ", (int)qs.Type);
                if (!String.IsNullOrEmpty(qs.UserName))
                    sb.AppendFormat("and t.UserName like '%{0}%'  ", qs.UserName.ToSafeSQLValue());

                if (qs.AuditStatus != AuditType.Null)
                    sb.AppendFormat(" and a.AuditStatus={0}", Convert.ToInt16(qs.AuditStatus));
                if (qs.ShareStatus != ShareType.Null)
                    sb.AppendFormat(" and a.ShareStatus = {0}", Convert.ToInt32(qs.ShareStatus));
            }
            string strWhere = sb.ToString();

            StringBuilder dtName = new StringBuilder(128);
            dtName.Append(" TK_Question a left join KS_TreeCategory b on a.QuestionBankID=b.CategoryID ");
            dtName.Append("left join (select OrganizationID ID,OrganizationName UserName from Organizations ");
            dtName.Append("union select UserID ID,UserName from users)t on b.OwnerID=t.ID ");

            StringBuilder fdName = new StringBuilder(128);
            fdName.Append("a.QuestionID,");
            fdName.Append("a.QuestionTitle,");
            fdName.Append("a.QuestionType,");
            fdName.Append("a.ObjectID,");
            fdName.Append("a.Difficulty,");
            fdName.Append("a.KnowledgePoints,");
            fdName.Append("isnull(a.QuestionSize,0) QuestionSize,");
            fdName.Append("isnull(a.UpdatedDate,a.CreatedDate) UpdatedDate,");
            fdName.Append("a.CreatedUserID,");
            fdName.Append("a.CreatedDate,");
            fdName.Append("isnull(a.AuditStatus,0) AuditStatus,");
            fdName.Append("isnull(a.ShareStatus,0) ShareStatus,");
            fdName.Append("b.CategoryName,");
            fdName.Append("t.UserName");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            ht.Add("tableName", dtName.ToString());
            ht.Add("fields", fdName.ToString());
            ht.Add("sqlWhere", strWhere);
            ht.Add("groupField", "");
            ht.Add("orderField", "ISNULL(a.updateddate,a.CreatedDate) desc");
            ht.Add("pageIndex", pageNo);
            ht.Add("pageSize", pageSize);
            ht.Add("totalRecord", 0);

            dt = DataMapperClient_Read.QueryForDataTable("QuestionSearch.GetResults", ht);
            total = (int)ht["totalRecord"];
            List<QuestionSearchResult> list = new List<QuestionSearchResult>();
            QuestionSearchResult tmp = null;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tmp = new QuestionSearchResult();
                    tmp.QuestionID = SafeGuid(dr["QuestionID"].ToString());
                    tmp.QuestionTitle = Convert.ToString(dr["QuestionTitle"]);
                    tmp.QuestionType = (QuestionType)Convert.ToInt32(dr["QuestionType"]);
                    tmp.ObjectID = Convert.ToInt16(dr["ObjectID"]);
                    tmp.Difficulty = Convert.ToInt16(dr["Difficulty"]);
                    tmp.OwnerName = Convert.ToString(dr["UserName"]);
                    tmp.KnowledgePoints = Convert.ToString(dr["KnowledgePoints"]);
                    tmp.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    tmp.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                    tmp.CreatedUserID = int.Parse(dr["CreatedUserID"].ToString());
                    tmp.BankName = Convert.ToString(dr["CategoryName"]);
                    tmp.QuestionSize = Convert.ToInt32(dr["QuestionSize"]);
                    tmp.AuditStatus = Convert.ToInt32(dr["AuditStatus"]);
                    tmp.ShareStatus = Convert.ToInt32(dr["ShareStatus"]);
                    list.Add(tmp);
                }
            }
            return list;
        }

        /// <summary>
        /// 用户查询,知识点和标题or查找
        /// </summary>
        /// <param name="ownerID"></param>
        /// <param name="qs"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<QuestionSearchResult> SearchQuestions(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            StringBuilder sb = new StringBuilder(128);
            sb.Append("a.[IsDelete]=0 ");
            if (ownerID != Guid.Empty)
            {
                sb.AppendFormat("and b.OwnerID='{0}' ", ownerID);
            }
            if (qs != null)
            {
                if (qs.Type > 0)
                    sb.AppendFormat("and a.QuestionType={0} ", (int)qs.Type);
                if (qs.Difficulty > 0)
                    sb.AppendFormat("and a.Difficulty={0} ", qs.Difficulty);

                //知识点或者标题包含了字串
                string title = qs.QuestionTitle;
                string knowledge = qs.KnowledgePoints;
                if (!String.IsNullOrEmpty(title) && !String.IsNullOrEmpty(knowledge))
                {
                    sb.AppendFormat("and (a.QuestionTitle like '%{0}%' or a.KnowledgePoints like '%{1}%')",
                        title.ToSafeSQLValue(), knowledge.ToSafeSQLValue());
                }
                else
                {
                    if (!String.IsNullOrEmpty(title))
                        sb.AppendFormat("and a.QuestionTitle like '%{0}%' ", title.ToSafeSQLValue());
                    else if (!String.IsNullOrEmpty(knowledge))
                        sb.AppendFormat("and a.KnowledgePoints like '%{0}%' ", knowledge.ToSafeSQLValue());
                }


                if (qs.ObjectID > 0)
                    sb.AppendFormat("and a.ObjectID={0} ", qs.ObjectID);
                if (!String.IsNullOrEmpty(qs.Subject))
                    sb.AppendFormat("and a.Subject like '%{0}%' ", qs.Subject.ToSafeSQLValue());
                if (qs.QuestionBankID != Guid.Empty)
                    sb.AppendFormat("and a.QuestionBankID='{0}' ", qs.QuestionBankID);
                if ((int)qs.Source > 0)
                    sb.AppendFormat("and b.OwnerType='{0}' ", (int)qs.Source);
                if (qs.AuditStatus != AuditType.Null)
                    sb.AppendFormat(" and a.AuditStatus={0}", Convert.ToInt16(qs.AuditStatus));
                if (qs.ShareStatus != ShareType.Null)
                    sb.AppendFormat(" and a.ShareStatus = {0}", Convert.ToInt32(qs.ShareStatus));
            };
            string strWhere = sb.ToString();

            StringBuilder dtName = new StringBuilder(50);
            dtName.Append("TK_Question a left join KS_TreeCategory b on a.QuestionBankID=b.CategoryID");

            StringBuilder fdName = new StringBuilder(128);
            fdName.Append("a.QuestionID,");
            fdName.Append("a.QuestionTitle,");
            fdName.Append("a.QuestionType,");
            fdName.Append("a.ObjectID,");
            fdName.Append("a.Difficulty,");
            fdName.Append("a.[Subject],");
            fdName.Append("a.KnowledgePoints,");
            fdName.Append("isnull(a.QuestionSize,0) QuestionSize,");
            fdName.Append("isnull(a.UpdatedDate,a.CreatedDate) UpdatedDate,");
            fdName.Append("a.CreatedUserID,");
            fdName.Append("a.CreatedDate,");
            fdName.Append("isnull(a.AuditStatus,0) AuditStatus,");
            fdName.Append("isnull(a.ShareStatus,0) ShareStatus,");
            fdName.Append("b.OwnerID,");
            fdName.Append("isnull(b.OwnerType,0) OwnerType, ");
            fdName.Append("b.CategoryName");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            ht.Add("tableName", dtName.ToString());
            ht.Add("fields", fdName.ToString());
            ht.Add("sqlWhere", strWhere);
            ht.Add("groupField", "");
            ht.Add("orderField", "ISNULL(a.updateddate,a.CreatedDate) desc");
            ht.Add("pageIndex", pageNo);
            ht.Add("pageSize", pageSize);
            ht.Add("totalRecord", 0);

            //返回
            dt = DataMapperClient_Read.QueryForDataTable("QuestionSearch.GetResults", ht);
            total = (int)ht["totalRecord"];

            //设置返回数据
            List<QuestionSearchResult> list = new List<QuestionSearchResult>();
            QuestionSearchResult tmp = null;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tmp = new QuestionSearchResult();
                    tmp.QuestionID = SafeGuid(dr["QuestionID"].ToString());
                    tmp.QuestionTitle = Convert.ToString(dr["QuestionTitle"]);
                    tmp.QuestionType = (QuestionType)Convert.ToInt32(dr["QuestionType"]);
                    tmp.ObjectID = Convert.ToInt16(dr["ObjectID"]);
                    tmp.Difficulty = Convert.ToInt16(dr["Difficulty"]);
                    tmp.Subject = Convert.ToString(dr["Subject"]);
                    tmp.KnowledgePoints = Convert.ToString(dr["KnowledgePoints"]);
                    tmp.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    tmp.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                    tmp.CreatedUserID = int.Parse(dr["CreatedUserID"].ToString());
                    tmp.BankName = Convert.ToString(dr["CategoryName"]);
                    tmp.OwnerID = SafeGuid(dr["OwnerID"].ToString());
                    tmp.OwnerType = Convert.ToInt16(dr["OwnerType"]);
                    tmp.AuditStatus = Convert.ToInt32(dr["AuditStatus"]);
                    tmp.ShareStatus = Convert.ToInt32(dr["ShareStatus"]);
                    tmp.QuestionSize = Convert.ToInt32(dr["QuestionSize"]);
                    list.Add(tmp);
                }
            }
            return list;
        }


        /// <summary>
        /// 得到查询后的试题列表
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> GetQuestionList(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            StringBuilder sb = new StringBuilder(128);
            //sb.Append("a.[IsDelete]=0 and a.AuditStatus=99 ");
            sb.Append("a.[IsDelete]=0 ");
            if (ownerID != Guid.Empty)
            {
                sb.AppendFormat("and b.OwnerID='{0}' ", ownerID);
            }
            if (qs != null)
            {
                if (qs.Type > 0)
                    sb.AppendFormat("and a.QuestionType={0} ", (int)qs.Type);
                if (qs.Difficulty > 0)
                    sb.AppendFormat("and a.Difficulty={0} ", qs.Difficulty);
                if (!String.IsNullOrEmpty(qs.QuestionTitle))
                    sb.AppendFormat("and a.QuestionTitle like '%{0}%' ", qs.QuestionTitle.ToSafeSQLValue());
                if (!String.IsNullOrEmpty(qs.KnowledgePoints))
                    sb.AppendFormat("and a.KnowledgePoints like '%{0}%' ", qs.KnowledgePoints.ToSafeSQLValue());
                if (qs.ObjectID > 0)
                    sb.AppendFormat("and a.ObjectID={0} ", qs.ObjectID);
                if (!String.IsNullOrEmpty(qs.Subject))
                    sb.AppendFormat("and a.Subject like '%{0}%' ", qs.Subject.ToSafeSQLValue());
                if (qs.QuestionBankID != Guid.Empty)
                    sb.AppendFormat("and a.QuestionBankID='{0}' ", qs.QuestionBankID);
                if ((int)qs.Source > 0)
                    sb.AppendFormat("and b.OwnerType='{0}' ", (int)qs.Source);
                if (qs.AuditStatus != AuditType.Null)
                    sb.AppendFormat(" and a.AuditStatus={0}", Convert.ToInt16(qs.AuditStatus));
                if (qs.ShareStatus != ShareType.Null)
                    sb.AppendFormat(" and a.ShareStatus = {0}", Convert.ToInt32(qs.ShareStatus));
            }
            string strWhere = sb.ToString();

            StringBuilder dtName = new StringBuilder(50);
            dtName.Append("TK_Question a left join KS_TreeCategory b on a.QuestionBankID=b.CategoryID");

            StringBuilder fdName = new StringBuilder(128);
            fdName.Append("a.QuestionID,");
            fdName.Append("a.QuestionTitle,");
            fdName.Append("a.QuestionType,");
            fdName.Append("a.ObjectID,");
            fdName.Append("a.Difficulty,");
            fdName.Append("a.[Subject],");
            fdName.Append("a.KnowledgePoints,");
            fdName.Append("isnull(a.QuestionSize,0) QuestionSize,");
            fdName.Append("isnull(a.UpdatedDate,a.CreatedDate) UpdatedDate,");
            fdName.Append("a.CreatedUserID,");
            fdName.Append("a.CreatedDate,");
            fdName.Append("isnull(a.AuditStatus,0) AuditStatus,");
            fdName.Append("isnull(a.ShareStatus,0) ShareStatus,");
            fdName.Append("b.OwnerID,");
            fdName.Append("isnull(b.OwnerType,0) OwnerType, ");
            fdName.Append("b.CategoryName");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            ht.Add("tableName", dtName.ToString());
            ht.Add("fields", fdName.ToString());
            ht.Add("sqlWhere", strWhere);
            ht.Add("groupField", "");
            ht.Add("orderField", "ISNULL(a.updateddate,a.CreatedDate) desc");
            ht.Add("pageIndex", pageNo);
            ht.Add("pageSize", pageSize);
            ht.Add("totalRecord", 0);

            //返回
            dt = DataMapperClient_Read.QueryForDataTable("QuestionSearch.GetResults", ht);
            total = (int)ht["totalRecord"];

            //设置返回数据
            List<QuestionSearchResult> list = new List<QuestionSearchResult>();
            QuestionSearchResult tmp = null;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tmp = new QuestionSearchResult();
                    tmp.QuestionID = SafeGuid(dr["QuestionID"].ToString());
                    tmp.QuestionTitle = Convert.ToString(dr["QuestionTitle"]);
                    tmp.QuestionType = (QuestionType)Convert.ToInt32(dr["QuestionType"]);
                    tmp.ObjectID = Convert.ToInt16(dr["ObjectID"]);
                    tmp.Difficulty = Convert.ToInt16(dr["Difficulty"]);
                    tmp.Subject = Convert.ToString(dr["Subject"]);
                    tmp.KnowledgePoints = Convert.ToString(dr["KnowledgePoints"]);
                    tmp.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    tmp.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                    tmp.CreatedUserID = int.Parse(dr["CreatedUserID"].ToString());
                    tmp.BankName = Convert.ToString(dr["CategoryName"]);
                    tmp.OwnerID = SafeGuid(dr["OwnerID"].ToString());
                    tmp.OwnerType = Convert.ToInt16(dr["OwnerType"]);
                    tmp.AuditStatus = Convert.ToInt32(dr["AuditStatus"]);
                    tmp.ShareStatus = Convert.ToInt32(dr["ShareStatus"]);
                    tmp.QuestionSize = Convert.ToInt32(dr["QuestionSize"]);
                    list.Add(tmp);
                }
            }
            return list;
        }

        /// <summary>
        /// 得到查询后的试题列表-课程设计
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="qs">查询实体</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> GetQuestionListInCourse(Guid ownerID, QuestionSearch qs, int pageSize, int pageNo, out int total)
        {
            StringBuilder sb = new StringBuilder(128);
            //sb.Append("a.[IsDelete]=0 and a.AuditStatus=99 ");
            sb.Append("a.[IsDelete]=0 ");
            if (ownerID != Guid.Empty)
            {
                sb.AppendFormat("and b.OwnerID='{0}' ", ownerID);
            }
            if (qs != null)
            {
                if (qs.Type > 0 & qs.Type != QuestionType.Group & qs.Type != QuestionType.Match)
                    sb.AppendFormat("and a.QuestionType={0} ", (int)qs.Type);
                else
                    sb.AppendFormat("and a.QuestionType!={0} and a.QuestionType!={1}", 6, 7);
                if (qs.Difficulty > 0)
                    sb.AppendFormat("and a.Difficulty={0} ", qs.Difficulty);
                if (!String.IsNullOrEmpty(qs.QuestionTitle))
                    sb.AppendFormat("and a.QuestionTitle like '%{0}%' ", qs.QuestionTitle.ToSafeSQLValue());
                if (!String.IsNullOrEmpty(qs.KnowledgePoints))
                    sb.AppendFormat("and a.KnowledgePoints like '%{0}%' ", qs.KnowledgePoints.ToSafeSQLValue());
                if (qs.ObjectID > 0)
                    sb.AppendFormat("and a.ObjectID={0} ", qs.ObjectID);
                if (!String.IsNullOrEmpty(qs.Subject))
                    sb.AppendFormat("and a.Subject like '%{0}%' ", qs.Subject.ToSafeSQLValue());
                if (qs.QuestionBankID != Guid.Empty)
                    sb.AppendFormat("and a.QuestionBankID='{0}' ", qs.QuestionBankID);
                if ((int)qs.Source > 0)
                    sb.AppendFormat("and b.OwnerType='{0}' ", (int)qs.Source);
                if (qs.AuditStatus != AuditType.Null)
                    sb.AppendFormat(" and a.AuditStatus={0}", Convert.ToInt16(qs.AuditStatus));
                if (qs.ShareStatus != ShareType.Null)
                    sb.AppendFormat(" and a.ShareStatus = {0}", Convert.ToInt32(qs.ShareStatus));
            }
            string strWhere = sb.ToString();

            StringBuilder dtName = new StringBuilder(50);
            dtName.Append("TK_Question a left join KS_TreeCategory b on a.QuestionBankID=b.CategoryID");

            StringBuilder fdName = new StringBuilder(128);
            fdName.Append("a.QuestionID,");
            fdName.Append("a.QuestionTitle,");
            fdName.Append("a.QuestionType,");
            fdName.Append("a.ObjectID,");
            fdName.Append("a.Difficulty,");
            fdName.Append("a.[Subject],");
            fdName.Append("a.KnowledgePoints,");
            fdName.Append("isnull(a.QuestionSize,0) QuestionSize,");
            fdName.Append("isnull(a.UpdatedDate,a.CreatedDate) UpdatedDate,");
            fdName.Append("a.CreatedUserID,");
            fdName.Append("a.CreatedDate,");
            fdName.Append("isnull(a.AuditStatus,0) AuditStatus,");
            fdName.Append("isnull(a.ShareStatus,0) ShareStatus,");
            fdName.Append("b.OwnerID,");
            fdName.Append("isnull(b.OwnerType,0) OwnerType, ");
            fdName.Append("b.CategoryName");

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            ht.Add("tableName", dtName.ToString());
            ht.Add("fields", fdName.ToString());
            ht.Add("sqlWhere", strWhere);
            ht.Add("groupField", "");
            ht.Add("orderField", "ISNULL(a.updateddate,a.CreatedDate) desc");
            ht.Add("pageIndex", pageNo);
            ht.Add("pageSize", pageSize);
            ht.Add("totalRecord", 0);

            //返回
            dt = DataMapperClient_Read.QueryForDataTable("QuestionSearch.GetResults", ht);
            total = (int)ht["totalRecord"];

            //设置返回数据
            List<QuestionSearchResult> list = new List<QuestionSearchResult>();
            QuestionSearchResult tmp = null;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tmp = new QuestionSearchResult();
                    tmp.QuestionID = SafeGuid(dr["QuestionID"].ToString());
                    tmp.QuestionTitle = Convert.ToString(dr["QuestionTitle"]);
                    tmp.QuestionType = (QuestionType)Convert.ToInt32(dr["QuestionType"]);
                    tmp.ObjectID = Convert.ToInt16(dr["ObjectID"]);
                    tmp.Difficulty = Convert.ToInt16(dr["Difficulty"]);
                    tmp.Subject = Convert.ToString(dr["Subject"]);
                    tmp.KnowledgePoints = Convert.ToString(dr["KnowledgePoints"]);
                    tmp.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                    tmp.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                    tmp.CreatedUserID = int.Parse(dr["CreatedUserID"].ToString());
                    tmp.BankName = Convert.ToString(dr["CategoryName"]);
                    tmp.OwnerID = SafeGuid(dr["OwnerID"].ToString());
                    tmp.OwnerType = Convert.ToInt16(dr["OwnerType"]);
                    tmp.AuditStatus = Convert.ToInt32(dr["AuditStatus"]);
                    tmp.ShareStatus = Convert.ToInt32(dr["ShareStatus"]);
                    tmp.QuestionSize = Convert.ToInt32(dr["QuestionSize"]);
                    list.Add(tmp);
                }
            }
            return list;
        }


        /// <summary>
        /// 得到查询后的试题列表
        /// </summary>
        /// <param name="ownerID">当前用户ID</param>
        /// <param name="questionBankID">分类ID</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageNo">页码</param>
        /// <param name="total">总记录数</param>
        /// <returns>试题列表</returns>
        public IList<QuestionSearchResult> GetQuestionList(Guid ownerID, Guid questionBankID, int pageSize, int pageNo, out int total)
        {
            QuestionSearch qs = new QuestionSearch();
            qs.QuestionBankID = questionBankID;
            return GetQuestionList(ownerID, qs, pageSize, pageNo, out total);
        }
        public IList<QuestionSearchResult> GetAuditedQuestionList(Guid ownerID, Guid questionBankID, int pageSize, int pageNo, out int total)
        {
            QuestionSearch qs = new QuestionSearch();
            qs.QuestionBankID = questionBankID;
            qs.AuditStatus = AuditType.Approve;
            return GetQuestionList(ownerID, qs, pageSize, pageNo, out total);
        }
        /// <summary>
        /// 返回正确的GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private Guid SafeGuid(string str)
        {
            if (String.IsNullOrEmpty(str) || str.Length < 32 || str.Length > 64)
            {
                return Guid.Empty;
            }
            try
            {
                return new Guid(str);
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
