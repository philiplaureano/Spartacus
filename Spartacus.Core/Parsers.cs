using Spartacus.Core.Primitives;

namespace Spartacus.Core;

public static class Parsers
{
    public static IParser Ascii => new AsciiParser();
    public static IParser ControlChar => new ControlCharParser();
    public static IParser Digit => new DigitParser();
    public static IParser Letter => new LetterParser();
    public static IParser LowerChar => new LowerCharParser();
    public static IParser Punctuation => new PunctuationParser();
    public static IParser Separator => new SeparatorParser();
    public static IParser UpperChar => new UpperCharParser();
    public static IParser Whitespace => new WhitespaceParser();

    public static IParser Char(char ch)
    {
        return new CharParser(ch);
    }

    public static IParser String(string text)
    {
        return new StringParser(text);
    }
}