namespace Spartacus.Core;

public class LetterParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsLetter(ch);
    }
}