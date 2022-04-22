using Optional;

namespace Spartacus.Core.Composites;

public class AlternativeParser : IParser
{
    private readonly IEnumerable<IParser> _parsers;

    public AlternativeParser(params IParser[] parsers)
    {
        _parsers = parsers;
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        if (!_parsers.Any())
            return Option.None<ReadOnlyMemory<char>>();

        foreach (var parser in _parsers)
        {
            // Only one rule needs to match in order to be successful
            var result = await parser.ParseAsync(input);
            if (result.HasValue)
                return result;
        }
        
        return Option.None<ReadOnlyMemory<char>>();
    }
}