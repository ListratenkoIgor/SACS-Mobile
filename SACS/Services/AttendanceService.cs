using Interfaces.Models.AttendanceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SACS.Services
{
    internal class AttendanceService : IAttendance
    {
        public async Task<bool> SendAttendances(IEnumerable<Attendance> attendances)
        {
            throw new NotImplementedException();
        }
    }
}
