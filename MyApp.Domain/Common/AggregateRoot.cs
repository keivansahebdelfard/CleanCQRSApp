using MyApp.Domain.Entities;

namespace MyApp.Domain.Common
{
    /// <summary>
    /// Represents the root of an aggregate in the domain model.
    /// Keep this type as a semantic marker so infrastructure (for example
    /// `ChangeTracker.Entries&lt;AggregateRoot&gt;`) can discover aggregate roots.
    /// Add aggregate-specific behaviors (business rules, helpers, equality, etc.)
    /// here in the future.
    /// </summary>
    public abstract class AggregateRoot : BaseEntity
    {
    }
}
