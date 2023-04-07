using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.DAL.Security;
namespace ETMS.Components.Basic.Implement.BLL.Security
{
    /// <summary>
    /// 功能管理
    /// </summary>
    public class FunctionLogic
    {
        private IDataAccess DAL = new FunctionDataAccess();

        public void Save(Function function)
        {
            if (function.FunctionID == 0)
            {
                DAL.Add(function);
            }
            else
            {
                DAL.Update(function);
            }
        }

        public void Remove(Function function)
        {
            DAL.Delete(function);
        }

        public Function GetFunctionByID(int functionID)
        {
            return (Function)DAL.Query((int)functionID);
        }
    }
}
