using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Models;
using SACS.Services;
using SACS.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Linq;

namespace SACS
{
    public class AppData
    {
        public PhysicalEntity User;
        public readonly RestClient RestClient;
        public string BaseUri;
        public string Role;
        public string token;
        public int CurrentWeek { get => GetCurrentWeek(); }
        public ItemsStorage<Lesson> CurrentLessons;
        //chang
        public ItemsStorage<Lesson> CurrentStudentsGroups;
        public ItemsStorage<Lesson> CurrentAttendance;
        public AppData()
        {
            BaseUri = "http://sacs-gateway.herokuapp.com/gateway";
            Role = null;
            token = null;
            User = null;
            RestClient = new RestClient(BaseUri);
            CurrentLessons = new ItemsStorage<Lesson>();
            CurrentStudentsGroups = new ItemsStorage<Lesson>();
            CurrentAttendance = new ItemsStorage<Lesson>();
        }
        public void Clear()
        {
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
        private int GetCurrentWeek()
        {
            return DependencyService.Get<ISchedule>().GetCurrentWeek().Result;
        }
    }
    public class AppDataToken
    {
        PhysicalEntity User;
        DateTime nbf;
        DateTime exp;
        DateTime iat;
        public AppDataToken(PhysicalEntity entity)
        {
            User = entity;
            iat = DateTime.Now;
            nbf = iat;
            exp = iat.AddMinutes(5);
        }
    }
    public static class AppDataTokenExtentions
    {
        public static string EncodeToken(this AppData appData)
        {
            return EncodeDecrypt(JsonConvert.SerializeObject(new AppDataToken(appData.User))).ToString();
        }
        public static AppDataToken DecodeToken(this string token)
        {
            return JsonConvert.DeserializeObject<AppDataToken>(EncodeDecrypt(token));
        }    
        public static string EncodeDecrypt(string str)
        {
            ushort secretKey = 0x0088; // Секретный ключ (длина - 16 bit).
            var ch = str.ToArray(); //преобразуем строку в символы
            string newStr = "";      //переменная которая будет содержать зашифрованную строку
            foreach (var c in ch)  //выбираем каждый элемент из массива символов нашей строки
                newStr += TopSecret(c, secretKey);  //производим шифрование каждого отдельного элемента и сохраняем его в строку
            return newStr;
        }

        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey);
            return character;
        }


    }

}
