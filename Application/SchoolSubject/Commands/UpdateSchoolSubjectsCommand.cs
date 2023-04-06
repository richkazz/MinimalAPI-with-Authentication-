using Domain.Models;
using MediatR;

namespace Application.SchoolSubject.Commands
{
    public class UpdateSchoolSubjectsCommand : IRequest<SchoolSubjects>
    {
        public int Id { get; set; }
        public string? Subjects { get; set; }
    }
}
