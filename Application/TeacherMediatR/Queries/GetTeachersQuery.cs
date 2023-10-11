using Domain.Models;
using MediatR;

namespace Application.TeacherMediatR.Queries
{
    public class GetTeachersQuery : IRequest<List<Teacher>?>
    {
    }
}
