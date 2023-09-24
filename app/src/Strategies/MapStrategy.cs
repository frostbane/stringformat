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
                "(?<!" + sf.GetEscapeStart() + ")" +
                sf.GetMatchStart() + " *" + kvp.Key + " *" + sf.GetMatchEnd() +
                "(?!" + sf.GetEscapeEnd() + ")";

#pragma warning disable CS8604
            result = Regex.Replace(result, exp, kvp.Value.ToString());
#pragma warning restore CS8604
        }

        result = sf.RemoveIgnoreTags(result);

        return result;
    }
}

