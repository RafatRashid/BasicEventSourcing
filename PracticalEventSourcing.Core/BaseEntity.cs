using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalEventSourcing.Core
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
