using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Models;
using SACS.Services;
using SACS.Models;

namespace SACS
{
    public class AppData
    {
        public PhysicalEntity User;
        public readonly RestClient RestClient;
        public string BaseUri;
        public string Role;
        public string token;
        public ItemsStorage<Lesson> CurrentLessons;
        //chang
        public ItemsStorage<Lesson> CurrentStudentsGroups;
        public ItemsStorage<Lesson> CurrentAttendance;
        public AppData() {
            BaseUri = "http://sacs-gateway.herokuapp.com/gateway";
            Role = null;
            token = null;
            User = null;
            RestClient = new RestClient(BaseUri);
            CurrentLessons = new ItemsStorage<Lesson>();
            CurrentStudentsGroups = new ItemsStorage<Lesson>();
            CurrentAttendance = new ItemsStorage<Lesson>();
        }
        public void Clear() {
            User = null;
            Role = null;
            token = null;
            ClearContext();
        }
        public void ClearContext()
        {
            CurrentLessons.Clear();
            CurrentStudentsGroups.Clear();
            CurrentAttendance.Clear();
        }
    }
}
