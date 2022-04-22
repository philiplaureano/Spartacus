namespace Spartacus.Core.Actions;

public static class SemanticActionExtensions
{
    public static IParser WithActionTriggeredOnSuccess(this IParser parser,
        Action<IParser, ReadOnlyMemory<char>, ReadOnlyMemory<char>> actionHandler)
    {
        return new TriggerActionOnSuccess(parser, actionHandler);
    }

    public static IParser WithActionTriggeredOnFailure(this IParser parser,
        Action<IParser, ReadOnlyMemory<char>> actionHandler)
    {
        return new TriggerActionOnFailure(parser, actionHandler);
    }

    public static IParser WithActionTriggeredOnException<TException>(this IParser parser,
        Action<IParser, ReadOnlyMemory<char>, Exception> handler)
        where TException : Exception
    {
        return new TriggerActionOnException<TException>(parser, handler);
    }
}