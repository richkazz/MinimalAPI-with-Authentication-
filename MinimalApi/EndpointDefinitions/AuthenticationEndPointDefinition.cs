using Application.Posts.Commands;
using Application.Posts.Queries;
using Application.Authentication.Commands;
using Domain.Models;
using MediatR;
using MinimalApi.Abstractions;
using MinimalApi.Filiters;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Azure.Core.HttpHeader;
using DataAccess.DataAccessException.AuthenticationException;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinimalApi.EndpointDefinitions
{
    public class AuthenticationEndPointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var auth = app.MapGroup("/api/auth");
            auth.MapPost("/Register", CreateAccount)
                .AddEndpointFilter<RegisterValidationFiliters>();
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
                throw new AuthenticationException(e.Message,e.InnerException);
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
                return TypedResults.NoContent();
            }
            else
            {
                var errors = createdRegister.Errors.Select(e => e.Description);
                return Results.BadRequest(new { Errors = errors });
            }

        }

    }
}
