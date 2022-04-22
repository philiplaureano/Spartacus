namespace Spartacus.Core.Primitives;

public class SeparatorParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsSeparator(ch);
    }
}