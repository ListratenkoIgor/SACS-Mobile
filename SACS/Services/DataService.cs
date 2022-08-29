using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SACS;
using Interfaces.Shedule.IisApi;
using Interfaces.Models;
using RestSharp;
using Newtonsoft.Json;
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
            RestResponse response = await AppData.RestClient.GetAsync(request);
            if (response.IsSuccessful)
            {
                try
                {
                    List<StudentDto> result = JsonConvert.DeserializeObject<List<StudentDto>>(response.Content);
                    return result;
                }
                catch { }
            }
            return null;
        }

        public async Task<StudentsStream> GetStudentsByGroups(List<string> groupsNumbers)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            request = new RestRequest($"data/Students/stream");
            request.AddQueryParameter("groupsNumbers", JsonConvert.SerializeObject(groupsNumbers));
            request.AddHeader("Authorization", $"Bearer {AppData.token}");
            RestResponse response = await AppData.RestClient.GetAsync(request);
            if (response.IsSuccessful)
            {
                try
                {
                    StudentsStream result = JsonConvert.DeserializeObject<StudentsStream>(response.Content);
                    return result;
                }
                catch { }
            }
            return null;
        }
    }
}
