using Application.Abstractions;
using Application.ApplicationExceptions;
using Application.TeacherMediatR.Queries;
using Domain.Models;
using MediatR;

namespace Application.TeacherMediatR.QueryHandlers
{
    public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, Teacher?>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherByIdQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Teacher?> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(Teacher), request.Id);
            }

            return teacher;
        }
    }
}
