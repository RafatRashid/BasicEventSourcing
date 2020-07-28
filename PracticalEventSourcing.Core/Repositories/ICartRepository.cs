using PracticalEventSourcing.Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.Repositories
{
    public interface ICartRepository: ICommandRepository<CartRM>
    {
        public void DeleteCartItem(Guid cartId, Guid productId);
    }
}
