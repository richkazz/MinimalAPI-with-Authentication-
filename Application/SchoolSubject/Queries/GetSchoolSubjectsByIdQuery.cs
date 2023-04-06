using Domain.Models;
using MediatR;

namespace Application.SchoolSubject.Queries
{
    public class GetSchoolSubjectsByIdQuery : IRequest<SchoolSubjects>
    {
        public int Id { get; set; }
    }
}
