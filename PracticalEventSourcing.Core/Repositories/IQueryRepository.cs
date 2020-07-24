using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.Repositories
{
    public interface IQueryRepository<T>
    {
        public Task<T> GetAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public IQueryable<T> ToQueryable();
    }
}