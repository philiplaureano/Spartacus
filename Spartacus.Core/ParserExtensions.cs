using Optional;
using Spartacus.Core.Adapters;

namespace Spartacus.Core;

public static class ParserExtensions
{
    public static async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(this IParser parser, string inputText)
    {
        if (parser == null) throw new ArgumentNullException(nameof(parser));
        if (inputText == null) throw new ArgumentNullException(nameof(inputText));

        var memory = inputText.AsMemory();
        return await parser.ParseAsync(memory);
    }

    public static IParser ToParser(this Func<string, Task<Option<string>>> lambda)
    {
        return new DelegateParserAdapter(lambda);
    }
}