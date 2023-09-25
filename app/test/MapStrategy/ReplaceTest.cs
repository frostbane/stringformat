using Dev.Frostbane;

namespace Dev.Frostbane.Test.MapStrategy;

public class ReplaceTest : IDisposable
{
    private StringFormat
    sf;

    /// <summary>
    /// SetUp
    /// </summary>
    public
    ReplaceTest()
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
    TestReplace()
    {
        var map = new Dictionary<string, object>()
        {
            { "int", 1218 },
            { "float", 12.18 },
            { "char", 'c' },
            { "string", "dec. 18" },
            { "bool", true },
#pragma warning disable CS8625
            { "null", null },
#pragma warning restore CS8625
        };

        string expected = "int=1218, float=12.18, char=c, string=dec. 18, bool=True, null=Null";
        string template = "int={{int}}, float={{float}}, char={{char}}, string={{string}}, bool={{bool}}, null={{null}}";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

}