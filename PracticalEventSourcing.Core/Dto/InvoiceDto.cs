using PracticalEventSourcing.Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.Dto
{
    public class InvoiceDto
    {
        public DateTime CartCreationDate { get; set; }
        public IEnumerable<ProductInvoiceDto> Products { get; set; }
    }

    public class ProductInvoiceDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
