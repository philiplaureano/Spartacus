using System.Threading.Tasks;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class UpperCharParserTest
{
    [Fact(DisplayName = "The upper char parser should be able to parse a single uppercase character")]
    public async Task ShouldParseUpperChar()
    {
        await SingleCharParserTester.ShouldBeAbleToParse<UpperCharParser>(char.IsUpper);
    }

    [Fact(DisplayName = "The upper char parser should return none if the parse does not match")]
    public async Task ShouldReturnNoneOnInvalidChar()
    {
        await SingleCharParserTester.ShouldReturnNothingIfParseFails<UpperCharParser>(char.IsUpper);
    }
}