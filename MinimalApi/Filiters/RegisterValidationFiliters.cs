using Domain.Models;
using System.Text.RegularExpressions;

namespace MinimalApi.Filiters
{
    public class RegisterValidationFiliters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var register = context.GetArgument<Register>(1);
            if (string.IsNullOrEmpty(register.Email) 
                || string.IsNullOrEmpty(register.UserName)
                || string.IsNullOrEmpty(register.Password)
                || !IsEmail(register.Email.Trim()))
                return await Task.FromResult(Results.BadRequest("Register not valid"));

            return await next(context);
        }
        public static bool IsEmail(string email)
        {
            // Define a regular expression pattern for email addresses
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the Regex object to match the input email address against the pattern
            Match match = regex.Match(email);

            // Return true if the input email address matches the pattern, false otherwise
            return match.Success;
        }
       
    }
}
