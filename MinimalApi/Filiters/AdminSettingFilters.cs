using Domain.Models;

namespace MinimalApi.Filiters
{
    public class AdminSettingFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            AdminSetting adminSetting = context.GetArgument<AdminSetting>(1);
            int examScoreType_60_40 = 1, examScoreType_70_30 = 2;

            if(adminSetting.CurrentGradingSystems == null)
                return await Task.FromResult(Results.BadRequest("Grading System not valid."));

            if (adminSetting.CurrentGradingSystems.GradingSystem == examScoreType_60_40 || adminSetting.CurrentGradingSystems.GradingSystem == examScoreType_70_30) return await next(context);

            return await Task.FromResult(Results.BadRequest("Grading System not valid, Can only be 1 or 2."));

        }
    }
}
