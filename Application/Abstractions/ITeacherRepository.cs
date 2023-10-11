using Domain.Models;
using Domain.ViewModel;

namespace Application.Abstractions
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetByIdAsync(int id);
        Task<List<Teacher>?> GetAllAsync();
        Task<Teacher?> CreateAsync(Teacher teacher ,TeacherViewModel teacherViewModels);
        Task<Teacher?> UpdateAsync(Teacher teacher, TeacherViewModel teacherViewModels);
        Task DeleteAsync(int id);
    }
}
