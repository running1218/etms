namespace ETMS.Components.Evaluation.API.Entity
{
    public class EvaluationExt : Evaluation_Item
    {
        public string ObjectID { get; set; }
        public int Score { get; set; }
        public int UserID { get; set; }
        public int ApproveStatus { get; set; }
    }
}
