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

    /// <inheritdoc />
    public StringFormatInterface
    SetMatchStart(string open)
    {
        matchStart = open;

        return this;
    }

    /// <inheritdoc />
    public string
    GetMatchStart()
    {
        return matchStart;
    }

    /// <inheritdoc />
    public StringFormatInterface
    SetMatchEnd(string close)
    {
        matchEnd = close;

        return this;
    }

    /// <inheritdoc />
    public string
    GetMatchEnd()
    {
        return matchEnd;
    }

    /// <inheritdoc />
    public StringFormatInterface
    SetEscapeStart(string open)
    {
        escapeStart = open;

        return this;
    }

    /// <inheritdoc />
    public string
    GetEscapeStart()
    {
        return escapeStart;
    }

    /// <inheritdoc />
    public StringFormatInterface
    SetEscapeEnd(string close)
    {
        escapeEnd = close;

        return this;
    }

    /// <inheritdoc />
    public string
    GetEscapeEnd()
    {
        return escapeEnd;
    }

    private string
    EscapeRegex(string text)
    {
        // . $ ^ { [ ( | ) * + ? \
        return text;
    }

    /// <inheritdoc />
    public string
    RemoveIgnoreTags(string template)
    {
        if (string.IsNullOrEmpty(template))
        {
            return template;
        }

        string result = template;

#pragma warning disable CS8604
        result = Regex.Replace(result, escapeStart + matchStart, matchStart);
        result = Regex.Replace(result, matchEnd + escapeEnd, matchEnd);
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
