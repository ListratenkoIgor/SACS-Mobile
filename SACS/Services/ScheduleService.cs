using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Shedule.IisApi;
using Interfaces.Models;
using RestSharp;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SACS.Services
{
    public class ScheduleService : ISchedule
    {
        public async Task<List<DayShedule>> LoadSchedule(PhysicalEntity user)
        {
            var AppData = DependencyService.Get<AppData>();
            RestRequest request;
            if (user.isStudent)
            {
                request = new RestRequest($"schedule/StudentsGroups/{user.Group}");
            }
            else
            {
                request = new RestRequest($"schedule/Employees/{user.UrlId}");
            }
            request.AddHeader("Authorization", $"Bearer {AppData.token}");
            RestResponse response = await AppData.RestClient.GetAsync(request);
            if (response.IsSuccessful)
            {
                try
                {
                    ScheduleResponseDto result = JsonConvert.DeserializeObject<ScheduleResponseDto>(response.Content);
                    return result.GetTodayShedule();
                }
                catch { }
            }
            return null;
        }
    }
}
