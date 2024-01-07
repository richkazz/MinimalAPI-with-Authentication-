using Domain.Models;
using Infrastructure.Mail;
using System.Text.RegularExpressions;

namespace MinimalApi.Filters
{
    public class RegisterValidationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var register = context.GetArgument<Register>(1);
            if (string.IsNullOrEmpty(register.Email) 
                || string.IsNullOrEmpty(register.UserName)
                || string.IsNullOrEmpty(register.Password)
                || !CheckMail.IsEmail(register.Email.Trim()))
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = 400,
                    StatusPharase = "Bad request",
                    TimeStamp = DateTime.Now
                };
                errorResponse.Errors.Add("Register not valid");
                return await Task.FromResult(Results.BadRequest(errorResponse));
            }

            return await next(context);
        }
       
       
    }
}
