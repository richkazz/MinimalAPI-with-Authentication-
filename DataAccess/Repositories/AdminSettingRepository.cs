using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class AdminSettingRepository : IAdminSettingRepository
    {
        private readonly SocialDbContext _dbContext;
        private readonly ILogger<AdminSettingRepository> _logger;
        private readonly ICurrentGradingSystemRepository _currentGradingSystemRepository;
        private readonly IJuniorSchoolSubjectRepository _juniorSchoolSubjectRepository;
        private readonly ISeniorSchoolSubjectRepository _seniorSchoolSubjectRepository;
        private readonly IClassInSchoolRepository _classInSchoolRepository;

        public AdminSettingRepository(SocialDbContext dbContext, ILogger<AdminSettingRepository> logger,
            ICurrentGradingSystemRepository currentGradingSystemRepository,
            IJuniorSchoolSubjectRepository juniorSchoolSubjectRepository,
            IClassInSchoolRepository classInSchoolRepository,
            ISeniorSchoolSubjectRepository seniorSchoolSubjectRepository)
        {
            _dbContext = dbContext;
            _logger = logger;
            _currentGradingSystemRepository = currentGradingSystemRepository;
            _juniorSchoolSubjectRepository = juniorSchoolSubjectRepository;
            _seniorSchoolSubjectRepository = seniorSchoolSubjectRepository;
            _classInSchoolRepository = classInSchoolRepository;
        }

        public async Task CreateAdminSettingAsync(AdminSetting? adminSetting)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (adminSetting == null)
                {
                    throw new AdminSettingException("Empty");
                }
                _logger.LogInformation("Creating admin settings");

                await _currentGradingSystemRepository.Create(adminSetting.CurrentGradingSystems!);

                await _juniorSchoolSubjectRepository.CreateRangeAsync(adminSetting.JuniorSchoolSubjects!);

                await _seniorSchoolSubjectRepository.CreateRangeAsync(adminSetting.SeniorSchoolSubjects!);

                await _classInSchoolRepository.AddRangeAsync(adminSetting.ClassInSchools);

                await transaction.CommitAsync();
                // await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error Creating admin settings");
                throw new AdminSettingException("Error Creating admin settings", ex);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public async Task<AdminSetting?> GetAdminSettingAsync()
        {
            try
            {
                _logger.LogInformation("Getting admin settings");
                var adminSetting = new AdminSetting
                {
                    CurrentGradingSystems = await _currentGradingSystemRepository.GetFirst(),
                    JuniorSchoolSubjects = await _juniorSchoolSubjectRepository.GetAllAsync(),
                    SeniorSchoolSubjects = await _seniorSchoolSubjectRepository.GetAllAsync(),
                    ClassInSchools = await _classInSchoolRepository.GetAllAsync()
                };

                return adminSetting;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting admin settings");
                throw new AdminSettingException("Error getting admin settings");
            }
        }

        public async Task UpdateAdminSettingAsync(AdminSetting? adminSetting)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Updating admin settings");

                var currentGradingSystem = await _currentGradingSystemRepository.Update(adminSetting.CurrentGradingSystems!);
                adminSetting.CurrentGradingSystems = currentGradingSystem;

                await _juniorSchoolSubjectRepository.UpdateAsync(adminSetting.JuniorSchoolSubjects!);

                await _seniorSchoolSubjectRepository.UpdateAsync(adminSetting.SeniorSchoolSubjects!);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating admin settings");
                throw new AdminSettingException("Error updating admin settings", ex);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }
    }

}
