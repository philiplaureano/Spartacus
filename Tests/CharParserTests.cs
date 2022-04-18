using System.Threading.Tasks;
using Spartacus.Core;
using Xunit;

namespace Tests;

public class CharParserTests
{
    [Fact(DisplayName = "The char parser should be able to parse a given character")]
    public async Task ShouldBeAbleToParseChar()
    {
        var targetChar = 'a';
        var parser = new CharParser(targetChar);
        var result = await parser.ParseAsync("a");
        
        Assert.True(result.HasValue);
    }

    [Fact(DisplayName = "The char parser should return nothing if the character does not match the input")]
    public async Task ShouldReturnNoneIfTheCharacterDoesNotMatch()
    {
        var targetChar = 'a';
        var parser = new CharParser(targetChar);
        var result = await parser.ParseAsync("b");
        
        Assert.False(result.HasValue);
    }

    [Fact(DisplayName = "The char parser should return nothing if the input text is empty")]
    public async Task ShouldReturnNoneIfTheInputIsEmpty()
    {
        var targetChar = 'a';
        var parser = new CharParser(targetChar);
        var result = await parser.ParseAsync(string.Empty);
        
        Assert.False(result.HasValue);
    }
}