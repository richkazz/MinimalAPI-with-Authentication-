namespace DataAccess.DataAccessException
{
    public class SchoolSubjectException:Exception
    {
        public SchoolSubjectException()
        {

        }

        public SchoolSubjectException(string message) : base(message)
        {

        }

        public SchoolSubjectException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
