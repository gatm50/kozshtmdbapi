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
        Assert.NotNull(requestInfo.TvResults);
        Assert.NotEmpty(requestInfo.TvResults);
        Assert.NotNull(requestInfo.TvResults[0]);

        Assert.NotNull(requestInfo.TvResults[0].Name);
        Assert.Equal("Scooby-Doo, Where Are You!".ToLowerInvariant(), requestInfo.TvResults[0].Name.ToLowerInvariant());
        Assert.Equal(926, requestInfo.TvResults[0].Id);
    }

    [Fact]
    public async Task GetAsync_TvSeason_ReturnsExpectedFields()
    {
        var requestAdapter = new HttpClientRequestAdapter(new TokenAuthenticationProvider(_cfg.ApiToken));
        var apiClient = new ApiClient(requestAdapter);

        var tvShowWithEpisodes = await apiClient
            .RootBuilder
            .Tv[926].Season[1].GetAsync(config =>
                {
                    config.QueryParameters.Language = "en-US";
                }, TestContext.Current.CancellationToken
            );

        Assert.NotNull(tvShowWithEpisodes);

        Assert.NotNull(tvShowWithEpisodes.Id);
        Assert.Equal("52540e7f19c295794031e163", tvShowWithEpisodes.Id);

        Assert.NotNull(tvShowWithEpisodes.Episodes);
        Assert.Equal(17, tvShowWithEpisodes.Episodes.Count);

        Assert.NotNull(tvShowWithEpisodes.AirDate);
        Assert.Equal(new DateOnly(1969,09,13), DateOnly.Parse(tvShowWithEpisodes.AirDate));
    }
}