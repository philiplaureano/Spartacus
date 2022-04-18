using Optional;

namespace Spartacus.Core;

public abstract class SingleCharParser : IParser
{
    public ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input)
    {
        if (input.Length == 0)
            return ValueTask.FromResult(Option.None<ReadOnlyMemory<char>>());

        var currentChar = input.ToArray()[0];
        return ValueTask.FromResult(!IsMatch(currentChar)
            ? Option.None<ReadOnlyMemory<char>>()
            : Option.Some(new ReadOnlyMemory<char>(new char[] { currentChar })));
    }

    protected abstract bool IsMatch(char ch);
}