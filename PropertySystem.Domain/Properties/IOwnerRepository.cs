using PropertySystem.Domain.Properties;

namespace PropertySystem.Domain.Properties;

public interface IOwnerRepository
{
    Task<Owner?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Owner owner);
    void Update(Owner owner);
    void Delete(Owner owner);
}
