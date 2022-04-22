﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Spartacus.Core;
using Xunit;

namespace Tests;

public class SemanticActionTests
{
    [Fact(DisplayName = "We should be able to trigger an action whenever a parse is successful")]
    public async Task ShouldTriggerActionOnSuccessfulParse()
    {
        var wasHandlerCalled = new ManualResetEvent(false);
        var parser = new DigitParser();

        void Handler(IParser currentParser, ReadOnlyMemory<char> input, ReadOnlyMemory<char> parsedValue)
        {
            wasHandlerCalled.Set();
        }
        
        var parserWithActions = parser.WithActionTriggeredOnSuccess(Handler);

        await parserWithActions.ParseAsync("1").ShouldBeSuccessful();
        wasHandlerCalled.WaitOne(TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = "We should be able to trigger an action if a parse fails")]
    public async Task ShouldTriggerActionOnFailedParseOperation()
    {
        var wasHandlerCalled = new ManualResetEvent(false);
        var parser = new DigitParser();

        void Handler(IParser currentParser, ReadOnlyMemory<char> input)
        {
            wasHandlerCalled.Set();
        }

        var parserWithActions = parser.WithActionTriggeredOnFailure(Handler);

        await parserWithActions.ParseAsync("abcd").ShouldNotBeSuccessful();
        wasHandlerCalled.WaitOne(TimeSpan.FromSeconds(1));
    }

    [Fact(DisplayName = "We should be able to trigger an action if an exception is thrown")]
    public async Task ShouldTriggerActionOnThrownException()
    {
        var wasHandlerCalled = new ManualResetEvent(false);
        var parser = A.Fake<IParser>();
        A.CallTo(() => parser.ParseAsync(A<ReadOnlyMemory<char>>._))
            .Throws<InvalidOperationException>();

        void Handler(IParser parser, ReadOnlyMemory<char> input, Exception exceptionThrown)
        {
            wasHandlerCalled.Set();
        }
        
        var parserWithActions = parser.WithActionTriggeredOnException<InvalidOperationException>(Handler);
        wasHandlerCalled.WaitOne(TimeSpan.FromSeconds(1));
    }
}