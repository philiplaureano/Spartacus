namespace Spartacus.Core.Primitives;

public class LetterParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsLetter(ch);
    }
}