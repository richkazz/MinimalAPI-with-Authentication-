using Application.Abstractions;
using Application.SchoolSubject.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SchoolSubject.QueryHandlers
{
    public class GetAllSchoolSubjectsQueryHandler : IRequestHandler<GetAllSchoolSubjectsQuery, List<SchoolSubjects>>
    {
        private readonly ISchoolSubjectsRepository _schoolSubjectsRepository;

        public GetAllSchoolSubjectsQueryHandler(ISchoolSubjectsRepository schoolSubjectsRepository)
        {
            _schoolSubjectsRepository = schoolSubjectsRepository;
        }
        public async Task<List<SchoolSubjects>> Handle(GetAllSchoolSubjectsQuery request, CancellationToken cancellationToken)
        {
            return await _schoolSubjectsRepository.GetAllAsync();
        }
    }
}
