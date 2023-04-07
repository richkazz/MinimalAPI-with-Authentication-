namespace DataAccess.DataAccessException
{
    public class CurrentGradingSystemException: DataAccessException
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
