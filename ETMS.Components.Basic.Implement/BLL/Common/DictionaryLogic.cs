
using System.Data;
using ETMS.Components.Basic.Implement.DAL.Common;
using ETMS.Components.Basic.API.Entity.Common;

namespace ETMS.Components.Basic.Implement.BLL.Common
{
    public class DictionaryLogic
    {
        private static readonly DictionaryDataAccess dictionary = new DictionaryDataAccess();

        public void DictionaryEdit(DictionaryParm parm)
        {
            dictionary.DictionaryEdit(parm);
        }

        public void DictionaryDelete(DictionaryParm parm)
        {
            dictionary.DictionaryDelete(parm.TableEnglishName, parm.ColumnCodeValue);
        }
        public DataTable GetDictionaryBycode(string tableName, string columnCode)
        {
            return dictionary.GetDictionaryBycode(tableName, columnCode);
        }
        public DataTable GetDictionaryList(string tableName)
        {
            return dictionary.GetDictionaryList(tableName);
        }

        public void SavaCatalog(int id, string code, string name)
        {
            dictionary.SavaCatalog(id, code, name);
        }

        public void SavaSpecialty(int id, string code, string name)
        {
            dictionary.SavaSpecialty(id, code, name);
        }
    }
}
