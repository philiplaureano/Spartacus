using System;
using System.Threading.Tasks;
using Spartacus.Core;
using Spartacus.Core.Primitives;
using Xunit;

using static Spartacus.Core.Parsers;
namespace Tests;

public class StringParserTests
{
    [Fact(DisplayName = "The string parser should match when a given phrase is met")]
    public async Task ShouldBeAbleToParseStringPhrase()
    {
        var phrase = Guid.NewGuid().ToString();
        IParser parser = String(phrase);

        await parser.ParseAsync(phrase).ShouldBeSuccessful(phrase);
    }

    [Fact(DisplayName = "The string parser should return nothing if the input text does not match the expected text")]
    public async Task ShouldReturnNoneIfTheTextDoesNotMatch()
    {
        var expectedPhrase = Guid.NewGuid().ToString();
        var invalidPhrase = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        
        IParser parser = String(expectedPhrase);
        await parser.ParseAsync(invalidPhrase).ShouldFail();
    }
}