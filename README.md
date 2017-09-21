# ColognePhoneticsSharp
This is an implementation of the Cologne phonetics algorithm (Kölner Phonetik) for .NET

It [was initially written](http://codekicker.de/news/csharp-Implementierung-Koelner-Phonetik--NET) by [Klaus Bock](https://twitter.com/klaus_b0) and he kindly gave me permission to create a nuget package for this.

# Getting started

## Install from [NuGet](https://www.nuget.org/packages/ColognePhoneticsSharp)
`Install-Package ColognePhoneticsSharp`

## Using the tool

```csharp
using ColognePhoneticSharp;

// expected result: "65752682";
var input = "Müller-Lüdenscheidt";
var output = ColognePhonetics.GetPhonetics(input);
```
