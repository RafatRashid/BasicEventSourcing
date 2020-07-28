using PracticalEventSourcing.Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.Repositories
{
    public class ProductRepository : CommandRepository<ProductRM>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public void UpdateProductCount(Guid productId, int newQuantity)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id.Equals(productId));
            product.AvailableQuantity = newQuantity;
        }

        public void DecrementProductCount(Guid productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id.Equals(productId));
            product.AvailableQuantity--;
        }
    }
}
