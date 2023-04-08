using Domain.Models;

namespace Application.Abstractions
{
    public interface IClassInSchoolRepository
    {
        Task<ClassInSchool?> GetByIdAsync(int id);
        Task<List<ClassInSchool>> GetAllAsync();
        Task<ClassInSchool> AddAsync(ClassInSchool classInSchool);
        Task<ClassInSchool> UpdateAsync(ClassInSchool classInSchool);
        Task DeleteAsync(int id);
        Task<List<ClassInSchool>> AddRangeAsync(List<ClassInSchool>? classes);
    }
}
