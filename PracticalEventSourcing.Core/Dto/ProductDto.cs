using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.Dto
{
    public class NewProductDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }



    public class ProductAddQuantityDto
    {
        public Guid? ProductId { get; set; }
        public int AddedQuantity { get; set; }
    }
}
