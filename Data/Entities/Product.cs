namespace Void.Chef.Data.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public double Quantity { get; set; }
    
    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;

    public Category Category { get; set; } = null!;
}