using Domain.Models;

namespace Application.Abstractions
{
    public interface IJuniorSchoolSubjectRepository
    {
        Task<JuniorSchoolSubject?> GetByIdAsync(int id);
        Task<List<JuniorSchoolSubject>> GetAllAsync();
        Task<JuniorSchoolSubject> CreateAsync(JuniorSchoolSubject juniorSchoolSubject);
        Task<JuniorSchoolSubject> UpdateAsync(JuniorSchoolSubject juniorSchoolSubject);
        Task<List<JuniorSchoolSubject>> CreateRangeAsync(List<JuniorSchoolSubject> juniorSchoolSubjects);
        Task UpdateAsync(List<JuniorSchoolSubject> juniorSchoolSubjects);
        Task DeleteAsync(int id);
    }

}
