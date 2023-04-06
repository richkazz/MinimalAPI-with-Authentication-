using Domain.Models;
using MediatR;

namespace Application.SchoolSubject.Queries
{
    public class GetAllSchoolSubjectsQuery : IRequest<List<SchoolSubjects>> { }
}
