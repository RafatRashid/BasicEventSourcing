using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.Repositories
{
    public interface ICommandRepository<T>
    {
        public Task<T> InsertAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public void Delete(T entity);
        public void Delete(Guid id);
        public void Save();
    }
}
