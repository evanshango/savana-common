using System;

namespace Savana.Common.Events.Savana
{
    public record UserCreatedEvent(string Id, string Name, string Email, bool Active, string Phone, string CreatedBy,
        string AccountType, DateTime CreatedAt);

    public record UserUpdatedEvent(string Id, string Name, bool Active, string Phone, string ModifiedBy,
        string AccountType, DateTime ModifiedAt);

    public record UserDeletedEvent(string Id, bool Active, string ModifiedBy, DateTime ModifiedAt);

    public record CategoryCreatedEvent(int Id, string Uuid, string Name, bool Active, string CreatedBy,
        DateTime CreatedAt);

    public record CategoryUpdatedEvent(int Id, string Name, bool Active, string ModifiedBy, DateTime ModifiedAt);

    public record CategoryDeletedEvent(int Id, bool Active, string ModifiedBy, DateTime ModifiedAt);

    public record ProductCreatedEvent(int Id, string Uuid, string Name, string Image, string Category, bool Active,
        int Stock, int Price, decimal Discount, bool Featured, string Owner, string OwnerPhone, string CreatedBy,
        DateTime CreatedAt);

    public record ProductUpdatedEvent(int Id, string Name, string Image, string Category, bool Active,
        int Stock, int Price, decimal Discount, bool Featured, string ModifiedBy, DateTime? ModifiedAt, string Owner,
        string OwnerPhone);

    public record ProductDeletedEvent(int Id, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    public record ImageUpsertedEvent(int ProductId, string Image, string ModifiedBy, DateTime? ModifiedAt);

    public record OrderCreatedEvent(int OrderId, string Uuid, string PaymentOption, decimal Total, decimal SubTotal,
        string Items, DateTime CreatedAt);

    public record OrderUpdatedEvent(int OrderId, string Status, string ModifiedBy, DateTime? ModifiedAt);

    public record ClearBasketEvent(string BasketId);
}