using System;

namespace ETMS.Components.Exam.API.Entity
{
    [Serializable]
    public class IDName
    { 
        // 摘要:
        //     ID
        public Guid ID { get; set; }
        //
        // 摘要:
        //     名称
        public string Name { get; set; }
    }
}
