namespace Spartacus.Core.Actions;

public static class SemanticActionExtensions
{
    public static IParser WithActionTriggeredOnSuccess(this IParser parser,
        Action<string> actionHandler)
    {
        void HandlerAdapter(IParser currentParser, string input, string parsedInput)
        {
            actionHandler(parsedInput);
        }

        return parser.WithActionTriggeredOnSuccess(HandlerAdapter);
    }

    public static IParser WithActionTriggeredOnSuccess(this IParser parser,
        Action<IParser, string, string> actionHandler)
    {
        void HandlerAdapter(IParser currentParser, ReadOnlyMemory<char> input, ReadOnlyMemory<char> parsedInput)
        {
            actionHandler(currentParser, input.ToString(), parsedInput.ToString());
        }

        return parser.WithActionTriggeredOnSuccess(HandlerAdapter);
    }

    public static IParser WithActionTriggeredOnSuccess(this IParser parser,
        Action<IParser, ReadOnlyMemory<char>, ReadOnlyMemory<char>> actionHandler)
    {
        return new TriggerActionOnSuccess(parser, actionHandler);
    }

    public static IParser WithActionTriggeredOnFailure(this IParser parser,
        Action<IParser, string> actionHandler)
    {
        void HandlerAdapter(IParser currentParser, ReadOnlyMemory<char> input)
        {
            actionHandler(currentParser, input.ToString());
        }

        return parser.WithActionTriggeredOnFailure(HandlerAdapter);
    }

    public static IParser WithActionTriggeredOnFailure(this IParser parser,
        Action<IParser, ReadOnlyMemory<char>> actionHandler)
    {
        return new TriggerActionOnFailure(parser, actionHandler);
    }

    public static IParser WithActionTriggeredOnException<TException>(this IParser parser,
        Action<IParser, string, TException> handler)
        where TException : Exception
    {
        void HandlerAdapter(IParser currentParser, ReadOnlyMemory<char> input, TException exceptionThrown)
        {
            handler(currentParser, input.ToString(), exceptionThrown);
        }

        return parser.WithActionTriggeredOnException<TException>(HandlerAdapter);
    }

    public static IParser WithActionTriggeredOnException<TException>(this IParser parser,
        Action<IParser, ReadOnlyMemory<char>, TException> handler)
        where TException : Exception
    {
        return new TriggerActionOnException<TException>(parser, handler);
    }
}