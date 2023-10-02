using System.Collections;
using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

public class EscapingExample
{
    [Fact]
    public void EscapeKeyExample()
    {
        FormatString sf = new ();

        var map = new Hashtable
        {
            { "query", "ak" },
            { "limit", 18 },
            { "domain", "frostbane.dev" },
            { "mode", "https" },
        };

        string template = "{{mode}}://{{domain}}?q=//{{query}}//&n={{limit}}";
        string url      = sf.Format(template, map);

        Assert.Equivalent("https://frostbane.dev?q={{query}}&n=18", url, strict: true);
        //                                         ^^^^^^^^^
    }

    [Fact]
    public void EscapeEscapeExample()
    {
        FormatString sf = new ();

        var map = new Hashtable
        {
            { "query", "ak" },
            { "limit", 18 },
            { "domain", "frostbane.dev" },
            { "mode", "https" },
        };

        string template = "{{mode}}://{{domain}}?q=////{{query}}////&n={{limit}}";
        string url      = sf.Format(template, map);

        Assert.Equivalent("https://frostbane.dev?q=//{{query}}//&n=18", url, strict: true);
        //                                         ^^^^^^^^^^^
    }
}
