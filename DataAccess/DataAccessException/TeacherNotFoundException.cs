namespace DataAccess.DataAccessException
{
    public class TeacherNotFoundException: TeacherException
    {
        public TeacherNotFoundException()
        {

        }

        public TeacherNotFoundException(string message) : base(message)
        {

        }

        public TeacherNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
