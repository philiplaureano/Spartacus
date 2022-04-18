using Optional;
using Optional.Unsafe;

namespace Spartacus.Core;

public class PlusParser : IParser
{
    private readonly IParser _parser;

    public PlusParser(IParser parser)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        // Plus parsers do not match empty strings
        if (input.Length == 0)
            return Option.None<ReadOnlyMemory<char>>();
        
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
        
        // There must be at least one match for this parser to be successful
        if (matches.Count ==0)
            return Option.None<ReadOnlyMemory<char>>();
        
        // Return the matches that were found
        var combinedText = matches.SelectMany(m => m.ToString()).ToArray();
        return Option.Some(new string(combinedText).AsMemory());
    }
}