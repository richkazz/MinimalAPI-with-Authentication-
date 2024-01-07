using Application.AdminSettings.Commands;
using Application.AdminSettings.Queries;
using Domain.Models;
using MediatR;
using MinimalApi.Abstractions;
using MinimalApi.Filters;

namespace MinimalApi.EndpointDefinitions
{
    public class AdminSettingEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var adminSettings = app.MapGroup("/api/adminsettings");
            adminSettings.MapGet("/", GetAdminSettings)
                .WithName("GetAdminSettings");
            adminSettings.MapPut("/", UpdateAdminSettings).AddEndpointFilter<AdminSettingFilters>();
            adminSettings.MapPost("/", CreateAdminSettings).AddEndpointFilter<AdminSettingFilters>();
        }

        private async Task<IResult> GetAdminSettings(IMediator mediator)
        {
            var adminSettings = await mediator.Send(new GetAdminSettingQuery());
            return TypedResults.Ok(adminSettings);
        }

        private async Task<IResult> CreateAdminSettings(
            IMediator mediator,AdminSetting adminSetting)
        {
            await mediator.Send(new CreateAdminSettingCommand
            {
                AdminSettings = adminSetting
            });
            return TypedResults.Ok();
        }

        private async Task<IResult> UpdateAdminSettings(
            IMediator mediator,AdminSetting adminSetting)
        {
            await mediator.Send(new UpdateAdminSettingCommand
            {
                AdminSettings = adminSetting
            });
            return TypedResults.Ok();
        }


    }

}
