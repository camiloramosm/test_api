using MediatR;
using PropertySystem.Application.Abstractions.Messaging;
using PropertySystem.Domain.Abstractions;
using PropertySystem.Domain.Properties;

namespace PropertySystem.Application.Properties.GetFilteredProperties;

internal sealed class GetFilteredPropertiesQueryHandler : IQueryHandler<GetFilteredPropertiesQuery, IEnumerable<PropertyResponse>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IOwnerRepository _ownerRepository;

    public GetFilteredPropertiesQueryHandler(
        IPropertyRepository propertyRepository,
        IOwnerRepository ownerRepository)
    {
        _propertyRepository = propertyRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task<IEnumerable<PropertyResponse>> Handle(GetFilteredPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = await _propertyRepository.GetFilteredPropertiesAsync(
            request.Name,
            request.Address,
            request.MinPrice,
            request.MaxPrice,
            cancellationToken);

        var propertyResponses = new List<PropertyResponse>();

        foreach (var property in properties)
        {
            var owner = await _ownerRepository.GetByIdAsync(property.OwnerId, cancellationToken);
            var firstImage = property.Images.FirstOrDefault()?.File;

            propertyResponses.Add(new PropertyResponse(
                property.Id,
                property.Name,
                property.Address,
                property.Price,
                property.CodeInternal,
                property.Year,
                property.OwnerId,
                owner?.Name ?? string.Empty,
                owner?.Address ?? string.Empty,
                firstImage
            ));
        }

        return propertyResponses;
    }

    Task<Result<IEnumerable<PropertyResponse>>> IRequestHandler<GetFilteredPropertiesQuery, Result<IEnumerable<PropertyResponse>>>.Handle(GetFilteredPropertiesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
