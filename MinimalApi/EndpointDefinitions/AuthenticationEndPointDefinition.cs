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
            var createLogin = new PasswordSignIn 
            { 
                Password = login.Password,
                UserName = login.UserName
            };
            var token = await mediator.Send(createLogin);
            if (string.IsNullOrEmpty(token))
            {
                return TypedResults.Problem(token);
            }
            else
            {
                return TypedResults.Ok(token);
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
                return Results.Problem(JsonSerializer.Serialize(createdRegister.Errors));
            }
            
        }

    }
}
