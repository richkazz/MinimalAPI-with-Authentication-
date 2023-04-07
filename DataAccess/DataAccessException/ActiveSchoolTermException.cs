namespace DataAccess.DataAccessException
{
    public class ActiveSchoolTermException: DataAccessException
    {
        public ActiveSchoolTermException()
        {

        }

        public ActiveSchoolTermException(string message) : base(message)
        {

        }

        public ActiveSchoolTermException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
