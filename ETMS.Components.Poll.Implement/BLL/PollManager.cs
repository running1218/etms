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
    /// 问卷调查视图管理器
    /// </summary>
    public abstract class PollManager
    {
        private static Poll_AnswerResultLogic AnswerResultLogic = new Poll_AnswerResultLogic();
        private static Poll_QueryResultLogic QueryResultLogic = new Poll_QueryResultLogic();
        private static Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
        private static Poll_UserResourceQueryResultLogic BatchLogic = new Poll_UserResourceQueryResultLogic();

        #region 路径信息

        /// <summary>
        /// 皮肤绝对路径信息
        /// </summary>
        /// <param name="themeName">皮肤文件名称</param>
        /// <returns>皮肤绝对路径信息</returns>
        private static string GetThemePhyPath(string themeName)
        {
            return string.Format(@"{0}Poll\Theme\{1}.xsl", AppDomain.CurrentDomain.BaseDirectory, themeName);
        }
        #endregion

        #region 公开接口
        /// <summary>
        /// 当前页面输出问卷作答视图
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        public static StringBuilder GetResponseView(int queryID, string userName, string userType, string resourceType, string resourceCode)
        {
            Poll_Query query = new Poll_QueryLogic().GetById(queryID);

            if (query.Status == 0)
            {
                //此问卷已经停用，则提示
                throw new ETMS.AppContext.BusinessException("问卷未启用！");
            }
            if (!query.IsPublish)
            {
                //问卷未发布，则提示
                throw new ETMS.AppContext.BusinessException("问卷未发布！");
            }
            if (query.BeginTime.CompareTo(DateTime.Now) > 0 || query.EndTime.AddDays(1).CompareTo(DateTime.Now) < 0)
            {
                //此问卷已到结束时间，则提示
                throw new ETMS.AppContext.BusinessException("调查已过截止时间！");
            }
            Poll_UserResourceQueryResultLogic userResourceQueryLogic = new Poll_UserResourceQueryResultLogic();
            if (!query.IsRepeat && userResourceQueryLogic.IsHasJoined(queryID, userName, userType, resourceType, resourceCode))
            {
                //此问卷不允许多次调查且此用户已经参与此问卷，则提示
                throw new ETMS.AppContext.BusinessException("您已经投过票了，感谢您的支持！");
            }
            else
            {
                return GetResponseViewPreView(queryID);
            }
        }
        /// <summary>
        /// 问卷作答视图预览
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <returns></returns>
        public static StringBuilder GetResponseViewPreView(int queryID)
        {
            return CreateView(queryID, null);
        }

        /// <summary>
        /// 问卷作答视图预览
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <returns></returns>
        public static StringBuilder GetResponseViewPreView(int queryID, int batchID)
        {
            return CreateView(queryID, null, batchID);
        }

        /// <summary>
        ///  当前页面输出问卷统计结果视图
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        public static StringBuilder GetResponseStatResultView(int queryID, string resourceType, string resourceCode)
        {
            return CreateStaticResultView(queryID, null, resourceType, resourceCode);
        }

        /// <summary>
        /// 当前页面输出问卷查看视图
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
        /// 当前页面输出问卷统计结果导出视图
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="resourceType">资源类型</param>
        /// <param name="resourceCode">资源唯一编码</param>
        /// <returns>HTML内容记录器</returns>
        public static StringBuilder GetStaticResultExportView(int queryID, string resourceType, string resourceCode)
        {
            return CreateStaticResultExportView(queryID, null, resourceType, resourceCode);
        }

        /// <summary>
        /// 当前页面输出用户问卷作答结果视图
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <returns>HTML内容记录器</returns>
        public static StringBuilder CreateAnswerXMLOfUserView(int batchID)
        {
            return CreateAnswerXMLOfUserView(batchID, null);
        }

        /// <summary>
        /// 提交问卷答题结果保存
        /// </summary>
        /// <param name="doc">问卷答题结果XML树</param>
        /// <param name="userName">用户名</param>
        /// <param name="userType">用户类型</param>
        public static void ViewSubmitResultSave(XmlDocument doc, string userName, string userType)
        {
            string ResourceType, ResourceCode;
            int QueryID;
            ResourceCode = doc.SelectSingleNode("/Query/ResourceCode").InnerXml;
            ResourceType = doc.SelectSingleNode("/Query/ResourceType").InnerXml;
            QueryID = Convert.ToInt32(doc.SelectSingleNode("/Query/QueryID").InnerXml);

            if (string.IsNullOrEmpty(ResourceCode) || string.IsNullOrEmpty(ResourceType))
            {
                throw new ETMS.AppContext.BusinessException("页面参数无效！");
            }
            /*
             * debug: 2008-7-3(重复提交问题)
            */
            Poll_UserResourceQueryResultLogic userResourceQueryLogic = new Poll_UserResourceQueryResultLogic();
            if (!userResourceQueryLogic.IsAllowMultJoin(QueryID, userName, userType, ResourceType, ResourceCode))
            {
                throw new ETMS.AppContext.BusinessException("您已经投过票了，感谢您的支持！");
            }
            //获取用户提交批次号
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
                    case 1://单选保存
                    case 2://多选保存
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
                        if (!string.IsNullOrEmpty(answers[0].ChildNodes[1].InnerXml))//其他
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
                    case 3://矩阵保存
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
                    case 4://简答题保存
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
        /// 创建问卷调查视图对应的缓存文件
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="themeName">生成此文件使用的皮肤（默认：default）</param>
        private static StringBuilder CreateView(int queryID, string themeName)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "default";
            }
            Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(QueryLogic.CreateXMLByQueryID(queryID));
            //1、保存XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //问卷答案对应的xml结构
            return writer.Replace("$$", QueryLogic.CreateAnswerXMLByQueryID(queryID));

        }
        /// <summary>
        /// 创建问卷调查视图对应的缓存文件
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="themeName">生成此文件使用的皮肤（默认：default）</param>
        private static StringBuilder CreateView(int queryID, string themeName, int batchID)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "detailResult";
            }
            Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(QueryLogic.CreateAnswerXMLByQueryID(queryID, batchID));
            //1、保存XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //问卷答案对应的xml结构
            return writer;

        }
        /// <summary>
        /// 用户调查列表
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
            //1、保存XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //问卷答案对应的xml结构
            return writer;
        }

        /// <summary>
        /// 创建问卷调查统计结果对应的缓存文件
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="themeName">生成此文件使用的皮肤（默认：result）</param>
        /// <param name="resourceType">资源类型</param>
        /// <param name="resourceCode">资源唯一编码</param>
        private static StringBuilder CreateStaticResultView(int queryID, string themeName, string resourceType, string resourceCode)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "result";
            }
            Poll_QueryPublishObjectLogic ResourceQueryLogic = new Poll_QueryPublishObjectLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(ResourceQueryLogic.CreateStatResultXMLByQueryID(queryID, resourceType, resourceCode));
            //保存XSLTTransferResult
            return TransXMLByXSLT(writer, GetThemePhyPath(themeName));
        }

        /// <summary>
        /// 创建问卷调查统计结果对应的缓存文件
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="themeName">生成此文件使用的皮肤（默认：result）</param>
        /// <param name="resourceType">资源类型</param>
        /// <param name="resourceCode">资源唯一编码</param>
        private static StringBuilder CreateStaticPollView(int queryID, string themeName, string resourceType, string resourceCode)
        {
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "view";
            }
            Poll_QueryLogic QueryLogic = new Poll_QueryLogic();
            StringBuilder writer = new StringBuilder();
            writer.Append(QueryLogic.CreateXMLByQueryID(queryID));
            //1、保存XSLTTransferResult
            writer = TransXMLByXSLT(writer, GetThemePhyPath(themeName));
            //问卷答案对应的xml结构
            return writer;
        }

        /// <summary>
        /// 创建问卷调查统计结果导出对应的HTML内容
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="themeName">问卷调查统计结果导出对应皮肤（默认：resultexport）</param>
        /// <param name="resourceType">资源类型</param>
        /// <param name="resourceCode">资源唯一编码</param>
        /// <returns>HTML内容记录器</returns>
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
        /// 创建用户问卷作答结果视图
        /// </summary>
        /// <param name="queryID">问卷ID</param>
        /// <param name="themeName">用户问卷作答结果皮肤（默认：detail）</param>
        /// <returns>HTML内容记录器</returns>
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
        /// XML通过XSL转换为HTML页面内容
        /// </summary>
        /// <param name="writer">xml内容记录器</param>
        /// <param name="xsltpath">xsl文件绝对路径</param>
        /// <returns>HTML页面内容记录器</returns>
        private static StringBuilder TransXMLByXSLT(StringBuilder writer, string xsltpath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(writer.ToString());   //导入XML   

            XslCompiledTransform trans = new XslCompiledTransform();
            trans.Load(xsltpath);   //导入XSL   

            using (StringWriter sw = new StringWriter())
            {
                trans.Transform(doc, null, sw);
                return sw.GetStringBuilder();
                //return sw.GetStringBuilder().Replace("$Root$", ApplicationPath).Replace("^$^", "<br />");
            }
        }

        ///// <summary>
        ///// 自动创建并保存问卷对应的Html文件
        ///// </summary>
        ///// <param name="queryID"></param>
        ///// <param name="type">1：问卷调查答题，2：问卷调查统计结果</param>
        ///// <param name="resourceType">资源类型</param>
        ///// <param name="resourceCode">资源唯一编码</param>
        //private static void AutoCreateHtmlCode(int queryID, int type, string resourceType, string resourceCode)
        //{
        //    switch (type)
        //    {
        //        case 1://问卷调查答题
        //            if (File.Exists(GetQuestionHtmlCodePhyPath(queryID)))
        //            {
        //                return;
        //            }
        //            CreateView(queryID, null);
        //            break;
        //        case 2://问卷调查统计结果
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
