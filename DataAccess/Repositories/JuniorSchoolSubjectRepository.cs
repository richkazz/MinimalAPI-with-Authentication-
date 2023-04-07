using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class JuniorSchoolSubjectRepository : IJuniorSchoolSubjectRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<JuniorSchoolSubjectRepository> _logger;

        public JuniorSchoolSubjectRepository(SocialDbContext dbContext, ILogger<JuniorSchoolSubjectRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<JuniorSchoolSubject?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting junior school subject with id {id}");
                return await _dbContext.JuniorSchoolSubjects
                    .Include(x => x.SchoolSubjects)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting junior school subject with id {id}");
                throw new JuniorSchoolSubjectException("Error getting junior school subject");
            }
        }

        public async Task<List<JuniorSchoolSubject>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all junior school subjects");
                return await _dbContext.JuniorSchoolSubjects
                    .Include(x => x.SchoolSubjects)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all junior school subjects");
                throw new JuniorSchoolSubjectException("Error getting junior school subjects");
            }
        }

        public async Task<JuniorSchoolSubject> CreateAsync(JuniorSchoolSubject juniorSchoolSubject)
        {
            try
            {
                _logger.LogInformation($"Creating new junior school subject {juniorSchoolSubject.Id}");
                var checkExist = await _dbContext.JuniorSchoolSubjects.AnyAsync(x => x.SubjectId == juniorSchoolSubject.SubjectId);
                if (checkExist)
                {
                    throw new JuniorSchoolSubjectException("Junior school subject already exists");
                }

                _dbContext.JuniorSchoolSubjects.Add(juniorSchoolSubject);
                await _dbContext.SaveChangesAsync();
                return juniorSchoolSubject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating junior school subject {juniorSchoolSubject.Id}");
                throw new JuniorSchoolSubjectException("Error creating junior school subject");
            }
        }

        public async Task<List<JuniorSchoolSubject>> CreateRangeAsync(List<JuniorSchoolSubject> juniorSchoolSubjects)
        {
            try
            {
                _logger.LogInformation($"Creating {juniorSchoolSubjects.Count} new junior school subjects");

                var existingSubjects = await _dbContext.JuniorSchoolSubjects
                    .Where(x => juniorSchoolSubjects.Select(y => y.SubjectId).Contains(x.SubjectId))
                    .ToListAsync();

                if (existingSubjects.Count > 0)
                {
                    var existingSubjectIds = existingSubjects.Select(x => x.SubjectId);
                    var newSubjectIds = juniorSchoolSubjects.Select(x => x.SubjectId);
                    var duplicateSubjectIds = existingSubjectIds.Intersect(newSubjectIds);

                    throw new JuniorSchoolSubjectException($"Junior school subjects with subjectIds {string.Join(",", duplicateSubjectIds)} already exist");
                }

                await _dbContext.JuniorSchoolSubjects.AddRangeAsync(juniorSchoolSubjects);
                await _dbContext.SaveChangesAsync();
                return juniorSchoolSubjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating {juniorSchoolSubjects.Count} junior school subjects");
                throw new JuniorSchoolSubjectException($"Error creating {juniorSchoolSubjects.Count} junior school subjects", ex);
            }
        }


        public async Task<JuniorSchoolSubject> UpdateAsync(JuniorSchoolSubject juniorSchoolSubject)
        {
            try
            {
                _logger.LogInformation($"Updating junior school subject with id {juniorSchoolSubject.Id}");
                var existingSubject = await GetByIdAsync(juniorSchoolSubject.Id);

                if (existingSubject == null)
                {
                    throw new JuniorSchoolSubjectException($"Junior school subject with id {juniorSchoolSubject.Id} not found");
                }

                var checkExist = await _dbContext.JuniorSchoolSubjects.AnyAsync(x => x.SubjectId == juniorSchoolSubject.SubjectId && x.Id != juniorSchoolSubject.Id);
                if (checkExist)
                {
                    throw new JuniorSchoolSubjectException("Junior school subject already exists");
                }

                existingSubject.SubjectId = juniorSchoolSubject.SubjectId;

                _dbContext.JuniorSchoolSubjects.Update(existingSubject);
                await _dbContext.SaveChangesAsync();
                return existingSubject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating junior school subject with id {juniorSchoolSubject.Id}");
                throw new JuniorSchoolSubjectException("Error updating junior school subject");
            }
        }

        public async Task UpdateAsync(List<JuniorSchoolSubject> juniorSchoolSubjects)
        {
            try
            {
                _logger.LogInformation($"Updating junior school subjects");

                var existingSubjects = await _dbContext.JuniorSchoolSubjects.Where(x => juniorSchoolSubjects.Select(y => y.Id).Contains(x.Id)).ToListAsync();

                foreach (var subject in juniorSchoolSubjects)
                {
                    var existingSubject = existingSubjects.FirstOrDefault(x => x.Id == subject.Id);

                    if (existingSubject == null)
                    {
                        throw new JuniorSchoolSubjectException($"Junior school subject with id {subject.Id} not found");
                    }

                    var checkExist = await _dbContext.JuniorSchoolSubjects.AnyAsync(x => x.SubjectId == subject.SubjectId && x.Id != subject.Id);
                    if (checkExist)
                    {
                        throw new JuniorSchoolSubjectException("Junior school subject already exists");
                    }

                    existingSubject.SubjectId = subject.SubjectId;
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating junior school subjects");
                throw new JuniorSchoolSubjectException("Error updating junior school subjects");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting junior school subject with id {id}");
                var existingSubject = await GetByIdAsync(id);

                if (existingSubject == null)
                {
                    throw new JuniorSchoolSubjectException($"junior school subject with id {id} not found");

                }
                _dbContext.JuniorSchoolSubjects.Remove(existingSubject);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting junior school subject with id {id}");
                throw new JuniorSchoolSubjectException($"Error deleting junior school subject with id {id}", ex);
            }
        }

    }
}
