using ETMS.Components.Basic.Implement.DAL;

namespace ETMS.Components.Basic.Implement.BLL
{
    public class PassportVistorLogic
    {
        protected readonly static PassportVistorDataAccess DAL = new PassportVistorDataAccess();
        public void Save(int id, int num)
        {
            DAL.Save(id, num);
        }

        public int GetNum(int id)
        {
            return DAL.GetTotalNum(id);
        }
    }
}
