using Void.Chef.Domain.Common;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Domain.Events.Products;

public class ProductCreatedEvent : BaseEvent
{
    public ProductCreatedEvent(Product entity)
    {
        Entity = entity;
    }

    public Product Entity { get; }
}
