using Void.Chef.Domain.Common;

namespace Void.Chef.Domain.Entities;

public class UnitOfMeasure : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    
    public string PluralName { get; set; } = null!;
}