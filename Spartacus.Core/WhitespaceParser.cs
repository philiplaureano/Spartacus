namespace Spartacus.Core;

public class WhitespaceParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsWhiteSpace(ch);
    }
}