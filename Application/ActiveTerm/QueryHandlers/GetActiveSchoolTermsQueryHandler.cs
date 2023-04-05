using Application.Abstractions;
using Application.ActiveTerm.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ActiveTerm.QueryHandlers
{
    public class GetActiveSchoolTermsQueryHandler : IRequestHandler<GetActiveSchoolTermsQuery, ActiveSchoolTerm?>
    {
        private readonly IActiveSchoolTermRepository _service;

        public GetActiveSchoolTermsQueryHandler(IActiveSchoolTermRepository service)
        {
            _service = service;
        }

        public async Task<ActiveSchoolTerm?> Handle(GetActiveSchoolTermsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetActiveSchoolTermsAsync();
        }
    }
}
