namespace DataAccess.DataAccessException.AuthenticationException
{
    public class LoginException: AuthenticationException
    {
        public LoginException()
        {

        }

        public LoginException(string message) : base(message)
        {

        }

        public LoginException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
