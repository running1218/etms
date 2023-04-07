using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Components.StudyClass.API.Entity.StudyClass;
namespace ETMS.Components.StudyClass.Implement.BLL.StudyClass
{
    /// <summary>
    /// �༶Ⱥ��ѧԱ��ҵ���߼�
    /// </summary>
    public partial class Sty_ClassSubgroupStudentLogic
	{
 		/// <summary>
		/// �������
		/// </summary>
		public void Save(Sty_ClassSubgroupStudent sty_ClassSubgroupStudent)
		{
            try
            {
			    if(sty_ClassSubgroupStudent.SubgroupStudentID.IsEmpty())
                {
                    //��������ID��������ΪGUID��Ч��Int���������ݿ�����������
                    sty_ClassSubgroupStudent.SubgroupStudentID=sty_ClassSubgroupStudent.SubgroupStudentID.NewID();;
                    Add(sty_ClassSubgroupStudent);
                }
                else
                {
                    Update(sty_ClassSubgroupStudent);
                }
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣
                if (ex.Message.IndexOf("Index_U_Sty_ClassSubgroupStudentCode",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassSubgroupStudent.CodeExists");
                }
                else if (ex.Message.IndexOf("Index_U_Sty_ClassSubgroupStudentName",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassSubgroupStudent.NameExists");
                }
                //�����δ�������׳�
                throw ex;
            } 
		} 

        /// <summary>
		/// ɾ��
		/// </summary>
		protected void doRemove(Guid subgroupStudentID)
		{
            try
            {
			     DAL.Remove(subgroupStudentID);
                 //��¼ɾ����־������IDɾ����
                 BizLogHelper.Operate(subgroupStudentID,"ɾ��");
            }
           catch (System.Data.SqlClient.SqlException ex)
            {
                //ö������Լ���쳣��ת��Ϊҵ���쳣�������Ѿ�ʹ��
                if (ex.Message.IndexOf("FK_",StringComparison.InvariantCultureIgnoreCase) != -1)
                {
				    throw new ETMS.AppContext.BusinessException("StudyClass.Sty_ClassSubgroupStudent.DataUsed");
                } 
                //�����δ�������׳�
                throw ex;
            }  
		}

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="subgroupStudentIDs"></param>
        public void Delete(Guid[] subgroupStudentIDs)
        {
            foreach (Guid g in subgroupStudentIDs)
            {
                doRemove(g);
            }
        }

        /// <summary>
        /// ���ݰ༶����ID����ȡѧԱ�б�(��ҳ��
        /// </summary>
        /// <param name="classSubgroupID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Sty_ClassStudent> GetGroupStudentByGroupID(Guid classSubgroupID, int pageIndex, int pageSize, out int totalRecords)
        {
            var student = DAL.GetGroupStudentByGroupID(classSubgroupID).ToList<Sty_ClassStudent>(out totalRecords).PageList<Sty_ClassStudent>(pageIndex, pageSize);
            student.ForEach(s => s.ClassPostion = new Sty_ClassLogic().GetClassPositions(s.ClassID, s.UserID));
            return student;
        }

        /// <summary>
        /// ���ݰ༶����ID����ȡѧԱ�б�
        /// </summary>
        /// <param name="classSubgroupID"></param>
        /// <returns></returns>
        public List<Sty_ClassStudent> GetGroupStudentByGroupID(Guid classSubgroupID)
        {
            return DAL.GetGroupStudentByGroupID(classSubgroupID).ToList<Sty_ClassStudent>();
        }

        /// <summary>
        /// Ⱥ���ѡ��ѧԱ�б�
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="realName"></param>
        /// <param name="workNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Sty_ClassStudent> ChoseGroupStudentByClassID(Guid classID, string realName, string workNo, int pageIndex, int pageSize, out int totalRecords)
        {
            var classStudent = new Sty_ClassLogic().GetClassStudentList(classID);
            var groupStudent = DAL.GetGroupStudentByClassID(classID).ToList<Sty_ClassStudent>();
            var list = (from t in classStudent
                        where !(from c in groupStudent
                                select c.ClassStudentID).Contains(t.ClassStudentID)
                        orderby t.IsLeader descending
                        select t).ToList();
            list = list.Where(t => t.RealName.Contains(realName) && t.WorkerNo.Contains(workNo)).ToList();
            list = list.PageList<Sty_ClassStudent>(pageIndex, pageSize, out totalRecords);
            list.ForEach(t => t.ClassPostion = new Sty_ClassLogic().GetClassPositions(classID, t.UserID));
            return list;
        }

        /// <summary>
        /// ���ݰ༶ѧԱID��ȡ����Ⱥ��
        /// </summary>
        /// <param name="classStudentID"></param>
        /// <returns></returns>
        public Sty_ClassSubgroup GetGroupByClassStudentID(Guid classStudentID)
        {
            return DAL.GetGroupByClassStudentID(classStudentID).ToList<Sty_ClassSubgroup>().SingleOrDefault();
        }

        /// <summary>
        /// ���ݰ༶ѧԱID��ȡ����Ⱥ������
        /// </summary>
        /// <param name="classStudentID"></param>
        /// <returns></returns>
        public string GetGroupNameByClassStudentID(Guid classStudentID)
        {
            var group = GetGroupByClassStudentID(classStudentID);
            return group == null ? string.Empty : group.ClassSubgroupName;
        }
	}	
}

