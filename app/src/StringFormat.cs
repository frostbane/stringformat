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
    format(string template,
           Dictionary<string, Object> map)
    {
        return template;
    }
}
