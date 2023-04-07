using Domain.Models;

namespace Application.Abstractions
{
    public interface ISeniorSchoolSubjectRepository
    {
        Task<List<SeniorSchoolSubject>> GetAllAsync();
        Task<SeniorSchoolSubject?> GetByIdAsync(int id);
        Task<SeniorSchoolSubject> CreateAsync(SeniorSchoolSubject seniorSchoolSubject);
        Task CreateRangeAsync(List<SeniorSchoolSubject> seniorSchoolSubjects);
        Task<SeniorSchoolSubject> UpdateAsync(SeniorSchoolSubject seniorSchoolSubject);
        Task UpdateAsync(List<SeniorSchoolSubject> seniorSchoolSubjects);
        Task DeleteAsync(int id);
    }

}
