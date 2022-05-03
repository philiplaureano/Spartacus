using Optional;
using Optional.Unsafe;

namespace Spartacus.Core.Adapters;

internal class DelegateParserAdapter : IParser
{
    private Func<string, Task<Option<string>>> _lambda;

    public DelegateParserAdapter(Func<string, Task<Option<string>>> lambda)
    {
        _lambda = lambda;
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        var result = await _lambda(input.ToString());

        return !result.HasValue ? Option.None<ReadOnlyMemory<char>>() : Option.Some(result.ValueOrFailure().AsMemory());
    }
}