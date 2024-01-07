using Domain.Models;

namespace MinimalApi.Filters
{
    public class ActiveSchoolTermValidationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var schoolTerm = context.GetArgument<ActiveSchoolTerm>(1);
            int firstTerm = 1, secondTerm = 2;
            if (schoolTerm.ActiveTerm == firstTerm ||  schoolTerm.ActiveTerm == secondTerm) return await next(context);
            var errorResponse = new ErrorResponse()
            {
                StatusCode = 400,
                StatusPharase = "Bad request",
                TimeStamp = DateTime.Now
            };
            errorResponse.Errors.Add("Term not valid, Can only be 1 or 2.");
            return await Task.FromResult(Results.BadRequest(errorResponse));
            
        }
    }
}
