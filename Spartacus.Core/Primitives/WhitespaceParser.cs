namespace Spartacus.Core.Primitives;

public class WhitespaceParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsWhiteSpace(ch);
    }
}