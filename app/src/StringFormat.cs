using System.Text.RegularExpressions;
using Dev.Frostbane.Strategies;

namespace Dev.Frostbane;

public class StringFormat : StringFormatInterface
{
    private string
    matchStart = "{{";

    private string
    matchEnd = "}}";

    private string
    escapeStart = "//";

    private string
    escapeEnd = "//";

    public StringFormat()
    {
    }

    /// <inheritdoc/>
    public StringFormatInterface
    SetMatchStart(string open)
    {
        matchStart = open;

        return this;
    }

    /// <inheritdoc/>
    public string
    GetMatchStart()
    {
        return EscapeSpecial(matchStart);
    }

    /// <inheritdoc/>
    public StringFormatInterface
    SetMatchEnd(string close)
    {
        matchEnd = close;

        return this;
    }

    /// <inheritdoc/>
    public string
    GetMatchEnd()
    {
        return EscapeSpecial(matchEnd);
    }

    /// <inheritdoc/>
    public StringFormatInterface
    SetEscapeStart(string open)
    {
        escapeStart = open;

        return this;
    }

    /// <inheritdoc/>
    public string
    GetEscapeStart()
    {
        return EscapeSpecial(escapeStart);
    }

    /// <inheritdoc/>
    public StringFormatInterface
    SetEscapeEnd(string close)
    {
        escapeEnd = close;

        return this;
    }

    /// <inheritdoc/>
    public string
    GetEscapeEnd()
    {
        return EscapeSpecial(escapeEnd);
    }

    /// <inheritdoc/>
    public string
    GetValue(Object obj)
    {
#pragma warning disable CS8603
        return obj == null ?
               "Null" :
               obj.ToString();
#pragma warning restore CS8603
    }

    private string
    EscapeSpecial(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        string[] specialChars = new string[] { "\\", ".", "$", "^", "[", "(", "|", ")", "]", "*", "+", "?" };
        // { ".", "$", "^", "{", "[", "(", "|", ")", "*", "+", "?", "\\" };
        string result = text;

        foreach(string c in specialChars)
        {
            result = result.Replace(c, "\\" + c);
        }

        return result;
    }

    /// <inheritdoc/>
    public string
    RemoveIgnoreTags(string template)
    {
        if (string.IsNullOrEmpty(template))
        {
            return template;
        }

        string exp =
            GetEscapeStart() +
            "(" + GetMatchStart() + " *[^(" + GetMatchEnd() + ") ]* *" + GetMatchEnd() + ")" +
            GetEscapeEnd();

        string result = template;

#pragma warning disable CS8604
        result = Regex.Replace(result, exp, m => m.Groups[1].Value);
#pragma warning restore CS8604

        return result;
    }

    /// <inheritdoc />
    public string
    Format(string template,
           Dictionary<string, Object> map)
    {
        return MapStrategy.GetInstance().Format(this, template, map);
    }
}
