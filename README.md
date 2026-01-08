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
