using Interfaces.Models.AttendanceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SACS.Services
{
    public interface IAttendance
    {
        Task<bool> SendAttendances(IEnumerable<Attendance> attendances);
    }
}
