using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApi.Filiters
{
    public class SchoolSubjectValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var schoolSubjects = context.GetArgument<SchoolSubjects>(1);
            if (string.IsNullOrEmpty(schoolSubjects.Subjects))
                return await Task.FromResult(Results.BadRequest("Subject not valid"));

            return await next(context);
        }
    }
}
