#:sdk Cake.Sdk@6.1.1

var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");
var project = "./KozshTheMovieDbApi/KozshTheMovieDbApi.csproj";
var artifactsDirectory = "./artifacts";
var nugetSource = Argument("nugetSource", "https://api.nuget.org/v3/index.json");
var nugetApiKey = Argument("nugetApiKey", string.Empty);
var publishToNuGet = Argument("publishToNuGet", false);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory(artifactsDirectory);
    CleanDirectories("./KozshTheMovieDbApi/bin");
    CleanDirectories("./KozshTheMovieDbApi/obj");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetBuild(project, new DotNetBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetTest(project, new DotNetTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    });
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    EnsureDirectoryExists(artifactsDirectory);

    DotNetPack(project, new DotNetPackSettings
    {
        Configuration = configuration,
        NoBuild = true,
        OutputDirectory = artifactsDirectory,
    });
});

Task("PublishNuGet")
    .IsDependentOn("Pack")
    .WithCriteria(() => !string.IsNullOrWhiteSpace(nugetApiKey))
    .Does(() =>
    {
        var packageFiles = GetFiles($"{artifactsDirectory}/*.nupkg")
            .Where(p => !p.FullPath.EndsWith(".symbols.nupkg", StringComparison.OrdinalIgnoreCase));

        foreach (var packageFile in packageFiles)
        {
            DotNetNuGetPush(packageFile.FullPath, new DotNetNuGetPushSettings
            {
                Source = nugetSource,
                ApiKey = nugetApiKey,
                SkipDuplicate = true,
            });
        }
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);