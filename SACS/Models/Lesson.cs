using Interfaces.Shedule.IisApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace SACS.Models
{
    //   https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/customizing-list-appearance
    public class Lesson
    {
        public string Id { get; set; }
        private List<string> auditories { get; set; }
        private string startLessonTime { get; set; }
        private string endLessonTime { get; set; }
        private int numSubgroup { get; set; }
        private string subject { get; set; }
        private string subjectFullName { get; set; }
        private string lessonTypeAbbrev { get; set; }
        public string Note { get; set; }
        private List<Employee> employees { get; set; }
        private List<Studentgroup> studentGroups { get; set; }
        public string Auditory
        {
            get
            {
                if ((auditories != null) && (auditories?.Count != 0))
                    return auditories[0];
                return "";
            }
        }
        public string Start { get => startLessonTime; }
        public string End { get => endLessonTime; }
        public string Subject { get => $"{subject} ({lessonTypeAbbrev})"; }
        public string LessonType { get => $"{lessonTypeAbbrev}"; }
        public string Employee { get => employees[0]?.GetLastNameInitiales(); }
        public string Groups { get => studentGroups.GetGroups(); }
        public List<string> GetGroupsList() { return studentGroups.GetGroupsList(); }
        public Lesson(string Id, DayShedule map) {
            this.Id = Id;
            this.auditories = map.auditories;
            this.endLessonTime = map.endLessonTime;
            this.lessonTypeAbbrev = map.lessonTypeAbbrev;
            this.numSubgroup = map.numSubgroup;
            this.startLessonTime = map.startLessonTime;
            this.studentGroups = map.studentGroups;
            this.subject = map.subject;
            this.subjectFullName = map.subjectFullName;
            this.employees = map.employees;
        }
    }
}
