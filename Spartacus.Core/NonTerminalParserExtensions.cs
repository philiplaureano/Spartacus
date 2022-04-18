namespace Spartacus.Core;

public static class NonTerminalParserExtensions
{
    public static AlternativeParser Or(this IParser parser, IParser otherParser)
    {
        return new AlternativeParser(parser, otherParser);
    }
    
    public static KleeneStarParser ZeroOrMoreInstances(this IParser parser)
    {
        return new KleeneStarParser(parser);
    }
}