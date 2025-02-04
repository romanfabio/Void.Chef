using MediatR;
using Microsoft.Extensions.Logging;
using Void.Chef.Domain.Events.Products;

namespace Void.Chef.Application.Products.EventHandlers;

public class ProductDeletedEventHandler : INotificationHandler<ProductDeletedEvent>
{
    private readonly ILogger<ProductDeletedEventHandler> _logger;

    public ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Void.Chef Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
