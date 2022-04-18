namespace Spartacus.Core;

public static class NonTerminalParserExtensions
{
    public static AlternativeParser Or(this IParser parser, params IParser[] parsers)
    {
        if (parsers == null) throw new ArgumentNullException(nameof(parsers));

        var otherParsers = new List<IParser> { parser };
        otherParsers.AddRange(parsers);

        return new AlternativeParser(otherParsers.ToArray());
    }

    public static SequenceParser And(this IParser parser, params IParser[] parsers)
    {
        if (parsers == null) throw new ArgumentNullException(nameof(parsers));

        var combinedParsers = new List<IParser> { parser };
        combinedParsers.AddRange(parsers);
        return new SequenceParser(combinedParsers.ToArray());
    }

    public static KleeneStarParser ZeroOrMoreInstances(this IParser parser)
    {
        return new KleeneStarParser(parser);
    }
}