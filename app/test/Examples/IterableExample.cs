using Dev.Frostbane;

namespace Dev.Frostbane.Test.Examples;

public class IterableExample
{
    [Fact]
    public void ListExample()
    {
        StringFormat sf = new ();

        var list = new List<string>()
        {
            "frostbane.dev",
            "https",
            "18",
            "ak",
        };

        string template = "{{1}}://{{0}}?q={{3}}&n={{2}}";
        string url      = sf.Format(template, list);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }

    [Fact]
    public void ArrayExample()
    {
        StringFormat sf = new ();

        var arr = new string[]
        {
            "frostbane.dev",
            "https",
            "18",
            "ak",
        };

        string template = "{{1}}://{{0}}?q={{3}}&n={{2}}";
        string url      = sf.Format(template, arr);

        Assert.Equivalent("https://frostbane.dev?q=ak&n=18", url, strict: true);
    }
}