using Spartacus.Core.Primitives;

namespace Spartacus.Core.Composites;

public static class StringParserConversionExtensions
{
    public static IParser[] ToParserArray(this IEnumerable<string> values)
    {
        return values.Select(t => new StringParser(t)).Cast<IParser>().ToArray();
    }
}