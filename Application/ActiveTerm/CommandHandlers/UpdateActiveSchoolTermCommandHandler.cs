using Application.Abstractions;
using Application.ActiveTerm.Commands;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ActiveTerm.CommandHandlers
{
    // Define the command handler for updating an existing active school term
    public class UpdateActiveSchoolTermCommandHandler : IRequestHandler<UpdateActiveSchoolTermCommand,Unit>
    {
        private readonly IActiveSchoolTermRepository _service;

        public UpdateActiveSchoolTermCommandHandler(IActiveSchoolTermRepository service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(UpdateActiveSchoolTermCommand request, CancellationToken cancellationToken)
        {
            var activeSchoolTerm = new ActiveSchoolTerm
            {
                ActiveTerm = request.ActiveTerm
            };

            await _service.UpdateActiveSchoolTermAsync(request.Id, activeSchoolTerm);

            return Unit.Value;
        }
    }

}
