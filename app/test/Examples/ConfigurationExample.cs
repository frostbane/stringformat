using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

public class ConfigurationExample
{
    private Dictionary<string, object>
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
        FormatString sf = new ();

        sf.SetMatchTokens("{", "}");

        Dictionary<string, object> urlInfo = getUrlInfo();

        string template = "{mode}://{domain}?q={query}&n={limit}";
        string url      = sf.Format(template, urlInfo);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }

    [Fact]
    public void
    ArrayExample()
    {
        FormatString sf = new ();

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
        FormatString sf = new ();

        sf.SetMatchTokens("{", "}")
          .SetEscapeTokens("!", "!");

        Dictionary<string, object> urlInfo = getUrlInfo();

        string template = "{mode}://!{domain}!?q={query}&n={limit}";
        string url      = sf.Format(template, urlInfo);

        Assert.Equivalent("https://{domain}?q=ak&n=18", url, strict: true);
        //                         ^^^^^^^^
    }

    [Fact]
    public void
    OpenMatcherOnlyExample()
    {
        FormatString sf = new ();

        sf.SetMatchTokens("$", string.Empty);

        Dictionary<string, object> urlInfo = getUrlInfo();

        string template = "$mode://$domain?q=$query&n=$limit";
        string url      = sf.Format(template, urlInfo);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }

    [Fact]
    public void
    OpenEscapeOnlyExample()
    {
        FormatString sf = new ();

        sf.SetMatchTokens("<", ">")
          .SetEscapeTokens("!", string.Empty);

        Dictionary<string, object> urlInfo = getUrlInfo();

        string template = "<mode>://!<domain>?q=<query>&n=<limit>";
        string url      = sf.Format(template, urlInfo);

        Assert.Equivalent("https://<domain>?q=ak&n=18", url, strict: true);
    }
}
