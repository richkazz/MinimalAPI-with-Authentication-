

namespace DataAccess.DataAccessException
{
    public class TeacherException:DataAccessException
    {
        public TeacherException()
        {

        }

        public TeacherException(string message) : base(message)
        {

        }

        public TeacherException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
