using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Spartacus.Core;
using Spartacus.Core.Actions;
using Spartacus.Core.Primitives;
using Xunit;

namespace Tests;

public class SemanticActionTests
{
    [Fact(DisplayName = "We should be able to trigger an action whenever a parse is successful")]
    public async Task ShouldTriggerActionOnSuccessfulParse()
    {
        var wasHandlerCalled = new ManualResetEvent(false);
        var parser = new DigitParser();

        void Handler(IParser currentParser, string input, string parsedValue)
        {
            wasHandlerCalled.Set();
        }

        var parserWithActions = parser.WithActionTriggeredOnSuccess(Handler);

        await parserWithActions.ParseAsync("1").ShouldBeSuccessful();
        Assert.True(wasHandlerCalled.WaitOne(TimeSpan.FromSeconds(1)));
    }

    [Fact(DisplayName = "We should be able to trigger an action if a parse fails")]
    public async Task ShouldTriggerActionOnFailedParseOperation()
    {
        var wasHandlerCalled = new ManualResetEvent(false);
        var parser = new DigitParser();

        void Handler(IParser currentParser, string input)
        {
            wasHandlerCalled.Set();
        }

        var parserWithActions = parser.WithActionTriggeredOnFailure(Handler);

        await parserWithActions.ParseAsync("abcd").ShouldFail();
        Assert.True(wasHandlerCalled.WaitOne(TimeSpan.FromSeconds(1)));
    }

    [Fact(DisplayName = "We should be able to trigger an action if an exception is thrown")]
    public async Task ShouldTriggerActionOnThrownException()
    {
        var wasHandlerCalled = new ManualResetEvent(false);
        var parser = A.Fake<IParser>();
        A.CallTo(() => parser.ParseAsync(A<ReadOnlyMemory<char>>._))
            .Throws<InvalidOperationException>();

        void Handler(IParser currentParser, string input, Exception exceptionThrown)
        {
            wasHandlerCalled.Set();
        }

        var parserWithActions = parser.WithActionTriggeredOnException<InvalidOperationException>(Handler);
        await parserWithActions.ParseAsync("abcd").ShouldFail();

        Assert.True(wasHandlerCalled.WaitOne(TimeSpan.FromSeconds(1)));
    }
}