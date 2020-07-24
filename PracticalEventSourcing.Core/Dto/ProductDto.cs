using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.Dto
{
    public class ProductDto
    {
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }

    public class NewProductDto
    {
        public string ProductName { get; set; }
    }

    public class ProductQuantityChangeDto
    {
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
