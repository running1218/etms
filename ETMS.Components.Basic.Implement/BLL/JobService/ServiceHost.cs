using System;

using OES.ExecuteUnit;
using OES.ExecuteUnit.Impl;
using System.Threading;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    ///ServiceHost 的摘要说明
    /// </summary>
    public class ServiceHost
    {
        public static ServiceHost DefaultHost = new ServiceHost();

        /// <summary>
        /// 需要初始化
        /// </summary>
        public static string DefaultUrl = "";
        /// <summary>
        /// 任务运行状态检测时间间隔(默认1分钟)（单位：秒）
        /// </summary>
        private const Int32 JobTaskRunTestTimeSpan = 60;//1分钟


        /// <summary>
        /// 邮件任务表轮询间隔(默认5分钟)（单位：秒）
        /// </summary>
        private const Int32 EmailJobTaskLoopTimeSpan = 60 * 5;//5分钟        
        /// <summary>
        /// 是否启用邮件发送Job
        /// </summary>
        private const bool IsEnableEmailJobTask = true;


        /// <summary>
        /// 业务数据触发任务表轮询间隔(默认1小时)（单位：秒）
        /// </summary>
        private const Int32 BizTriggerJobTaskLoopTimeSpan = 60 * 60;//1小时
        /// <summary>
        /// 是否启用业务数据触发任务Job
        /// </summary>
        private const bool IsEnableBizTriggerJobTask = true;


        /// <summary>
        /// 应用程序池激活任务轮询间隔(默认15分钟)（单位:秒）
        /// </summary>
        private const Int32 AppPoolActiveJobTaskLoopTimeSpan = 60 * 15;//15分钟
        /// <summary>
        /// 应用程序池激活任务Job
        /// </summary>
        private const bool IsEnableAppPoolActiveJobTask = true;

        /// <summary>
        /// 是否调试（调试模式下启用单线程）
        /// </summary>
        private const bool IsDebug = false;

        private BusinessUnit HostUnit = null;
        public void Start()
        {
            if (HostUnit != null)
            {
                return;
            }

            /* Host层次图
             * 
             * ServiceHost（总服务宿主）
             *     |---SendEmailJobHost（邮件发送任务宿主，职责：按照特定的周期轮询，发送邮件！）
             *     |---DayBizTriggerHost（每日业务数据触发任务宿主，职责：按照特定的周期轮询，完成每天触发特定的业务提醒信息）
             *     |---AppPoolActiveHost（应用程序池激活任务宿主，职责：按照特定的周期轮询，定期请求当前站点default.aspx页面，保证应用不停止）
             * 
             */

            HostUnit = new BusinessUnit();
            HostUnit.Name = "ServiceHost(100%保证业务子单元一直可靠运行)";//直到Stop()方的调用为止
            HostUnit.MulThreadSupport = !IsDebug;
            HostUnit.Logger = new OESLoggerAdapter(HostUnit.Name);
            HostUnit.UnitHandler = () =>
            {
                //定期检查，保证管理的业务单位出于运行状态           
                while (this.HostUnit.CurrentState == ExecuteState.Runing)
                {
                    Thread.Sleep(JobTaskRunTestTimeSpan * 1000);//休眠1分钟进入下次操作

                    this.HostUnit.Run();
                }
            };

            if (IsEnableEmailJobTask)
            {
                //1、装配邮件发送任务
                BusinessUnit SendMailJobHost = new BusinessUnit();
                HostUnit.ChildUnits.Add(SendMailJobHost);

                IJobService SendMailJob = new SendEmailJob();
                IJobService ResetMailStatusJob = new ResetEmailStatusJob();
                SendMailJobHost.Name = "定期发送邮件Host";
                SendMailJobHost.MulThreadSupport = !IsDebug;
                SendMailJobHost.Logger = new OESLoggerAdapter(SendMailJobHost.Name);
                SendMailJobHost.UnitHandler = () =>
                {
                    //邮件任务表轮询Loop        
                    while (SendMailJobHost.CurrentState == ExecuteState.Runing)
                    {
                        Thread.Sleep(EmailJobTaskLoopTimeSpan * 1000);//邮件任务表轮询间隔
                        int count = SendMailJob.DoJob();
                        if (SendMailJobHost.Logger.IsDebug)
                        {
                            SendMailJobHost.Logger.Debug(string.Format("本次轮询共发送{0}封邮件!", count));
                        }
                        count = ResetMailStatusJob.DoJob();
                        if (SendMailJobHost.Logger.IsDebug)
                        {
                            SendMailJobHost.Logger.Debug(string.Format("本次轮询共重置{0}封失败邮件!", count));
                        }
                    }
                };
                SendMailJob.Logger = SendMailJobHost.Logger;
                ResetMailStatusJob.Logger = SendMailJobHost.Logger;
            }

            if (IsEnableBizTriggerJobTask)
            {
                //2、装配每天业务触发任务（如课程或考试、作业到期提醒）
                BusinessUnit DayBizTriggerHost = new BusinessUnit();
                HostUnit.ChildUnits.Add(DayBizTriggerHost);

                IJobService DayBizTriggerJob = new DayBizTriggerJob();
                DayBizTriggerHost.Name = "每天业务触发任务";
                DayBizTriggerHost.MulThreadSupport = !IsDebug;
                DayBizTriggerHost.Logger = new OESLoggerAdapter(DayBizTriggerHost.Name);
                DayBizTriggerHost.UnitHandler = () =>
                {
                    //每天业务触发任务Loop        
                    while (DayBizTriggerHost.CurrentState == ExecuteState.Runing)
                    {
                        Thread.Sleep(BizTriggerJobTaskLoopTimeSpan * 1000);//每天业务触发任务间隔
                        DayBizTriggerJob.DoJob();
                    }
                };
                DayBizTriggerJob.Logger = DayBizTriggerHost.Logger;
            }

            if (IsEnableAppPoolActiveJobTask)
            {
                //3、防止应用程序池长期不用自动停止
                BusinessUnit AppPoolActiveHost = new BusinessUnit();
                HostUnit.ChildUnits.Add(AppPoolActiveHost);

                IJobService AppPoolActiveJob = new AppPoolActiveJob(DefaultUrl);
                AppPoolActiveHost.Name = "防止应用程序池长期不用自动停止";
                AppPoolActiveHost.MulThreadSupport = !IsDebug;
                AppPoolActiveHost.Logger = new OESLoggerAdapter(AppPoolActiveHost.Name);
                AppPoolActiveHost.UnitHandler = () =>
                {
                    while (AppPoolActiveHost.CurrentState == ExecuteState.Runing)
                    {
                        Thread.Sleep(AppPoolActiveJobTaskLoopTimeSpan * 1000);//防止应用程序池长期不用自动停止
                        AppPoolActiveJob.DoJob();
                    }
                };
                AppPoolActiveJob.Logger = AppPoolActiveHost.Logger;
            }

            HostUnit.Run();
        }

        public void Stop()
        {
            if (HostUnit == null)
            {
                return;
            }

            HostUnit.Stop();

            HostUnit = null;
        }
    }
}
