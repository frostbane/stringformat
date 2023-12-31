using Dev.Frostbane;

namespace Dev.Frostbane.Test.MapStrategy;

public class DoubleMatchTest : IDisposable
{
    private FormatString sf;

    /// <summary>
    /// SetUp
    /// </summary>
    public
    DoubleMatchTest()
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
    TestDoubleMatch()
    {
        var map = new Dictionary<string, object>()
        {
            { "key", "{{id}}" },
            { "name", "user" },
            { "value", "1218" },
            { "id", "uid" },
        };

        string expected = "?{{id}}=user&uid=1218";
        string format   = "?{{key}}={{name}}&{{id}}={{value}}";
        string result   = sf.Format(format, map);

        Assert.Equivalent(expected, result, strict: true);
    }
}
