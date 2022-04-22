namespace Spartacus.Core.Primitives;

public class CharParser : SingleCharParser
{
    private readonly char _targetChar;

    public CharParser(char targetChar)
    {
        _targetChar = targetChar;
    }

    protected override bool IsMatch(char ch)
    {
        return ch == _targetChar;
    }
}