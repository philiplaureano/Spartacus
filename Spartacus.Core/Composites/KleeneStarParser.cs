using Optional;
using Optional.Unsafe;

namespace Spartacus.Core.Composites;

public class KleeneStarParser : IParser
{
    private readonly IParser _parser;

    public KleeneStarParser(IParser parser)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        // Kleene stars match empty strings
        if (input.Length == 0)
            return Option.Some(ReadOnlyMemory<char>.Empty);

        var matches = new List<ReadOnlyMemory<char>>();
        var currentInput = input;
        var numberOfCharsRead = 0;
        while (currentInput.Length > 0)
        {
            var currentResult = await _parser.ParseAsync(currentInput);
            if (!currentResult.HasValue)
                break;

            var value = currentResult.ValueOrFailure();
            numberOfCharsRead += value.Length;

            matches.Add(value);

            // Read the remaining characters
            currentInput = input[numberOfCharsRead..];
        }

        var combinedText = matches.SelectMany(m => m.ToString()).ToArray();
        return Option.Some(new string(combinedText).AsMemory());
    }
}