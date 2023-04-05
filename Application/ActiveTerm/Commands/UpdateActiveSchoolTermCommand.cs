using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ActiveTerm.Commands
{
    public class UpdateActiveSchoolTermCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int ActiveTerm { get; set; }
    }
}
