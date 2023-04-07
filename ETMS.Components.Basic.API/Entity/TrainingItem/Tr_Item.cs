
using ETMS.AppContext;
namespace ETMS.Components.Basic.API.Entity.TrainingItem
{
    /// <summary>
    /// 培训项目表业务实体
    /// </summary>
    public partial class Tr_Item:AbstractObject
	{
        public int CourseCount
        {
            get;
            set;
        }
	}
}
