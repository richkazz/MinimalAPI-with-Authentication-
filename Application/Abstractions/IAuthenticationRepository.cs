using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IAuthenticationRepository
    {
        Task<IdentityResult> CreateAccount(Register register);
        Task<string> PasswordSignIn(Login login);
    }
}
