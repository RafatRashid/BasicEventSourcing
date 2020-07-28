using PracticalEventSourcing.Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.Repositories
{
    public interface IProductRepository:ICommandRepository<ProductRM>
    {
        public void UpdateProductCount(Guid productId, int newQuantity);
        public void DecrementProductCount(Guid productId);
    }
}
