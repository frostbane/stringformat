using Dev.Frostbane;
using Dev.Frostbane.Test.Fixtures;

namespace Dev.Frostbane.Test.EnumerableStrategy;

public class BasicTest : IDisposable
{
    private StringFormat sf;

    /// <summary>
    /// SetUp
    /// </summary>
    public BasicTest()
    {
        sf = new ();

        sf.SetMatchStart("{")
          .SetMatchEnd("}");
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
    TestBasicReplace()
    {
        List<string> names = new ()
        {
            "mae",
            "andrea",
            "miso",
            "sheene",
            "lory",
            "daphne",
            "nana",
            "akane",
            "shashee",
        };

        string expected = "akane, sheene, miso, and andrea";
        string template = "{7}, { 3}, {2 }, and { 1 }";
        string result   = sf.Format(template, names);

        Assert.Equivalent(expected, result, strict: true);
    }
}