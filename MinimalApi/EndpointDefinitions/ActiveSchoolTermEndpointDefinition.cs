using Application.ActiveTerm.Commands;
using Application.ActiveTerm.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Abstractions;
using MinimalApi.Filiters;

namespace MinimalApi.EndpointDefinitions
{
    public class ActiveSchoolTermEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var activeSchoolTerm = app.MapGroup("/api/active-school-term");
            activeSchoolTerm.MapGet("/", GetActiveSchoolsTerm)
                .WithName("GetActiveSchoolTerm");
            activeSchoolTerm.MapPost("/", CreateActiveSchoolTerm)
                .AddEndpointFilter<ActiveSchoolTermValidationFilters>();
            activeSchoolTerm.MapPut("/", UpdateActiveSchoolTerm)
                .AddEndpointFilter<ActiveSchoolTermValidationFilters>();
        }

        private async Task<IResult> GetActiveSchoolsTerm(IMediator mediator)
        {
            var query = new GetActiveSchoolTermsQuery();
            var activeSchoolTerm = await mediator.Send(query);
            return TypedResults.Ok(activeSchoolTerm);
        }

        private async Task<IResult> CreateActiveSchoolTerm(IMediator mediator, ActiveSchoolTerm activeSchoolTerm)
        {
            var command = new CreateActiveSchoolTermCommand
            {
                ActiveTerm = activeSchoolTerm.ActiveTerm
            };
            var createdActiveSchoolTerm = await mediator.Send(command);
            return Results.CreatedAtRoute("GetActiveSchoolTerm", null, createdActiveSchoolTerm);
        }

        
        private async Task<IResult> UpdateActiveSchoolTerm(IMediator mediator, ActiveSchoolTerm activeSchoolTerm)
        {
            var command = new UpdateActiveSchoolTermCommand
            {
                Id = activeSchoolTerm.Id,
                ActiveTerm = activeSchoolTerm.ActiveTerm
            };
            var updatedActiveSchoolTerm = await mediator.Send(command);
            return TypedResults.Ok(); ;
        }
    }

}
