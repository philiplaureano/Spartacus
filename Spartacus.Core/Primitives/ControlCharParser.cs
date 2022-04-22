namespace Spartacus.Core.Primitives;

public class ControlCharParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsControl(ch);
    }
}