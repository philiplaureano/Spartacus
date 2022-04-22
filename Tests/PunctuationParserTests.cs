using System.Threading.Tasks;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class PunctuationParserTests
{
    [Fact(DisplayName = "The punctuation parser should be able to parse a punctuation character")]  
    public async Task ShouldParseValidInput()
    {
        await SingleCharParserTester.ShouldBeAbleToParse<PunctuationParser>(char.IsPunctuation);
    }
    [Fact(DisplayName = "The punctuation parser should return none if the parse does not match")]
    public async Task ShouldReturnNoneOnInvalidChar()
    {
        await SingleCharParserTester.ShouldReturnNothingIfParseFails<PunctuationParser>(char.IsPunctuation);
    }
}