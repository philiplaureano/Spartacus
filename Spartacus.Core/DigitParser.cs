namespace Spartacus.Core;

public class DigitParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsDigit(ch);
    }
}