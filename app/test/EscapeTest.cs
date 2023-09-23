using Dev.Frostbane;

namespace Dev.Frostbane.Test;

public class EscapeTest
{
    [Theory]
    [InlineData(".")]
    [InlineData("$")]
    [InlineData("^")]
    [InlineData("[")]
    [InlineData("(")]
    [InlineData("|")]
    [InlineData(")")]
    [InlineData("*")]
    [InlineData("+")]
    [InlineData("?")]
    [InlineData("\\")]
    public void
    TestRegexEscape(string c)
    {
        StringFormat sf = new ();

        string m = c + c;

        sf.SetMatchStart(m)
          .SetMatchEnd(m);

        var map = new Dictionary<string, object>()
        {
            { "col", "id" },
            { "table", "t_users" },
        };

        string expected = "select id from t_users;";
        string template = "select " + m + "col" + m + " from " + m + "table" + m +";";
        string result   = sf.Format(template , map);

        Assert.Equivalent(expected, result, strict: true);
    }
}
