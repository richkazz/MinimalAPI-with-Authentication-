using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class SeniorSchoolSubjectRepository : ISeniorSchoolSubjectRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<SeniorSchoolSubjectRepository> _logger;

        public SeniorSchoolSubjectRepository(SocialDbContext dbContext, ILogger<SeniorSchoolSubjectRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<SeniorSchoolSubject?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting senior school subject with id {id}");
                return await _dbContext.SeniorSchoolSubjects
                    .Include(x => x.SchoolSubjects)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting senior school subject with id {id}");
                throw new SeniorSchoolSubjectException("Error getting senior school subject");
            }
        }

        public async Task<List<SeniorSchoolSubject>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all senior school subjects");
                return await _dbContext.SeniorSchoolSubjects
                    .Include(x => x.SchoolSubjects)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all senior school subjects");
                throw new SeniorSchoolSubjectException("Error getting senior school subjects");
            }
        }

        public async Task<SeniorSchoolSubject> CreateAsync(SeniorSchoolSubject seniorSchoolSubject)
        {
            try
            {
                _logger.LogInformation($"Creating new senior school subject {seniorSchoolSubject.Id}");
                var checkExist = await _dbContext.SeniorSchoolSubjects.AnyAsync(x => x.SubjectId == seniorSchoolSubject.SubjectId);
                if (checkExist)
                {
                    throw new SeniorSchoolSubjectException("Senior school subject already exists");
                }

                _dbContext.SeniorSchoolSubjects.Add(seniorSchoolSubject);
                await _dbContext.SaveChangesAsync();
                return seniorSchoolSubject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating senior school subject {seniorSchoolSubject.Id}");
                throw new SeniorSchoolSubjectException("Error creating senior school subject");
            }
        }
        public async Task CreateRangeAsync(List<SeniorSchoolSubject> seniorSchoolSubjects)
        {
            try
            {
                _logger.LogInformation($"Creating new senior school subjects");

                var existingSubjects = await _dbContext.SeniorSchoolSubjects
                    .Where(x => seniorSchoolSubjects.Select(y => y.SubjectId).Contains(x.SubjectId))
                    .ToListAsync();

                if (existingSubjects.Any())
                {
                    throw new SeniorSchoolSubjectException($"One or more senior school subjects already exist");
                }

                await _dbContext.SeniorSchoolSubjects.AddRangeAsync(seniorSchoolSubjects);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating senior school subjects");
                throw new SeniorSchoolSubjectException("Error creating senior school subjects");
            }
        }

        public async Task<SeniorSchoolSubject> UpdateAsync(SeniorSchoolSubject seniorSchoolSubject)
        {
            try
            {
                _logger.LogInformation($"Updating senior school subject with id {seniorSchoolSubject.Id}");
                var existingSubject = await GetByIdAsync(seniorSchoolSubject.Id);

                if (existingSubject == null)
                {
                    throw new SeniorSchoolSubjectException($"Senior school subject with id {seniorSchoolSubject.Id} not found");
                }

                var checkExist = await _dbContext.SeniorSchoolSubjects.AnyAsync(x => x.SubjectId == seniorSchoolSubject.SubjectId && x.Id != seniorSchoolSubject.Id);
                if (checkExist)
                {
                    throw new SeniorSchoolSubjectException("Senior school subject already exists");
                }

                existingSubject.SubjectId = seniorSchoolSubject.SubjectId;

                _dbContext.SeniorSchoolSubjects.Update(existingSubject);
                await _dbContext.SaveChangesAsync();
                return existingSubject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating senior school subject with id {seniorSchoolSubject.Id}");
                throw new SeniorSchoolSubjectException("Error updating senior school subject");
            }
        }

        public async Task UpdateAsync(List<SeniorSchoolSubject> seniorSchoolSubjects)
        {
            try
            {
                _logger.LogInformation($"Updating senior school subjects");

                var existingSubjects = await _dbContext.SeniorSchoolSubjects.Where(x => seniorSchoolSubjects.Select(s => s.Id).Contains(x.Id)).ToListAsync();

                foreach (var seniorSchoolSubject in seniorSchoolSubjects)
                {
                    var existingSubject = existingSubjects.FirstOrDefault(x => x.Id == seniorSchoolSubject.Id);

                    if (existingSubject == null)
                    {
                        throw new SeniorSchoolSubjectException($"Senior school subject with id {seniorSchoolSubject.Id} not found");
                    }

                    var checkExist = await _dbContext.SeniorSchoolSubjects.AnyAsync(x => x.SubjectId == seniorSchoolSubject.SubjectId && x.Id != seniorSchoolSubject.Id);
                    if (checkExist)
                    {
                        throw new SeniorSchoolSubjectException("Senior school subject already exists");
                    }

                    existingSubject.SubjectId = seniorSchoolSubject.SubjectId;

                    _dbContext.SeniorSchoolSubjects.Update(existingSubject);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating senior school subjects");
                throw new SeniorSchoolSubjectException("Error updating senior school subjects");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting senior school subject with id {id}");
                var existingSubject = await GetByIdAsync(id);

                if (existingSubject == null)
                {
                    throw new SeniorSchoolSubjectException($"Senior school subject with id {id} not found");

                }
                _dbContext.SeniorSchoolSubjects.Remove(existingSubject);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Senior school subject with id {id}");
                throw new SeniorSchoolSubjectException($"Error deleting Senior school subject with id {id}", ex);
            }
        }

        
    }
            
}
