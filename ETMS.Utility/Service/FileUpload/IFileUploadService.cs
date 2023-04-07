namespace ETMS.Utility.Service.FileUpload
{
    /// <summary>
    /// 文件上传服务
    /// </summary>
    public interface IFileUploadService
    {
        /// <summary>
        /// 申请上传
        /// </summary>
        /// <param name="functionConfigName">上传功能类型</param>
        /// <returns>文件上传卡</returns>
        FileUploadCard Apply(string functionType);

        /// <summary>
        /// 根据口令卡ID获取口令卡实体
        /// </summary>
        /// <param name="id">口令卡ID</param>
        /// <returns>如果存在，返回上传口令实体，否则：返回null</returns>
        FileUploadCard Get(string id);

        /// <summary>
        /// 获取功能配置信息
        /// </summary>
        /// <param name="id">口令卡ID</param>
        /// <returns>功能配置信息</returns>
        FileUploadConfig GetFunctionConfig(string id);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="id">口令卡id</param>
        /// <param name="file">上传文件</param>   
        /// <param name="fileIndex">文件索引（第几个文件）</param>
        void Upload(string id, System.Web.HttpPostedFile file, int fileIndex);

        /// <summary>
        /// 移除文件列表中的某一个文件
        /// </summary>
        /// <param name="id">口令卡id</param>
        /// <param name="fileIndex">文件索引编号,{-1：标识清除全部,其它标识清除第几个文件}</param>
        void Remove(string id, int fileIndex);

        /// <summary>
        /// 最终保存上传文件
        /// 具体操作：将临时区文件拷贝至永久区！
        /// </summary>
        /// <param name="id">口令卡id</param>
        FileUploadCard Save(string id);
    }
}
