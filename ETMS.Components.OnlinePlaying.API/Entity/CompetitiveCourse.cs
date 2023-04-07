using System;
using System.Collections.Generic;


namespace ETMS.Components.OnlinePlaying.API.Entity
{
    public class CompetitiveCourse
    {
        public Guid CourseID{get;set;}
        public string CourseName{get;set;}
        public decimal Price{get;set;}
        public decimal DiscountPrice{get;set;}
        public string ForObject{get;set;}
        public decimal CourseHours{get;set;}
        public string ThumbnailURL { get; set; }

        public string TeacherName { get; set; }
        public int livingNum { get; set; }

        public Res_Living Living { get; set; }

        public List<Res_Living> Livings { get; set; }

        public string LivingTime { get; set; }
        public int SignupNum { get; set; }

        public string CourseOutline { get; set; }

        public string CourseIntroduction { get; set; }
    }
}
