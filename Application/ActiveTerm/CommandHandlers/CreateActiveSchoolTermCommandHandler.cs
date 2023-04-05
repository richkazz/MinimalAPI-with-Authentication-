using Application.Abstractions;
using Application.ActiveTerm.Commands;
using Domain.Models;
using MediatR;

namespace Application.ActiveTerm.CommandHandlers
{
    public class CreateActiveSchoolTermCommandHandler : IRequestHandler<CreateActiveSchoolTermCommand, Unit>
    {
        private readonly IActiveSchoolTermRepository _service;

        public CreateActiveSchoolTermCommandHandler(IActiveSchoolTermRepository service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(CreateActiveSchoolTermCommand request, CancellationToken cancellationToken)
        {
            var activeSchoolTerm = new ActiveSchoolTerm
            {
                ActiveTerm = request.ActiveTerm
            };

            await _service.CreateActiveSchoolTermAsync(activeSchoolTerm);

            return Unit.Value;
        }

    }

}
