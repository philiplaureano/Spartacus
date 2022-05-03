namespace Spartacus.Core.Primitives;

internal class CustomerCharParser : SingleCharParser
{
    private readonly Func<char, bool> _implementation;

    public CustomerCharParser(Func<char, bool> implementation)
    {
        _implementation = implementation;
    }

    protected override bool IsMatch(char ch)
    {
        return _implementation(ch);
    }
}