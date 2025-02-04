using MediatR;
using Microsoft.Extensions.Logging;
using Void.Chef.Domain.Events.Products;

namespace Void.Chef.Application.Products.EventHandlers;

public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
{
    private readonly ILogger<ProductUpdatedEventHandler> _logger;

    public ProductUpdatedEventHandler(ILogger<ProductUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Void.Chef Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
