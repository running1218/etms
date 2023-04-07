using System.Web.UI.WebControls.WebParts;
using System.Collections;

namespace ETMS.Controls
{
    /// <summary>
    /// 个性化控件模式枚举定义
    /// </summary>
    public enum PersonalizableControlMode
    {
        /// <summary>
        /// 浏览模式
        /// </summary>
        Browser = 0,
        /// <summary>
        /// 设计模式
        /// </summary>
        Designer = 1
    }
    /// <summary>
    /// 个性化控件策略接口
    /// 如果控件需要保存自身的一些策略，则须实现此接口。
    /// </summary>
    public interface IPersonalizableControlPolicy
    {
        /// <summary>
        /// 控件策略
        /// 策略:即控制控件如何展示的一些参数。
        /// 由于：元数据Personalizable不能继承，因此，控件开发者在实现此接口时须手动添加[Personalizable(true)]
        /// </summary>
        [Personalizable(true)]
        string Policy { get;set;}

        /// <summary>
        /// 策略字典
        /// 将控件策略以字典的方式提供给控件开发者，方便开发。
        /// </summary>
        Hashtable ControlParms { get;set;}
        /// <summary>
        /// 创建控件策略
        /// 具体是指：将控件设计窗口中的参数整理出来。
        /// </summary>
        void BuildPolicy();

        /// <summary>
        /// 将控件策略应用到设计窗口中的控件
        /// </summary>
        void ApplyPolicy_Design();

        /// <summary>
        /// 将控件策略应用到浏览窗口中的控件
        /// </summary>
        void ApplyPolicy_Browse();

        ///// <summary>
        ///// 控件模式
        ///// </summary>
        //PersonalizableControlMode ControlMode { get;set;}

        ///// <summary>
        ///// 是否应用WebPart模式（默认开启）
        ///// </summary>
        //bool EnableWebPartMode { get;set;}

    }
}
