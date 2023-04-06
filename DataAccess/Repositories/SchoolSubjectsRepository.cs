using Application.Abstractions;
using Azure.Core;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class SchoolSubjectsRepository : ISchoolSubjectsRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<SchoolSubjectsRepository> _logger;

        public SchoolSubjectsRepository(SocialDbContext dbContext, ILogger<SchoolSubjectsRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<SchoolSubjects>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all subjects");
                return await _dbContext.SchoolSubjects.OrderBy(x => x.Subjects).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all subjects");
                throw new SchoolSubjectException("Error getting all subjects", ex);
            }
        }

        public async Task<SchoolSubjects?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting subject with id {id}");
                var schoolSubject = await _dbContext.SchoolSubjects.FindAsync(id);
                if (schoolSubject == null)
                {
                    throw new SchoolSubjectException("School subject not found");
                }
                return schoolSubject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting subject with id {id}");
                throw new SchoolSubjectException($"Error getting subject with id {id}", ex);
            }
        }

        public async Task<SchoolSubjects> CreateAsync(SchoolSubjects schoolSubjects)
        {
            try
            {
                var checkExist = _dbContext.SchoolSubjects.Any(x => x.Subjects!.ToLower() == schoolSubjects.Subjects!.ToLower());
                if (checkExist)
                {
                    throw new SchoolSubjectException("School subject already exists");
                }
                _logger.LogInformation("Creating new subject");
                _dbContext.SchoolSubjects.Add(schoolSubjects);
                await _dbContext.SaveChangesAsync();
                return schoolSubjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating new subject {schoolSubjects.Subjects}");
                throw new SchoolSubjectException($"Error creating new subject {schoolSubjects.Subjects}", ex);
            }
        }

        public async Task<SchoolSubjects> UpdateAsync(SchoolSubjects schoolSubjects)
        {
            try
            {
                _logger.LogInformation($"Updating subject with id {schoolSubjects.Id}");
                var existingSubject = await GetByIdAsync(schoolSubjects.Id!);
                var checkExist = _dbContext.SchoolSubjects.Any(x => x.Id != schoolSubjects.Id && x.Subjects!.ToLower() == schoolSubjects.Subjects!.ToLower());
                if (checkExist)
                {
                    throw new SchoolSubjectException("School subject already exists");
                }
                existingSubject!.Subjects = schoolSubjects.Subjects;
                _dbContext.SchoolSubjects.Update(existingSubject);
                await _dbContext.SaveChangesAsync();
                return existingSubject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating subject with id {schoolSubjects.Id}");
                throw new SchoolSubjectException($"Error updating subject with id {schoolSubjects.Id}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting subject with id {id}");
                var schoolSubject = await GetByIdAsync(id);
                if (schoolSubject != null)
                {
                    _dbContext.SchoolSubjects.Remove(schoolSubject);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new SchoolSubjectException("Subject not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting subject with id {id}");
                throw new SchoolSubjectException($"Error deleting subject with id {id}", ex);
            }

        }
    }

}
