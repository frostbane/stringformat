using System.Text.RegularExpressions;

namespace Dev.Frostbane;

public class StringFormat
{
    private static
    StringFormat? instance = null;

    private StringFormat()
    {
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
            string exp = "{{ *" + kvp.Key + " *}}";
            result = Regex.Replace(result, exp, kvp.Value.ToString());
        }

        return result;
    }
}
