
namespace DataAccess.DataAccessException
{
    public class JuniorSchoolSubjectException : SchoolSubjectException
    {
        public JuniorSchoolSubjectException()
        {

        }

        public JuniorSchoolSubjectException(string message) : base(message)
        {

        }

        public JuniorSchoolSubjectException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
