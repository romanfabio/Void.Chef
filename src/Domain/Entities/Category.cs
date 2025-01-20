using Void.Chef.Domain.Common;

namespace Void.Chef.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
}