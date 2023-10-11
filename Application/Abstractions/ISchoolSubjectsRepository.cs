using Domain.Models;

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
