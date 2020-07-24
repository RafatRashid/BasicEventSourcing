using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PracticalEventSourcing.Core.ReadModels
{
    public class ProductRM : BaseEntity
    {
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
    }

    public class ProductRMMap : IEntityTypeConfiguration<ProductRM>
    {
        public void Configure(EntityTypeBuilder<ProductRM> builder)
        {
            builder.ToTable("Product");
            builder.HasKey("Id");
            builder.Property(x => x.Id).HasColumnType<Guid>("nvarchar");
        }
    }
}
