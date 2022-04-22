using System;
using System.Threading.Tasks;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class ControlCharParserTest
{
    [Fact(DisplayName = "The control char parser should be able to parse a single control char")]
    public async Task ShouldParseControlChar()
    {
        await SingleCharParserTester.ShouldBeAbleToParse<ControlCharParser>(char.IsControl);
    }

    [Fact(DisplayName = "The control char parser should return none if the parse does not match")]
    public async Task ShouldReturnNoneOnInvalidControlChar()
    {
        await SingleCharParserTester.ShouldReturnNothingIfParseFails<ControlCharParser>(char.IsControl);
    }
}