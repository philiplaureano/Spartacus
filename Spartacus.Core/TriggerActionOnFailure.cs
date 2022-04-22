using Optional;

namespace Spartacus.Core;

internal class TriggerActionOnFailure : IParser
{
    private readonly IParser _parser;
    private readonly Action<IParser, ReadOnlyMemory<char>> _actionHandler;

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