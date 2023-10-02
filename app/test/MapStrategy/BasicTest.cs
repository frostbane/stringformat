using System.Collections;
using Dev.Frostbane;

namespace Dev.Frostbane.Test.MapStrategy;

public class BasicTest : IDisposable
{
    private FormatString sf;

    /// <summary>
    /// SetUp
    /// </summary>
    public
    BasicTest()
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

    [Theory]
    [InlineData("{{ col }}")]
    [InlineData("{{col }}")]
    [InlineData("{{ col}}")]
    [InlineData("{{col}}")]
    public void
    TestBasicReplace(string template)
    {
        var map = new Hashtable
        {
            { "col", "id" },
        };

        string expected = "id";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    /// <summary>
    /// spaces in the key should be replaced by underscores
    /// </summary>
    [Fact]
    public void
    TestBasicReplace_space()
    {
        var map = new Hashtable
        {
            { "col name", "id" },
        };

        string expected = "id";
        string template = "{{col_name}}";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Theory]
    [InlineData("select {{ col }} from {{ table }};")]
    [InlineData("select {{col }} from {{table }};")]
    [InlineData("select {{ col}} from {{ table}};")]
    [InlineData("select {{col}} from {{table}};")]
    public void
    TestBasicReplaceSql(string template)
    {
        var map = new Hashtable
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select id from t_users;";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Theory]
    [InlineData("select {{ col }} from {{ table }};")]
    [InlineData("select {{col }} from {{table }};")]
    [InlineData("select {{ col}} from {{ table}};")]
    [InlineData("select {{col}} from {{table}};")]
    public void
    TestBasicReplaceSqlDictionary(string template)
    {
        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select id from t_users;";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestBasicReplaceUrl()
    {
        sf.SetMatchTokens("[", "]");

        var map = new Hashtable
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

    [Theory]
    [InlineData("//{{ col }}//")]
    [InlineData("//{{col }}//")]
    [InlineData("//{{ col}}//")]
    [InlineData("//{{col}}//")]
    public void
    TestBasicIgnoreTag(string template)
    {
        var map = new Hashtable
        {
            { "col", "id" },
        };

        string expected = template[2..^2];
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Theory]
    [InlineData("////{{ col }}////")]
    [InlineData("////{{col }}////")]
    [InlineData("////{{ col}}////")]
    [InlineData("////{{col}}////")]
    public void
    TestBasicIgnoreTag_withIgnore(string template)
    {
        var map = new Hashtable
        {
            { "col", "id" },
        };

        string expected = template[2..^2];
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreTagSql()
    {
        var map = new Hashtable
        {
            { "col", "id" },
            { "table", "t_users" },
            { "login", "1" },
        };

        string expected = "select {{ col }} from t_users where login = '//{{ login }}//';";
        string template = "select //{{ col }}// from {{ table }} where login = '////{{ login }}////';";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestMatchNotInKey()
    {
        var map = new Hashtable
        {
            { "col", "id" },
        };

        string expected = "{{table}}";
        string template = "{{table}}";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestIgnoreNotInKey()
    {
        var map = new Hashtable
        {
            { "col", "id" },
        };

        string expected = "//{{table}}//";
        string template = "//{{table}}//";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestCaseSensitive()
    {
        var map = new Hashtable
        {
            { "DOMAINNAME", "FROSTBANE.DEV" },
            { "domainname", "frostbane.dev" },
        };

        string expected = "uppercase: FROSTBANE.DEV, lowercase: frostbane.dev";
        string template = "uppercase: {{DOMAINNAME}}, lowercase: {{domainname}}";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }
}
