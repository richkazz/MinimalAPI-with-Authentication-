using Domain.Models;

namespace Application.Abstractions
{
    public interface IAdminSettingRepository
    {
        Task<AdminSetting?> GetAdminSettingAsync();
        Task CreateAdminSettingAsync(AdminSetting? adminSetting);
        Task UpdateAdminSettingAsync(AdminSetting? adminSetting);
    }
}
