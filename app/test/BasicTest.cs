using Dev.Frostbane;

namespace Dev.Frostbane.Test;

public class BasicTest
{
    [Fact]
    public void
    TestBasicReplace()
    {
        StringFormat sf = new ();

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
        };

        string expected = "id";
        string result   = sf.Format("{{ col }}", map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestBasicReplaceSql()
    {
        StringFormat sf = new ();

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select id from t_users;";
        string result   = sf.Format("select {{ col }} from {{ table }};", map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestBasicReplaceUrl()
    {
        StringFormat sf = new ();

        sf.SetMatchStart("[")
          .SetMatchEnd("]");

        var map = new Dictionary<string, object>()
        {
            { "limit", 18 },
            { "query", "ak" },
            { "domain", "frostbane.dev" },
            { "mode", "https" },
        };

        string expected = "https://frostbane.dev?q=ak&n=18";
        string template = "[mode]://[domain]?q=[query]&n=[limit]";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestMatchStartOnly()
    {
        StringFormat sf = new ();

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
        StringFormat sf = new ();

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
    TestBasicIgnoreTag()
    {
        StringFormat sf = new ();

        sf.SetEscapeStart("//")
          .SetEscapeEnd("//");

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
        };

        string expected = "{{ col }}";
        string result   = sf.Format("//{{ col }}//", map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTagSql()
    {
        StringFormat sf = new ();

        sf.SetEscapeStart("//")
          .SetEscapeEnd("//");

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select {{ col }} from t_users;";
        string result   = sf.Format("select //{{ col }}// from {{ table }};", map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTag_StartOnly()
    {
        StringFormat sf = new ();

        sf.SetEscapeStart("//")
          .SetEscapeEnd("//");

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
        StringFormat sf = new ();

        sf.SetEscapeStart("//")
          .SetEscapeEnd("//");

        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "1218// is date";
        string template = "{{id}}// is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTag_MatchStartOnly()
    {
        StringFormat sf = new ();

        sf.SetEscapeStart("//")
          .SetEscapeEnd("//");

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
        StringFormat sf = new ();

        sf.SetEscapeStart("//")
          .SetEscapeEnd("//");

        var map = new Dictionary<string, object>()
        {
            { "id", "1218" },
        };

        string expected = "id}}// is date";
        string template = "id}}// is date";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }
}
