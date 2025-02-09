using MediatR;

namespace CQRSMediatR.Notifications
{
    public record ProductCreatedNotification(Guid Id) : INotification;
}
