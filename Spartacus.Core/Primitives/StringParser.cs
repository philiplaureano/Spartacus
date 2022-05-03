using Optional;

#pragma warning disable CS1998

namespace Spartacus.Core.Primitives;

public class StringParser : IParser
{
    private readonly string _phrase;

    public StringParser(string phrase)
    {
        _phrase = phrase ?? throw new ArgumentNullException(nameof(phrase));
    }

    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        var phraseLength = _phrase.Length;

        // Skip the comparison if the string length doesn't match
        if (phraseLength > input.Length)
            return Option.None<ReadOnlyMemory<char>>();

        var maxLength = input.Length <= phraseLength ? input.Length : phraseLength;
        var slice = input.Slice(0, maxLength);
        var inputText = slice.ToString();

        var result = string.CompareOrdinal(_phrase, inputText) == 0
            ? Option.Some(inputText.AsMemory())
            : Option.None<ReadOnlyMemory<char>>();

        return result;
    }
}