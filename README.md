# kozshtmdbapi

`kozshtmdbapi` is a .NET client library for The Movie DB REST API.

It is generated with Kiota and provides strongly typed request builders and models to call The Movie DB endpoints from C# code.

## Compatibility

This package targets:

- `netstandard2.0`
- `netstandard2.1`

## Installation

Install from NuGet:

`dotnet add package KozshTheMovieDbApi`

## Basic usage

1. Create an adapter/auth configuration.
2. Create `ApiClient`.
3. Use request builders to call endpoints.

Example:

```csharp
using Kozshplxapi;

// Configure your Kiota request adapter here
// var adapter = ...;

// var client = new ApiClient(adapter);
// var serverStatus = await client.Status.GetAsync();
```

## Development

Build and pack locally:

- `dotnet run ci-pipeline.cs -- --target Pack --configuration Release`

CI pipeline uses Cake (`ci-pipeline.cs`) and GitHub Actions (`.github/workflows/cake.yml`) to build and publish packages.