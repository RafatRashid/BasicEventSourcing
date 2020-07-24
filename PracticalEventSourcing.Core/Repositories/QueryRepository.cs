using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public QueryRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> ToQueryable()
        {
            return _context.Set<T>();
        }
    }
}
