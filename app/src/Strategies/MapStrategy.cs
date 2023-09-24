using System.Text.RegularExpressions;
using Dev.Frostbane;

namespace Dev.Frostbane.Strategies;

public class MapStrategy
{
    private static
    MapStrategy? instance = null;

    private MapStrategy()
    {
    }

    public static
    MapStrategy GetInstance()
    {
        instance ??= new ();

        return instance;
    }

    public string
    Format(StringFormatInterface sf,
           string template,
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
                "(?<!(" + sf.GetEscapeStart() + "))" +
                sf.GetMatchStart() + " *" + kvp.Key + " *" + sf.GetMatchEnd() +
                "(?!(" + sf.GetEscapeEnd() + "))";

#pragma warning disable CS8600
            string val = kvp.Value.ToString();
            string keyRegex = "^" + sf.GetMatchStart() + " *[^(" + sf.GetMatchEnd() + ")]+ *" + sf.GetMatchEnd() + "$";
#pragma warning restore CS8600

#pragma warning disable CS8604
            if (Regex.Match(val, keyRegex).Success)
            {
                result = Regex.Replace(result, exp, sf.GetEscapeStart() + val + sf.GetEscapeEnd());
            }
            else
            {
                result = Regex.Replace(result, exp, val);
            }
#pragma warning restore CS8604
        }

        result = sf.RemoveIgnoreTags(result);

        return result;
    }
}

