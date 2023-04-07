using System;
using System.Collections.Generic;

namespace ETMS.AppContext.Component
{
    /// <summary>
    /// 默认组件仓库实现
    /// </summary>
    public class DefaultComponentRepository : IComponentRepository
    {
        private IDictionary<string, IComponent> m_Components;
        private ApplicationContext m_AppContext;

        /// <summary>
        /// 延迟初始化组件
        /// 在组件第一次调用时初始化
        /// </summary>
        public bool LazyInitComponent { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appContext">应用上下文</param>
        public DefaultComponentRepository(ApplicationContext appContext)
        {
            m_Components = new Dictionary<string, IComponent>(StringComparer.CurrentCultureIgnoreCase);
            m_AppContext = appContext;
        }

        /// <summary>
        /// 所有组件集合
        /// </summary>
        public ICollection<IComponent> AllComponents
        {
            get { return this.m_Components.Values; }
        }

        /// <summary>
        /// 根据组件ID获取组件信息
        /// </summary>
        /// <param name="componentID">组件ID</param>
        /// <returns></returns>
        public IComponent GetComponentByID(string componentID)
        {
            //按照ID匹配
            if (!this.m_Components.Keys.Contains(componentID))
            {
                //未购买此模块
                throw new BusinessException("System.ComponentRepository.UnBuy", new object[] { componentID });
            }
            IComponent findItem = this.m_Components[componentID];
            //如果组件没有初始化，则完成初始化！
            if (!findItem.IsInited)
            {
                findItem.Init(this.AppContext);
            }
            return findItem;
        }

        /// <summary>
        /// 注册组件
        /// </summary>
        /// <param name="component">组件信息</param>
        public void RegisterComponent(IComponent component)
        {
            string registerID = component.ID;
            if (!string.IsNullOrEmpty(component.GroupID))
            {
                //如果GroupID不为空，组建ID注册规则：component.GroupID+"_"+component.ID
                registerID = component.GroupID + "_" + component.ID;
            }
            if (this.m_Components.ContainsKey(registerID))
            {
                //如果组件重复注册，则跳过
                return;
                //throw new Exception(string.Format("组件ID已经存在！组件信息GroupID={2},ID={0},Name={1}", component.ID, component.Name, component.GroupID));
            }
            //注册进库
            this.m_Components.Add(registerID, component);
            //非延迟初始化，则在组件注册时初始化
            if (!LazyInitComponent)
            {
                component.Init(this.AppContext);
            }
        }

        /// <summary>
        /// 应用上下文信息
        /// </summary>
        public ApplicationContext AppContext
        {
            get { return this.m_AppContext; }
        }

        /// <summary>
        /// typeof(T).Name作为组件ID时获取组件实例
        /// T为【注册时】组件实现的任意接口类型或组件本身类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetBizComponentByID<T>() where T : IComponent
        {
            string componentID = typeof(T).Name;
            return GetBizComponentByID<T>(componentID);
        }

        /// <summary>
        /// 【注册时组件ID】获取组件实例，T为组件实现的任意接口类型或组件本身类型
        /// 在一个组件实现N多个接口时，获取某一接口实例时使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="componentID"></param>
        /// <returns></returns>
        public T GetBizComponentByID<T>(string componentID) where T : IComponent
        {
            IComponent findComponent = GetComponentByID(componentID);
            //类型转换
            if (findComponent is T)
            {
                return (T)findComponent;
            }
            else
            {
                throw new BusinessException("System.ComponentRepository.ExpectTypeException", new object[] { findComponent, typeof(T) });
            }
        }

        /// <summary>
        /// 获取一组业务模式相同的组件集合,默认typeof(T).Name做为分组表示
        /// 如：课程下的资源（课件、在线作业）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetBizComponentsByGroupID<T>() where T : IComponent
        {
            string groupID = typeof(T).Name;
            List<T> result = new List<T>();
            foreach (string key in this.m_Components.Keys)
            {
                if (key.StartsWith(groupID, StringComparison.InvariantCultureIgnoreCase))
                {
                    result.Add((T)this.m_Components[key]);
                }
            }
            //排序处理
            result.Sort(new Comparison<T>((A, B) =>
                {
                    return A.OrderNo.CompareTo(B.OrderNo);
                }));

            if (result.Count == 0)
            {
                //未购买此模块
                throw new BusinessException("System.ComponentRepository.UnBuy", new object[] { groupID });
            }
            return result;
        }
    }
}
