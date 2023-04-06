using Application.Abstractions;
using Application.SchoolSubject.Commands;
using Domain.Models;
using MediatR;

namespace Application.SchoolSubject.CommandHandlers
{
    public class CreateSchoolSubjectsCommandHandler : IRequestHandler<CreateSchoolSubjectsCommand, SchoolSubjects>
    {
        private readonly ISchoolSubjectsRepository _schoolSubjectsRepository;

        public CreateSchoolSubjectsCommandHandler(ISchoolSubjectsRepository schoolSubjectsRepository)
        {
            _schoolSubjectsRepository = schoolSubjectsRepository;
        }
        public async Task<SchoolSubjects> Handle(CreateSchoolSubjectsCommand request, CancellationToken cancellationToken)
        {
            var schoolSubjects = new SchoolSubjects
            {
                Subjects = request.Subjects
            };

            return await _schoolSubjectsRepository.CreateAsync(schoolSubjects);
        }
    }
}
