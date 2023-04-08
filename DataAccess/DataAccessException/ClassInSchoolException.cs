namespace DataAccess.DataAccessException
{
    public class ClassInSchoolException : DataAccessException
    {
        public ClassInSchoolException()
        {

        }

        public ClassInSchoolException(string message) : base(message)
        {

        }

        public ClassInSchoolException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
