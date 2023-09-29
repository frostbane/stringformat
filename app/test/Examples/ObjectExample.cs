using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

#pragma warning disable CS8618
public class UrlBase
{
    public string mode;
    public static string domain = "frostbane.dev";
}

public class UrlInfo : UrlBase
{
    public string query;
    public int limit;
}
#pragma warning restore CS8618

public class ObjectExample
{
    [Fact]
    public void Example()
    {
        StringFormat sf  = new ();

        UrlInfo urlInfo = new ()
        {
            mode  = "https",
            query = "ak",
            limit = 18,
        };

        string template = "{{mode}}://{{domain}}?q={{query}}&n={{limit}}";
        string url      = sf.Format(template, urlInfo);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }
}