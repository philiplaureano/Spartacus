using System;
using System.Threading.Tasks;
using Spartacus.Core;
using Spartacus.Core.Primitives;
using Xunit;

using static Spartacus.Core.Parsers;
namespace Tests;

public class LetterParserTests
{
    [Fact(DisplayName = "The letter parser should be able to parse a letter character")]
    public async Task ShouldParseLetters()
    {
        var parser = Letter;
        var expectedText = "a";
        var inputText = "a";
        await parser.ParseAsync(inputText).ShouldBeSuccessful(expectedText);
    }

    [Fact(DisplayName = "The letter parser should also be able to parse an upper case letter")]
    public async Task ShouldParseUppercaseLetter()
    {
        var parser = Letter;
        var expectedText = "A";
        var inputText = "A";
        await parser.ParseAsync(inputText).ShouldBeSuccessful(expectedText);
    }

    [Fact(DisplayName = "The letter parser should return nothing if the input is invalid")]
    public async Task ShouldReturnNothingIfTheInputIsInvalid()
    {
        var parser = Letter;
        var invalidText = "5";
        await parser.ParseAsync(invalidText).ShouldFail();
    }
}