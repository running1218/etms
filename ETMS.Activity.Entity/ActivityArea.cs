namespace ETMS.Activity.Entity
{
    public partial class ActivityArea
    {
        public int AreaID { get; set; }
        public int RegionLevel { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string ParentCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
