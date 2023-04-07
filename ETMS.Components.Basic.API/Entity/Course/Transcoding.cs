using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    public class Transcoding : AbstractObject
    {
        public Transcoding() { }
        public Guid TaskID { get; set; }

        public string Status { get; set; }

        public int Duration { get; set; }

        public string Outpath { get; set; }

        #region Override

        public override string DefaultKeyName
        {
            get { return "TaskID"; }
        }

        public override object KeyValue
        {
            get
            {
                return this.TaskID;
            }
            set
            {
                this.TaskID = (Guid)value;
            }
        }

        #endregion override

    }
}
