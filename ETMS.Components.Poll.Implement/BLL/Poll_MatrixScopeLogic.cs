using System;
using System.Collections.Generic;

using System.Xml;
using ETMS.Components.Poll.API.Entity;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// �����������ַ�Χҵ����
    /// ˵����������õ�������ģʽ
    /// </summary>
    public class Poll_MatrixScopeLogic
    {
        #region ���Ա����
        /// <summary>
        /// xml�ļ�·��
        /// </summary>
        private static string s_XMLPath = "assembly://ETMS.Components.Poll.Implement/ETMS.Components.Poll.Implement.BLL/MatrixScope.xml";
        /// <summary>
        /// SingleFactory Instance
        /// </summary>
        private static Poll_MatrixScopeLogic s_FatoryInstance;
        /// <summary>
        /// �߳���
        /// </summary>
        private static object s_lockobj = new object();
        #endregion

        #region �����Ա����
        private XmlDocument m_XmlDoc;
        #endregion

        #region ����
        public List<Poll_MatrixScope> GetScopeList()
        {
            List<Poll_MatrixScope> ScopeList = new List<Poll_MatrixScope>();

            XmlNodeList nodes = m_XmlDoc.SelectNodes("/scopes/*");
            foreach (XmlNode node in nodes)
            {
                ScopeList.Add(new Poll_MatrixScope(Convert.ToInt32(node.Attributes["id"].Value), node.Attributes["name"].Value));
            }
            return ScopeList;
        }
        public Poll_MatrixScope GetScopeById(int id)
        {
            XmlNode node = m_XmlDoc.SelectSingleNode(string.Format("/scopes/scope[@id='{0}']", id));
            return new Poll_MatrixScope(Convert.ToInt32(node.Attributes["id"].Value), node.Attributes["name"].Value);
        }
        /// <summary>
        /// �������ַ�ΧID��ȡ����ϸ��
        /// </summary>
        /// <param name="id">���ַ�ΧID</param>
        /// <returns></returns>
        public List<string> GetScopeValues(int id)
        {
            XmlNodeList nodes = m_XmlDoc.SelectNodes(string.Format("/scopes/scope[@id='{0}']/*", id));
            List<string> Values = new List<string>();
            foreach (XmlNode node in nodes)
            {
                Values.Add(node.InnerText);
            }
            return Values;
        }
        /// <summary>
        /// �������ַ�Χʵ���ȡ����ϸ��
        /// </summary>
        /// <param name="id">���ַ�Χʵ��</param>
        /// <returns></returns>
        public List<string> GetScopeValues(Poll_MatrixScope matrixScope)
        {
            return GetScopeValues(matrixScope.ID);
        }
        #endregion

        #region ���캯��
        private Poll_MatrixScopeLogic()
        {
        }
        private Poll_MatrixScopeLogic(string xmlPath)
        {
            m_XmlDoc = new XmlDocument();
            Autumn.Core.IO.IResource resource = new Autumn.Core.IO.AssemblyResource(s_XMLPath);
            m_XmlDoc.Load(resource.InputStream);
        }
        #endregion

        #region �����෽��
        public static Poll_MatrixScopeLogic GetInstance()
        {
            if (s_FatoryInstance == null)
            {
                lock (s_lockobj)
                {
                    s_FatoryInstance = new Poll_MatrixScopeLogic(s_XMLPath);
                }
            }            
            return s_FatoryInstance;
        }
        #endregion

    }
}
