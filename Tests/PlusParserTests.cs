using System;
using System.Threading.Tasks;
using Spartacus.Core;
using Spartacus.Core.Composites;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class PlusParserTests
{
    [Fact(DisplayName = "The plus parser should match one or more instances")]
    public async Task ShouldMatchOneOrMoreInstances()
    {
        var letterParser = new LetterParser();
        var parser = letterParser.OneOrMoreInstances();

        // Parse multiple letters
        await parser.ParseAsync("abcd").ShouldBeSuccessful("abcd");
        
        // Parse single letters
        await parser.ParseAsync("a").ShouldBeSuccessful();
    }

    [Fact(DisplayName = "The plus parser should fail if no instances are found")]
    public async Task ShouldFailParsingIfNoInstancesAreFound()
    {
        var letterParser = new LetterParser();
        var parser = letterParser.OneOrMoreInstances();

        await parser.ParseAsync("123").ShouldNotBeSuccessful();
    }
}