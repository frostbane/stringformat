namespace Dev.Frostbane;

public interface StringFormatInterface
{
    StringFormatInterface SetMatchStart(string open);

    string GetMatchStart();

    StringFormatInterface SetMatchEnd(string close);

    string GetMatchEnd();

    StringFormatInterface SetEscapeStart(string open);

    string GetEscapeStart();

    StringFormatInterface SetEscapeEnd(string close);

    string GetEscapeEnd();

    string GetValue(Object obj);

    string Format(string template, Object map);
}
