using PropertySystem.Application.Abstractions.Authentication;
using PropertySystem.Domain.Users;
using PropertySystem.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace PropertySystem.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    private const string PasswordCredentialType = "password";
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);

        userRepresentationModel.Credentials = new CredentialsRepresentationModel[]
        {
            new()
            {
                Value = user.Password.Value,
                Temporary = false,
                Type = PasswordCredentialType
            }
        };

        var response = await _httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);

        return ExtracIdentityIdFromLocationHeader(response);
    }

    private static string ExtracIdentityIdFromLocationHeader(HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("The Location header is missing in the response.");
        }

        var userSegmentValueIndex = locationHeader.IndexOf(usersSegmentName, StringComparison.InvariantCultureIgnoreCase);

        var userIndetityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

        return userIndetityId;
    }
}
