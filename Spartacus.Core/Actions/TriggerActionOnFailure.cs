using Optional;

namespace Spartacus.Core.Actions;

internal class TriggerActionOnFailure : IParser
{
    private readonly Action<IParser, ReadOnlyMemory<char>> _actionHandler;
    private readonly IParser _parser;

    public TriggerActionOnFailure(IParser parser, Action<IParser, ReadOnlyMemory<char>> actionHandler)
    {
        _parser = parser;
        _actionHandler = actionHandler;
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        var result = await _parser.ParseAsync(input);
        if (!result.HasValue)
            _actionHandler(_parser, input);

        return result;
    }
}