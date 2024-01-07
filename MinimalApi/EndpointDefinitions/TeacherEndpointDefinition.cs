using Application.ApplicationExceptions;
using Application.TeacherMediatR.Commands;
using Application.TeacherMediatR.Queries;
using Domain.Models;
using Domain.ViewModel;
using MediatR;
using MinimalApi.Abstractions;
using MinimalApi.Filters;

namespace MinimalApi.EndpointDefinitions
{
    public class TeacherEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var teacher = app.MapGroup("/api/teacher");
            teacher.MapGet("/", GetTeachers)
                .WithName("GetTeachers");
            teacher.MapGet("/{id}", GetTeacherById)
                .WithName("GetTeacherById");
            teacher.MapPost("/", CreateTeacher)
                .AddEndpointFilter<TeacherValidationFilters>();
            teacher.MapPut("/", UpdateTeacher)
                .AddEndpointFilter<TeacherValidationFilters>();
            teacher.MapDelete("/{id}", DeleteTeacher);
        }

        private async Task<IResult> GetTeachers(IMediator mediator)
        {
            var query = new GetTeachersQuery();
            var teachers = await mediator.Send(query);
            return TypedResults.Ok(teachers);
        }

        private async Task<IResult> GetTeacherById(IMediator mediator, int id)
        {
            var query = new GetTeacherByIdQuery { Id = id };
            var teacher = await mediator.Send(query);
            if (teacher == null)
            {
                throw new NotFoundException($"Teacher with id {id} not found");
            }
            return TypedResults.Ok(teacher);
        }

        private async Task<IResult> DeleteTeacher(IMediator mediator, int id)
        {
            var query = new DeleteTeacherCommand { Id = id };
            await mediator.Send(query);
            
            return TypedResults.Ok();
        }

        private async Task<IResult> CreateTeacher(IMediator mediator, TeacherViewModel teacher)
        {
            var command = new CreateTeacherCommand
            {
               TeacherViewModels = teacher
            };
            var createdTeacher = await mediator.Send(command);
            if (createdTeacher == null) throw new ArgumentNullException();
            return Results.CreatedAtRoute("GetTeacherById", new { id = createdTeacher.Id }, createdTeacher);
        }

        private async Task<IResult> UpdateTeacher(IMediator mediator, TeacherViewModel teacher)
        {
            var command = new UpdateTeacherCommand
            {
                TeacherViewModels = teacher

            };
            var updatedTeacher = await mediator.Send(command);
            return TypedResults.Ok(updatedTeacher);
        }
    }

}
