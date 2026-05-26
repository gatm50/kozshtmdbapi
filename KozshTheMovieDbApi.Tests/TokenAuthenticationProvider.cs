using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace KozshTheMovieDbApi.Tests
{
    public sealed class TokenAuthenticationProvider(string apiToken) : IAuthenticationProvider
    {
        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            request.Headers.TryAdd("Authorization", $"Bearer {apiToken}");
            return Task.CompletedTask;
        }
    }
}
