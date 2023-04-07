namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// IJobService
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// 开始任务
        /// </summary>
        int DoJob();

        /// <summary>
        /// 日志环境
        /// </summary>
        OES.Logger.ILog Logger { get; set; }
    }
}
