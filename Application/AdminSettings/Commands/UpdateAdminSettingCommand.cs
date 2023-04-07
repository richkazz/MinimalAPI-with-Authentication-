using Domain.Models;
using MediatR;


namespace Application.AdminSettings.Commands
{
    public class UpdateAdminSettingCommand: IRequest<Unit>
    {
        public AdminSetting? AdminSettings { get; set; }
    }
}
