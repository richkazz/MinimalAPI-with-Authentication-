using Domain.Models;

namespace MinimalApi.Filiters
{
    public class ActiveSchoolTermValidationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var schoolTerm = context.GetArgument<ActiveSchoolTerm>(1);
            int firstTerm = 1, secondTerm = 2;
            if (schoolTerm.ActiveTerm == firstTerm ||  schoolTerm.ActiveTerm == secondTerm) return await next(context);

            return await Task.FromResult(Results.BadRequest("Term not valid, Can only be 1 or 2."));
            
        }
    }
}
