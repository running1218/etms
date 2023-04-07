using System;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls.WebParts;

namespace ETMS.Controls
{

    /// <summary>
    /// �������ؼ�PersonalizMultiView��
    /// 1.��ҪWebPartManager֧��
    /// 2.�ؼ����ܱ�û��ʵ��IPersonalizableControlPolicy�ӿڵĿؼ�����
    /// </summary>
    public class PersonalizMultiView : System.Web.UI.WebControls.MultiView, IPersonalizableControlPolicy
    {
        /// <summary>
        /// ���л��Ļ��з���
        /// </summary>
        private const string SplitArg = "^";

        #region IPersonalizableControlPolicy ��Ա

        /// <summary>
        /// �Ƿ���WebPartģʽ��Ĭ�Ͽ�����
        /// A������״̬���ؼ���������ͨ��WebPartManager����Policy�������ṩ
        /// B���ر�״̬���ؼ�����������Ҫʹ����ͨ������ControlParms�������ṩ
        /// ע�⣺�����󣬵�ǰҳ�������WebPartManager��
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
        /// �ؼ�ģʽ
        /// A��PersonalizableControlMode.Browser    ���ģʽ(Ĭ��)
        /// B��PersonalizableControlMode.Designer   ���ģʽ
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
        /// ��ȡ��ǰҳ��WebPartManager
        /// �����ǰҳ�治֧��WebPart���򷵻�null��
        /// </summary>
        private WebPartManager CurrentWebPartManager
        {
            get
            {
                if (this.Page.Items.Contains(typeof(WebPartManager)))
                {
                    return (WebPartManager)this.Page.Items[typeof(WebPartManager)];
                }
                else if (this.EnableWebPartMode)//��ǰ�ؼ�ģʽ������WebPartģʽ
                {
                    if (!this.DesignMode)
                        throw new Exception("��ǰ�ؼ�ģʽ������WebPartģʽ��Ҫ��ҳ�����WebPartManager��");
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
        /// �ؼ�����
        /// get:�ؼ��������²��Բ�����
        /// set:�ؼ�Ӧ��ԭ�в���
        /// </summary>
        public string Policy
        {
            get
            {
                //ֻ��WebPartManager��SavePageViewState�����µ��ô˷������������洢���Ի����ݵ���Ч�������������ڵĵ��þ�Ϊ��Ч������Ϊ������ܣ�ֱ�ӷ���null��
                if (!base.ChildControlsCreated || this.m_ControlParms == null)
                    return null;
                /*fixed �Ѵ˲����·Ÿ��û�btn_save�¼�����ɣ�ע�⣺���뱣֤��̬�ؼ�����ʱ�ؼ�Ĭ�ϲ��ԣ���*/

                if (this.m_ControlParms.Count == 0)
                {
                    this.BuildPolicy();//����ؼ���̬�����������ȡĬ�ϲ��ԡ�
                    this.ApplyPolicy_Design();//��֤Ĭ�ϲ���չ�ֳ���
                }
                return PersonalizMultiView.Serialization(this.ControlParms);
            }
            set
            {
                this.m_ControlParms = PersonalizMultiView.DeSerialization(value);
                this.ApplyPolicy();//Ӧ�ò���
            }
        }

        private void CurrentWebPartManager_DisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
        {

        }
        private Hashtable m_ControlParms;

        /// <summary>
        /// �ؼ��������ֵ���ʽ�ṩ
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
                //δ����WebPart�����£��豣֤ͨ����ControlParms��ֵ��Ҳ��Ӧ�ÿؼ����ԡ�
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
        /// Ӧ�ò���
        /// ��Ե�ǰ�ؼ���������ͼģʽӦ�ò���
        /// Control.OnPreRender�¼���Ӧ�ò��ԣ����Ч�ʡ����䣺��WebPartManager.DisplayModeChangeʱ��ֹ��ͼ��һ�¡�
        /// </summary>
        private void ApplyPolicy()
        {
            this.PreRender += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    switch (this.ActiveViewIndex)
                    {
                        case 0://���ģʽ
                            //���ģʽӦ�ò��ԣ�����1��Get���� ����2��Post������֮ǰ�������ģʽ
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
        /// ��д���෽������ǰ��ͼģʽ
        /// 0�����ģʽ
        /// 1�����ģʽ 
        /// </summary>
        public override int ActiveViewIndex
        {
            get
            {
                if (!this.EnableWebPartMode)//���δ����WebPartģʽ���򷵻ػ����е�ActiveViewIndex;
                {
                    return (base.ActiveViewIndex < 0 ? 0 : base.ActiveViewIndex);//Ĭ�ϼ������ģʽ��ͼ
                }
                //����WebPartģʽ������ݵ�ǰWebPartManage.DisplayMode������
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
                if (!this.EnableWebPartMode)//���δ����WebPartģʽ�������û����е�ActiveViewIndex;
                {
                    base.ActiveViewIndex = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// ��HashTable��Ϣ���л�
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
        /// ����Ϣ�����л���һ��Hashtableʵ��
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
        /// ����ProxyWebPart���ģʽ�±���
        /// ProxyWebPart��ָUserControl����װΪGenericWebPart
        /// </summary>
        /// <param name="userControl">����װ��UserControl</param>
        /// <param name="title">����</param>
        public static void SetProxyWebPartDesignTitle(System.Web.UI.Control userControl, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "δ���ñ���";
            }
            title = string.Format("{0} - ����", title);
            userControl.Page.PreRenderComplete += new System.EventHandler(delegate(object sender, System.EventArgs e)
            {
                if (
               (userControl.Page.Items.Contains(typeof(WebPartManager)) && ((WebPartManager)userControl.Page.Items[typeof(WebPartManager)]).DisplayMode.Name.Equals("design", System.StringComparison.InvariantCultureIgnoreCase))//�����ģʽ
               && (userControl.Parent != null && userControl.Parent is GenericWebPart)//�û��ؼ���GenericWebPart��װ
               )
                {
                    //���ñ���
                    GenericWebPart webPartPorxy = userControl.Parent as GenericWebPart;
                    webPartPorxy.Title = title;
                }
            }
            );
        }

    }
}
