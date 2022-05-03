using Optional;
using Spartacus.Core.Adapters;
using Spartacus.Core.Primitives;

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

    public static IParser AsParser(this string phrase)
    {
        return new StringParser(phrase);
    }

    public static IParser ToParser(this Func<char, bool> characterChecker)
    {
        return new CustomerCharParser(characterChecker);
    }
    public static IParser ToParser(this Func<string, Task<Option<string>>> lambda)
    {
        return new DelegateParserAdapter(lambda);
    }
}