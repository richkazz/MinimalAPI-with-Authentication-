using Domain.Models;

namespace MinimalApi.Filters
{
    public class AdminSettingFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            AdminSetting adminSetting = context.GetArgument<AdminSetting>(1);
            int examScoreType_60_40 = 1, examScoreType_70_30 = 2;
            var errorResponse = new ErrorResponse()
            {
                StatusCode = 400,
                StatusPharase = "Bad request",
                TimeStamp = DateTime.Now
            };
            
            if (adminSetting.CurrentGradingSystems == null)
            {
                errorResponse.Errors.Add("Grading System not valid.");
                return await Task.FromResult(Results.BadRequest(errorResponse));
            }

            if (adminSetting.CurrentGradingSystems.GradingSystem == examScoreType_60_40 || adminSetting.CurrentGradingSystems.GradingSystem == examScoreType_70_30) return await next(context);
            errorResponse.Errors.Add("Grading System not valid, Can only be 1 or 2.");
            return await Task.FromResult(Results.BadRequest(errorResponse));

        }
    }
}
