using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.Dto
{
    public class CartDto
    {
    }

    public class ModifyCartProductDto
    {
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
    }
}
