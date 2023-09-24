using Dev.Frostbane;

namespace Dev.Frostbane.Test.MapStrategy;

public class DoubleMatchTest
{
    [Fact]
    public void
    TestDoubleMatch()
    {
        StringFormat sf = new ();

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

