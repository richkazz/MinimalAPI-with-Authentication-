using MediatR;

namespace Application.TeacherMediatR.Commands
{
    public class DeleteTeacherCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
