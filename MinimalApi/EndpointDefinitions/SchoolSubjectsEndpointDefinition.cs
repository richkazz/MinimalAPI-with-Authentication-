using Application.SchoolSubject.Commands;
using Application.SchoolSubject.Queries;
using Domain.Models;
using MediatR;
using MinimalApi.Abstractions;
using MinimalApi.Filiters;

namespace MinimalApi.EndpointDefinitions
{
    public class SchoolSubjectsEndpointDefinition : IEndpointDefinition
    {

        public void RegisterEndpoints(WebApplication app)
        {
            var subjects = app.MapGroup("/api/subjects");
            subjects.MapGet("/", GetAllSubjects)
                .WithName("GetAllSubjects");
            subjects.MapGet("/{id}", GetSubjectById)
                .WithName("GetSubjectById");
            subjects.MapPost("/", CreateSubject)
                .AddEndpointFilter<SchoolSubjectValidationFilter>();
            subjects.MapPut("/{id}", UpdateSubject)
                .AddEndpointFilter<SchoolSubjectValidationFilter>();
            subjects.MapDelete("/{id}", DeleteSubject);
        }

        private async Task<IResult> GetAllSubjects(IMediator mediator)
        {
            
            var subjects = await mediator.Send(new GetAllSchoolSubjectsQuery());
            return TypedResults.Ok(subjects);
        }

        private async Task<IResult> GetSubjectById(IMediator mediator, int id)
        {
           
            var subject = await mediator.Send(new GetSchoolSubjectsByIdQuery { Id = id });
            if (subject == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(subject);
        }

        private async Task<IResult> CreateSubject(IMediator mediator, SchoolSubjects subject)
        {
            var createdSubject = await mediator.Send(new CreateSchoolSubjectsCommand { Subjects = subject.Subjects });
            return Results.CreatedAtRoute("GetSubjectById", new { createdSubject.Id }, createdSubject);
        }

        private async Task<IResult> UpdateSubject(IMediator mediator, SchoolSubjects subject, int id)
        {
            
            var updatedSubject = await mediator.Send(new UpdateSchoolSubjectsCommand { Id = id, Subjects = subject.Subjects });
            return TypedResults.Ok(updatedSubject);
        }

        private async Task<IResult> DeleteSubject(IMediator mediator, int id)
        {
            
            await mediator.Send(new DeleteSchoolSubjectsCommand { Id = id });
            return TypedResults.Ok();
        }
    }
}
