using Application.Abstractions;
using Azure.Core;
using DataAccess.DataAccessException;
using Domain.Models;
using Domain.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<TeacherRepository> _logger;
        private readonly ISubjectTeachingRepository _subjectTeachingRepository;
        public TeacherRepository(SocialDbContext dbContext, ILogger<TeacherRepository> logger, ISubjectTeachingRepository subjectTeachingRepositoryr)
        {
            _dbContext = dbContext;
            _logger = logger;
            _subjectTeachingRepository = subjectTeachingRepositoryr;

        }

        public async Task<Teacher?> CreateAsync(Teacher teacher, TeacherViewModel teacherViewModels)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation($"Creating teacher with Id {teacher.Id}");

                await _dbContext.Teachers.AddAsync(teacher);
                await _dbContext.SaveChangesAsync();

                if (teacher == null) throw new ArgumentNullException();
                if (teacherViewModels.SubjectTeachingList == null || !teacherViewModels.SubjectTeachingList.Any()) return teacher;

                
                
                await _subjectTeachingRepository.AddRangeAsync(teacherViewModels.SubjectTeachingList!);
                
                await transaction.CommitAsync();
                _logger.LogInformation($"Created teacher with Id {teacher.Id}");
                return teacher;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Error creating teacher with Id {teacher.Id}");
                throw new TeacherException($"Error creating teacher with Id {teacher.Id}", ex);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public async Task<List<Teacher>?> GetAllAsync()
        {
            try
            {
                var z = await _dbContext.SubjectTeachings.Where(y => y.SchoolTeacherId == 3).ToListAsync();
                _logger.LogInformation("Getting all teachers");
                var teachers = await _dbContext.Teachers
                 .Include(x => x.SubjectTeaching).ThenInclude(x=>x.SchoolSubjects)
                 .ToListAsync();
                return teachers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all teachers");
                throw new TeacherException("Error getting all teachers", ex);
            }
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Getting teacher with Id {id}");
                var teachers = await _dbContext.Teachers
                    .Include(x => x.SubjectTeaching)
                    .ThenInclude(x => x.SchoolSubjects)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (teachers == null) throw new TeacherNotFoundException($"Teacher with Id {id} not found");
                return teachers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting teacher with Id {id}");
                throw new TeacherException($"Error getting teacher with Id {id}", ex);
            }
        }

        public async Task<Teacher?> UpdateAsync(Teacher teacher, TeacherViewModel teacherViewModels)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation($"Updating teacher with Id {teacher.Id}");

                _dbContext.Entry(teacher).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                if (teacher == null) throw new ArgumentNullException();
                if (teacherViewModels.SubjectTeachingList == null || !teacherViewModels.SubjectTeachingList.Any()) return teacher;

                
                await _subjectTeachingRepository.UpdateRangeAsync(teacherViewModels.SubjectTeachingList!);

                _logger.LogInformation($"Updated teacher with Id {teacher.Id}");
                await transaction.CommitAsync();
                return teacher;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Error updating teacher with Id {teacher.Id}");
                throw new TeacherException($"Error updating teacher with Id {teacher.Id}", ex);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting teacher with Id {id}");

                var teacher = await _dbContext.Teachers.FindAsync(id);
                if (teacher == null)
                {
                    throw new TeacherNotFoundException($"Teacher with Id {id} not found");
                }

                _dbContext.Teachers.Remove(teacher);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Deleted teacher with Id {id}");
            }
            catch (TeacherNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting teacher with Id {id}");
                throw new TeacherException($"Error deleting teacher with Id {id}", ex);
            }
        }
    }

}
