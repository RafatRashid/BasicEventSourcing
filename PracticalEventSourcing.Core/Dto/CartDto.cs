using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.Dto
{
    public class CartDto
    {
    }

    public class AddProductDto
    {
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
    }
}
