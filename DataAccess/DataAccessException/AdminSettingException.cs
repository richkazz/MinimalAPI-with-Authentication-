namespace DataAccess.DataAccessException
{
    public class AdminSettingException:DataAccessException
    {
        public AdminSettingException()
        {

        }

        public AdminSettingException(string message) : base(message)
        {

        }

        public AdminSettingException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
