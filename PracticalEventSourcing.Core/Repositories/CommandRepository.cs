using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            _context.Set<T>().Remove(entity);
        }

        public void Delete(Guid id)
        {
            var set = _context.Set<T>();
            set.Remove(set.SingleOrDefault(x => x.Id == id));
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
