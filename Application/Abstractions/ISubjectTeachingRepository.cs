using Domain.Models;
namespace Application.Abstractions
{
    public interface ISubjectTeachingRepository
    {
        Task<List<SubjectTeaching>> AddRangeAsync(List<SubjectTeaching> subjectTeachings);
        Task<List<SubjectTeaching>> UpdateRangeAsync(List<SubjectTeaching> subjectTeachings);
    }
}
