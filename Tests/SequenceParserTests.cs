using System.Threading.Tasks;
using Spartacus.Core;
using Xunit;

namespace Tests;

public class SequenceParserTests
{
    [Fact(DisplayName = "The sequence parser should return the parsed sequence if all the sub parsers are successful")]
    public async Task ShouldParseSuccessfullyIfTheInputMatchesTheGivenSequence()
    {
        var digitParser = new DigitParser();
        var letterParser = new LetterParser();
        var parser = new SequenceParser(digitParser, letterParser);

        var inputText = "1a";
        await parser.ParseAsync(inputText).ShouldBeSuccessful(inputText);
    }

    [Fact(DisplayName = "The sequence parser should return nothing if one of its parsers in the sequence fails")]
    public async Task ShouldReturnNothingIfOneSubParserFails()
    {
        var digitParser = new DigitParser();
        var letterParser = new LetterParser();
        var parser = new SequenceParser(digitParser, letterParser);

        var inputText = "2-";
        await parser.ParseAsync(inputText).ShouldNotBeSuccessful();
    }
}