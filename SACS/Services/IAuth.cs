using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SACS.Services
{
    public interface IAuth
    {
        Task<string> Login(string username, string password);
        //string Register(UserRegisterModel model)
        void Logout();
    }
}
