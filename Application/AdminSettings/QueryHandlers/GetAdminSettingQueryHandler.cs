using Application.Abstractions;
using Application.AdminSettings.Queries;
using Domain.Models;
using MediatR;

namespace Application.AdminSettings.QueryHandlers
{
    public class GetAdminSettingQueryHandler : IRequestHandler<GetAdminSettingQuery, AdminSetting?>
    {
        private readonly IAdminSettingRepository _adminSettingRepository;

        public GetAdminSettingQueryHandler(IAdminSettingRepository adminSettingRepository)
        {
            _adminSettingRepository = adminSettingRepository;
        }
        public async Task<AdminSetting?> Handle(GetAdminSettingQuery request, CancellationToken cancellationToken)
        {
            return await _adminSettingRepository.GetAdminSettingAsync();
        }
    }
}
