namespace Spartacus.Core.Composites;

public static class Ops
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

    public static PlusParser OneOrMoreInstances(this IParser parser)
    {
        return new PlusParser(parser);
    }

    public static SequenceParser Repeat(this IParser parser, int numberOfTimes)
    {
        // Use the same parser in a sequence for N number of times
        var parsers = Enumerable.Range(0, numberOfTimes)
            .Select(_ => parser).ToArray();

        return new SequenceParser(parsers);
    }
}