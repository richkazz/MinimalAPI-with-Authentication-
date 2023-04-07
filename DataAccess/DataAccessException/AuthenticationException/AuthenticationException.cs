namespace DataAccess.DataAccessException.AuthenticationException
{
    public class AuthenticationException : DataAccessException
    {
        public AuthenticationException()
        {
            
        }

        public AuthenticationException(string message):base(message)
        {
            
        }

        public AuthenticationException(string message, Exception inner):base(message,inner)
        {
            
        }

    }
}
