namespace DataAccess.DataAccessException.AuthenticationException
{
    public class RegisterException : AuthenticationException
    {
        public RegisterException()
        {

        }

        public RegisterException(string message) : base(message)
        {

        }

        public RegisterException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
