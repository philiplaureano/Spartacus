using Optional;

namespace Spartacus.Core;

internal class TriggerActionOnException<TException> : IParser
    where TException : Exception
{
    private readonly IParser _parser;
    private readonly Action<IParser, ReadOnlyMemory<char>, Exception> _exceptionHandler;

    public TriggerActionOnException(IParser parser, Action<IParser, ReadOnlyMemory<char>, Exception> exceptionHandler)
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