using Domain.Models;
using Domain.ViewModel;
using Infrastructure.Mail;

namespace MinimalApi.Filters
{
    public class TeacherValidationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var teacher = context.GetArgument<TeacherViewModel>(1);
            var errorResponse = new ErrorResponse()
            {
                StatusCode = 400,
                StatusPharase = "Bad request",
                TimeStamp = DateTime.Now
            };
            if (teacher.FirstName==null||string.IsNullOrEmpty(teacher.FirstName))
            {
                errorResponse.Errors.Add("First name not valid");
            }

            if ((teacher.LastName == null || string.IsNullOrEmpty(teacher.LastName)))
            {
                errorResponse.Errors.Add("Last name not valid");
            }

            if (teacher.Email == null || string.IsNullOrEmpty(teacher.Email) || !CheckMail.IsEmail(teacher.Email))
            {
                errorResponse.Errors.Add("Email not valid");
            }
            if ((teacher.PhoneNumber == null || string.IsNullOrEmpty(teacher.PhoneNumber) || teacher.PhoneNumber.Length != 11))
            {
                errorResponse.Errors.Add("PhoneNumber not valid");
            }
            if (teacher.NextOfKinName == null || (string.IsNullOrEmpty(teacher.NextOfKinName)))
            {
                errorResponse.Errors.Add("NextOfKinName not valid");
            }
            if (teacher.Gender == null || (string.IsNullOrEmpty(teacher.Gender)))
            {
                errorResponse.Errors.Add("Gender not valid");
            }
            if (teacher.Qualification == null || (string.IsNullOrEmpty(teacher.Qualification)))
            {
                errorResponse.Errors.Add("Qualification not valid");
            }
            if (teacher.NextOfkinNumber == null || string.IsNullOrEmpty(teacher.NextOfkinNumber)|| teacher.NextOfkinNumber.Length != 11)
            {
                errorResponse.Errors.Add("NextOfkinNumber not valid");
            }
            if (teacher.Address == null||string.IsNullOrEmpty(teacher.Address))
            {
                errorResponse.Errors.Add("Address not valid");
            }
            if (teacher.DateEmployed == null)
            {
                errorResponse.Errors.Add("DateEmployed not valid");
            }

            if(errorResponse.Errors.Any()) return await Task.FromResult(Results.BadRequest(errorResponse));


            return await next(context);
        }
    }

}
