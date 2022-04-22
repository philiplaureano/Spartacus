namespace Spartacus.Core.Primitives;

public class PunctuationParser : SingleCharParser
{
    protected override bool IsMatch(char ch)
    {
        return char.IsPunctuation(ch);
    }
}