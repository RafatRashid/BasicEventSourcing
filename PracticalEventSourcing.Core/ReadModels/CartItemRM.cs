using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.ReadModels
{
    public class CartItemRM: BaseEntity
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }


    public class CartItemRMMap : IEntityTypeConfiguration<CartItemRM>
    {
        public void Configure(EntityTypeBuilder<CartItemRM> builder)
        {
            builder.ToTable("CartItem");
            builder.HasKey("Id");
        }
    }
}
