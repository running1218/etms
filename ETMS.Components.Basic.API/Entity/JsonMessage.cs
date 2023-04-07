namespace ETMS.Components.Basic.API.Entity
{
    public class JsonMessage<T>
    {
        public bool Status { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ResultData
    {
        public string ID { get; set; }
        public int Pages { get; set; }
    }
}
