using Interfaces.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SACS.Models
{
    public class AttendanceStudent
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string RecordbookNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Group { get; set; }
        private string _attendanceTime { get; set; }
        private bool _isPresent { get; set; }
        public string AttendanceTime { get => _attendanceTime.IsNullOrEmpty() ? "-" : _attendanceTime; set => _attendanceTime = value; }
        public bool IsPresent { get => _isPresent; set => SetIsPresent(value); }
        private void SetIsPresent(bool value) { 
            _isPresent = value;
            var now = DateTime.Now;
            AttendanceTime = value ?$"{now.Hour}:{now.Minute}":null;
        }
    }
    public static class StudentsStreamExtention
    {
        public static List<AttendanceStudent> GetAttendanceStudents(this List<StudentDto> stream)
        {
            var list = new List<AttendanceStudent>();
            foreach (var student in stream)
            {
                list.Add(student.GetAttendanceStudent());
            }
            return list;
        }
        public static AttendanceStudent GetAttendanceStudent(this StudentDto student)
        {
            return new AttendanceStudent
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                ImageUrl = student.ImageUrl,
                Id = student.Id,
                Group = student.Group,
                RecordbookNumber = student.RecordbookNumber,
                AttendanceTime = null,
                IsPresent = false
            };
        }
        public static List<AttendanceStudent> GetStudents(this StudentsStream student)
        {
            var result = new List<AttendanceStudent>();
            foreach (var pair in student) {
                result.AddRange(pair.Value.GetAttendanceStudents());
            }
            return result.Select(x=>x).OrderBy(x=>x.Group).ToList();
        }
    }
}
