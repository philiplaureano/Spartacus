using System;
using System.Threading.Tasks;
using Spartacus.Core;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class StringParserTests
{
    [Fact(DisplayName = "The string parser should match when a given phrase is met")]
    public async Task ShouldBeAbleToParseStringPhrase()
    {
        var phrase = Guid.NewGuid().ToString();
        IParser parser = new StringParser(phrase);

        await parser.ParseAsync(phrase).ShouldBeSuccessful(phrase);
    }

    [Fact(DisplayName = "The string parser should return nothing if the input text does not match the expected text")]
    public async Task ShouldReturnNoneIfTheTextDoesNotMatch()
    {
        var expectedPhrase = Guid.NewGuid().ToString();
        var invalidPhrase = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        
        IParser parser = new StringParser(expectedPhrase);
        await parser.ParseAsync(invalidPhrase).ShouldFail();
    }
}