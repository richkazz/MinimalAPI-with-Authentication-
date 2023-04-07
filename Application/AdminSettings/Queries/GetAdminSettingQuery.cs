using MediatR;
using Domain.Models;

namespace Application.AdminSettings.Queries
{
    public class GetAdminSettingQuery : IRequest<AdminSetting?>
    {
    }
}
