using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spartacus.Core;
using Xunit;

namespace Tests;

public static class SingleCharParserTester
{
    public static async Task ShouldBeAbleToParse<TParser>(Func<char, bool> checkCharacter)
        where TParser : IParser,
        new()
    {
        var parser = new TParser();
        var validChars = GetApplicableChars(checkCharacter).ToArray();

        // If this statement fails, that means that there are no valid characters here
        Assert.NotEmpty(validChars);

        // Pick one random char so that the test doesn't run forever
        var selectedChar = PickRandomChar(validChars);

        await parser.ParseAsync(selectedChar.ToString()).ShouldBeSuccessful(selectedChar.ToString());
    }

    public static async Task ShouldReturnNothingIfParseFails<TParser>(Func<char, bool> checkCharacter)
        where TParser : IParser,
        new()
    {
        var parser = new TParser();
        var invalidChars = GetNonApplicableChars(checkCharacter).ToArray();
        
        Assert.NotEmpty(invalidChars);

        var selectedChar = PickRandomChar(invalidChars);
        await parser.ParseAsync(selectedChar.ToString()).ShouldNotBeSuccessful();
    }

    public static IEnumerable<char> GetNonApplicableChars(Func<char, bool> checkCharacter)
    {
        var nonControlChars = Enumerable.Range(0x0, 0xFFFF)
            .Select(Convert.ToChar)
            .Where(ch => !checkCharacter(ch));

        return nonControlChars;
    }

    public static IEnumerable<char> GetApplicableChars(Func<char, bool> checkCharacter)
    {
        var controlChars = Enumerable.Range(0x0, 0xFFFF)
            .Select(Convert.ToChar)
            .Where(checkCharacter);

        return controlChars;
    }

    private static char PickRandomChar(IEnumerable<char> currentChars)
    {
        var random = new Random();
        var chars = currentChars.ToArray();
        var validIndices = new Range(0, chars.Length);
        var selectedChar = chars[random.Next(validIndices.Start.Value, validIndices.End.Value)];
        return selectedChar;
    }
}