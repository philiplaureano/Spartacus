using System.Threading.Tasks;
using Spartacus.Core;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class WhitespaceParserTests
{
    [Fact(DisplayName = "The whitespace parser should parse white space characters")]
    public async Task ShouldParseWhitespaceChars()
    {
        var whitespace = " ";
        var parser = new WhitespaceParser();
        await parser.ParseAsync(whitespace).ShouldBeSuccessful(" ");
    }

    [Fact(DisplayName = "The whitespace parser should not parse invalid input")]
    public async Task ShouldNotParseInvalidInput()
    {
        var invalidInput = "X";
        var parser = new WhitespaceParser();
        await parser.ParseAsync(invalidInput).ShouldNotBeSuccessful();
    }
    
    [Fact(DisplayName = "The whitespace parser should not parse empty strings")]
    public async Task ShouldNotParseEmptyStrings()
    {
        var text = string.Empty;
        var parser = new WhitespaceParser();
        await parser.ParseAsync(text).ShouldNotBeSuccessful();
    }
}