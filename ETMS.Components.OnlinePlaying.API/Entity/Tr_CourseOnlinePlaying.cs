using System;

namespace ETMS.Components.OnlinePlaying.API.Entity
{
    [Serializable]
    public partial class Tr_CourseOnlinePlaying
    {
        public string OnlinePlayingID {get;set;}
        public string PlayingNo {get;set;}
        public Guid TrainingItemCourseID {get;set;}
        public string PlayingSubject {get;set;}
        public DateTime StartTime {get;set;}
        public DateTime EndTime { get; set; }
        public int PlayingTime {get;set;}
        public int TeacherID {get;set;}
        public string TeacherName {get;set;}
        public int WindowSize {get;set;}
        public string Skin {get;set;}
        public string OrganizerJoinUrl {get;set;}
        public string OrganizerToken {get;set;}
        public string PanelistJoinUrl {get;set;}
        public string PanelistToken {get;set;}
        public string AttendeeJoinUrl {get;set;}
        public string AttendeeToken {get;set;}
        public bool OnlineStatus {get;set;}
        public string Description { get; set; }
        public DateTime CreateTime {get;set;}
        public string CreateUser {get;set;}
        public int CreateUserID { get; set; } 
    }
}
