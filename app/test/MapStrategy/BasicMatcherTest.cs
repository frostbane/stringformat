using System.Collections;
using Dev.Frostbane;

namespace Dev.Frostbane.Test.MapStrategy;

public class BasicMatcherTest : IDisposable
{
    /// <summary>
    /// SetUp
    /// </summary>
    public
    BasicMatcherTest()
    {
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
    [InlineData("!")]
    [InlineData("%")]
    [InlineData("$")]
    [InlineData("\"")]
    [InlineData("/")]
    [InlineData("()")]
    [InlineData("'")]
    [InlineData("#")]
    [InlineData("@")]
    [InlineData("&")]
    [InlineData("{}")]
    [InlineData("[]")]
    [InlineData("*")]
    [InlineData(".")]
    [InlineData("+")]
    [InlineData("<>")]
    [InlineData("?")]
    [InlineData("~")]
    [InlineData(":")]
    [InlineData("=")]
    [InlineData("^")]
    [InlineData("|")]
    [InlineData("_")]
    public void
    TestBasicReplace(string matcher)
    {
        StringFormat sf = new ();

        string start = matcher[0].ToString();
        string end = matcher.Length == 2 ?
                     matcher[1].ToString() :
                     start;

        sf.SetMatchTokens(start, end);

        var map = new Hashtable
        {
            { "field", "name" },
            { "error", "is required" },
        };

        string expected = "error: name is required.";
        string template = "error: " + start + " field " + end + " " + start + " error " + end + ".";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }

    [Theory]
    [InlineData("!")]
    [InlineData("%")]
    [InlineData("$")]
    [InlineData("\"")]
    [InlineData("/")]
    [InlineData("'")]
    [InlineData("#")]
    [InlineData("@")]
    [InlineData("&")]
    [InlineData("*")]
    [InlineData(".")]
    [InlineData("+")]
    [InlineData("?")]
    [InlineData("~")]
    [InlineData(":")]
    [InlineData("=")]
    [InlineData("^")]
    [InlineData("|")]
    [InlineData("_")]
    public void
    TestBasicReplaceOpenOnly(string matcher)
    {
        StringFormat sf = new ();

        string start = matcher;
        string end = start;

        sf.SetMatchTokens(start, end);

        var map = new Hashtable
        {
            { "field", "name" },
            { "error", "is required" },
        };

        string expected = "error: name is required.";
        string template = "error: " + start + " field " + end + " " + start + " error " + end + ".";
        string result   = sf.Format(template, map);

        Assert.Equivalent(expected, result, strict: true);
    }
}
