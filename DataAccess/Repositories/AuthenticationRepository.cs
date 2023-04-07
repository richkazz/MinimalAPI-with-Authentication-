using Application.Abstractions;
using DataAccess.DataAccessException.AuthenticationException;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly ILogger<AuthenticationRepository> _logger;
        public AuthenticationRepository(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            IJwtProvider jwtProvider,
            ILogger<AuthenticationRepository> logger
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateAccount(Register register)
        {
            try
            {
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, register.Email!.Trim(), CancellationToken.None);
                await _emailStore.SetEmailAsync(user, register.Email!.Trim(), CancellationToken.None);
                user.UserName = register.UserName!.Trim();
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, register.Password!.Trim());

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User '{user.UserName}' created an account with password.");
                }
                else
                {
                    _logger.LogWarning($"Failed to create account for user '{user.UserName}'. Errors: {string.Join(", ", result.Errors)}");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating account for user '{register.UserName}'.");
                throw new RegisterException($"Error creating account for user '{register.UserName}'.", ex);
            }
        }

        public async Task<string> PasswordSignIn(Login login)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User '{login.UserName}' logged in successfully.");
                    return _jwtProvider.Generate(login);
                }
                else
                {
                    _logger.LogWarning($"Failed login attempt for user '{login.UserName}'.");
                    throw new LoginException("Invalid login attempt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error logging in user '{login.UserName}'.");
                throw new LoginException($"Error logging in user '{login.UserName}'.", ex);
            }
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
