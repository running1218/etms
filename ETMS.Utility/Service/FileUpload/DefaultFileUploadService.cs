using System;
using System.Collections.Generic;
using System.Collections;
using Autumn.Caching;
using Autumn.Objects.Factory;
using Common.Logging;
using System.IO;
namespace ETMS.Utility.Service.FileUpload
{
    /// <summary>
    /// 默认文件上传服务,支持两种上传过程，请根据实际场景使用：
    /// 1、支持临时文件存储
    ///  优点：如果用户业务上未确认保存，则上传文件被以垃圾文件的方式处理
    ///  缺点：涉及文件多次拷贝，性能影响！
    ///  场景：用户没有对应的网络磁盘（即无法限制用户个人空间的大小），需防治垃圾数据！
    ///     步骤1、申请令牌
    ///     步骤2、上传文件（临时保存）
    ///     步骤3、确认保存（将临时数据Copy至正式文件服务器）
    /// 2、文件立即保存
    ///  优点：用户文件上传后立即存入正式文件服务器，一次文件保存操作
    ///  缺点：如果用户业务上未确认保存，则此上传文件即没有被引用，实际成为垃圾文件。
    ///  场景：用户有对应的个人存储空间（可限制大小），用户可以管理自己上传的所有资源，如果空间不足时，可删除之前的垃圾文件。
    ///     步骤1、申请令牌
    ///     步骤2、文件上传（即时保存至文件服务器）
    ///     步骤3、确认保存，仅返回此处上传的文件列表信息，供业务使用。
    ///  
    ///  配置属性：UsingTempSave、TempFileArea
    ///  
    /// </summary>
    public class DefaultFileUploadService : IFileUploadService, IInitializingObject
    {
        private static ILog Logger = LogManager.GetLogger(typeof(DefaultFileUploadService));

        #region property
        /// <summary>
        /// 口令卡缓存
        /// </summary>
        public ICache Cache { get; set; }

        /// <summary>
        /// 文件上传策略定义（对应用功能的支持）
        /// </summary>
        public IFileUploadStrategyService StrategyService { get; set; }

        /// <summary>
        /// 临时文件区域
        /// </summary>
        public string TempFileArea { get; set; }

        /// <summary>
        /// 是否启用临时文件保存机制（默认不启用）
        /// </summary>
        public bool UsingTempSave { get; set; }
        #endregion

        #region protected

        /// <summary>
        /// 获取放入缓存中的键
        /// 默认格式：{FileUploadService}.{id}
        /// </summary>
        /// <param name="id">动态上传口令卡</param>
        /// <returns>缓存中的键名</returns>
        protected virtual string doGetCacheKey(string id)
        {
            return string.Format("FileUploadService.{0}", id);
        }

        /// <summary>
        /// 根据上传功能类型获取对应的上传策略配置
        /// </summary>
        /// <param name="functionType">上传功能类型</param>
        /// <returns>上传策略配置</returns>
        protected virtual FileUploadConfig FindFileUploadConfig(string functionType)
        {
            return StrategyService.GetStrategy(functionType);

        }
        #endregion

        #region IInitializingObject
        public void AfterPropertiesSet()
        {
            Autumn.Util.AssertUtils.ArgumentNotNull(this.Cache, "请先设置属性Cache！");
        }
        #endregion

        #region IFileUploadService
        /// <summary>
        /// 申请上传
        /// </summary>
        /// <param name="functionType">上传功能类型</param>
        /// <returns>文件上传卡</returns>        
        public FileUploadCard Apply(string functionType)
        {
            if (null == FindFileUploadConfig(functionType))
            {
                string message = string.Format("未找到名为“{0}”的功能上传配置！", functionType.ToString());
                if (Logger.IsErrorEnabled)
                {
                    Logger.Error(message);
                }
                throw new Exception(message);
            }

            FileUploadCard entity = new FileUploadCard();
            entity.ID = Guid.NewGuid().ToString("n");
            entity.FunctionType = functionType;
            entity.Files = new List<string>();
            entity.FileDetails = new List<UploadFileDefine>();
            //放入缓存
            {
                string cacheKey = doGetCacheKey(entity.ID);
                Cache.Insert(cacheKey, entity);
            }
            return entity;
        }

        /// <summary>
        /// 根据口令卡ID获取上传口令实体
        /// </summary>
        /// <param name="id">口令卡ID</param>
        /// <returns>如果存在，返回上传口令实体，否则：返回null</returns>
        public FileUploadCard Get(string id)
        {
            //缓存中取
            string cacheKey = doGetCacheKey(id);
            FileUploadCard entity = (Cache.Get(cacheKey) as FileUploadCard);
            if (entity == null)
            {
                string message = "文件上传失败，原因：错误的令牌码！";
                if (Logger.IsErrorEnabled)
                {
                    Logger.Error(message);
                }
                throw new ETMS.AppContext.BusinessException("FileUploadService.InvalidCardID");
            }
            return entity;
        }
        /// <summary>
        /// 获取功能配置信息
        /// </summary>
        /// <param name="id">口令卡ID</param>
        /// <returns>功能配置信息</returns>
        public FileUploadConfig GetFunctionConfig(string id)
        {
            //1、提取口令实体
            FileUploadCard entity = Get(id);
            return this.FindFileUploadConfig(entity.FunctionType);

        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="id">口令卡id</param>
        /// <param name="file">上传文件</param>   
        /// <param name="fileIndex">文件索引（第几个文件）</param>
        public void Upload(string id, System.Web.HttpPostedFile file, int fileIndex)
        {
            #region 1、提取口令实体
            FileUploadCard entity = Get(id);
            #endregion

            #region 2、文件上传规则校验
            FileUploadConfig functionConfigInfo = this.FindFileUploadConfig(entity.FunctionType);
            string fileName = Path.GetFileName(file.FileName);
            //规则校验
            {
                //2.1、文件数量
                if (functionConfigInfo.MaxFileCount < entity.Files.Count + 1)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        string message = string.Format("“{0}”文件上传失败，原因：超过最大文件数（{1}）限制！", entity.FunctionType, functionConfigInfo.MaxFileCount);
                        Logger.Error(message);
                    }
                    throw new ETMS.AppContext.BusinessException("FileUploadService.OutMaxFileCount");
                }
                //2.2、文件类型
                string currentFileType = Path.GetExtension(file.FileName);
                if (!Array.Exists<string>(functionConfigInfo.FileTypes, new Predicate<string>(
                    delegate(string item)
                    {
                        return (item.Equals(currentFileType, StringComparison.InvariantCultureIgnoreCase));
                    }
                    )))
                {
                    if (Logger.IsErrorEnabled)
                    {
                        string message = string.Format("“{0}”文件上传失败，原因：文件类型（{1}）不支持。支持的类型：{2}",
                            entity.FunctionType,
                            currentFileType,
                            Autumn.Util.StringUtils.CollectionToCommaDelimitedString(functionConfigInfo.FileTypes));
                        Logger.Error(message);
                    }
                    throw new ETMS.AppContext.BusinessException("FileUploadService.UnSupportFileType");
                }
                //2.3、文件大小
                if (functionConfigInfo.MaxFileSize < (file.ContentLength / 1024))
                {
                    if (Logger.IsErrorEnabled)
                    {
                        string message = string.Format("“{0}”文件上传失败，原因：超过最大文件大小（{1}）限制！", entity.FunctionType, functionConfigInfo.MaxFileSize);
                        Logger.Error(message);
                    }
                    throw new ETMS.AppContext.BusinessException("FileUploadService.OutMaxFileSize");
                }
            }
            #endregion

            #region 3、保存上传文件（支持临时存储或立即存储两种模式）
            if (this.UsingTempSave && functionConfigInfo.UsingTempSave)//（支持临时文件区域保存机制）
            {
                //3.1、构建临时文件全路径格式：{TempFileArea}\{entity.id}\{fileName}
                string tempFilePath = string.Format(@"{0}\{1}", TempFileArea, entity.ID);

                //3.2、创建路径
                if (!Directory.Exists(tempFilePath))
                {
                    try
                    {
                        Directory.CreateDirectory(tempFilePath);
                    }
                    catch (Exception ex)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            string message = string.Format("“{0}”文件上传失败，原因：自动创建路径（{1}）失败!", entity.FunctionType, tempFilePath);
                            Logger.Error(message, ex);
                        }
                        throw;
                    }
                }
                //3.3、保存文件
                try
                {
                    file.SaveAs(tempFilePath + "\\" + fileName);
                }
                catch (Exception ex)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        string message = string.Format("“{0}”文件上传失败，原因：{1}!", entity.FunctionType, ex.Message);
                        Logger.Error(message, ex);
                    }
                    throw;
                }
            }
            else//立即保存至正式文件服务器
            {
                string filePath = functionConfigInfo.Root;
                fileName = GetNewFileName(functionConfigInfo, fileName);
                //3.3、保存文件
                try
                {
                    file.SaveAs(filePath + "\\" + fileName);
                }
                catch (Exception ex)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        string message = string.Format("“{0}”文件上传失败，原因：{1}!", entity.FunctionType, ex.Message);
                        Logger.Error(message, ex);
                    }
                    throw;
                }
            }
            #endregion

            #region 4、更新口令实体
            entity.Files.Insert(fileIndex, fileName);
            UploadFileDefine fileDefine = new UploadFileDefine();
            fileDefine.BizUrl = fileName;
            fileDefine.FileName = Path.GetFileName(file.FileName);
            fileDefine.FileType = Path.GetExtension(file.FileName);
            fileDefine.FileSize = file.ContentLength;
            entity.FileDetails.Insert(fileIndex, fileDefine);
            #endregion

            #region 5、缓存更新
            string cacheKey = doGetCacheKey(entity.ID);
            Cache.Insert(cacheKey, entity);
            #endregion
        }
        /// <summary>
        /// 最终保存上传文件
        /// 具体操作：将临时区文件拷贝至永久区！
        /// </summary>
        /// <param name="id">口令卡id</param>
        public FileUploadCard Save(string id)
        {
            #region 1、提取口令实体
            FileUploadCard entity = Get(id);
            FileUploadCard newEntity = entity;
            FileUploadConfig functionConfigInfo = this.FindFileUploadConfig(entity.FunctionType);
            #endregion

            #region 处理文件保存，支持临时文件及立即保存两种模式
            //2、将临时区上传文件拷贝至持久化区域
            if (this.UsingTempSave && functionConfigInfo.UsingTempSave && entity.Files.Count > 0)
            {
                newEntity = new FileUploadCard();
                newEntity.ID = entity.ID;
                newEntity.FunctionType = entity.FunctionType;
                newEntity.Files = new List<string>();
                {
                    string tempFileFullPath = string.Format(@"{0}\{1}\", TempFileArea, entity.ID);
                    string filePath = functionConfigInfo.Root;
                    //循环处理上传的文件
                    foreach (string fileName in entity.Files)
                    {
                        if (string.IsNullOrEmpty(fileName))
                            continue;

                        string newFileName = GetNewFileName(functionConfigInfo, fileName);
                        //将临时区上传文件拷贝至持久化区域（如果存在，则覆盖）
                        File.Copy(tempFileFullPath + fileName, filePath + "\\" + newFileName, true);
                        //记录持久化后的文件名，以相对路径方式（提供业务保存到数据表中）
                        newEntity.Files.Add(string.Format("{0}", newFileName).Replace('\\', '/'));
                    }
                }
            }
            #endregion

            ////3、移除缓存，目前不考虑异常缓存，由缓存本身过期策略绝对。
            //如果开启，现有模式下一个上传部件只能上传一次
            //string cacheKey = doGetCacheKey(entity.ID);
            //Cache.Remove(cacheKey);

            return newEntity;
        }
        /// <summary>
        /// 移除文件列表中的某一个文件
        /// </summary>
        /// <param name="id">口令卡id</param>
        /// <param name="fileIndex">文件索引编号,{-1：标识清除全部,其它标识清除第几个文件}</param>
        public void Remove(string id, int fileIndex)
        {
            //1、提取口令实体
            FileUploadCard entity = Get(id);

            //2、移除文件
            if (fileIndex == -1)
            {
                //删除物理文件
                doRemoveFiles(entity.FunctionType, entity.Files.ToArray());
                //清空上传文件列表
                entity.Files.Clear();
            }
            else if (entity.Files.Count - 1 >= fileIndex)
            {
                //删除物理文件
                doRemoveFiles(entity.FunctionType, entity.Files[fileIndex]);
                //上传文件列表中删除文件信息
                entity.Files.RemoveAt(fileIndex);
                entity.FileDetails.RemoveAt(fileIndex);
            }
            //3、缓存更新
            string cacheKey = doGetCacheKey(entity.ID);
            Cache.Insert(cacheKey, entity);
        }

        #endregion

        #region helper
        /// <summary>
        /// 动态表达式计算，获取新的文件名称
        /// </summary>
        /// <param name="functionConfigInfo">功能上传配置信息</param> 
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetNewFileName(FileUploadConfig functionConfigInfo, string fileName)
        {
            string newFileName = fileName;
            if (functionConfigInfo.FileNameExpression != null)
            {
                //通过动态表达式计算新的文件名
                Hashtable variables = new Hashtable();
                //fixed bug:如果filename中包含路径，则也要原始返回！
                variables.Add("FileName", Path.GetDirectoryName(fileName).Equals(string.Empty) ? Path.GetFileNameWithoutExtension(fileName) : Path.GetDirectoryName(fileName) + @"\" + Path.GetFileNameWithoutExtension(fileName));
                variables.Add("FileType", Path.GetExtension(fileName));
                variables.Add("RequestParams", System.Web.HttpContext.Current.Request.Params);
                newFileName = (string)functionConfigInfo.FileNameExpression.GetValue(null, variables);
                if (Logger.IsDebugEnabled)
                {
                    //动态表达式计算
                    Logger.Debug(string.Format("上传文件名动态计算，原始名：{0}，新名：{1}", fileName, newFileName));
                }
            }
            //动态表达式可能会创建路径，因此需要在此重新计算路径是否存在，不存在自动创建！
            string path = Path.GetDirectoryName(functionConfigInfo.Root + "\\" + newFileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return newFileName;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="files"></param>
        private void doRemoveFiles(string functionType, params string[] files)
        {
            FileUploadConfig config = this.FindFileUploadConfig(functionType);
            foreach (string file in files)
            {
                try
                {
                    File.Delete(config.Root + @"\" + file.Replace("/", @"\"));
                }
                //文件删除失败时跳过
                catch { }
            }
        }
        #endregion
    }
}
