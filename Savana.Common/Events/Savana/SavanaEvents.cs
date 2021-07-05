using System;
using System.Collections.Generic;

namespace Savana.Common.Events.Savana
{
    public record UserCreatedEvent(string Id, string FirstName, string LastName, string Email, bool Active,
        string Gender, string Phone, IEnumerable<string> Roles);

    public record UserUpdatedEvent(string Id, string FirstName, string LastName, bool Active, string Phone,
        string Gender, string ModifiedBy, IEnumerable<string> Roles, DateTime ModifiedAt);

    public record UserDeletedEvent(string Id, bool Active, DateTime ModifiedAt);

    public record CategoryCreatedEvent(int Id, string Uuid, string Name, bool Active, string CreatedBy,
        DateTime CreatedAt);

    public record CategoryUpdatedEvent(int Id, string Name, bool Active, string ModifiedBy, DateTime ModifiedAt);

    public record CategoryDeletedEvent(int Id, bool Active, string ModifiedBy, DateTime ModifiedAt);

    public record ProductCreatedEvent(int Id, string Uuid, string Name, string Image, string Category, bool Active,
        int Stock, int Price, bool Featured);

    public record ProductUpdatedEvent(int Id, string Name, string Image, string Category, bool Active,
        int Stock, int Price, bool Featured, string ModifiedBy, DateTime? ModifiedAt);

    public record ProductDeletedEvent(int Id, bool Active, string ModifiedBy, DateTime? ModifiedAt);

    public record ImageUpserted(int ProductId,string Image, string ModifiedBy, DateTime? ModifiedAt);
}