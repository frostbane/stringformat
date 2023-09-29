using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

public class ConfigurationExample
{
    private Dictionary<string, string>
    getUrlInfo()
    {
        return new ()
        {
            { "query", "ak" },
            { "limit", "18" },
            { "domain", "frostbane.dev" },
            { "mode", "https" },
        };
    }

    private List<string>
    getUrlInfos()
    {
        return new List<string>()
        {
            "frostbane.dev",
            "https",
            "18",
            "ak",
        };
    }

    [Fact]
    public void
    MapExample()
    {
        StringFormat sf = new ();

        sf.SetMatchTokens("{", "}");

        Dictionary<string, string> urlInfo = getUrlInfo();

        string template = "{mode}://{domain}?q={query}&n={limit}";
        string url      = sf.Format(template, urlInfo);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }

    [Fact]
    public void
    ArrayExample()
    {
        StringFormat sf = new ();

        sf.SetMatchTokens("[", "]");

        List<string> list = getUrlInfos();

        string template = "[1]://[0]?q=[3]&n=[2]";
        string url      = sf.Format(template, list);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }

    [Fact]
    public void
    EscapeExample()
    {
        StringFormat sf = new ();

        sf.SetMatchTokens("{", "}")
          .SetEscapeTokens("!", "!");

        Dictionary<string, string> urlInfo = getUrlInfo();

        string template = "{mode}://!{domain}!?q={query}&n={limit}";
        string url      = sf.Format(template, urlInfo);

        Assert.Equivalent("https://frostbane.dev?q={query}&n=18", url, strict: true);
    }
}