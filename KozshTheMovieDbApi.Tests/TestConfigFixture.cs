using Microsoft.Extensions.Configuration;

namespace KozshTheMovieDbApi.Tests
{
    public sealed class TestConfigFixture
    {
        public IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json", optional: false)
            .AddUserSecrets("9df7cb33-f13f-4c38-831a-45841f9a8f9a")
            .AddEnvironmentVariables()
            .Build();

        public string ApiToken => Configuration["TMDB:Token"]!;
    }
}
