using System;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls.WebParts;

namespace ETMS.Controls
{

    /// <summary>
    /// 第三方控件PersonalizMultiView，
    /// 1.需要WebPartManager支持
    /// 2.控件不能被没有实现IPersonalizableControlPolicy接口的控件调用
    /// </summary>
    public class PersonalizMultiView : System.Web.UI.WebControls.MultiView, IPersonalizableControlPolicy
    {
        /// <summary>
        /// 序列化的换行符号
        /// </summary>
        private const string SplitArg = "^";

        #region IPersonalizableControlPolicy 成员

        /// <summary>
        /// 是否开启WebPart模式（默认开启）
        /// A、开启状态：控件策略数据通过WebPartManager设置Policy属性来提供
        /// B、关闭状态：控件策略数据需要使用者通过设置ControlParms属性来提供
        /// 注意：开启后，当前页面必须有WebPartManager。
        /// </summary>
        private bool m_EnableWebPartMode = true;
        public bool EnableWebPartMode
        {
            get
            {
                return m_EnableWebPartMode;
            }
            set
            {
                m_EnableWebPartMode = value;
            }
        }

        /// <summary>
        /// 控件模式
        /// A、PersonalizableControlMode.Browser    浏览模式(默认)
        /// B、PersonalizableControlMode.Designer   设计模式
        /// </summary>
        public PersonalizableControlMode ControlMode
        {
            get
            {
                return (PersonalizableControlMode)this.ActiveViewIndex;
            }
            set
            {
                this.ActiveViewIndex = (int)value;
            }
        }

        /// <summary>
        /// 获取当前页面WebPartManager
        /// 如果当前页面不支持WebPart，则返回null。
        /// </summary>
        private WebPartManager CurrentWebPartManager
        {
            get
            {
                if (this.Page.Items.Contains(typeof(WebPartManager)))
                {
                    return (WebPartManager)this.Page.Items[typeof(WebPartManager)];
                }
                else if (this.EnableWebPartMode)//当前控件模式：开启WebPart模式
                {
                    if (!this.DesignMode)
                        throw new Exception("当前控件模式：开启WebPart模式，要求页面包含WebPartManager！");
                    else
                        return new WebPartManager();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 控件策略
        /// get:控件构建最新策略并返回
        /// set:控件应用原有策略
        /// </summary>
        public string Policy
        {
            get
            {
                //只有WebPartManager在SavePageViewState周期下调用此方法才是真正存储个性化数据的有效操作。其他周期的调用均为无效操作，为提高性能，直接返回null！
                if (!base.ChildControlsCreated || this.m_ControlParms == null)
                    return null;
                /*fixed 把此操作下放给用户btn_save事件来完成（注意：必须保证动态控件加入时控件默认策略）！*/

                if (this.m_ControlParms.Count == 0)
                {
                    this.BuildPolicy();//如果控件动态加入进来，则取默认策略。
                    this.ApplyPolicy_Design();//保证默认策略展现出来
                }
                return PersonalizMultiView.Serialization(this.ControlParms);
            }
            set
            {
                this.m_ControlParms = PersonalizMultiView.DeSerialization(value);
                this.ApplyPolicy();//应用策略
            }
        }

        private void CurrentWebPartManager_DisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
        {

        }
        private Hashtable m_ControlParms;

        /// <summary>
        /// 控件策略以字典形式提供
        /// </summary>
        public Hashtable ControlParms
        {
            get
            {
                if (this.m_ControlParms == null)
                    this.m_ControlParms = new Hashtable();
                return this.m_ControlParms;
            }
            set
            {
                this.m_ControlParms = value;
                //未开启WebPart环境下，需保证通过对ControlParms赋值，也能应用控件策略。
                if (!this.EnableWebPartMode)
                {
                    this.ApplyPolicy();
                }
            }
        }

        public void BuildPolicy()
        {
            ((IPersonalizableControlPolicy)this.Parent).BuildPolicy();
        }

        /// <summary>
        /// 应用策略
        /// 针对当前控件所处的视图模式应用策略
        /// Control.OnPreRender事件中应用策略，提高效率。尤其：当WebPartManager.DisplayModeChange时防止视图不一致。
        /// </summary>
        private void ApplyPolicy()
        {
            this.PreRender += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    switch (this.ActiveViewIndex)
                    {
                        case 0://浏览模式
                            //浏览模式应用策略：条件1：Get请求 条件2：Post请求，且之前不是设计模式
                            this.ApplyPolicy_Browse();
                            break;
                        case 1:
                            this.ApplyPolicy_Design();
                            break;
                        default:
                            break;
                    }
                });
        }

        public void ApplyPolicy_Design()
        {
            if (this.ControlParms.Count == 0)
                return;
            PersonalizMultiView.SetProxyWebPartDesignTitle(this.Parent, (string)this.m_ControlParms["Title"]);
            ((IPersonalizableControlPolicy)this.Parent).ApplyPolicy_Design();
        }

        public void ApplyPolicy_Browse()
        {
            if (this.ControlParms.Count == 0)
                return;
            ((IPersonalizableControlPolicy)this.Parent).ApplyPolicy_Browse();
        }

        /// <summary>
        /// 重写父类方法，当前视图模式
        /// 0：浏览模式
        /// 1：设计模式 
        /// </summary>
        public override int ActiveViewIndex
        {
            get
            {
                if (!this.EnableWebPartMode)//如果未开启WebPart模式，则返回基类中的ActiveViewIndex;
                {
                    return (base.ActiveViewIndex < 0 ? 0 : base.ActiveViewIndex);//默认激活浏览模式视图
                }
                //开启WebPart模式，则根据当前WebPartManage.DisplayMode来决定
                if (this.CurrentWebPartManager.DisplayMode.Name.ToLower() == "design")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (!this.EnableWebPartMode)//如果未开启WebPart模式，就设置基类中的ActiveViewIndex;
                {
                    base.ActiveViewIndex = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// 把HashTable信息序列化
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string Serialization(Hashtable table)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DictionaryEntry entry in table)
            {
                if (!string.Empty.Equals(entry.Value))
                {
                    builder.Append(entry.Key + "=" + entry.Value);
                    builder.Append(SplitArg);
                }
            }

            if (builder.Length == 0)
                return String.Empty;
            else
                builder.Remove(builder.Length - SplitArg.Length, SplitArg.Length);
            return builder.ToString();
        }

        /// <summary>
        /// 将信息反序列化成一个Hashtable实例
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public static Hashtable DeSerialization(string policy)
        {
            Hashtable table = new Hashtable();
            if (string.IsNullOrEmpty(policy))
                return table;
            //policy = policy.Replace("\r\n", "^");
            string[] policyArray = policy.Split(SplitArg.ToCharArray());
            int idx = -1;
            for (int i = 0; i < policyArray.Length; i++)
            {
                idx = policyArray[i].IndexOf('=');

                if (idx != -1 && idx != policyArray[i].Length - 1)
                {
                    table[policyArray[i].Substring(0, idx)] = policyArray[i].Substring(idx + 1);
                }
            }
            return table;
        }

        /// <summary>
        /// 设置ProxyWebPart设计模式下标题
        /// ProxyWebPart是指UserControl被包装为GenericWebPart
        /// </summary>
        /// <param name="userControl">被包装的UserControl</param>
        /// <param name="title">标题</param>
        public static void SetProxyWebPartDesignTitle(System.Web.UI.Control userControl, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "未设置标题";
            }
            title = string.Format("{0} - 设置", title);
            userControl.Page.PreRenderComplete += new System.EventHandler(delegate(object sender, System.EventArgs e)
            {
                if (
               (userControl.Page.Items.Contains(typeof(WebPartManager)) && ((WebPartManager)userControl.Page.Items[typeof(WebPartManager)]).DisplayMode.Name.Equals("design", System.StringComparison.InvariantCultureIgnoreCase))//仅设计模式
               && (userControl.Parent != null && userControl.Parent is GenericWebPart)//用户控件经GenericWebPart包装
               )
                {
                    //设置标题
                    GenericWebPart webPartPorxy = userControl.Parent as GenericWebPart;
                    webPartPorxy.Title = title;
                }
            }
            );
        }

    }
}
