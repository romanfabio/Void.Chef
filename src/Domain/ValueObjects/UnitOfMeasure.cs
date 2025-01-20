using Void.Chef.Domain.Common;
using Void.Chef.Domain.Exceptions;

namespace Void.Chef.Domain.ValueObjects;

public class UnitOfMeasure(string name, UnitOfMeasureCategory category, string code, string? pluralCode = null) : ValueObject
{
    public static UnitOfMeasure From(string code)
    {
        var unit = SupportedUnitOfMeasures.FirstOrDefault(u => u.Code == code);

        if (unit is null)
        {
            throw new UnsupportedUnitOfMeasureException(code);
        }

        return unit;
    }
    
    public string Name { get; private set; } = string.IsNullOrWhiteSpace(name) ? "Item": name;
    
    public UnitOfMeasureCategory Category { get; private set; } = category;
    
    public string Code { get; private set; } = string.IsNullOrWhiteSpace(code) ? "item" : code;
    public string PluralCode { get; private set; } = string.IsNullOrWhiteSpace(pluralCode)? "items": pluralCode;

    public static UnitOfMeasure Item => new("Item", UnitOfMeasureCategory.Other, "item", "items");
    public static UnitOfMeasure Kilogram => new("Kilogram", UnitOfMeasureCategory.Mass, "kg");
    public static UnitOfMeasure Gram => new("Gram", UnitOfMeasureCategory.Mass, "g");
    public static UnitOfMeasure Milligram => new("Milligram", UnitOfMeasureCategory.Mass, "mg");
    public static UnitOfMeasure Pound => new("Pound", UnitOfMeasureCategory.Mass, "lb");
    public static UnitOfMeasure Ounce => new("Ounce", UnitOfMeasureCategory.Mass, "oz");
    public static UnitOfMeasure Ton => new("Ton", UnitOfMeasureCategory.Mass, "t");
    
    public static UnitOfMeasure Liter => new("Liter", UnitOfMeasureCategory.Volume, "L");
    public static UnitOfMeasure Milliliter => new("Milliliter", UnitOfMeasureCategory.Volume, "mL");
    public static UnitOfMeasure Gallon => new("Gallon", UnitOfMeasureCategory.Volume, "gal");
    public static UnitOfMeasure Quart => new("Quart", UnitOfMeasureCategory.Volume, "qt");
    public static UnitOfMeasure Pint => new("Pint", UnitOfMeasureCategory.Volume, "pt");
    public static UnitOfMeasure FluidOunce => new("Fluid Ounce", UnitOfMeasureCategory.Volume, "fl oz");
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Category;
        yield return Code;
        yield return PluralCode;
    }
    
    protected static IEnumerable<UnitOfMeasure> SupportedUnitOfMeasures
    {
        get
        {
            yield return Item;
            
            yield return Kilogram;
            yield return Gram;
            yield return Milligram;
            yield return Pound;
            yield return Ounce;
            yield return Ton;

            yield return Liter;
            yield return Milliliter;
            yield return Gallon;
            yield return Quart;
            yield return Pint;
            yield return FluidOunce;
        }
    }
}

public enum UnitOfMeasureCategory
{
    Other = 1,
    Mass = 2,
    Volume = 3
}