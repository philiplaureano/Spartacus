using System;
using System.Threading.Tasks;
using Optional.Unsafe;
using Spartacus.Core.Primitives;
using Xunit;

using static Spartacus.Core.Parsers;
namespace Tests;

public class DigitParserTests
{
    [Fact(DisplayName = "The digit parser should be able to parse a single digit")]
    public async Task ShouldBeAbleToParseDigit()
    {
        var parser = Digit;

        var inputText = "1";
        var input = inputText.AsMemory();
        var result = await parser.ParseAsync(input);
        
        Assert.True(result.HasValue);
        Assert.Equal("1",result.ValueOrFailure().ToString());
    }

    [Fact(DisplayName = "The digit parser should return none if it is unable to parse the given character")]
    public async Task ShouldReturnNoneIfInvalidInputIsGiven()
    {
        var parser = new DigitParser();
        var inputText = "X";
        
        var input = inputText.AsMemory();
        var result = await parser.ParseAsync(input);
        
        Assert.False(result.HasValue);
    }

    [Fact(DisplayName = "The digit parser should return none if the input is empty")]
    public async Task ShouldReturnNoneIfInputIsEmpty()
    {
        var parser = new DigitParser();
        var inputText = string.Empty;
        
        var input = inputText.AsMemory();
        var result = await parser.ParseAsync(input);
        
        Assert.False(result.HasValue);
    }
}