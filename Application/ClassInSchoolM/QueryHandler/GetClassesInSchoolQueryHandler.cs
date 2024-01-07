using Application.Abstractions;
using Application.ClassInSchoolM.Query;
using Domain.Models;
using MediatR;

namespace Application.ClassInSchoolM.QueryHandler
{
    public class GetClassesInSchoolQueryHandler : IRequestHandler<GetClassesInSchoolQuery, List<ClassInSchool>>
    {
        private readonly IClassInSchoolRepository _classInSchoolRepository;
        public GetClassesInSchoolQueryHandler(IClassInSchoolRepository classInSchoolRepository)
        {
            _classInSchoolRepository = classInSchoolRepository;
        }
        public async Task<List<ClassInSchool>> Handle(GetClassesInSchoolQuery request, CancellationToken cancellationToken)
        {
            return await _classInSchoolRepository.GetAllAsync();
        }
    }
}
