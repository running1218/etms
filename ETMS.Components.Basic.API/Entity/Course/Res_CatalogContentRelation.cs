using ETMS.AppContext;
using System;

namespace ETMS.Components.Basic.API.Entity.Course
{
    [Serializable]
    public partial class Res_CatalogContentRelation : AbstractObject
    {
        public Res_CatalogContentRelation() { }

        #region Properties
        /// <summary>
		/// 外键
		/// <summary>
		public Guid CatalogID { get; set; }
        /// <summary>
		/// 外键
		/// <summary>
		public Guid ContentID { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public Int32 Sort { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public Int32 Status { get; set; }
        /// <summary>
		/// 
		/// <summary>
		public bool IsOpen { get; set; }

        #endregion Properties

        #region Override

        public override string DefaultKeyName
        {
            get { return ""; }
        }

        public override object KeyValue
        {
            get
            {
                return null;
            }
            set
            {
                //this.CatalogID = (Guid)value;
            }
        }

        #endregion override

    }
}
