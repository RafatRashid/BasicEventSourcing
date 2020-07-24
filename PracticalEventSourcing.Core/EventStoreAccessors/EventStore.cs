using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core.EventStoreAccessors
{
    public class EventStore : BaseEntity
    {
        public Guid AggregateId { get; set; }
        public string Payload { get; set; }
        public DateTime Timestamp { get; set; }
        public int Version { get; set; }
        public string EventType { get; set; }
    }

    public class EventStoreMap : IEntityTypeConfiguration<EventStore>
    {
        public void Configure(EntityTypeBuilder<EventStore> builder)
        {
            builder.ToTable("EventStore");
            builder.HasKey("Id");
            builder.Property(x => x.Id).HasColumnType<Guid>("nvarchar");
            builder.Property(x => x.AggregateId).HasColumnType<Guid>("nvarchar");
        }
    }
}
