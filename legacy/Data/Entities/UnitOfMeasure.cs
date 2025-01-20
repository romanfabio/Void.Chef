namespace Void.Chef.Data.Entities;

public class UnitOfMeasure : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public bool IsDefault { get; set; }

    public UnitOfMeasureCategory Category { get; set; } = null!;
}