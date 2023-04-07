using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DataAccess.Repositories
{
    public class CurrentGradingSystemRepository : ICurrentGradingSystemRepository
    {
        private readonly SocialDbContext _context;
        private readonly ILogger<CurrentGradingSystemRepository> _logger;

        public CurrentGradingSystemRepository(SocialDbContext context, ILogger<CurrentGradingSystemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CurrentGradingSystem> Create(CurrentGradingSystem gradingSystem)
        {
            try
            {
                _logger.LogInformation("Creating Current grading system with ID {Id}.", gradingSystem.Id);
                if (_context.CurrentGradingSystems.Any())
                {
                    _logger.LogInformation("CurrentGradingSystem already exist");
                    return gradingSystem;
                }
                _context.CurrentGradingSystems.Add(gradingSystem);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Current grading system with ID {Id} created.", gradingSystem.Id);
                return gradingSystem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating current grading system.");
                throw new CurrentGradingSystemException("Error creating current grading system.", ex);
            }
        }

        public async Task<CurrentGradingSystem?> GetFirst()
        {
            try
            {
                _logger.LogInformation("Fetching all current grading systems.");
                return await _context.CurrentGradingSystems.FirstOrDefaultAsync();
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching current grading system.");
                throw new CurrentGradingSystemException("Error fetching current grading system.", ex);
            }
        }

        public async Task<CurrentGradingSystem> Update(CurrentGradingSystem gradingSystem)
        {
            try
            {
                var gradingSystemCurr = await _context.CurrentGradingSystems.FindAsync(gradingSystem.Id) ?? throw new CurrentGradingSystemException($"Current grading system with ID {gradingSystem.Id} not found.");
                gradingSystemCurr.GradingSystem = gradingSystem.GradingSystem;
                _context.CurrentGradingSystems.Update(gradingSystemCurr);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Current grading system with ID {Id} updated.", gradingSystem.Id);
                return gradingSystem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating current grading system with ID {Id}.", gradingSystem.Id);
                throw new CurrentGradingSystemException("Error updating current grading system with ID {Id}.", ex);
            }
        }
    }

}
