using System;
using System.Threading.Tasks;
using Spartacus.Core;
using Xunit;

namespace Tests;

public class KleeneStarParserTests
{
    [Fact(DisplayName = "The kleene star parser should be successful if it matches it sub parser zero or more times")]
    public async Task ShouldParseSuccessfullyIfZeroOrMoreSubParsersAreSuccessful()
    {
        var digitParser = new DigitParser();
        var parser = digitParser.ZeroOrMoreInstances();

        // Match the non-zero instances
        await parser.ParseAsync("12345678").ShouldBeSuccessful("12345678");
        await parser.ParseAsync("8675309").ShouldBeSuccessful("8675309");
        
        // Match the zero instance
        await parser.ParseAsync(string.Empty).ShouldBeSuccessful();
    }
}