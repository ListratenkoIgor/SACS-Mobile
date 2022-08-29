using Interfaces.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SACS.Services
{
    public class AuthService : IAuth
    {
        /*
        public async Task<string> Login(string username, string password)
        {
            var request = new RestRequest("auth/Account/Login")
                         .AddHeader("Content-Type", "application/json;charset=utf-8")
                         .AddHeader("Content-Length", 0)
                         .AddQueryParameter("username", username)
                         .AddQueryParameter("password", password);
            var response = await AppData.RestClient.PostAsync(request);
            if (response.IsSuccessful)
            {
                string token = response.Content;
                if (token != null)
                {
                    AppData.token = token;
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    var user = new PhysicalEntity()
                    {
                        FirstName = jwtSecurityToken.Payload["FirstName"].ToString(),
                        LastName = jwtSecurityToken.Payload["LastName"].ToString(),
                        MiddleName = jwtSecurityToken.Payload["MiddleName"].ToString(),
                        isStudent = (bool)jwtSecurityToken.Payload["MiddleName"],
                        RecordBookNumber = jwtSecurityToken.Payload["RecordBookNumber"].ToString(),
                        Group = jwtSecurityToken.Payload["Group"].ToString(),
                        UrlId = jwtSecurityToken.Payload["UrlId"].ToString()
                    };
                    AppData.Role = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Role).Value;
                    AppData.User = user;
                }
                return string.Empty;
            }
            else
            {
                return $"Error {response.StatusCode}. {response.ErrorMessage}";
            }
        }
        */
        public async Task<string> Login(string username, string password)
        {          
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImctZGFuaWxvdmEiLCJuYW1laWQiOiJnLWRhbmlsb3ZhIiwicm9sZSI6IkVtcGxveWVlIiwibmJmIjoxNjYxNzU1ODEwLCJleHAiOjE2NjE3OTE4MTAsImlhdCI6MTY2MTc1NTgxMCwiRmlyc3ROYW1lIjoi0JPQsNC70LjQvdCwIiwiTGFzdE5hbWUiOiLQlNCw0L3QuNC70L7QstCwIiwiTWlkZGxlTmFtZSI6ItCS0LvQsNC00LjQvNC40YDQvtCy0L3QsCIsImlzU3R1ZGVudCI6ZmFsc2UsIlVybElkIjoiZy1kYW5pbG92YSJ9.aXzbmoPon9QY0k1sYZshXz4UzbvTDqug8q6R0mcKGh4";
            if (token != null)
            {
                var AppData = DependencyService.Get<AppData>();
                AppData.token = token;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var user = new PhysicalEntity()
                {
                    FirstName = jwtSecurityToken.PayloadExist("FirstName").ToString(),
                    LastName = jwtSecurityToken.PayloadExist("LastName").ToString(),
                    MiddleName = jwtSecurityToken.PayloadExist("MiddleName").ToString(),
                    isStudent = (bool)jwtSecurityToken.PayloadExist("isStudent"),
                    RecordBookNumber = jwtSecurityToken.PayloadExist("RecordBookNumber")?.ToString(),
                    Group = jwtSecurityToken.PayloadExist("Group")?.ToString(),
                    UrlId = jwtSecurityToken.PayloadExist("UrlId")?.ToString()
                };
                AppData.Role = jwtSecurityToken.PayloadExist("role").ToString();
                //AppData.Role = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Role).Value ;
                AppData.User = user;
            }
            return string.Empty;
        }      
        public void Logout()
        {
            var AppData = DependencyService.Get<AppData>();
            var request = new RestRequest("auth/Account/Logout")
                         .AddHeader("Authorization", $"Bearer {AppData.token}");
            AppData.RestClient.GetAsync(request);
            AppData.Clear();
        }
    }
    public static class PayloadExtention {
        public static object PayloadExist(this JwtSecurityToken jwtSecurityToken, string subject)
        {
            try
            {
                return jwtSecurityToken.Payload[subject];
            }
            catch
            {
                return null;
            }
        }
    }
}
