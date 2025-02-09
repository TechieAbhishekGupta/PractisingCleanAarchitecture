using MediatR;

namespace CQRSMediatR.Notifications
{
    public class RandomHandler(ILogger<RandomHandler> logger) : INotificationHandler<ProductCreatedNotification>
    {
        public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            //logger.LogInformation($"Handling notification for product creation with id : {notification.Id}. performing random action.");
            logger.LogInformation("Handling notification for product creation with id: {ProductId}. Performing random action.", notification.Id);
            return Task.CompletedTask;
        }
    }
}