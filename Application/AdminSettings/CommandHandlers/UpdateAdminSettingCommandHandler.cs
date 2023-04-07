using Application.Abstractions;
using Application.AdminSettings.Commands;
using Domain.Models;
using MediatR;


namespace Application.AdminSettings.CommandHandlers
{
    public class UpdateAdminSettingCommandHandler : IRequestHandler<UpdateAdminSettingCommand, Unit>
    {
        private readonly IAdminSettingRepository _adminSettingRepository;

        public UpdateAdminSettingCommandHandler(IAdminSettingRepository adminSettingRepository)
        {
            _adminSettingRepository = adminSettingRepository;
        }
        public async Task<Unit> Handle(UpdateAdminSettingCommand request, CancellationToken cancellationToken)
        {
            await _adminSettingRepository.UpdateAdminSettingAsync(request.AdminSettings);
            return Unit.Value;
        }
    }
}
