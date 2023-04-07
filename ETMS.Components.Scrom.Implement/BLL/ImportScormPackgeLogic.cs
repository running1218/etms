using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using ETMS.Components.Scrom.Implement.DAL;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Scrom.API;
using ScormProvider = Open.Scorm.Provider;
using System.Data.SqlClient;
using ETMS.Components.Scrom.API.Entity;
using SevenZip;
using System.Web;
using Open.Scorm.Entities;

namespace ETMS.Components.Scrom.Implement.BLL
{
    #region 放弃代码
    public partial class ImportScormPackgeLogic
    {
        StringBuilder sb = null;

        //依次读取Item章节
        int number = 0;

        #region 公用变量
        
        /// <summary>
        /// 解压前文件名称
        /// </summary>
        private string FileName = string.Empty;

        /// <summary>
        /// 解压前文件放的路径
        /// </summary>
        private string FilePathRAR = string.Empty;

        /// <summary>
        /// zip文件的物理路径
        /// \\10.96.33.223\Files\SCORM\2012\03\29\20120329104248148.zip
        /// </summary>
        private string FileRootPath = string.Empty;

        /// <summary>
        /// 课件编码
        /// 616f7123-26c5-4bd4-91a0-83dc7ed579b6
        /// </summary>
        private string strCoursewareID = string.Empty;

        /// <summary>
        /// 解压后，Scorm文件存的路径
        /// \\10.96.33.223\Files\SCORM\
        /// </summary>
        private string CompressPath = string.Empty;

        /// <summary>
        /// 解压后，Scorm文件放置的文件夹名称，用课件编码CoursewareID命名的
        /// 616f7123-26c5-4bd4-91a0-83dc7ed579b6
        /// </summary>
        private string CompressName = string.Empty;

        /// <summary>
        /// Scorm文件夹地址 \\10.96.33.223\Files\SCORM\616f7123-26c5-4bd4-91a0-83dc7ed579b6
        /// </summary>
        private string ScormFilePath = string.Empty;

        #endregion

        /// <summary>
        /// 导入Scorm包
        /// </summary>
        /// <param name="filePath">上传后的文件名称 20120329104248148.zip</param>
        /// <param name="coursewareID">课件ID</param>
        /// <returns></returns>
        public void ImportScormPackge(string filePath,string fileName, string coursewareID, string schemaFile, string schemaFile1, string unCompressPath)
        {
            #region 初始化公用变量

            FilePathRAR = filePath;
            FileName = fileName;
            //FileRootPath = ETMS.Utility.StaticResourceUtility.GetFullRootPathByFileType("SCORM", filePath);;
            strCoursewareID = coursewareID;

            CompressPath = ETMS.Utility.StaticResourceUtility.GetRootPathByFileType("SCORM");
            CompressName = unCompressPath;

            #endregion

            try
            {
                //1、解压 
                UnCompressScorm();

                //2、读清单文件
                //清单文件地址 imsmanifest.xml
                ScormFilePath = CompressPath + CompressName;
                string ImsmanifestFile = string.Format(@"{0}\{1}", ScormFilePath, "imsmanifest.xml");
                //ImsmanifestFile = @"\\10.96.33.223\Files\SCORM\616f7123-26c5-4bd4-91a0-83dc7ed579b6\imsmanifest.xml";

                //3、校验清单文件xml格式是否符合Scorm标准，入库
                SaveScorm(ImsmanifestFile, schemaFile, schemaFile1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 解压Scorm压缩包

        /// <summary>
        /// 解压Scorm压缩包
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="coursewareID"></param>
        /// <returns></returns>
        public void UnCompressScorm()
        {
            try
            {
                UnCompress.UnCompress unCompress = new UnCompress.UnCompress();
                unCompress.UnRAR(CompressPath + CompressName, FilePathRAR, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }                     
        }
     
        #endregion

        #region 校验清单文件xml格式是否符合Scorm标准，入库
        
        /// <summary>
        /// 校验清单文件xml格式是否符合Scorm标准，入库
        /// </summary>
        /// <param name="ImsmanifestFile"></param>
        private void SaveScorm(string ImsmanifestFile, string schemaFile, string schemaFile1)
        {
            //初始化 次序
            number = 0;

            try
            {
                #region 校验清单文件imsmanifest.xml是否是Scorm
                //string namespaceUrl = "http://www.imsproject.org/xsd/imscp_rootv1p1p2";//命名空间的信息
                //string namespaceUrl1 = "http://www.adlnet.org/xsd/adlcp_rootv1p2";     //命名空间的信息1

                if (System.IO.Path.GetExtension(ImsmanifestFile).ToLower() != ".xml") //验证格式是不是XML文件
                {
                    throw new ETMS.AppContext.BusinessException("Scorm标准课件中的清单文件必须为XML格式文件！");
                }
                //else if (!ValidateFile(ImsmanifestFile, schemaFile, namespaceUrl, schemaFile1, namespaceUrl1)) //使用架构(XSD)验证XML文件 
                //{
                //    throw new ETMS.AppContext.BusinessException(sb.ToString());
                //}
                #endregion

                #region 读取清单文件,并将数据入库

                DataSet ds = readXML(ImsmanifestFile);

                ImportScormPackgeDataAccess importScormDal = new ImportScormPackgeDataAccess();

                //删除临时表数据 z_Item，z_Resource
                importScormDal.del_Z_ItemANDz_Resource();

                //写入章节信息
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    importScormDal.Add_Z_Item(row["课件编号"].ToString(), row["组织ID"].ToString(), row["组织名称"].ToString(), row["章节ID"].ToString(), row["章节名称"].ToString(), row["父章节ID"].ToString(), row["资源ID"].ToString(), row["是否可见"].ToString().ToBoolean() ? 1 : 0, row["序号"].ToInt());
                }

                //写入资源信息
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    importScormDal.Add_Z_Resource(row["资源ID"].ToString(), row["资源类型"].ToString(), row["地址"].ToString(),row["文件路径"].ToString());
                }

                //从临时表中导入到课件表中
                importScormDal.inputScorm();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
        }

        #endregion

        #region 读取清单文件，查看XML中是否有Scorm该有的属性值

        /// <summary>
        /// 读取清单文件
        /// </summary>
        /// <param name="xmlPath">文件路径</param>
        /// <returns>返回DataSet集合 （包含 章节表、资源表）</returns>
        private DataSet readXML(string xmlPath)
        {
            DataSet ds = new DataSet();//章节、资源

            try
            {
                if (System.IO.File.Exists(xmlPath))
                {
                    //加载XML文档
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);

                    //获得根节点
                    XmlElement root = xmlDoc.DocumentElement;
                    //判断是否为根节点
                    if (root.Name == "manifest")
                    {
                        foreach (XmlNode xNode in root.ChildNodes)
                        {
                            //章节
                            if (xNode.Name == "organizations")
                            {
                                ds.Tables.Add(readItem(xNode));
                            }
                            //资源
                            else if (xNode.Name == "resources")
                            {
                                ds.Tables.Add(readResource(xNode));
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Scorm课件导入失败，请检查课件或与管理员联系！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion

        #region 读取章节、资源
        /// <summary>
        /// 读取Item
        /// </summary>
        /// <param name="xNode"></param>
        /// <returns></returns>
        private DataTable readItem(XmlNode xNode)
        {
            #region tabItem
            DataTable tabItem = new DataTable();
            tabItem.Columns.Add("课件编号");
            tabItem.Columns.Add("组织ID");
            tabItem.Columns.Add("组织名称");
            tabItem.Columns.Add("章节ID");
            tabItem.Columns.Add("章节名称");
            tabItem.Columns.Add("父章节ID");
            tabItem.Columns.Add("资源ID");
            tabItem.Columns.Add("是否可见");
            tabItem.Columns.Add("序号");
            #endregion

            foreach (XmlNode organization in xNode.ChildNodes)
            {
                if (organization.Name == "organization")
                {
                    string organizedID = ((XmlElement)organization).GetAttribute("identifier");//织组ID
                    string organizedName = "";//组织名称

                    //组织名称
                    foreach (XmlNode item in organization.ChildNodes)
                    {
                        if (item.Name == "title")
                        {
                            organizedName = item.InnerText;
                            break;
                        }
                    }
                    //添加章节
                    xmlNodeChild(tabItem, organization, organizedID, organizedName, "");
                }
            }
            return tabItem;
        }

        /// <summary>
        /// 添加章节 表对象、XML节点、组织ID、组织名称、父章节ID
        /// </summary>
        /// <param name="tabItem"></param>
        /// <param name="organization"></param>
        /// <param name="organizedID"></param>
        /// <param name="organizedName"></param>
        /// <param name="identifierParent"></param>
        private void xmlNodeChild(DataTable tabItem, XmlNode organization, string organizedID, string organizedName, string identifierParent)
        {

            foreach (XmlNode item in organization.ChildNodes)
            {
                //章节信息
                if (item.Name == "item")
                {
                    number++;
                    //添加行 新行、XML节点、组织ID、组织名称、父章节ID、编号
                    tabItem.Rows.Add(addRowItem(tabItem.NewRow(), item, organizedID, organizedName, identifierParent, number));

                    //如果资源ID为空 说明有下级子章节
                    if (((XmlElement)item).GetAttribute("identifierref") == "")
                    {
                    //递归调用 添加章节
                    xmlNodeChild(tabItem, item, organizedID, organizedName, ((XmlElement)item).GetAttribute("identifier"));
                    }
                }
            }
        }

        /// <summary>
        /// 添加行 新行对象、XML节点、组织ID、组织名称、父章节ID、编号
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="item"></param>
        /// <param name="organizedID"></param>
        /// <param name="organizedName"></param>
        /// <param name="identifierParent"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private DataRow addRowItem(DataRow newRow, XmlNode item, string organizedID, string organizedName, string identifierParent, int number)
        {
            newRow["课件编号"] = strCoursewareID;
            newRow["组织ID"] = organizedID;
            newRow["组织名称"] = organizedName;
            newRow["章节ID"] = ((XmlElement)item).GetAttribute("identifier");
            newRow["父章节ID"] = identifierParent;
            newRow["资源ID"] = ((XmlElement)item).GetAttribute("identifierref");
            if (string.IsNullOrEmpty(((XmlElement)item).GetAttribute("isvisible")))
            {
                newRow["是否可见"] = "true";
            }
            else
            {
                newRow["是否可见"] = ((XmlElement)item).GetAttribute("isvisible");
            }
            newRow["序号"] = number;

            //章节名称
            XmlNodeList xnl = item.ChildNodes;
            newRow["章节名称"] = xnl.Count > 0 && xnl[0].Name == "title" ? xnl[0].InnerText : "";

            return newRow;
        }
        
        /// <summary>
        /// 资源表
        /// </summary>
        /// <param name="xNode"></param>
        /// <returns></returns>
        private DataTable readResource(XmlNode xNode)
        {
            #region tabResource
            DataTable tabResource = new DataTable();
            tabResource.Columns.Add("资源ID");
            tabResource.Columns.Add("资源类型");
            tabResource.Columns.Add("地址");
            tabResource.Columns.Add("文件路径");
            #endregion

            string identifier = ""; //资源ID
            string scormtype = "";  //资源类型
            string href = "";       //地址
            foreach (XmlNode resource in xNode.ChildNodes)
            {
                if (resource.Name == "resource")
                {
                    identifier = ((XmlElement)resource).GetAttribute("identifier");//资源ID
                    scormtype = ((XmlElement)resource).GetAttribute("adlcp:scormtype");//资源类型
                    href = ((XmlElement)resource).GetAttribute("href");//地址

                    //获得文件路径
                    foreach (XmlNode file in resource.ChildNodes)
                    {
                        if (file.Name == "file")
                        {
                            DataRow row = tabResource.NewRow();
                            row["资源ID"] = identifier;
                            row["资源类型"] = scormtype;
                            row["地址"] = string.Format(@"\{0}", href);
                            row["文件路径"] = ((XmlElement)file).GetAttribute("href");//文件路径
                            tabResource.Rows.Add(row);
                        }
                    }
                }
            }
            return tabResource;
        }

        #endregion

        #region 使用架构(XSD)验证XML文件
        /// <summary>
        /// 验证XML文件
        /// </summary>
        /// <param name="dataFile"> XML文件路径</param>
        /// <param name="schemaFile">验证XSD文件路径</param>
        /// <param name="namespaceUrl">命名空间的信息</param>
        /// <param name="schemaFile1">验证XSD文件路径1 可为空 为空时表示没有第二个XSD文件</param>
        /// <param name="namespaceUrl1">命名空间的信息1</param>
        /// <returns></returns>
        private bool ValidateFile(string dataFile, string schemaFile, string namespaceUrl, string schemaFile1, string namespaceUrl1)
        {
            bool res = false;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(namespaceUrl, schemaFile);

            //这里要加第二个文件
            if (schemaFile1 != "")
                settings.Schemas.Add(namespaceUrl1, schemaFile1);

            settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(settings_ValidationEventHandler);

            sb = new StringBuilder();

            try
            {
                using (XmlReader reader = XmlReader.Create(dataFile, settings))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Document && reader.NamespaceURI != namespaceUrl)
                        {
                            sb.AppendFormat("{0}\\n", "这不是一个合乎规范的数据文件");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ETMS.AppContext.BusinessException(sb.AppendFormat("{0}\\n", ex.Message).ToString());
            }

            if (sb.Length == 0)
                res = true;

            return res;
        }

        void settings_ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            sb.AppendFormat("{0}\\n", e.Message);
        }

        #endregion        
    }
    #endregion

    public partial class ImportScormPackgeLogic
    {
        protected ImportScormPackgeDataAccess DAL = new ImportScormPackgeDataAccess();
        public void ImportScormPackge(string rarFilePath, string fileName, Guid courseWareID, string unCompressPath)
        {
            string scormDirectory = StaticResourceUtility.GetRootPathByFileType("SCORM") + unCompressPath;

            try
            {
                //解压
                UnCompressScorm(scormDirectory, rarFilePath, fileName);
                OrganizationResource entity = ScormProvider.XmlProvider.OrganizationAndResources(string.Format("{0}{1}", scormDirectory, "imsmanifest.xml"));

                //验证Scorm清单数据是否正确
                Validate(entity);

                //存储Scorm信息
                SaveScorm(entity, courseWareID);
            }
            catch (ScormProvider.BusinessException ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
            catch
            {
                throw new ETMS.AppContext.BusinessException(Open.Scorm.Message.DefineError.Error_NotKnow);
            }
        }

        /// <summary>
        /// 解压Scorm信息
        /// </summary>
        /// <param name="scormDirectory"></param>
        /// <param name="rarFilePath"></param>
        /// <param name="fileName"></param>
        private void UnCompressScorm(string scormDirectory, string rarFilePath, string fileName)
        {
            try
            {
                UnRAR(scormDirectory, rarFilePath, fileName);
            }
            catch
            {
                throw new ScormProvider.BusinessException(BizErrorDefine.Failed_UnCompress);
            }
        }
        public bool UnRAR(string path, string rarPath, string rarName)
        {
            SevenZipExtractor.SetLibraryPath(HttpContext.Current.Server.MapPath("Zip/7z.dll"));
            SevenZipExtractor extr = new SevenZipExtractor(rarPath + "\\" + rarName);
            //pb_ExtractWork.Maximum = (int)extr.FilesCount;
            //extr.ExtractionFinished += new EventHandler<EventArgs>(extr_ExtractionFinished);
            extr.ExtractArchive(path);
            extr.Dispose();
            return true;
        }
        /// <summary>
        /// 验证Scorm信息
        /// </summary>
        /// <param name="entity"></param>
        private void Validate(OrganizationResource entity)
        {
            if (null == entity.OrganizationNodes || entity.OrganizationNodes.Count == 0)
            {
                throw new ScormProvider.BusinessException(BizErrorDefine.Failed_NoItem);
            }
            else
            {
                foreach (OrganizationNode node in entity.OrganizationNodes)
                {
                    if (null == node.OrganizationItems || node.OrganizationItems.Count == 0)
                    {
                        throw new ScormProvider.BusinessException(BizErrorDefine.Failed_NoItem);
                    }
                }
            }

            if (null == entity.ResourceItems || entity.ResourceItems.Count == 0)
            {
                throw new ScormProvider.BusinessException(BizErrorDefine.Failed_NoResource);
            }

            if (entity.ResourceItems.Exists(f => f.ScormType.Equals(string.Empty)))
            {
                throw new ScormProvider.BusinessException(BizErrorDefine.Failed_ResourceType);
            }
        }

        /// <summary>
        /// 存储Scorm信息
        /// </summary>
        /// <param name="entity"></param>
        private void SaveScorm(OrganizationResource entity, Guid courseWareID)
        {
            SqlTransaction sqlTrans = null;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConnectionString.ScormWrite))
                {
                    if (sqlConn.State == ConnectionState.Closed)
                        sqlConn.Open();
                    sqlTrans = sqlConn.BeginTransaction();

                    // 删除课件信息
                    DAL.DeleteScormData(courseWareID, sqlTrans);

                    foreach (OrganizationNode node in entity.OrganizationNodes)
                    {
                        int result = DAL.SaveOrganization(new ScormOrganization()
                                                {
                                                    OrgID = node.ID,
                                                    OrgTitle = node.OrgTitle,
                                                    CourseWareID = courseWareID,
                                                    Identifier = node.Identifier,
                                                    StructureCode = node.StructureCode,
                                                    Creator = AppContext.UserContext.Current.RealName,
                                                    CreateTime = DateTime.Now
                                                }, sqlTrans);

                        foreach (OrganizationItem item in node.OrganizationItems)
                        {
                            DAL.SaveItem(new ScormItem()
                                            {
                                                ItemID = item.ID,
                                                CoursewareID = courseWareID,
                                                OrgID = node.ID,
                                                ItemTitle = item.IdentifierName,
                                                ParentItemID = GetItemParentID(node.OrganizationItems, item),
                                                SequenceNo = item.Index,
                                                IsVisible = item.Isvisible ? 1 : 0,
                                                ItemIdentifier = item.IdentifierResourceID,
                                                ResourceID = GetItemResourceID(entity.ResourceItems, item),
                                                Creator = UserContext.Current.RealName,
                                                CreateTime = DateTime.Now,
                                                Delete = 0
                                            }, sqlTrans);
                        }
                    }

                    foreach (ResourceItem resource in entity.ResourceItems)
                    {
                        DAL.SaveResource(new ScormResource()
                                            {
                                                ResourceID = resource.ID,
                                                ScormTypeCode = resource.ScormType,
                                                ResourceName = string.Empty,
                                                ResourceHref = resource.ResourcePath,
                                                Type = resource.ResourceType,
                                                Resourceidentifier = resource.ResourceID,
                                                CoursewareID = courseWareID,
                                                Creator = UserContext.Current.RealName,
                                                CreateTime = DateTime.Now
                                            }, sqlTrans);

                        foreach (ResourceFile file in resource.Files)
                        {
                            DAL.SaveResourceFile(new ScormFile()
                                                {
                                                    ResourceID = resource.ID,
                                                    FileHref = file.FilePath
                                                }, sqlTrans);
                        }
                    }

                    sqlTrans.Commit();
                }
            }
            catch (ScormProvider.BusinessException ex)
            {
                sqlTrans.Rollback();
                throw ex;
            }            
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                if (ex.Message.Contains("FK_"))
                    throw new ScormProvider.BusinessException(BizErrorDefine.Failed_ScormResourceIsUsing);
                else
                throw new ScormProvider.BusinessException(Open.Scorm.Message.DefineError.Error_NotKnow);
            }
        }

        /// <summary>
        /// 获取章节的上级机构ID
        /// </summary>
        /// <param name="items"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private Guid GetItemParentID(List<OrganizationItem> items, OrganizationItem item)
        {
            if (item.IdentifierParentID != string.Empty)
            {
                var entity = items.SingleOrDefault(s => s.IdentifierID.Equals(item.IdentifierParentID));
                if (null == entity)
                    throw new ScormProvider.BusinessException(string.Format(BizErrorDefine.Failed_UnFindSubItemParentItem, item.IdentifierID, item.IdentifierParentID));
                else
                    return entity.ID;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 章节资源
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private Guid GetItemResourceID(List<ResourceItem> resources, OrganizationItem item)
        {
            var entity = resources.SingleOrDefault(s=>s.ResourceID.Equals(item.IdentifierResourceID));
            if (null == entity)
                return Guid.Empty;
            else
                return entity.ID;
        }
    }
}
