namespace PropertySystem.Application.Properties.GetFilteredProperties;

public sealed record PropertyResponse(
    Guid Id,
    string Name,
    string Address,
    decimal Price,
    string CodeInternal,
    int Year,
    Guid OwnerId,
    string OwnerName,
    string OwnerAddress,
    string? PropertyImage);
