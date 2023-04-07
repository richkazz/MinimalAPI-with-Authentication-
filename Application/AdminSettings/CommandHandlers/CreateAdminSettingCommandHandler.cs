using Application.Abstractions;
using Application.AdminSettings.Commands;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AdminSettings.CommandHandlers
{
    public class CreateAdminSettingCommandHandler : IRequestHandler<CreateAdminSettingCommand, bool>
    {
        private readonly IAdminSettingRepository _adminSettingRepository;

        public CreateAdminSettingCommandHandler(IAdminSettingRepository adminSettingRepository)
        {
            _adminSettingRepository = adminSettingRepository;
        }
        public async Task<bool> Handle(CreateAdminSettingCommand request, CancellationToken cancellationToken)
        {
            await _adminSettingRepository.CreateAdminSettingAsync(request.AdminSettings);
            return true ;
        }
    }
}
