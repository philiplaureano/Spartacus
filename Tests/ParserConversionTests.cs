using System;
using System.Threading;
using System.Threading.Tasks;
using Optional;
using Optional.Unsafe;
using Spartacus.Core;
using Xunit;

namespace Tests;

public class ParserConversionTests
{
    [Fact(DisplayName = "We should be able to convert a lambda into a working parser instance")]
    public async Task ShouldBeAbleToConvertLambdaIntoParser()
    {
        var wasParserCalled = new ManualResetEvent(false);
        var expectedReturnValue = Guid.NewGuid().ToString();

        Task<Option<string>> ParseAsync(string inputText)
        {
            wasParserCalled?.Set();

            return Task.FromResult(Option.Some(expectedReturnValue))!;
        }

        var lambda = ParseAsync;
        IParser parser = lambda.ToParser();

        var result = await parser.ParseAsync("abcd");

        Assert.True(result.HasValue);
        Assert.Equal(expectedReturnValue, result.ValueOrFailure().ToString());
        Assert.True(wasParserCalled.WaitOne(TimeSpan.FromSeconds(2)));
    }

    [Fact(DisplayName = "We should be able to convert a literal string into a string parser")]
    public async Task ShouldBeAbleToConvertLiteralStringIntoStringParser()
    {
        var phrase = Guid.NewGuid().ToString();

        IParser parser = phrase.AsParser();
        await parser.ParseAsync(phrase).ShouldBeSuccessful(phrase);
    }

    [Fact(DisplayName = "We should be able to convert a Func<char, bool> into a custom parser")]
    public async Task ShouldBeAbleToConvertFuncToCustomCharParser()
    {
        var testCharacter = 'x';
        var wasParserCalled = new ManualResetEvent(false);

        bool IsMatch(char ch)
        {
            wasParserCalled?.Set();
            return ch == testCharacter;
        }

        Func<char, bool> customCharParser = IsMatch;
        IParser parser = customCharParser.ToParser();

        var result = await parser.ParseAsync(testCharacter.ToString());
        Assert.True(result.HasValue);
        Assert.True(result.ValueOrFailure().ToString() == testCharacter.ToString());
    }
}