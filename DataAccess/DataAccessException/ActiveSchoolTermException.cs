using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessException
{
    public class ActiveSchoolTermException:Exception
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
