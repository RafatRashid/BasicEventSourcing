using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.ReadModels
{
    public class CartRM : BaseEntity
    {
        public string ProductIds { get; set; }
        public string ShippingInformation { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class CartRMMap : IEntityTypeConfiguration<CartRM>
    {
        public void Configure(EntityTypeBuilder<CartRM> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey("Id");
        }
    }
}
