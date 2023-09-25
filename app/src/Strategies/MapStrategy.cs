using System.Text.RegularExpressions;
using Dev.Frostbane;

namespace Dev.Frostbane.Strategies;

public class MapStrategy : StrategyInterface
{
    private StringFormatInterface sf;
    private Dictionary<string, object> map;

#pragma warning disable CS8618
    public MapStrategy(StringFormatInterface sf)
#pragma warning restore CS8618
    {
        this.sf  = sf;
#pragma warning disable CS8625
        this.map = null;
#pragma warning restore CS8625
    }

    private string
    RemoveIgnoreTags(string template)
    {
        if (string.IsNullOrEmpty(template))
        {
            return template;
        }

        string result = template;

        foreach(KeyValuePair<string, object> kvp in map)
        {
            string exp =
                sf.GetEscapeStart() +
                "(" + sf.GetMatchStart() + " *" + kvp.Key + " *" + sf.GetMatchEnd() + ")" +
                sf.GetEscapeEnd();

#pragma warning disable CS8604
            result = Regex.Replace(result, exp, m => m.Groups[1].Value);
#pragma warning restore CS8604
        }

        return result;
    }

    private bool
    ReplacementIsAToken(string replacement)
    {
        string keyRegex = "^" + sf.GetMatchStart() + " *[^(" + sf.GetMatchEnd() + ")]+ *" + sf.GetMatchEnd() + "$";

        return Regex.Match(replacement, keyRegex).Success;
    }

    public string
    Format(string template, object obj)
    {
        if (obj == null)
        {
            return template;
        }

        map = (Dictionary<string, object>)obj;

        string result = template;

        foreach(KeyValuePair<string, object> kvp in map)
        {
            string match =
                sf.GetMatchStart() + " *" + kvp.Key.Replace(" ", "_") + " *" + sf.GetMatchEnd();
            string exp =
                "(?<!(" + sf.GetEscapeStart() + "))" + match +
                "|" +
                match + "(?!(" + sf.GetEscapeEnd() + "))";

#pragma warning disable CS8600
        string val = sf.GetValue(kvp.Value);
#pragma warning restore CS8600

#pragma warning disable CS8604
            if (ReplacementIsAToken(val))
            {
                result = Regex.Replace(result, exp, sf.GetEscapeStart() + val + sf.GetEscapeEnd());
            }
            else
            {
                result = Regex.Replace(result, exp, m => m.Groups[1].Value + val + m.Groups[3].Value);
            }
#pragma warning restore CS8604
        }

        result = RemoveIgnoreTags(result);

        return result;
    }
}

