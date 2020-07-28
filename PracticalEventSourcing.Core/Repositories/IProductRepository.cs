using PracticalEventSourcing.Core.Dto;
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
        public InvoiceDto GetInvoice(Guid cartId);
    }
}
