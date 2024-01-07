using Application.Authentication.Commands;
using Domain.Models;
using MediatR;
using MinimalApi.Abstractions;
using MinimalApi.Filters;
using DataAccess.DataAccessException.AuthenticationException;

namespace MinimalApi.EndpointDefinitions
{
    public class AuthenticationEndPointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var auth = app.MapGroup("/api/auth");
            auth.MapPost("/Register", CreateAccount)
                .AddEndpointFilter<RegisterValidationFilters>();
            auth.MapPost("/login", SignIn);
        }
        private async Task<IResult> SignIn(IMediator mediator, Login login)
        {
            try
            {
                var createLogin = new PasswordSignIn
                {
                    Password = login.Password,
                    UserName = login.UserName
                };
                var token = await mediator.Send(createLogin);
                return TypedResults.Ok(token);
            }
            catch(LoginException e)
            {
                throw new AuthenticationException(e.Message);
            }
            
        }
        private async Task<IResult> CreateAccount(IMediator mediator, Register register)
        {
            var createRegister = new CreateAccount 
            { 
                Email = register.Email,
                Password = register.Password,
                UserName = register.UserName
            };
            var createdRegister = await mediator.Send(createRegister);
            if (createdRegister.Succeeded)
            {
                return TypedResults.Ok();
            }
            else
            {
                var errorResponse = new ErrorResponse()
                {
                    StatusCode = 400,
                    StatusPharase = "Bad Request",
                    TimeStamp = DateTime.Now
                };
                errorResponse.Errors.AddRange(createdRegister.Errors.Select(e => e.Description));
                return Results.BadRequest(errorResponse);
            }

        }

    }
}
