using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IActiveSchoolTermRepository
    {
        Task<ActiveSchoolTerm?> GetActiveSchoolTermsAsync();
        Task<ActiveSchoolTerm?> GetActiveSchoolTermByIdAsync(int id);
        Task CreateActiveSchoolTermAsync(ActiveSchoolTerm activeSchoolTerm);
        Task UpdateActiveSchoolTermAsync(int id, ActiveSchoolTerm activeSchoolTerm);
        Task DeleteActiveSchoolTermAsync(int id);
    }
}
