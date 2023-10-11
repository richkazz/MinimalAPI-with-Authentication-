using Domain.Models;
using MediatR;

namespace Application.TeacherMediatR.Queries
{
    public class GetTeacherByIdQuery : IRequest<Teacher?>
    {
        public int Id { get; set; }
    }
}
