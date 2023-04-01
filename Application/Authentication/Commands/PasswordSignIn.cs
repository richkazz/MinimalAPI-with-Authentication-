using MediatR;

namespace Application.Authentication.Commands
{
    public class PasswordSignIn: IRequest<string>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
