using Application.Abstractions;
using Application.Authentication.Commands;
using Domain.Models;
using MediatR;

namespace Application.Authentication.CommandHandlers
{
    internal class PasswordSignInHandler : IRequestHandler<PasswordSignIn, string>
    {
        private readonly IAuthenticationRepository _autRepository;
        public PasswordSignInHandler(IAuthenticationRepository autRepository)
        {
            _autRepository = autRepository;
        }
        public async Task<string> Handle(PasswordSignIn request, CancellationToken cancellationToken)
        {
            var login = new Login()
            {
                Password = request.Password,
                UserName = request.UserName
            };
            return await _autRepository.PasswordSignIn(login);
        }
    }
}
