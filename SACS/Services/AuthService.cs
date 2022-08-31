using Interfaces.Models;
using Interfaces.Models.AuthModels;
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
        private string _baseUri = "https://sacs-auth.herokuapp.com";
        public async Task<string> Login(string username, string password)
        {
            //var request = new RestRequest("auth/Account/Login", Method.Post);
                         //.AddHeader("Content-Type", "application/json;charset=utf-8")
                         //.AddHeader("Content-Length", 0)
                         //.AddQueryParameter("username", username).AddQueryParameter("password", password);
            var request = new RestRequest("Account/Login", Method.Post);
            request.RequestFormat = DataFormat.Json;
            //request.AddBody(new LoginModel { Username=username,Password=password});
            request.AddBody(new LoginModel { Username="g-danilova",Password="Test2Test2"});
            //request.AddBody(new LoginModel { Username="85100093",Password="Test1Test1"});
            var AppData = DependencyService.Get<AppData>();
            //var response = AppData.RestClient.PostAsync(request);
            var client = new RestClient(_baseUri);
            var response = client.ExecuteAsync(request);
            try
            {
                response.Wait();
                if (response.Result.IsSuccessful)
                {
                    string token = response.Result.Content;
                    if (token != null)
                    {
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
                else
                {
                    return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}";
                }
            }
            catch { return $"Error {response.Result.StatusCode}. {response.Result.ErrorMessage}\n{response.Result.Content}"; }
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
                user.UrlId = "s-kulikov";
                AppData.User = user;

            }
            return string.Empty;
        }      
        public void Logout()
        {
            var AppData = DependencyService.Get<AppData>();
            var request = new RestRequest("Account/Logout")
            //var request = new RestRequest("auth/Account/Logout")
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
