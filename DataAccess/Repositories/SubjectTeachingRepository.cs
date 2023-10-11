using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class SubjectTeachingRepository : ISubjectTeachingRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<SubjectTeachingRepository> _logger;

        public SubjectTeachingRepository(SocialDbContext dbContext, ILogger<SubjectTeachingRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<SubjectTeaching>> AddRangeAsync(List<SubjectTeaching> subjectTeachings)
        {
            try
            {
                _logger.LogInformation($"Adding {subjectTeachings.Count} new subject teachings");

                await _dbContext.SubjectTeachings.AddRangeAsync(subjectTeachings);
                await _dbContext.SaveChangesAsync();
                return subjectTeachings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding {subjectTeachings.Count} subject teachings");
                throw new SubjectTeachingException($"Error adding {subjectTeachings.Count} subject teachings", ex);
            }
        }

        public async Task<List<SubjectTeaching>> UpdateRangeAsync(List<SubjectTeaching> subjectTeachings)
        {
            try
            {
                _logger.LogInformation($"Updating {subjectTeachings.Count} subject teachings");

                var existingTeachings = await _dbContext.SubjectTeachings
                    .Where(x => subjectTeachings.Select(y => y.SchoolTeacherId).Contains(x.SchoolTeacherId))
                    .ToListAsync();

                var deletedTeachings = existingTeachings.Where(x => !subjectTeachings.Select(y => y.SchoolSubjectsId).Contains(x.SchoolSubjectsId));
                if (deletedTeachings.Any())
                {
                    _dbContext.SubjectTeachings.RemoveRange(deletedTeachings);
                }

                foreach (var teaching in subjectTeachings)
                {
                    _dbContext.Entry(teaching).State = teaching.Id == 0 ? EntityState.Added : EntityState.Modified;
                }

                await _dbContext.SaveChangesAsync();
                return subjectTeachings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating {subjectTeachings.Count} subject teachings");
                throw new SubjectTeachingException($"Error updating {subjectTeachings.Count} subject teachings", ex);
            }
        }

    }

}
