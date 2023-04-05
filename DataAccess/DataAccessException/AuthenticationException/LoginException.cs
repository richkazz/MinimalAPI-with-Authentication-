using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessException.AuthenticationException
{
    public class LoginException: AuthenticationException
    {
        public LoginException()
        {

        }

        public LoginException(string message) : base(message)
        {

        }

        public LoginException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
