using Optional;
using Optional.Unsafe;

namespace Spartacus.Core.Actions;

internal class TriggerActionOnSuccess : IParser
{
    private readonly IParser _parser;
    private readonly Action<IParser, ReadOnlyMemory<char>, ReadOnlyMemory<char>> _actionHandler;

    public TriggerActionOnSuccess(IParser parser,
        Action<IParser, ReadOnlyMemory<char>, ReadOnlyMemory<char>> actionHandler)
    {
        _parser = parser;
        _actionHandler = actionHandler;
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        var result = await _parser.ParseAsync(input);
        if (result.HasValue)
            _actionHandler(_parser, input, result.ValueOrFailure());

        return result;
    }
}