namespace Spartacus.Core.Primitives;

public class LowerCharParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsLower(ch);
    }
}