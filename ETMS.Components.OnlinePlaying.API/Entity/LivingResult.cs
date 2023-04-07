namespace ETMS.Components.OnlinePlaying.API.Entity
{
    public class LivingResult
    {
        public string code { get; set; }

        public string msg { get; set; }

        public LivingData data { get; set; }
    }

    public class LivingData
    {
        public string partner_id { get; set; }
        public string bid { get; set; }
        public string zhubo_key { get; set; }
        public string admin_key { get; set; }
        public string user_key { get; set; }
        public string course_id { get; set; }
    }
}
