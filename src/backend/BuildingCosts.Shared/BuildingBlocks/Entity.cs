namespace BuildingCosts.Shared.BuildingBlocks;

public class Entity<TType>
{
    private readonly ISet<IDomainEvent> _domainEvents = new HashSet<IDomainEvent>();

    public TType Id { get; protected set; }

    public int Version { get; protected set; }

    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

    public void ClearEvents() => _domainEvents.Clear();

    protected void AddEvent(IDomainEvent domainEvent)
    {
        if (!_domainEvents.Any())
        {
            Version++;
        }

        _domainEvents.Add(domainEvent);
    }
}