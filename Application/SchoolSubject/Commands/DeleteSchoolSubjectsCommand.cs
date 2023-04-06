using MediatR;

namespace Application.SchoolSubject.Commands
{
    public class DeleteSchoolSubjectsCommand : IRequest
    {
        public int Id { get; set; }
    }
}
