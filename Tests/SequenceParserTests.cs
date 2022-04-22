using System;
using System.Threading.Tasks;
using Spartacus.Core;
using Xunit;

namespace Tests;

using static Spartacus.Core.Parsers;
using static Spartacus.Core.Composites.Ops;

public class SequenceParserTests
{
    [Fact(DisplayName = "The sequence parser should return the parsed sequence if all the sub parsers are successful")]
    public async Task ShouldParseSuccessfullyIfTheInputMatchesTheGivenSequence()
    {
        var parser = Digit.And(Letter);
        var inputText = "1a";
        await parser.ParseAsync(inputText).ShouldBeSuccessful(inputText);
    }

    [Fact(DisplayName = "The sequence parser should return nothing if one of its parsers in the sequence fails")]
    public async Task ShouldReturnNothingIfOneSubParserFails()
    {
        var parser = Digit.And(Letter);

        var inputText = "2-";
        await parser.ParseAsync(inputText).ShouldFail();
    }

    [Fact(DisplayName = "The sequence parser should allow for multiple repeat instances (using syntactic sugar)")]
    public async Task ShouldAllowRepeats()
    {
        var parser = Digit.Repeat(4);
        await parser.ParseAsync("1234").ShouldBeSuccessful("1234");
    }
}