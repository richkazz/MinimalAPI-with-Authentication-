using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface ISchoolSubjectsRepository
    {
        Task<List<SchoolSubjects>> GetAllAsync();
        Task<SchoolSubjects?> GetByIdAsync(int id);
        Task<SchoolSubjects> CreateAsync(SchoolSubjects schoolSubjects);
        Task<SchoolSubjects> UpdateAsync(SchoolSubjects schoolSubjects);
        Task DeleteAsync(int id);
    }
}
