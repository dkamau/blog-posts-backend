using System;
using System.Collections.Generic;
using BlogPostsBackend.SharedKernel.Interfaces;

namespace BlogPostsBackend.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity : IAggregateRoot
    {
        public int Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
