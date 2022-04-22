namespace Spartacus.Core.Primitives;

public class AsciiParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsAscii(ch);
    }
}