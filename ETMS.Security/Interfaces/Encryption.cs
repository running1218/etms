namespace ETMS.Security
{
    /// <summary>
    /// ʵ��Ticket���ܡ����ܵĽӿ�
    /// </summary>
    public interface ITicketEncryption
    {
        /// <summary>
        /// ����Ticket
        /// </summary>
        /// <param name="ticket">Ticket����</param>
        /// <param name="oParam">���ӵĲ���</param>
        /// <returns>����һ�����ܹ��Ķ�������</returns>
        byte[] EncryptTicket(ITicket ticket, object oParam);

        /// <summary>
        /// ����Ticket
        /// </summary>
        /// <param name="encryptedData">���ܹ���ticket�Ķ���������</param>
        /// <param name="oParam">���ӵĲ���</param>
        /// <returns>���ܺ��ticket����</returns>
        ITicket DecryptTicket(byte[] encryptedData, object oParam);
    }

    /// <summary>
    /// ʵ��string(Cookie)�ļ��ܡ����ܵĽӿ�
    /// </summary>
    public interface IStringEncryption
    {
        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="strData">�ַ�������</param>
        /// <returns>���ܺ�Ķ�������</returns>
        byte[] EncryptString(string strData);

        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="encryptedData">���ܹ�������</param>
        /// <returns>���ܺ���ַ���</returns>
        string DecryptString(byte[] encryptedData);
    }
}
