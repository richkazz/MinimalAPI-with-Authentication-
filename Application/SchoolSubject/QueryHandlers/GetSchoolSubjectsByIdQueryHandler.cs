using Application.Abstractions;
using Application.SchoolSubject.Queries;
using Domain.Models;
using MediatR;

namespace Application.SchoolSubject.QueryHandlers
{
    public class GetSchoolSubjectsByIdQueryHandler : IRequestHandler<GetSchoolSubjectsByIdQuery, SchoolSubjects>
    {
        private readonly ISchoolSubjectsRepository _schoolSubjectsRepository;

        public GetSchoolSubjectsByIdQueryHandler(ISchoolSubjectsRepository schoolSubjectsRepository)
        {
            _schoolSubjectsRepository = schoolSubjectsRepository;
        }
        public async Task<SchoolSubjects> Handle(GetSchoolSubjectsByIdQuery request, CancellationToken cancellationToken)
        {
            var schoolSubjects = await _schoolSubjectsRepository.GetByIdAsync(request.Id);
            if (schoolSubjects == null)
            {
                throw new Exception("School subject not found");
            }
            return schoolSubjects;
        }
    }
}
