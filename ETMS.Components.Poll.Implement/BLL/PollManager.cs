using System;
using System.Text;
using System.Linq;
using ETMS.Components.Poll.API.Entity;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
namespace ETMS.Components.Poll.Implement.BLL
{
    /// <summary>
    /// �ʾ������ͼ������
    /// </summary>
    public abstract class PollManager
    {
        private static Poll_AnswerResultLogic AnswerResultLogic = new Poll_AnswerResultLogic();
        private static Poll_QueryResultLogic QueryResultLogic = new Poll_QueryResultLogic();
        private static Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
        private static Poll_UserResourceQueryResultLogic BatchLogic = new Poll_UserResourceQueryResultLogic();

        #region ·����Ϣ

        /// <summary>
        /// Ƥ������·����Ϣ
        /// </summary>
        /// <param name="themeName">Ƥ���ļ�����</param>
        /// <returns>Ƥ������·����Ϣ</returns>
        private static string GetThemePhyPath(string themeName)
        {
            return string.Format(@"{0}Poll\Theme\{1}.xsl", AppDomain.CurrentDomain.BaseDirectory, themeName);
        }
        #endregion

        #region �����ӿ�
        /// <summary>
        /// ��ǰҳ������ʾ�������ͼ
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        public static StringBuilder GetResponseView(int queryID, string userName, string userType, string resourceType, string resourceCode)
        {
            Poll_Query query = new Poll_QueryLogic().GetById(queryID);

            if (query.Status == 0)
            {
                //���ʾ��Ѿ�ͣ�ã�����ʾ
                throw new ETMS.AppContext.BusinessException("�ʾ�δ���ã�");
            }
            if (!query.IsPublish)
            {
                //�ʾ�δ����������ʾ
                throw new ETMS.AppContext.BusinessException("�ʾ�δ������");
            }
            if (query.BeginTime.CompareTo(DateTime.Now) > 0 || query.EndTime.AddDays(1).CompareTo(DateTime.Now) < 0)
            {
                //���ʾ��ѵ�����ʱ�䣬����ʾ
                throw new ETMS.AppContext.BusinessException("�����ѹ���ֹʱ�䣡");
            }
            Poll_UserResourceQueryResultLogic userResourceQueryLogic = new Poll_UserResourceQueryResultLogic();
            if (!query.IsRepeat && userResourceQueryLogic.IsHasJoined(queryID, userName, userType, resourceType, resourceCode))
            {
                //���ʾ������ε����Ҵ��û��Ѿ�������ʾ�����ʾ
                throw new ETMS.AppContext.BusinessException("���Ѿ�Ͷ��Ʊ�ˣ���л����֧�֣�");
            }
            else
            {
                return GetResponseViewPreView(queryID);
            }
        }
        /// <summary>
        /// �ʾ�������ͼԤ��
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <returns></returns>
        public static StringBuilder GetResponseViewPreView(int queryID)
        {
            return CreateView(queryID, null);
        }

        /// <summary>
        /// �ʾ�������ͼԤ��
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <returns></returns>
        public static StringBuilder GetResponseViewPreView(int queryID, int batchID)
        {
            return CreateView(queryID, null, batchID);
        }

        /// <summary>
        ///  ��ǰҳ������ʾ�ͳ�ƽ����ͼ
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        public static StringBuilder GetResponseStatResultView(int queryID, string resourceType, string resourceCode)
        {
            return CreateStaticResultView(queryID, null, resourceType, resourceCode);
        }

        /// <summary>
        /// ��ǰҳ������ʾ�鿴��ͼ
        /// </summary>
        /// <param name="queryID"></param>
        /// <param name="resourceType"></param>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        public static StringBuilder GetResponsePollView(int queryID, string resourceType, string resourceCode)
        {
            return CreateStaticPollView(queryID, null, resourceType, resourceCode);
        }

        /// <summary>
        /// ��ǰҳ������ʾ�ͳ�ƽ��������ͼ
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <param name="resourceType">��Դ����</param>
        /// <param name="resourceCode">��ԴΨһ����</param>
        /// <returns>HTML���ݼ�¼��</returns>
        public static StringBuilder GetStaticResultExportView(int queryID, string resourceType, string resourceCode)
        {
            return CreateStaticResultExportView(queryID, null, resourceType, resourceCode);
        }

        /// <summary>
        /// ��ǰҳ������û��ʾ���������ͼ
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <returns>HTML���ݼ�¼��</returns>
        public static StringBuilder CreateAnswerXMLOfUserView(int batchID)
        {
            return CreateAnswerXMLOfUserView(batchID, null);
        }

        /// <summary>
        /// �ύ�ʾ����������
        /// </summary>
        /// <param name="doc">�ʾ������XML��</param>
        /// <param name="userName">�û���</param>
        /// <param name="userType">�û�����</param>
        public static void ViewSubmitResultSave(XmlDocument doc, string userName, string userType)
        {
            string ResourceType, ResourceCode;
            int QueryID;
            ResourceCode = doc.SelectSingleNode("/Query/ResourceCode").InnerXml;
            ResourceType = doc.SelectSingleNode("/Query/ResourceType").InnerXml;
            QueryID = Convert.ToInt32(doc.SelectSingleNode("/Query/QueryID").InnerXml);

            if (string.IsNullOrEmpty(ResourceCode) || string.IsNullOrEmpty(ResourceType))
            {
                throw new ETMS.AppContext.BusinessException("ҳ�������Ч��");
            }
            /*
             * debug: 2008-7-3(�ظ��ύ����)
            */
            Poll_UserResourceQueryResultLogic userResourceQueryLogic = new Poll_UserResourceQueryResultLogic();
            if (!userResourceQueryLogic.IsAllowMultJoin(QueryID, userName, userType, ResourceType, ResourceCode))
            {
                throw new ETMS.AppContext.BusinessException("���Ѿ�Ͷ��Ʊ�ˣ���л����֧�֣�");
            }
            //��ȡ�û��ύ���κ�
            Poll_UserResourceQueryResult tb_r_UserResourceQueryResult = new Poll_UserResourceQueryResult();
            tb_r_UserResourceQueryResult.CreateTime = DateTime.Now;
            tb_r_UserResourceQueryResult.QueryID = QueryID;
            tb_r_UserResourceQueryResult.UserName = userName;
            tb_r_UserResourceQueryResult.UserType = userType;
            tb_r_UserResourceQueryResult.ResourceTypeCode = ResourceType;
            tb_r_UserResourceQueryResult.ResourceCode = ResourceCode;
#if EnableDTC
            using (TransactionScope ts = new TransactionScope())
            {
#endif

            BatchLogic.Add(tb_r_UserResourceQueryResult);

            XmlNodeList titles = doc.SelectNodes("//Title");
            foreach (XmlNode title in titles)
            {
                int TitleID = Convert.ToInt32(title.SelectSingleNode("TitleID").InnerXml);
                int TitleTypeID = Convert.ToInt32(title.SelectSingleNode("TitleTypeID").InnerXml);
                XmlNodeList answers = title.SelectNodes(".//Answer");
                int OptionID = 0;
                int HeaderID = 0;
                string Other = "";
                switch (TitleTypeID)
                {
                    case 1://��ѡ����
                    case 2://��ѡ����
                        if (!string.IsNullOrEmpty(answers[0].ChildNodes[0].InnerXml))
                        {
                            foreach (string optionstr in answers[0].ChildNodes[0].InnerXml.Split(','))
                            {
                                if (string.IsNullOrEmpty(optionstr))
                                    continue;
                                OptionID = Convert.ToInt32(optionstr);
                                QueryResultLogic.Add(new Poll_QueryResult()
                                {
                                    BatchID = tb_r_UserResourceQueryResult.BatchID,
                                    OptionID = OptionID,
                                    HeadID = 0,
                                    OtherText = Other,
                                });
                            }
                        }
                        if (!string.IsNullOrEmpty(answers[0].ChildNodes[1].InnerXml))//����
                        {
                            string[] strs = answers[0].ChildNodes[1].InnerXml.Split('|');
                            OptionID = Convert.ToInt32(strs[0]);
                            Other = strs[1];
                            QueryResultLogic.Add(new Poll_QueryResult()
                            {
                                BatchID = tb_r_UserResourceQueryResult.BatchID,
                                OptionID = OptionID,
                                HeadID = 0,
                                OtherText = Other,
                            });
                        }
                        break;
                    case 3://���󱣴�
                        foreach (XmlNode answer in answers)
                        {
                            OptionID = Convert.ToInt32(answer.ChildNodes[0].InnerXml);
                            HeaderID = Convert.ToInt32(answer.ChildNodes[1].InnerXml);
                            QueryResultLogic.Add(new Poll_QueryResult()
                            {
                                BatchID = tb_r_UserResourceQueryResult.BatchID,
                                OptionID = OptionID,
                                HeadID = HeaderID,
                                OtherText = Other,
                            });
                        }
                        break;
                    case 4://����Ᵽ��
                        AnswerResultLogic.Add(new Poll_AnswerResult()
                        {
                            BatchID = tb_r_UserResourceQueryResult.BatchID,
                            TitleID = TitleID,
                            Answer = answers[0].ChildNodes[1].InnerXml
                        });
                        break;
                }
            }
#if EnableDTC
            ts.Complete();
                }
#endif

        }
        #endregion

        #region Helper

        /// <summary>
        /// �����ʾ������ͼ��Ӧ�Ļ����ļ�
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <param name="themeName">���ɴ��ļ�ʹ�õ�Ƥ����Ĭ�ϣ�default��</param>
        private static StringBuilder CreateView(int queryID, string themeName)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "default";
            }
            Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(QueryLogic.CreateXMLByQueryID(queryID));
            //1������XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //�ʾ�𰸶�Ӧ��xml�ṹ
            return writer.Replace("$$", QueryLogic.CreateAnswerXMLByQueryID(queryID));

        }
        /// <summary>
        /// �����ʾ������ͼ��Ӧ�Ļ����ļ�
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <param name="themeName">���ɴ��ļ�ʹ�õ�Ƥ����Ĭ�ϣ�default��</param>
        private static StringBuilder CreateView(int queryID, string themeName, int batchID)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "detailResult";
            }
            Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(QueryLogic.CreateAnswerXMLByQueryID(queryID, batchID));
            //1������XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //�ʾ�𰸶�Ӧ��xml�ṹ
            return writer;

        }
        /// <summary>
        /// �û������б�
        /// </summary>
        /// <param name="queryID"></param>
        /// <returns></returns>
        public static StringBuilder CreateResltListXMLByQueryID(int queryID, string themeName)
        {
            int totalCount = 0;
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "queryAnswerList";
            }
            Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
            StringBuilder writer = new StringBuilder();
            var x = BatchLogic.GetEntityList(1, int.MaxValue-1, string.Format("QueryID='{0}'", queryID), "", out totalCount).Select(p => p.BatchID);
            foreach (var i in x)
            {
                writer.Append(QueryLogic.CreateResltListXMLByQueryID(queryID, i));
            }
            //1������XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //�ʾ�𰸶�Ӧ��xml�ṹ
            return writer;
        }

        /// <summary>
        /// �����ʾ����ͳ�ƽ����Ӧ�Ļ����ļ�
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <param name="themeName">���ɴ��ļ�ʹ�õ�Ƥ����Ĭ�ϣ�result��</param>
        /// <param name="resourceType">��Դ����</param>
        /// <param name="resourceCode">��ԴΨһ����</param>
        private static StringBuilder CreateStaticResultView(int queryID, string themeName, string resourceType, string resourceCode)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "result";
            }
            Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(ResourceQueryLogic.CreateStatResultXMLByQueryID(queryID, resourceType, resourceCode));
            //����XSLTTransferResult
            return TransXMLByXSLT(writer, GetThemePhyPath(themeName));
        }

        /// <summary>
        /// �����ʾ����ͳ�ƽ����Ӧ�Ļ����ļ�
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <param name="themeName">���ɴ��ļ�ʹ�õ�Ƥ����Ĭ�ϣ�result��</param>
        /// <param name="resourceType">��Դ����</param>
        /// <param name="resourceCode">��ԴΨһ����</param>
        private static StringBuilder CreateStaticPollView(int queryID, string themeName, string resourceType, string resourceCode)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "view";
            }
            Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(QueryLogic.CreateXMLByQueryID(queryID));
            //1������XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //�ʾ�𰸶�Ӧ��xml�ṹ
            return writer;
        }

        /// <summary>
        /// �����ʾ����ͳ�ƽ��������Ӧ��HTML����
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <param name="themeName">�ʾ����ͳ�ƽ��������ӦƤ����Ĭ�ϣ�resultexport��</param>
        /// <param name="resourceType">��Դ����</param>
        /// <param name="resourceCode">��ԴΨһ����</param>
        /// <returns>HTML���ݼ�¼��</returns>
        private static StringBuilder CreateStaticResultExportView(int queryID, string themeName, string resourceType, string resourceCode)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "resultexport";
            }
            Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(ResourceQueryLogic.CreateStatResultXMLExportByQueryID(queryID, resourceType, resourceCode));
            return TransXMLByXSLT(writer, GetThemePhyPath(themeName));
        }

        /// <summary>
        /// �����û��ʾ���������ͼ
        /// </summary>
        /// <param name="queryID">�ʾ�ID</param>
        /// <param name="themeName">�û��ʾ�������Ƥ����Ĭ�ϣ�detail��</param>
        /// <returns>HTML���ݼ�¼��</returns>
        public static StringBuilder CreateAnswerXMLOfUserView(int batchID, string themeName)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "detail";
            }
            Poll_UserResourceQueryResultLogic UserResourceQueryLogic = new Poll_UserResourceQueryResultLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(UserResourceQueryLogic.CreateAnswerXMLOfUserByQueryID(batchID));
            return TransXMLByXSLT(writer, GetThemePhyPath(themeName));
        }

        /// <summary>
        /// XMLͨ��XSLת��ΪHTMLҳ������
        /// </summary>
        /// <param name="writer">xml���ݼ�¼��</param>
        /// <param name="xsltpath">xsl�ļ�����·��</param>
        /// <returns>HTMLҳ�����ݼ�¼��</returns>
        private static StringBuilder TransXMLByXSLT(StringBuilder writer, string xsltpath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(writer.ToString());   //����XML   

            XslCompiledTransform trans = new XslCompiledTransform();
            trans.Load(xsltpath);   //����XSL   

            using (StringWriter sw = new StringWriter())
            {
                trans.Transform(doc, null, sw);
                return sw.GetStringBuilder();
                //return sw.GetStringBuilder().Replace("$Root$", ApplicationPath).Replace("^$^", "<br />");
            }
        }

        ///// <summary>
        ///// �Զ������������ʾ��Ӧ��Html�ļ�
        ///// </summary>
        ///// <param name="queryID"></param>
        ///// <param name="type">1���ʾ������⣬2���ʾ����ͳ�ƽ��</param>
        ///// <param name="resourceType">��Դ����</param>
        ///// <param name="resourceCode">��ԴΨһ����</param>
        //private static void AutoCreateHtmlCode(int queryID, int type, string resourceType, string resourceCode)
        //{
        //    switch (type)
        //    {
        //        case 1://�ʾ�������
        //            if (File.Exists(GetQuestionHtmlCodePhyPath(queryID)))
        //            {
        //                return;
        //            }
        //            CreateView(queryID, null);
        //            break;
        //        case 2://�ʾ����ͳ�ƽ��
        //            string FilePath = GetStatResultHtmlCodePhyPath(queryID, resourceType, resourceCode);
        //            if (File.Exists(FilePath) && File.GetLastWriteTime(FilePath).Subtract(DateTime.Now).TotalMinutes <= StatResultRefresMinutes)
        //            {
        //                return;
        //            }
        //            CreateStaticResultView(queryID, null, resourceType, resourceCode);
        //            break;
        //    }

        //}


        #endregion
    }
}
