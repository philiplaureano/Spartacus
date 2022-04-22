using Optional;

namespace Spartacus.Core.Actions;

internal class TriggerActionOnException<TException> : IParser
    where TException : Exception
{
    private readonly IParser _parser;
    private readonly Action<IParser, ReadOnlyMemory<char>, TException> _exceptionHandler;

    public TriggerActionOnException(IParser parser, Action<IParser, ReadOnlyMemory<char>, TException> exceptionHandler)
    {
        _parser = parser;
        _exceptionHandler = exceptionHandler;
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        var result = Option.None<ReadOnlyMemory<char>>();
        try
        {
            result = await _parser.ParseAsync(input);
        }
        catch (TException e)
        {
            _exceptionHandler(_parser, input, e);
        }

        return result;
    }
}