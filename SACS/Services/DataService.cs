using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SACS;
using Interfaces.Shedule.IisApi;
using Interfaces.Models;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xamarin.Forms;

namespace SACS.Services
{
    public class DataService : IData
    {
        public async Task<List<StudentDto>> GetStudentsByGroup(string groupsNumber)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            request = new RestRequest($"data/Students/group/");
            request.AddQueryParameter("groupsNumber", groupsNumber);
            var response = AppData.RestClient.Get<List<StudentDto>>(request);
            return response;
        }

        public async Task<StudentsStream> GetStudentsByGroups(List<string> groupsNumbers)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            request = new RestRequest($"data/Students/stream");
            request.AddQueryParameter("groupsNumbers", JsonSerializer.Serialize(groupsNumbers));
            request.AddHeader("Authorization", $"Bearer {AppData.token}");
            var response = AppData.RestClient.Get<StudentsStream>(request);
            return response;
        }
    }
}
