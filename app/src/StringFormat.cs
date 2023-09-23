using System.Text.RegularExpressions;

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
        return this;
    }

    /// <inheritdoc />
    public StringFormatInterface
    SetMatchEnd(string close)
    {
        return this;
    }

    /// <inheritdoc />
    public StringFormatInterface
    SetEscapeStart(string open)
    {
        return this;
    }

    /// <inheritdoc />
    public StringFormatInterface
    SetEscapeEnd(string close)
    {
        return this;
    }

    private String
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
        if (string.IsNullOrEmpty(template))
        {
            return template;
        }

        string result = template;

        foreach(KeyValuePair<string, Object> kvp in map)
        {
            string exp = 
                "(?<!" + escapeStart + ")" +
                matchStart + " *" + kvp.Key + " *" + matchEnd +
                "(?!" + escapeEnd + ")";

#pragma warning disable CS8604
            result = Regex.Replace(result, exp, kvp.Value.ToString());
#pragma warning restore CS8604
        }

        result = RemoveIgnoreTags(result);

        return result;
    }
}
