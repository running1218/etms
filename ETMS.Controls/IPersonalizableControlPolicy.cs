using System.Web.UI.WebControls.WebParts;
using System.Collections;

namespace ETMS.Controls
{
    /// <summary>
    /// ���Ի��ؼ�ģʽö�ٶ���
    /// </summary>
    public enum PersonalizableControlMode
    {
        /// <summary>
        /// ���ģʽ
        /// </summary>
        Browser = 0,
        /// <summary>
        /// ���ģʽ
        /// </summary>
        Designer = 1
    }
    /// <summary>
    /// ���Ի��ؼ����Խӿ�
    /// ����ؼ���Ҫ���������һЩ���ԣ�����ʵ�ִ˽ӿڡ�
    /// </summary>
    public interface IPersonalizableControlPolicy
    {
        /// <summary>
        /// �ؼ�����
        /// ����:�����ƿؼ����չʾ��һЩ������
        /// ���ڣ�Ԫ����Personalizable���ܼ̳У���ˣ��ؼ���������ʵ�ִ˽ӿ�ʱ���ֶ����[Personalizable(true)]
        /// </summary>
        [Personalizable(true)]
        string Policy { get;set;}

        /// <summary>
        /// �����ֵ�
        /// ���ؼ��������ֵ�ķ�ʽ�ṩ���ؼ������ߣ����㿪����
        /// </summary>
        Hashtable ControlParms { get;set;}
        /// <summary>
        /// �����ؼ�����
        /// ������ָ�����ؼ���ƴ����еĲ������������
        /// </summary>
        void BuildPolicy();

        /// <summary>
        /// ���ؼ�����Ӧ�õ���ƴ����еĿؼ�
        /// </summary>
        void ApplyPolicy_Design();

        /// <summary>
        /// ���ؼ�����Ӧ�õ���������еĿؼ�
        /// </summary>
        void ApplyPolicy_Browse();

        ///// <summary>
        ///// �ؼ�ģʽ
        ///// </summary>
        //PersonalizableControlMode ControlMode { get;set;}

        ///// <summary>
        ///// �Ƿ�Ӧ��WebPartģʽ��Ĭ�Ͽ�����
        ///// </summary>
        //bool EnableWebPartMode { get;set;}

    }
}
