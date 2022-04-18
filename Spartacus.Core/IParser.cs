using Optional;

namespace Spartacus.Core;

public interface IParser
{
    ValueTask<Option<ReadOnlyMemory<char>>> ParseAsync(ReadOnlyMemory<char> input);
}