using System.Collections;
using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

public class MapExample
{
    [Fact]
    public void Example()
    {
        FormatString sf = new ();

        var map = new Hashtable
        {
            { "limit", 18 },
            { "query", "ak" },
            { "domain", "frostbane.dev" },
            { "mode", "https" },
        };

        string template = "{{mode}}://{{domain}}?q={{query}}&n={{limit}}";
        string url      = sf.Format(template, map);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }
}
