namespace Dev.Frostbane;

public interface StringFormatInterface
{
    StringFormatInterface
    SetMatchStart(string open);

    string
    GetMatchStart();

    StringFormatInterface
    SetMatchEnd(string close);

    string
    GetMatchEnd();

    StringFormatInterface
    SetEscapeStart(string open);

    string
    GetEscapeStart();

    StringFormatInterface
    SetEscapeEnd(string close);

    string
    GetEscapeEnd();

    string
    RemoveIgnoreTags(string template);

    string
    Format(string template,
           Dictionary<string, Object> map);
}