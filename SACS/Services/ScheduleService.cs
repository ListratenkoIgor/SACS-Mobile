using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Shedule.IisApi;
using Interfaces.Models;
using RestSharp;
using Xamarin.Forms;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SACS.Services
{
    public class ScheduleService : ISchedule
    {
        private string _baseUri = "https://sacs-shedule.herokuapp.com/Schedule";

        public async Task<int> GetCurrentWeek()
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            request = new RestRequest($"CurrentWeek");
            try
            {
                var client = new RestClient(_baseUri);
                var response1 = client.GetAsync(request);
                response1.Wait();
                var response = response1.Result;
                //RestResponse response = await AppData.RestClient.GetAsync(request);
                if (response.IsSuccessful)
                {
                    int result = JsonConvert.DeserializeObject<int>(response.Content);
                    return result;
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }
            }
            catch
            {
                throw;
            }
            return 0;
        }
    

        public async Task<List<DayShedule>> LoadSchedule(PhysicalEntity user)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            if (user.isStudent)
            {
                request = new RestRequest($"StudentsGroups/{user.Group}");
                //request = new RestRequest($"Schedule/StudentsGroups/{user.Group}");
            }
            else
            {
                request = new RestRequest($"Employees/{user.UrlId}");
                //request = new RestRequest($"Schedule/Employees/{user.UrlId}");
            }
            try
            {
                var client = new RestClient(_baseUri);
                var response = client.GetAsync(request);
                response.Wait();
                //RestResponse response = await AppData.RestClient.GetAsync(request);
                if (response.Result.IsSuccessful)
                {
                    ScheduleResponseDto result = JsonConvert.DeserializeObject<ScheduleResponseDto>(response.Result.Content);
                    return result.GetWeekShedule();
                }
                else
                {
                    throw new Exception(response.Result.ErrorMessage);
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

    }
}
