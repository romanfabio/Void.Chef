using Void.Chef.Domain.Common;

namespace Void.Chef.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    
    public float Quantity { get; set; }
    
    public Category Category { get; set; } = null!;
    
    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;
}