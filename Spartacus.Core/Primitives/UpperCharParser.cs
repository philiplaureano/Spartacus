namespace Spartacus.Core.Primitives;

public class UpperCharParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsUpper(ch);
    }
}