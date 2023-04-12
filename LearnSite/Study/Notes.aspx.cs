﻿using ETMS.Utility;
using System;

namespace ETMS.Studying.Study
{
    public partial class Notes : System.Web.UI.Page
    {

        /// <summary>
        /// 项目课程关系ID
        /// </summary>
        public Guid TrainingItemCourseID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            TrainingItemCourseID = Request["TrainingItemCourseID"].ToGuid();
        }
    }
}