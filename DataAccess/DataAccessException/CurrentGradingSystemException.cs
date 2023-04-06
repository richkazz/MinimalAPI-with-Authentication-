namespace DataAccess.DataAccessException
{
    public class CurrentGradingSystemException:Exception
    {
        public CurrentGradingSystemException()
        {

        }

        public CurrentGradingSystemException(string message) : base(message)
        {

        }

        public CurrentGradingSystemException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
