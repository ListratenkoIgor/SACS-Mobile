using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Shedule.IisApi;
using Interfaces.Models;
using RestSharp;

namespace SACS.Services
{
    public interface ISchedule
    {
        Task<List<DayShedule>> LoadSchedule(PhysicalEntity user);
    }
}
