namespace Savana.Common.Events
{
    public record UserCreatedEvent(string Body);
    public record UserUpdatedEvent(string Body);
    public record UserDeletedEvent(string Body);

    public record GroupCreatedEvent(string Body);
    public record GroupUpdatedEvent(string Body);
    public record GroupDeletedEvent(string Body);
    
    public record GroupMemberCreatedEvent(string Body);
    public record GroupMemberUpdatedEvent(string Body); 
    public record GroupMemberDeletedEvent(string Body);

    public record LoanCreatedEvent(string Body);
    public record LoanUpdatedEvent(string Body);
}   