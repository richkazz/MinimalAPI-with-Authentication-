namespace DataAccess.DataAccessException
{
    public class SeniorSchoolSubjectException: SchoolSubjectException
    {
        public SeniorSchoolSubjectException()
        {

        }

        public SeniorSchoolSubjectException(string message) : base(message)
        {

        }

        public SeniorSchoolSubjectException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
