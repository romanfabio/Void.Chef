using Void.Chef.Features.Shared.Dtos;

namespace Void.Chef.Features.Products.Shared;

public class ProductEditorModel
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }
    
    public double? Quantity { get; set; }
    
    public SimpleLookup? UnitOfMeasure { get; set; }

    public SimpleLookup? Category { get; set; }
}