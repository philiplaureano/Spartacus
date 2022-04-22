using System;
using System.Threading.Tasks;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class LowerCharParserTests
{
    [Fact(DisplayName = "The lower char parser should be able to parse a single lowercase character")]
    public async Task ShouldParseLowerChar()
    {
        await SingleCharParserTester.ShouldBeAbleToParse<LowerCharParser>(char.IsLower);
    }

    [Fact(DisplayName = "The lower char parser should return none if the parse does not match")]
    public async Task ShouldReturnNoneOnInvalidChar()
    {
        await SingleCharParserTester.ShouldReturnNothingIfParseFails<LowerCharParser>(char.IsLower);
    }
}