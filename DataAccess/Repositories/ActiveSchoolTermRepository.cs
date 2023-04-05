using Application.Abstractions;
using DataAccess.DataAccessException;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ActiveSchoolTermRepository: IActiveSchoolTermRepository
    {
        private readonly SocialDbContext _dbContext;

        public ActiveSchoolTermRepository(SocialDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActiveSchoolTerm?> GetActiveSchoolTermsAsync()
        {
            return await _dbContext.ActiveSchoolTerms.FirstOrDefaultAsync();
        }

        public async Task<ActiveSchoolTerm?> GetActiveSchoolTermByIdAsync(int id)
        {
            return await _dbContext.Set<ActiveSchoolTerm>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateActiveSchoolTermAsync(ActiveSchoolTerm activeSchoolTerm)
        {
            if (_dbContext.ActiveSchoolTerms.Any())
            {
                throw new ActiveSchoolTermException("Can not create more than one");
            }
            _dbContext.Set<ActiveSchoolTerm>().Add(activeSchoolTerm);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateActiveSchoolTermAsync(int id, ActiveSchoolTerm activeSchoolTerm)
        {
            var existingActiveSchoolTerm = await GetActiveSchoolTermByIdAsync(id);
            if (existingActiveSchoolTerm == null)
            {
                throw new ActiveSchoolTermException("ActiveSchoolTerm not found.");
            }

            existingActiveSchoolTerm.ActiveTerm = activeSchoolTerm.ActiveTerm;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteActiveSchoolTermAsync(int id)
        {
            var existingActiveSchoolTerm = await GetActiveSchoolTermByIdAsync(id);
            if (existingActiveSchoolTerm == null)
            {
                throw new ActiveSchoolTermException("ActiveSchoolTerm not found.");
            }

            _dbContext.ActiveSchoolTerms.Remove(existingActiveSchoolTerm);
            await _dbContext.SaveChangesAsync();
        }
    }
}

