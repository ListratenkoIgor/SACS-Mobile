using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SACS.Services
{
    public interface IData
    {
        Task<StudentsStream> GetStudentsByGroups(List<string> groupsNumbers);
        Task<List<StudentDto>> GetStudentsByGroup(string groupsNumbers);
    }
}
