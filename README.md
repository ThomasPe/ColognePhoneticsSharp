# ColognePhoneticsSharp
This is an implementation of the Cologne phonetics algorithm (Kölner Phonetik) for .NET

It was initially written by Klaus Bock (originally published at codekicker.de) and he kindly gave permission to create a nuget package for this.

# Getting started

## Install from [NuGet](https://www.nuget.org/packages/ColognePhoneticsSharp)
`Install-Package ColognePhoneticsSharp`

## Using the tool

```csharp
using ColognePhoneticsSharp;

// expected result: "65752682";
var input = "Müller-Lüdenscheidt";
var output = ColognePhonetics.GetPhonetics(input);
```

## Releasing

To publish a release, update the `<Version>` and `<PackageReleaseNotes>` values in `ColognePhoneticsSharp/ColognePhoneticsSharp.csproj` and commit the changes. Then create and push a matching version tag (for example, `v1.2.0` for package version `1.2.0`).

Pushing a `v*` tag runs the **Release to NuGet** GitHub Actions workflow. It verifies that the tag matches the project version, builds and tests the package, publishes it to NuGet, and creates a GitHub Release with automatically generated notes. Configure the repository's `NUGET_API_KEY` Actions secret with a NuGet API key before the first release. GitHub Releases provide the release history; no separate changelog is maintained.
