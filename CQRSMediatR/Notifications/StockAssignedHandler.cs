﻿using MediatR;

namespace CQRSMediatR.Notifications
{
    public class StockAssignedHandler(ILogger<StockAssignedHandler> logger) : INotificationHandler<ProductCreatedNotification>
    {
        public Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            //logger.LogInformation($"Handling notification for product creation with id : {notification.Id}. assigning stocks.");
            logger.LogInformation("Handling notification for product creation with id: {ProductId}. Assigning stocks.", notification.Id);
            return Task.CompletedTask;
        }
    }
}