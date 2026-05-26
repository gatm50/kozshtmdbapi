using KozshTheMovieDbApi;
using KozshTheMovieDbApi.v3.Find.Item;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace KozshTheMovieDbApi.Tests;

public class FindTvShowByImdbIdTests(TestConfigFixture config) : IClassFixture<TestConfigFixture>
{
    private readonly TestConfigFixture _cfg = config ?? throw new ArgumentNullException(nameof(config));

    [Fact]
    public async Task ToGetRequestInformation_UsesFindEndpointWithImdbExternalSource()
    {
        var requestAdapter = new HttpClientRequestAdapter(new TokenAuthenticationProvider(_cfg.ApiToken));
        var apiClient = new ApiClient(requestAdapter);

        WithExternal_GetResponse? requestInfo = await apiClient
            .RootBuilder
            .Find["tt0063950"] //Scooby-Doo, Where Are You!
            .GetAsync(config =>
            {
                config.QueryParameters.ExternalSource = GetExternal_sourceQueryParameterType.Imdb_id;
                config.QueryParameters.Language = "en-US";
            }, TestContext.Current.CancellationToken);

        Assert.NotNull(requestInfo);
        Assert.NotNull(requestInfo.TvResults[0]);
        Assert.Equal("Scooby-Doo, Where Are You!".ToLowerInvariant(), requestInfo.TvResults[0].Name.ToLowerInvariant());
        Assert.Equal(926, requestInfo.TvResults[0].Id);

        //Assert.Equal("GET", requestInfo.HttpMethod.ToString());
        //Assert.Equal(
        //    "https://api.themoviedb.org/3/find/tt0944947?external_source=imdb_id",
        //    requestInfo.URI.ToString());
    }
}
