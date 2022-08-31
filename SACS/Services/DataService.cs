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
using Newtonsoft.Json;
using Nancy.Json;

namespace SACS.Services
{
    public class DataService : IData
    {
        private string _baseUri = "https://sacs-data.herokuapp.com";
        public async Task<List<StudentDto>> GetStudentsByGroup(string groupsNumber)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            request = new RestRequest($"Students/group/");
            //request = new RestRequest($"data/Students/group/");
            request.AddQueryParameter("groupsNumber", groupsNumber);
            request.AddHeader("Authorization", $"Bearer {AppData.token}");
            var client = new RestClient(_baseUri);
            var response = await client.GetAsync<List<StudentDto>>(request);
            //var response = AppData.RestClient.Get<List<StudentDto>>(request);
            return response;
        }

        public async Task<StudentsStream> GetStudentsByGroups(List<string> groupsNumbers)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            //request = new RestRequest($"data/Students/stream");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var groups = javaScriptSerializer.Serialize(groupsNumbers);
            //var groups = Newtonsoft.Json.JsonConvert.SerializeObject(new{ groupsNumbers=groupsNumbers});
            request = new RestRequest($"Students/stream/{groups}");
            //request.AddQueryParameter("groupsNumbers", groups);
            request.AddHeader("Authorization", $"Bearer {AppData.token}");

            //var client = new RestClient(_baseUri);
            //var response =  client.Get<StudentsStream>(request);
            //var response = AppData.RestClient.Get<StudentsStream>(request);

            try
            {
                var client = new RestClient(_baseUri);
                var response = client.GetAsync(request);
                response.Wait();
                //RestResponse response = await AppData.RestClient.GetAsync(request);
                if (response.Result.IsSuccessful)
                {
                    var result = JsonConvert.DeserializeObject<StudentsStream>(response.Result.Content);
                    return result;
                }
                else
                {
                    throw new Exception(response.Result.ErrorMessage);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            //return response;
        }
    }
}
