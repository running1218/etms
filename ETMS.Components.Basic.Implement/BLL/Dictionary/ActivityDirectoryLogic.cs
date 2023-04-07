using System.Data;
using ETMS.Components.Basic.Implement.DAL.Dictionary;

namespace ETMS.Components.Basic.Implement.BLL.Dictionary
{
    /// <summary>
    /// contains folow directories
    /// {ActivityArea/ActivityGroup/ActivityPrize/ActivityProductType/ActivityRegion/ActivityShape/ActivityType}
    /// </summary>
    public partial class ActivityDirectoryLogic
    {
        private static readonly ActivityDirectoryDataAccess DAL = new ActivityDirectoryDataAccess();
        public DataTable GetAreaList(int level)
        {
            return DAL.GetAreaList(level);
        }

        public DataTable GetAreaListByParent(string parent)
        {
            return DAL.GetAreaListByParent(parent);
        }

        public DataTable GetGroupList()
        {
            return DAL.GetGroupList();
        }

        public DataTable GetPrizeList()
        {
            return DAL.GetPrizeList();
        }

        public DataTable GetProductTypeList()
        {
            return DAL.GetProductTypeList();
        }

        public DataTable GetRegionList()
        {
            return DAL.GetRegionList();
        }

        public DataTable GetShapeList()
        {
            return DAL.GetShapeList();
        }

        public DataTable GetActivityTypeList()
        {
            return DAL.GetActivityTypeList();
        }
    }
}
