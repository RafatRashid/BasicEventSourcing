using PracticalEventSourcing.Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalEventSourcing.Core.Repositories
{
    public class CartRepository : CommandRepository<CartRM>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public void DeleteCartItem(Guid cartId, Guid productId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(x => x.CartId.Equals(cartId) && x.ProductId.Equals(productId));
            _context.Remove(cartItem);
        }
    }
}
