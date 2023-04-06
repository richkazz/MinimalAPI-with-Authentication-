using Application.Abstractions;
using Application.SchoolSubject.Commands;
using Domain.Models;
using MediatR;

namespace Application.SchoolSubject.CommandHandlers
{
    public class UpdateSchoolSubjectsCommandHandler : IRequestHandler<UpdateSchoolSubjectsCommand, SchoolSubjects>
    {
        private readonly ISchoolSubjectsRepository _schoolSubjectsRepository;

        public UpdateSchoolSubjectsCommandHandler(ISchoolSubjectsRepository schoolSubjectsRepository)
        {
            _schoolSubjectsRepository = schoolSubjectsRepository;
        }
        public async Task<SchoolSubjects> Handle(UpdateSchoolSubjectsCommand request, CancellationToken cancellationToken)
        {
            return await _schoolSubjectsRepository
                .UpdateAsync(new SchoolSubjects() { Subjects = request.Subjects,Id = request.Id});
        }
    }
}
