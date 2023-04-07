using System.Collections.Generic;

namespace ETMS.AppContext.Component
{
    /// <summary>
    /// 组件仓库
    /// </summary>
    public interface IComponentRepository
    {
        /// <summary>
        /// 应用上下文
        /// </summary>
        ApplicationContext AppContext { get; }

        /// <summary>
        /// 获取所有组件信息
        /// </summary>
        ICollection<IComponent> AllComponents { get; }

        /// <summary>
        /// 获取组件信息
        /// </summary>
        /// <param name="componentID"></param>
        /// <returns></returns>
        IComponent GetComponentByID(string componentID);

        /// <summary>
        /// 获取业务组件
        /// </summary>
        /// <typeparam name="T">业务组件Facade接口类型</typeparam>
        /// <returns>业务组件接口实例</returns>
        /// <remarks>
        /// 如果组件没有找到，则返回“当前产品没有购买此模块！”
        /// </remarks>
        T GetBizComponentByID<T>() where T : IComponent;

        /// <summary>
        /// 【注册时组件ID】获取组件实例，T为组件实现的任意接口类型或组件本身类型
        /// 在一个组件实现N多个接口时，获取某一接口实例时使用
        /// </summary>
        /// <typeparam name="T">业务组件Facade接口类型</typeparam>
        /// <param name="componentID">业务组件ID</param>
        /// <returns>业务组件接口实例</returns>
        /// <remarks>如果组件没有找到，则返回“当前产品没有购买此模块！”</remarks>
        T GetBizComponentByID<T>(string componentID) where T : IComponent;

        /// <summary>
        /// 获取一组相同模式的业务组件集合
        /// </summary>
        /// <typeparam name="T">一组业务组件Facade接口类型</typeparam>
        /// <returns>一组相同模式的业务组件接口实例</returns>
        /// <remarks>
        /// 如果组件没有找到，则返回“当前产品没有购买此模块！”
        /// </remarks>
        IList<T> GetBizComponentsByGroupID<T>() where T : IComponent;

        /// <summary>
        /// 注册组件
        /// </summary>
        /// <param name="component"></param>
        void RegisterComponent(IComponent component);

    }
}
