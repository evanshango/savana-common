namespace Savana.Common.Events
{
    public record ItemCreatedEvent(int Id, string Body);
    public record ItemUpdatedEvent(int Id, string Body);
    public record ItemRemovedEvent(int Id);
}