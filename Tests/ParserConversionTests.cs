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
        Assert.Equal(expectedReturnValue,result.ValueOrFailure().ToString());
        Assert.True(wasParserCalled.WaitOne(TimeSpan.FromSeconds(2)));
    }
}