using PropertySystem.Application.Properties.GetFilteredProperties;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PropertySystem.Api.Controllers.Properties;

[ApiController]
[Route("api/properties")]
public class PropertiesController : ControllerBase
{
    private readonly ISender _sender;

    public PropertiesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("filter")]
    [Authorize]
    public async Task<IActionResult> GetFilteredProperties(
        [FromQuery] string? name = null,
        [FromQuery] string? address = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetFilteredPropertiesQuery(name, address, minPrice, maxPrice);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
