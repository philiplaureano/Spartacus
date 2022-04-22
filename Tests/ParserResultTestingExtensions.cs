using System;
using System.Threading.Tasks;
using Optional;
using Optional.Unsafe;
using Spartacus.Core;
using Xunit;

namespace Tests;

public static class ParserResultTestingExtensions
{
    public static async ValueTask ShouldBeSuccessful(this ValueTask<Option<ReadOnlyMemory<char>>> parseTask,
        string expectedText)
    {
        var result = await ShouldBeSuccessful(parseTask);

        var actualText = result.ValueOrFailure().ToString();
        Assert.Equal(expectedText, actualText);
    }

    public static async Task<Option<ReadOnlyMemory<char>>> ShouldBeSuccessful(
        this ValueTask<Option<ReadOnlyMemory<char>>> parseTask)
    {
        var result = await parseTask;
        Assert.True(result.HasValue);
        return result;
    }

    public static async ValueTask ShouldFail(this ValueTask<Option<ReadOnlyMemory<char>>> parseTask)
    {
        var result = await parseTask;
        Assert.False(result.HasValue);
    }

    public static async ValueTask ShouldReturnNothingOnEmptyString(this IParser parser)
    {
        await parser.ParseAsync(string.Empty).ShouldFail();
    }
}