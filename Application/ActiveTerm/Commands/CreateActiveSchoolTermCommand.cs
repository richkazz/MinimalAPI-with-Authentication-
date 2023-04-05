using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ActiveTerm.Commands
{
    public class CreateActiveSchoolTermCommand : IRequest<Unit>
    {
        public int ActiveTerm { get; set; }
    }
}
