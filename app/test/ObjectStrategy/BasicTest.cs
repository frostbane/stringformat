using Dev.Frostbane;
using Dev.Frostbane.Test.Fixtures;

namespace Dev.Frostbane.Test.ObjectStrategy;

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

        sf.SetMatchTokens("{", "}");
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
        Pet tarot = new ()
        {
            Specie = "cat",
            Name   = "Tarot",
            Age    = 2,
            Color  = "tabby"
        };

        string expected = "I have a tabby cat named Tarot.";
        string template = "I have a {Color} {Specie} named {Name}.";
        string result   = sf.Format(template, tarot);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestBasicReplace_Static()
    {
        Pet alpha = new ()
        {
            Specie = "dog",
            Name   = "Alpha",
            Age    = 14,
            Color  = "white"
        };

        string expected = "animal has dna";
        string template = "{Type} has {Core}";
        string result   = sf.Format(template, alpha);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Fact]
    public void
    TestBasicReplacePrivate()
    {
        Pet tata = new ()
        {
            Specie = "otter",
            Name   = "Okja",
            Age    = 5,
            Color  = "brown"
        };

        string expected = "{ParentPrivate} {ParentPrivateStatic} {ChildPrivate} {ChildPrivateStatic}";
        string template = "{ParentPrivate} {ParentPrivateStatic} {ChildPrivate} {ChildPrivateStatic}";
        string result   = sf.Format(template, tata);

        Assert.Equivalent(expected, result, strict: true);
    }
}
