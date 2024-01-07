using Domain.Models;
using MediatR;

namespace Application.ClassInSchoolM.Query
{
    public class GetClassesInSchoolQuery : IRequest<List<ClassInSchool>>
    {
    }
}
