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
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = 400,
                    StatusPharase = "Bad request",
                    TimeStamp = DateTime.Now
                };
                errorResponse.Errors.Add("Subject not valid");
                return await Task.FromResult(Results.BadRequest(errorResponse));
            }

            return await next(context);
        }
    }
}
