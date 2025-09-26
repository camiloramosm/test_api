using PropertySystem.Application.Abstractions.Messaging;

namespace PropertySystem.Application.Properties.GetFilteredProperties;

public sealed record GetFilteredPropertiesQuery(
    string? Name = null,
    string? Address = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null) : IQuery<IEnumerable<PropertyResponse>>;
