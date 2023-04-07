namespace ETMS.Utility.Service.FileUpload
{
    /// <summary>
    /// 文件上传策略服务
    /// </summary>
    public interface IFileUploadStrategyService
    {
        /// <summary>
        /// 获取文件上传策略
        /// </summary>
        /// <param name="functionConfigName">上传功能类型</param>
        /// <returns>文件上传策略</returns>
        FileUploadConfig GetStrategy(string functionType); 
    }
}
