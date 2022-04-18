using System.Threading.Tasks;
using Spartacus.Core;
using Xunit;

namespace Tests;

public class AlternativeParserTests
{
    [Fact(DisplayName = "The alternative parser should return a value if one of the sub parsers is successful")]
    public async Task ShouldParseSuccessfullyIfOneOfTheSubParsersSucceeds()
    {
        var fizz = new StringParser("fizz");
        var buzz = new StringParser("buzz");

        var parser = fizz.Or(buzz);
        await parser.ParseAsync("buzz").ShouldBeSuccessful("buzz");
        await parser.ParseAsync("fizz").ShouldBeSuccessful("fizz");
    }

    [Fact(DisplayName = "The alternative parser should return nothing if none of its sub parsers are successful")]
    public async Task ShouldReturnNothingIfAllSubParsersFailToMatch()
    {
        var fizz = new StringParser("fizz");
        var buzz = new StringParser("buzz");

        var parser = fizz.Or(buzz);
        await parser.ParseAsync("beep").ShouldNotBeSuccessful();
    }
}