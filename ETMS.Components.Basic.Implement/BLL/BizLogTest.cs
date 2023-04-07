using System;
using ETMS.Utility.Logging;
namespace ETMS.Components.Basic.Implement.BLL
{
    [Serializable]
    public class BizTestItem : ETMS.AppContext.AbstractObject
    {
        public Guid ID { get; set; }
        public override string DefaultKeyName
        {
            get { return "ID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.ID;
            }
            set
            {
                ID = (Guid)value;
            }
        }


        public string Name { get; set; }
    }
    public class BizLogTest
    {
        public void Add(BizTestItem item)
        {
            BizLogHelper.AddOperate(item);
        }
        //public void Update(Guid id)
        //{
        //    BizLogHelper.Log(this.GetType(), id, "修改记录");
        //}
        //public void Delete(Guid id)
        //{
        //    BizLogHelper.Log(this.GetType(), id, "删除记录");
        //}
    }
}
