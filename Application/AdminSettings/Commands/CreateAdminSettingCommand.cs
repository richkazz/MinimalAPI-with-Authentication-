using Domain.Models;
using MediatR;

namespace Application.AdminSettings.Commands
{
    public class CreateAdminSettingCommand : IRequest<bool>
    {
        public AdminSetting? AdminSettings { get; set; }
    }
}
