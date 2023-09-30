using System.Collections;
using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

public class SpacesInKeyExample
{
    [Fact]
    public void Example()
    {
        StringFormat sf = new ();

        var map = new Hashtable
        {
            { "query string", "ak" },
            { "domain name", "frostbane.dev" },
            { "mode", "https" },
            { "result limit", 18}
        };

        string template = "{{mode}}://{{domain_name}}?q={{query_string}}&n={{result_limit}}";
        string url      = sf.Format(template, map);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }
}