using System;
using System.Collections.Generic;
using Open.Scorm.Entities;
using System.IO;
using System.Xml.Serialization;
using Open.Scorm.Message;

namespace Open.Scorm.Provider
{
    public class XmlProvider
    {
        /// <summary>
        /// 反序列化返回xml所有节点信息
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static Manifest Deserialize(string xmlPath)
        {
            Manifest entity = null;

            try
            {
                using (FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Manifest));
                    entity = (Manifest)xmlSerializer.Deserialize((Stream)fs);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("xmlns="))
                {
                    throw new BusinessException(DefineError.Error_Namesapce);
                }
                else if (ex.ToString().Contains("imsmanifest.xml"))
                {
                    throw new BusinessException(DefineError.Error_Path);
                }
                else
                {
                    throw new BusinessException(DefineError.Error_ImsmanifestFile);
                }
            }

            return entity;
        }

        /// <summary>
        /// 反序列化返回xml所有节点信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlPath)
        {
            T entity = default(T);

            try
            {
                using (FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    entity = (T)xmlSerializer.Deserialize((Stream)fs);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().ToLower().Contains("xmlns="))
                {
                    throw new BusinessException(DefineError.Error_Namesapce);
                }
                else if (ex.ToString().Contains("未能找到路径"))
                {
                    throw new BusinessException(DefineError.Error_Path);
                }
                else
                {
                    throw new BusinessException(DefineError.Error_NotKnow);
                }
            }

            return entity;
        }

        /// <summary>
        /// 获取清单机构与资源文件信息
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static OrganizationResource OrganizationAndResources(string xmlPath)
        {
            OrganizationResource result = null;

            try
            {
                var entity = Deserialize(xmlPath);

                if (null == entity)
                {
                    throw new Exception(DefineError.Error_InvalidResource);
                }
                else
                {
                    result = new OrganizationResource()
                    {
                        OrganizationNodes = OrganizationItems(entity),
                        ResourceItems = ResourceItems(entity)
                    };
                }
            }
            catch (BusinessException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new BusinessException(DefineError.Error_NotKnow);
            }

            return result;
        }

        /// <summary>
        /// 获取组织机构章节
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        private static List<OrganizationNode> OrganizationItems(Manifest entity)
        {
            var organizationNodes = new List<OrganizationNode>();

            foreach (Organizations organizations in entity.Organizations)
            {
                foreach (Organization organization in organizations.Organization)
                {
                    OrganizationNode organizationNode = new OrganizationNode();
                    organizationNode.ID = Guid.NewGuid();
                    organizationNode.Identifier = organization.Identifier;
                    organizationNode.OrgTitle = organization.Title != null ? organization.Title.OriginalValue : string.Empty;
                    organizationNode.StructureCode = organization.Structure.ToString();
                    organizationNode.OrganizationItems = new List<OrganizationItem>();
                   
                    foreach (Item item in organization.Item)
                    {
                        OrganizationItem(organizationNode.OrganizationItems, organization, item, string.Empty);
                    }

                    organizationNodes.Add(organizationNode);
                }
            }

            return organizationNodes;
        }

        /// <summary>
        /// 递归遍历Item项
        /// </summary>
        /// <param name="organizationItems"></param>
        /// <param name="organization"></param>
        /// <param name="item"></param>
        /// <param name="indentifierParentId"></param>
        private static void OrganizationItem(List<OrganizationItem> organizationItems, Organization organization, Item item, string indentifierParentId)
        {
            organizationItems.Add(new OrganizationItem(){ 
                                        ID = Guid.NewGuid(),
                                        Index = organizationItems.Count + 1, 
                                        OrganizationID = organization.Identifier,
                                        OrganizationName = organization.Title != null? organization.Title.OriginalValue : string.Empty,
                                        IdentifierID = item.Identifier,
                                        IdentifierName = item.Title != null ? item.Title.OriginalValue : string.Empty,
                                        IdentifierParentID = indentifierParentId,
                                        IdentifierResourceID = item.IdentifierRef,
                                        Isvisible = item.IsVisible
                                    });

            if (null != item.Items && item.Items.Count > 0)
            {
                foreach (Item subItem in item.Items)
                {
                    OrganizationItem(organizationItems, organization, subItem, item.Identifier);
                }
            }
        }

        /// <summary>
        /// 获取资源文件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static List<ResourceItem> ResourceItems(Manifest entity)
        {
            var resourceItems = new List<ResourceItem>();
            foreach (Resources resources in entity.Resources)
            {
                foreach (Resource resource in resources.Resource)
                { 
                    resourceItems.Add(new ResourceItem(){
                        ID = Guid.NewGuid(),
                        ResourceID = resource.Identifier,
                        ResourcePath = resource.Href,
                        ResourceType = resource.Type.ToString(),
                        ScormType = resource.Scormtype.ToString(),
                        Files = ResourceFiles(resource)
                    });
                }
            }

            return resourceItems;
        }

        /// <summary>
        /// 资源文件File
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        private static List<ResourceFile> ResourceFiles(Resource resource)
        {
            var resourceFiles = new List<ResourceFile>();
            foreach(Open.Scorm.Entities.File file in resource.File)
            {
                resourceFiles.Add(new ResourceFile() { FilePath = file.Href });
            }

            return resourceFiles;
        }
    }
}
