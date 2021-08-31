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

    public record CategoryUpdatedEvent(int Id, string Name, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    public record CategoryDeletedEvent(int Id, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    public record BrandCreatedEvent(int Id, string Uuid, string Name, string Category, bool Active, string CreatedBy,
        DateTime CreatedAt);

    public record BrandUpdatedEvent(int Id, string Name, string Category, string ModifiedBy, DateTime? ModifiedAt);

    public record BrandDeletedEvent(int Id, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    public record ProductCreatedEvent(int Id, string Uuid, string Name, string Image, string Brand, bool Active,
        int Stock, int Price, bool Featured, string Owner, string OwnerPhone, string CreatedBy, DateTime CreatedAt);

    public record ProductUpdatedEvent(int Id, string Name, string Image, string Brand, bool Active,
        int Stock, int Price, bool Featured, string ModifiedBy, DateTime? ModifiedAt, string Owner, string OwnerPhone);

    public record ProductDeletedEvent(int Id, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    public record PromotionCreatedEvent(int Id, string Title, string Uuid, int ProductId, decimal Discount,
        DateTime? ExpiresAt, string CreatedBy, DateTime CreatedAt);

    public record PromotionUpdatedEvent(int Id, string Title, decimal Discount, DateTime ExpiresAt, string ModifiedBy,
        DateTime? ModifiedAt);

    public record PromotionDeletedEvent(int Id);

    public record ImageUpsertedEvent(int ProductId, string Image, string ModifiedBy, DateTime? ModifiedAt);

    public record OrderCreatedEvent(int OrderId, string Uuid, string PaymentOption, decimal Total, decimal SubTotal,
        string Items, string Status, DateTime CreatedAt);

    public record OrderUpdatedEvent(int OrderId, string Status, string ModifiedBy, DateTime? ModifiedAt);

    public record ClearBasketEvent(string BasketId);

    public record StockUpdateEvent(string Items);
}