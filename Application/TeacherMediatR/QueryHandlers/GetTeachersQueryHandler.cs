using Application.Abstractions;
using Application.TeacherMediatR.Queries;
using Domain.Models;
using MediatR;

namespace Application.TeacherMediatR.QueryHandlers
{
    public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, List<Teacher>?>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeachersQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<List<Teacher>?> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            return await _teacherRepository.GetAllAsync();
        }
    }
}
