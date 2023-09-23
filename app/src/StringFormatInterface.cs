namespace Dev.Frostbane;

public interface StringFormatInterface
{
    StringFormatInterface
    SetMatchStart(string open);

    StringFormatInterface
    SetMatchEnd(string close);

    StringFormatInterface
    SetEscapeStart(string open);

    StringFormatInterface
    SetEscapeEnd(string close);

    string
    Format(string template,
           Dictionary<string, Object> map);
}
