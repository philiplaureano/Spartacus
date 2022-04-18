using System.Text;
using Optional;
using Optional.Unsafe;

namespace Spartacus.Core;

public class SequenceParser : IParser
{
    private readonly IEnumerable<IParser> _parsers;

    public SequenceParser(params IParser[] parsers)
    {
        _parsers = parsers;
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        if (!_parsers.Any())
            return Option.None<ReadOnlyMemory<char>>();

        var nextInput = input;
        var parsedValues = new StringBuilder();
        var numberOfCharsRead = 0;
        foreach (var parser in _parsers)
        {
            var result = await parser.ParseAsync(nextInput);
            if (!result.HasValue)
                return Option.None<ReadOnlyMemory<char>>();

            if (!result.HasValue) 
                continue;
            
            var currentValue = result.ValueOrFailure();
            parsedValues.Append(currentValue);

            numberOfCharsRead += currentValue.Length;
            nextInput = input[numberOfCharsRead..];
        }
        
        var combinedValues = parsedValues.ToString();
        return Option.Some(combinedValues.AsMemory());
    }
}