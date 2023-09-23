using System.Text.RegularExpressions;

namespace Dev.Frostbane;

public class StringFormat
{
    private static
    StringFormat? instance = null;

    private string
    matchStart = "{{";

    private string
    matchEnd = "}}";

    private string
    escapeStart = "//";

    private string
    escapeEnd = "//";

    private StringFormat()
    {
    }

    public StringFormat
    SetMatchStart(string open)
    {
        return this;
    }

    public StringFormat
    SetMatchEnd(string close)
    {
        return this;
    }

    public StringFormat
    SetEscapeStart(string open)
    {
        return this;
    }

    public StringFormat
    SetEscapeEnd(string close)
    {
        return this;
    }

    public static
    StringFormat GetInstance()
    {
        instance ??= new StringFormat();

        return instance;
    }

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
            string exp = matchStart + " *" + kvp.Key + " *" + matchEnd;

#pragma warning disable CS8604
            result = Regex.Replace(result, exp, kvp.Value.ToString());
#pragma warning restore CS8604
        }

        return result;
    }
}
