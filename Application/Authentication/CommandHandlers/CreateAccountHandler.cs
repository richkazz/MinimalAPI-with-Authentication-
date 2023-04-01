using Application.Abstractions;
using Application.Authentication.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.CommandHandlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccount, IdentityResult>
    {
        private readonly IAuthenticationRepository _registerRepository;
        public CreateAccountHandler(IAuthenticationRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        public async Task<IdentityResult> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            var newRegister = new Register()
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password
            };
            return await _registerRepository.CreateAccount(newRegister);
        }
    }
}
