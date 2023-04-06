using Domain.Models;
using MediatR;

namespace Application.SchoolSubject.Commands
{
    public class CreateSchoolSubjectsCommand : IRequest<SchoolSubjects>
    {
        public string? Subjects { get; set; }
    }
}
