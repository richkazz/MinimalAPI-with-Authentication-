using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class ClassInSchoolRepository : IClassInSchoolRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<ClassInSchoolRepository> _logger;

        public ClassInSchoolRepository(SocialDbContext dbContext, ILogger<ClassInSchoolRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<ClassInSchool>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all classes");
                return await _dbContext.ClassInSchools.OrderBy(x => x.ClassName).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all classes");
                throw new ClassInSchoolException("Error getting all classes", ex);
            }
        }

        public async Task<ClassInSchool?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting class with id {id}");
                var classInSchool = await _dbContext.ClassInSchools.FindAsync(id);
                if (classInSchool == null)
                {
                    throw new ClassInSchoolException("Class not found");
                }
                return classInSchool;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting class with id {id}");
                throw new ClassInSchoolException($"Error getting class with id {id}", ex);
            }
        }

        public async Task<ClassInSchool> AddAsync(ClassInSchool classInSchool)
        {
            try
            {
                var checkExist = _dbContext.ClassInSchools.Any(x => x.ClassName!.ToLower() == classInSchool.ClassName!.ToLower());
                if (checkExist)
                {
                    throw new ClassInSchoolException("Class already exists");
                }
                _logger.LogInformation("Creating new class");
                _dbContext.ClassInSchools.Add(classInSchool);
                await _dbContext.SaveChangesAsync();
                return classInSchool;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating new class {classInSchool.ClassName}");
                throw new ClassInSchoolException($"Error creating new class {classInSchool.ClassName}", ex);
            }
        }
        
        public async Task<List<ClassInSchool>> AddRangeAsync(List<ClassInSchool>? classes)
        {
            try
            {
                _logger.LogInformation($"Adding {classes.Count} new classes");

                var existingClassNames = await _dbContext.ClassInSchools
                    .Where(x => classes.Select(y => y.ClassName.ToLower()).Contains(x.ClassName.ToLower()))
                    .ToListAsync();
               
                if (existingClassNames.Any())
                {
                    var existingClassNamesList = existingClassNames.Select(x => x.ClassName);
                    var newClassNamesList = classes.Select(x => x.ClassName);
                    var duplicateClassNamesList = existingClassNamesList.Intersect(newClassNamesList);

                    throw new ClassInSchoolException($"Classes with names {string.Join(",", duplicateClassNamesList)} already exist");
                }

                await _dbContext.ClassInSchools.AddRangeAsync(classes);
                await _dbContext.SaveChangesAsync();
                return classes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding {classes.Count} new classes");
                throw new ClassInSchoolException($"Error adding {classes.Count} new classes", ex);
            }
        }

        public async Task<ClassInSchool> UpdateAsync(ClassInSchool classInSchool)
        {
            try
            {
                _logger.LogInformation($"Updating class with id {classInSchool.Id}");
                var existingClass = await GetByIdAsync(classInSchool.Id!);
                var checkExist = _dbContext.ClassInSchools.Any(x => x.Id != classInSchool.Id && x.ClassName!.ToLower() == classInSchool.ClassName!.ToLower());
                if (checkExist)
                {
                    throw new ClassInSchoolException("Class already exists");
                }
                existingClass!.ClassName = classInSchool.ClassName;
                _dbContext.ClassInSchools.Update(existingClass);
                await _dbContext.SaveChangesAsync();
                return existingClass;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating class with id {classInSchool.Id}");
                throw new ClassInSchoolException($"Error updating class with id {classInSchool.Id}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting class with id {id}");
                var classInSchool = await GetByIdAsync(id);
                if (classInSchool != null)
                {
                    _dbContext.ClassInSchools.Remove(classInSchool);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ClassInSchoolException("Class not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Class with id {id}");
                throw new ClassInSchoolException($"Error deleting class with id {id}", ex);
            }
        }

    }
}
