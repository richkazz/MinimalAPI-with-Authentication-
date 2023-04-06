using Domain.Models;
namespace Application.Abstractions
{
    public interface ICurrentGradingSystemRepository
    {
        Task<CurrentGradingSystem> Create(CurrentGradingSystem gradingSystem);
        Task<CurrentGradingSystem> Update(CurrentGradingSystem gradingSystem);
        Task<CurrentGradingSystem?> GetFirst();
    }
}
