using System.Threading.Tasks;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class SeparatorParserTests
{
    [Fact(DisplayName = "The separator parser should be able to parse a separator character")]  
    public async Task ShouldParseValidInput()
    {
        await SingleCharParserTester.ShouldBeAbleToParse<SeparatorParser>(char.IsSeparator);
    }
    [Fact(DisplayName = "The separator parser should return none if the parse does not match")]
    public async Task ShouldReturnNoneOnInvalidChar()
    {
        await SingleCharParserTester.ShouldReturnNothingIfParseFails<SeparatorParser>(char.IsSeparator);
    }
}