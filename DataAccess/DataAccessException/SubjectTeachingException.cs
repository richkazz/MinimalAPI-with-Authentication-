namespace DataAccess.DataAccessException
{
    public class SubjectTeachingException: TeacherException
    {
        public SubjectTeachingException()
        {

        }

        public SubjectTeachingException(string message) : base(message)
        {

        }

        public SubjectTeachingException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
