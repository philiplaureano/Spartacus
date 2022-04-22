using System;
using System.Threading.Tasks;
using Spartacus.Core;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class AsciiParserTests
{
    [Fact(DisplayName = "The ascii parser should be able to determine if a char is an ascii character")]    
    public async Task ShouldBeAbleToParseAsciiChar()
    {
        var asciiRange = new Range(0, 0x7f);
        var random = new Random();
        var charValue = random.Next(asciiRange.Start.Value, asciiRange.End.Value);
        var input = char.ConvertFromUtf32(charValue);
        
        var parser = new AsciiParser();
        await parser.ParseAsync(input).ShouldBeSuccessful();
    }

    [Fact(DisplayName = "The ascii parser should return none if the character is not ascii")]
    public async Task ShouldReturnNoneIfNotAsciiChar()  
    {
        var random = new Random();
        var nonAsciiRange = new Range(0x7f, 0xffff);
        var charValue = random.Next(nonAsciiRange.Start.Value, nonAsciiRange.End.Value);
            
        var input = char.ConvertFromUtf32(charValue);
        
        var parser = new AsciiParser();
        await parser.ParseAsync(input).ShouldFail();
    }
}