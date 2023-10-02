using System.Text.RegularExpressions;
using Dev.Frostbane.Strategies;

namespace Dev.Frostbane;

public class FormatString : FormatStringInterface
{
    private string matchStart = "{{";
    private string matchEnd = "}}";

    private string escapeStart = "//";
    private string escapeEnd = "//";

    /// <inheritdoc/>
    public FormatStringInterface
    SetMatchTokens(string open, string close)
    {
        matchStart = open;
        matchEnd   = close;

        return this;
    }

    /// <inheritdoc/>
    public string
    GetMatchStart()
    {
        return EscapeSpecial(matchStart);
    }

    /// <inheritdoc/>
    public string
    GetMatchEnd()
    {
        return EscapeSpecial(matchEnd);
    }

    /// <inheritdoc/>
    public FormatStringInterface
    SetEscapeTokens(string open, string close)
    {
        escapeStart = open;
        escapeEnd   = close;

        return this;
    }

    /// <inheritdoc/>
    public string
    GetEscapeStart()
    {
        return EscapeSpecial(escapeStart);
    }

    /// <inheritdoc/>
    public string
    GetEscapeEnd()
    {
        return EscapeSpecial(escapeEnd);
    }

    /// <inheritdoc/>
    public string
    GetValue(object obj)
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

    /// <inheritdoc />
    public string
    Format(string template, object obj)
    {
        if (string.IsNullOrEmpty(template))
        {
            return template;
        }

        StrategyInterface strategy =
            StrategyFactory.GetInstance()
                           .GetStrategy(obj)
                           .SetStringFormatter(this);

        return strategy.Format(template, obj);
    }
}
