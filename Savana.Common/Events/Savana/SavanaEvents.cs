using System;
using System.Collections.Generic;

namespace Savana.Common.Events.Savana
{
    /// <summary>
    /// Emits a User created event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Email"></param>
    /// <param name="Active"></param>
    /// <param name="Phone"></param>
    /// <param name="GroupIds"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="AccountType"></param>
    /// <param name="CreatedAt"></param>
    public record UserCreatedEvent(string Id, string Name, string Email, bool Active, string Phone,
        IEnumerable<int?> GroupIds,
        string CreatedBy, string AccountType, DateTime CreatedAt);

    /// <summary>
    /// Emits a User updated event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Active"></param>
    /// <param name="Phone"></param>
    /// <param name="GroupIds"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="AccountType"></param>
    /// <param name="ModifiedAt"></param>
    public record UserUpdatedEvent(string Id, string Name, bool Active, string Phone, IEnumerable<int?> GroupIds,
        string ModifiedBy, string AccountType, DateTime ModifiedAt);

    /// <summary>
    /// Emits a User deleted event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record UserDeletedEvent(string Id, string ModifiedBy, DateTime ModifiedAt);

    /// <summary>
    /// Emits a Group created event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Uuid"></param>
    /// <param name="Name"></param>
    /// <param name="PhoneNumber"></param>
    /// <param name="PayBillNo"></param>
    /// <param name="AccountNo"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    public record GroupCreatedEvent(int Id, string Uuid, string Name, string PhoneNumber, string PayBillNo,
        string AccountNo, string CreatedBy, DateTime CreatedAt);

    /// <summary>
    /// Emits a Group updated event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="UserId"></param>
    /// <param name="Phone"></param>
    /// <param name="PayBillNo"></param>
    /// <param name="AccountNo"></param>
    /// <param name="Active"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record GroupUpdatedEvent(int Id, string Name, string UserId, string Phone, string PayBillNo,
        string AccountNo, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Group deleted event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record GroupDeletedEvent(int Id, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Category created event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Uuid"></param>
    /// <param name="Name"></param>
    /// <param name="Active"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    public record CategoryCreatedEvent(int Id, string Uuid, string Name, bool Active, string CreatedBy,
        DateTime CreatedAt);

    /// <summary>
    /// Emits a Category updated event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Active"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record CategoryUpdatedEvent(int Id, string Name, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Category deleted event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record CategoryDeletedEvent(int Id, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Brand created event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Uuid"></param>
    /// <param name="Name"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    public record BrandCreatedEvent(int Id, string Uuid, string Name, string CreatedBy,
        DateTime CreatedAt);

    /// <summary>
    /// Emits a Brand updated event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Active"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record BrandUpdatedEvent(int Id, string Name, bool Active, string ModifiedBy,
        DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Brand deleted event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record BrandDeletedEvent(int Id, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Product created event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Uuid"></param>
    /// <param name="Name"></param>
    /// <param name="Image"></param>
    /// <param name="Category"></param>
    /// <param name="Brand"></param>
    /// <param name="Stock"></param>
    /// <param name="Price"></param>
    /// <param name="Featured"></param>
    /// <param name="Owner"></param>
    /// <param name="OwnerPhone"></param>
    /// <param name="GroupName"></param>
    /// <param name="GroupPhone"></param>
    /// <param name="GroupPayBill"></param>
    /// <param name="GroupAccount"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    public record ProductCreatedEvent(int Id, string Uuid, string Name, string Image, string Category, string Brand,
        int Stock, int Price, bool Featured, string Owner, string OwnerPhone, string GroupName, string GroupPhone,
        string GroupPayBill, string GroupAccount, string CreatedBy, DateTime CreatedAt);

    /// <summary>
    /// Emits a Product updated event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Image"></param>
    /// <param name="Category"></param>
    /// <param name="Brand"></param>
    /// <param name="Active"></param>
    /// <param name="Stock"></param>
    /// <param name="Price"></param>
    /// <param name="Featured"></param>
    /// <param name="Owner"></param>
    /// <param name="OwnerPhone"></param>
    /// <param name="GroupName"></param>
    /// <param name="GroupPhone"></param>
    /// <param name="GroupPayBill"></param>
    /// <param name="GroupAccount"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record ProductUpdatedEvent(int Id, string Name, string Image, string Category, string Brand, bool Active,
        int Stock, int Price, bool Featured, string Owner, string OwnerPhone, string GroupName, string GroupPhone,
        string GroupPayBill, string GroupAccount, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Product deleted event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record ProductDeletedEvent(int Id, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Promotion created event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Title"></param>
    /// <param name="Uuid"></param>
    /// <param name="ProductId"></param>
    /// <param name="Discount"></param>
    /// <param name="ExpiresAt"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    public record PromotionCreatedEvent(int Id, string Title, string Uuid, int ProductId, decimal Discount,
        DateTime? ExpiresAt, string CreatedBy, DateTime CreatedAt);

    /// <summary>
    /// Emits a Promotion updated event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Title"></param>
    /// <param name="Discount"></param>
    /// <param name="ExpiresAt"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record PromotionUpdatedEvent(int Id, string Title, decimal Discount, DateTime ExpiresAt, string ModifiedBy,
        DateTime? ModifiedAt);

    /// <summary>
    /// Emits a Promotion deleted event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    public record PromotionDeletedEvent(int Id);

    /// <summary>
    /// Emits an Image updated or inserted event to an event bus
    /// </summary>
    /// <param name="ProductId"></param>
    /// <param name="Image"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record ImageUpsertedEvent(int ProductId, string Image, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits an Order created event to an event bus
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="Uuid"></param>
    /// <param name="PaymentOption"></param>
    /// <param name="Total"></param>
    /// <param name="SubTotal"></param>
    /// <param name="Items"></param>
    /// <param name="Status"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    public record OrderCreatedEvent(int OrderId, string Uuid, string PaymentOption, decimal Total, decimal SubTotal,
        string Items, string Status, string CreatedBy, DateTime CreatedAt);

    /// <summary>
    /// Emits an Order updated event to an event bus
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="Status"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record OrderUpdatedEvent(int OrderId, string Status, string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits a clear basket event to an event bus
    /// </summary>
    /// <param name="BasketId"></param>
    public record ClearBasketEvent(string BasketId);

    /// <summary>
    /// Emits a Stock update event to an event bus
    /// </summary>
    /// <param name="Items"></param>
    public record StockUpdateEvent(string Items);

    /// <summary>
    /// Emits an Occasion created event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Uuid"></param>
    /// <param name="GroupId"></param>
    /// <param name="Category"></param>
    /// <param name="Price"></param>
    /// <param name="CreatedBy"></param>
    /// <param name="CreatedAt"></param>
    public record OccasionCreatedEvent(int Id, string Name, string Uuid, int GroupId, string Category, decimal Price,
        string CreatedBy, DateTime CreatedAt);

    /// <summary>
    /// Emits an Occasion updated event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="GroupId"></param>
    /// <param name="Category"></param>
    /// <param name="Price"></param>
    /// <param name="Active"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record OccasionUpdatedEvent(int Id, string Name, int GroupId, string Category, decimal Price, bool Active,
        string ModifiedBy, DateTime? ModifiedAt);

    /// <summary>
    /// Emits an Occasion deleted event to an event bus
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record OccasionDeletedEvent(int Id, string ModifiedBy, DateTime? ModifiedAt);
    
    /// <summary>
    /// Emits a Payment made event to an event bus
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="PaymentStatus"></param>
    /// <param name="ModifiedBy"></param>
    /// <param name="ModifiedAt"></param>
    public record PaymentMade(int OrderId, string PaymentStatus, string ModifiedBy, DateTime? ModifiedAt);
}