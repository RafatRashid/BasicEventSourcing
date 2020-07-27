using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.Dto
{
    public class NewProductDto
    {
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }



    public class ProductQuantityChangeDto
    {
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
