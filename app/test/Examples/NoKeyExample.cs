using System.Collections;
using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

public class NoKeyExample
{
    [Fact]
    public void MapExample()
    {
        FormatString sf = new ();

        var map = new Hashtable
        {
            { "query", "ak" },
            { "domain", "frostbane.dev" },
            { "mode", "https" },
        };

        string template = "{{mode}}://{{domain}}?q={{query}}&n={{limit}}";
        string url      = sf.Format(template, map);

        Assert.Equivalent("https://frostbane.dev?q=ak&n={{limit}}", url, strict: true);
        //                                              ^^^^^^^^^
    }

    [Fact]
    public void ArrayExample()
    {
        FormatString sf = new ();

        var arr = new string[]
        {
            "frostbane.dev",
            "https",
        };

        string template = "{{1}}://{{0}}?q={{3}}&n={{2}}";
        string url      = sf.Format(template, arr);

        Assert.Equivalent("https://frostbane.dev?q={{3}}&n={{2}}", url, strict: true);
        //                                         ^^^^^   ^^^^^
    }
}
