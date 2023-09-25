using Dev.Frostbane;

namespace Dev.Frostbane.Test.MapStrategy;

public class UnpairedTest : IDisposable
{
    private StringFormat
    sf;

    /// <summary>
    /// SetUp
    /// </summary>
    public
    UnpairedTest()
    {
        sf = new ();
    }

    /// <summary>
    /// TearDown
    /// </summary>
    public void
    Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void
    TestMatchStartOnly()
    {
        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "{{id is date";
        string template = "{{id is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestMatchEndOnly()
    {
        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "id}} is date";
        string template = "id}} is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestUnopenedMatch()
    {
        var map = new Dictionary<string, object>()
        {
            { "key", "id" },
            { "value", "1218" },
        };

        string expected = "?id=value}}";
        string format   = "?{{key}}=value}}";
        string result   = sf.Format(format, map);


        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestUnclosedMatch()
    {
        var map = new Dictionary<string, object>()
        {
            { "key", "id" },
            { "value", "1218" },
        };

        string expected = "?{{key=1218";
        string format   = "?{{key={{value}}";
        string result   = sf.Format(format, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTag_MatchStartOnly()
    {
        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "//{{id is date";
        string template = "//{{id is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTag_MatchEndOnly()
    {
        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "id}}// is date";
        string template = "id}}// is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestUnopenedIgnore()
    {
        var map = new Dictionary<string, object>()
        {
            { "key", "id" },
            { "value", "1218" },
        };

        string expected = "?{{key}}=value}}//";
        string format   = "?//{{key}}//=value}}//";
        string result   = sf.Format(format, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestUnclosedIgnore()
    {
        var map = new Dictionary<string, object>()
        {
            { "key", "id" },
            { "value", "1218" },
        };

        string expected = "?//{{key={{value}}";
        string format   = "?//{{key=//{{value}}//";
        string result   = sf.Format(format, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTag_StartOnly()
    {
        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "//1218 is date";
        string template = "//{{id}} is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTag_EndOnly()
    {
        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "1218// is date";
        string template = "{{id}}// is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }
}
