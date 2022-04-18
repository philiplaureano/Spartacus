
using Optional;

namespace Spartacus.Core;

public abstract class SingleCharParser : IParser
{
    public async ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        if (input.Length == 0)
            return Option.None<ReadOnlyMemory<char>>();

        var currentChar = input.ToArray()[0];
        if (!IsMatch(currentChar))
            return Option.None<ReadOnlyMemory<char>>();;

        return Option.Some(new ReadOnlyMemory<char>(new char[] { currentChar }));
    }

    protected abstract bool IsMatch(char ch);
}