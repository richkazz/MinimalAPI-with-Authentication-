using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class ActiveSchoolTermRepository : IActiveSchoolTermRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<ActiveSchoolTermRepository> _logger;

        public ActiveSchoolTermRepository(SocialDbContext dbContext, ILogger<ActiveSchoolTermRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ActiveSchoolTerm?> GetActiveSchoolTermsAsync()
        {
            _logger.LogInformation("Getting active school terms");
            return await _dbContext.ActiveSchoolTerms.FirstOrDefaultAsync();
        }

        public async Task<ActiveSchoolTerm?> GetActiveSchoolTermByIdAsync(int id)
        {
            _logger.LogInformation($"Getting active school term with id {id}");
            return await _dbContext.Set<ActiveSchoolTerm>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateActiveSchoolTermAsync(ActiveSchoolTerm activeSchoolTerm)
        {
            try
            {
                _logger.LogInformation("Creating new active school term");
                if (_dbContext.ActiveSchoolTerms.Any())
                {
                    throw new ActiveSchoolTermException("Can not create more than one");
                }
                _dbContext.Set<ActiveSchoolTerm>().Add(activeSchoolTerm);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating active school term");
                throw new ActiveSchoolTermException("Error occurred while creating active school term",ex);
            }
        }

        public async Task UpdateActiveSchoolTermAsync(int id, ActiveSchoolTerm activeSchoolTerm)
        {
            try
            {
                _logger.LogInformation($"Updating active school term with id {id}");
                var existingActiveSchoolTerm = await GetActiveSchoolTermByIdAsync(id);
                if (existingActiveSchoolTerm == null)
                {
                    throw new ActiveSchoolTermException("ActiveSchoolTerm not found.");
                }

                existingActiveSchoolTerm.ActiveTerm = activeSchoolTerm.ActiveTerm;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating active school term with id {id}");
                throw new ActiveSchoolTermException($"Error occurred while updating active school term with id {id}",ex);
            }
        }

        public async Task DeleteActiveSchoolTermAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting active school term with id {id}");
                var existingActiveSchoolTerm = await GetActiveSchoolTermByIdAsync(id);
                if (existingActiveSchoolTerm == null)
                {
                    throw new ActiveSchoolTermException("ActiveSchoolTerm not found.");
                }

                _dbContext.ActiveSchoolTerms.Remove(existingActiveSchoolTerm);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting active school term with id {id}");
                throw new ActiveSchoolTermException($"Error occurred while deleting active school term with id {id}",ex);
            }
        }
    }

}

