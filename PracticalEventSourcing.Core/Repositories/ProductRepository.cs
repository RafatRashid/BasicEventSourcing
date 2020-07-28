using PracticalEventSourcing.Core.Dto;
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
        public ProductRepository(AppDbContext context) : base(context)
        {
        }


        public void UpdateProductCount(Guid productId, int newQuantity)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id.Equals(productId));
            product.AvailableQuantity += newQuantity;
        }

        public InvoiceDto GetInvoice(Guid cartId)
        {
            var invoiceDto = new InvoiceDto();
            var cart = _context.Carts.FirstOrDefault(x => x.Id.Equals(cartId));
            invoiceDto.CartCreationDate = cart.CreatedAt;

            var cartProducts = (from ci in _context.CartItems
                               join p in _context.Products on ci.ProductId equals p.Id
                               select new ProductInvoiceDto
                               {
                                   ProductName = p.Name,
                                   Quantity = ci.Quantity
                               }).ToList();
            invoiceDto.Products = cartProducts.GroupBy(x => x.ProductName)
                .Select(x => new ProductInvoiceDto
                {
                    ProductName = x.Key,
                    Quantity = x.Sum(z => z.Quantity)
                });

            return invoiceDto;
        }
    }
}
